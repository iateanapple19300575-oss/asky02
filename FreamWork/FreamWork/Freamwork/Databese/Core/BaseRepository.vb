Imports System.Data.SqlClient

''' <summary>
''' 汎用リポジトリ基底クラス。
''' </summary>
Public MustInherit Class BaseRepository(Of T As {BaseEntity, New})

    Private Const ERR_MSG_01 As String = "データベースへの接続中に問題が発生しました。しばらくしてから再度お試しください。"
    Private Const ERR_MSG_02 As String = "処理を完了できませんでした。ネットワークまたはサーバーの状態をご確認ください。"
    Private Const ERR_MSG_03 As String = "データの取得に失敗しました。時間をおいて再度実行してください。"
    Private Const ERR_MSG_04 As String = "データベース処理中にエラーが発生しました。担当者へご連絡ください。"


    Private execSqlStr As String = ""

    Protected ReadOnly Exec As SqlExecutor

    Protected Sub New(exec As SqlExecutor)
        Me.Exec = exec
    End Sub

    Protected MustOverride ReadOnly Property TableName As String
    Protected MustOverride ReadOnly Property PrimaryKey As String

    Protected Overridable ReadOnly Property ExcludeColumns As List(Of String)
        Get
            Return New List(Of String)() From {
                "ID", "Create_Date", "Create_User", "Update_Date", "Update_User", "RowVersion"
            }
        End Get
    End Property

    Public Overridable Function FindAll() As List(Of T)
        Dim sql As String = "SELECT * FROM " & TableName
        Dim list As New List(Of T)()
        Using reader As SqlDataReader = Exec.ExecuteReader(sql, Nothing)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of T)(reader))
            End While
        End Using
        Return list
    End Function

    ' ★ DomainService から呼べるように Public にする
    Public Function BeginTransaction() As SqlTransaction
        Return Exec.BeginTransaction()
    End Function

    Public Overridable Function Insert(entity As T) As Integer
        ' 非監査列のみ INSERT
        Dim t As Type = GetType(T)
        Dim columns As New List(Of String)()
        Dim values As New List(Of String)()
        Dim parameters As New List(Of SqlParameter)()

        Try
            For Each prop In t.GetProperties()
                If ExcludeColumns.Contains(prop.Name) Then
                    Continue For
                End If
                columns.Add(prop.Name)
                Dim paramName As String = "@" & prop.Name
                values.Add(paramName)
                Dim v As Object = prop.GetValue(entity, Nothing)
                parameters.Add(New SqlParameter(paramName, If(v Is Nothing, DBNull.Value, v)))
            Next

            Dim sql As String =
"INSERT INTO " & TableName & "(" & String.Join(",", columns.ToArray()) & ")" & " " &
"VALUES (" & String.Join(",", values.ToArray()) & ")"

            execSqlStr = BuildExecutedSql(sql, parameters)

            Dim count As Integer = Exec.ExecuteNonQuery(sql, parameters)
            LogService.WriteOperation("Insert", GetType(T).Name, entity.ID)
            Return count

        Catch ex As Exception
            Throw New RepositoryException(ex, execSqlStr, ERR_MSG_04)
        End Try

    End Function

    Public Overridable Function Update(before As T, after As T) As Integer
        Dim t As Type = GetType(T)
        Dim setList As New List(Of String)()
        Dim parameters As New List(Of SqlParameter)()

        Try
            For Each prop In t.GetProperties()
                If ExcludeColumns.Contains(prop.Name) OrElse prop.Name = PrimaryKey OrElse prop.Name = "RowVersion" Then
                    Continue For
                End If

                Dim beforeValue As Object = prop.GetValue(before, Nothing)
                Dim afterValue As Object = prop.GetValue(after, Nothing)

                Dim isDifferent As Boolean = Not ObjectEquals(beforeValue, afterValue)
                If isDifferent Then
                    Dim paramName As String = "@" & prop.Name
                    setList.Add(prop.Name & " = " & paramName)
                    Dim v As Object = afterValue
                    parameters.Add(New SqlParameter(paramName, If(v Is Nothing, DBNull.Value, v)))
                End If
            Next

            If setList.Count = 0 Then
                Return 0
            End If

            Dim sql As String =
    "UPDATE " & TableName & vbCrLf &
    "SET " & String.Join(","c, setList.ToArray()) & vbCrLf &
    "WHERE " & PrimaryKey & " = @ID"

            parameters.Add(New SqlParameter("@ID", after.ID))

            execSqlStr = BuildExecutedSql(sql, parameters)

            Dim count As Integer = Exec.ExecuteNonQuery(sql, parameters)
            LogService.WriteOperation("Update", GetType(T).Name, after.ID)
            Return count

        Catch ex As Exception
            Throw New RepositoryException(ex, execSqlStr, ERR_MSG_04)
        End Try

    End Function

    Public Overridable Function Delete(entity As T) As Integer
        Try
            Dim sql As String =
"DELETE FROM " & TableName & " WHERE " & PrimaryKey & " = @ID"

            Dim parameters As New List(Of SqlParameter)()
            parameters.Add(New SqlParameter("@ID", entity.ID))

            execSqlStr = BuildExecutedSql(sql, parameters)

            Dim count As Integer = Exec.ExecuteNonQuery(sql, parameters)
            LogService.WriteOperation("Delete", GetType(T).Name, entity.ID)
            Return count

        Catch ex As Exception
            Throw New RepositoryException(ex, execSqlStr, ERR_MSG_04)

        End Try
    End Function

    Private Function ObjectEquals(a As Object, b As Object) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        Return a.Equals(b)
    End Function

    Public Shared Function BuildExecutedSql(sql As String, params As IList(Of SqlParameter)) As String

        If params Is Nothing OrElse params.Count = 0 Then
            Return sql
        End If

        Dim result As String = sql

        For Each parameters As SqlParameter In params

            Dim value As String

            If parameters.Value Is Nothing OrElse parameters.Value Is DBNull.Value Then
                value = "NULL"

            ElseIf TypeOf parameters.Value Is String OrElse TypeOf parameters.Value Is DateTime Then
                ' 文字列・日付はシングルクォートで囲む
                value = "'" & parameters.Value.ToString().Replace("'", "''") & "'"

            ElseIf TypeOf parameters.Value Is Boolean Then
                value = If(CBool(parameters.Value), "1", "0")

            Else
                ' 数値などはそのまま
                value = parameters.Value.ToString()
            End If

            ' パラメータ名を置換
            result = result.Replace(parameters.ParameterName, value)

        Next

        Return result

    End Function
End Class

