Imports System.Data
Imports System.Data.SqlClient

Namespace Framework
    Public NotInheritable Class SqlExecutor

        Private Sub New()
        End Sub

        Private Shared _connectionString As String

        Public Shared Sub Initialize(connectionString As String)
            _connectionString = connectionString
        End Sub

        ' ▼ SELECT（複数行）
        Public Shared Function Query(Of T)(sql As String,
                                           mapper As Func(Of SqlDataReader, T),
                                           Optional param As Object = Nothing) As List(Of T)

            Dim result As New List(Of T)

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)

                    AddParameters(cmd, param)

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            result.Add(mapper(reader))
                        End While
                    End Using

                End Using
            End Using

            Return result
        End Function

        ' ▼ SELECT（1行）
        Public Shared Function QuerySingle(Of T)(sql As String,
                                                 mapper As Func(Of SqlDataReader, T),
                                                 Optional param As Object = Nothing) As T

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)

                    AddParameters(cmd, param)

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return mapper(reader)
                        End If
                    End Using

                End Using
            End Using

            Return Nothing
        End Function

        ' ▼ INSERT / UPDATE / DELETE
        Public Shared Function Execute(sql As String,
                                       Optional param As Object = Nothing) As Integer

            Using conn As New SqlConnection(_connectionString)
                Using cmd As New SqlCommand(sql, conn)

                    AddParameters(cmd, param)

                    conn.Open()
                    Return cmd.ExecuteNonQuery()

                End Using
            End Using

        End Function

        ' ▼ パラメータ自動生成（FW3.5対応）
        Private Shared Sub AddParameters(cmd As SqlCommand, param As Object)
            If param Is Nothing Then Return

            Dim props = param.GetType().GetProperties()

            For Each p In props
                Dim value = p.GetValue(param, Nothing)
                If value Is Nothing Then value = DBNull.Value
                cmd.Parameters.AddWithValue("@" & p.Name, value)
            Next
        End Sub

    End Class
End Namespace