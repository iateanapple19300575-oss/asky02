Namespace Framework
    Public NotInheritable Class QueryBuilder

        Private Sub New()
        End Sub

        ' ▼ SELECT * FROM Table
        Public Shared Function BuildSelect(def As TableDefinition) As String
            Return "SELECT " & String.Join(", ", def.Columns.ToArray()) &
                   " FROM " & def.TableName
        End Function

        ' ▼ INSERT INTO Table (A,B,C) VALUES (@A,@B,@C)
        Public Shared Function BuildInsert(def As TableDefinition) As String
            Dim colList As String = String.Join(", ", def.Columns.ToArray())
            Dim paramList As String = "@" & String.Join(", @", def.Columns.ToArray())

            Return "INSERT INTO " & def.TableName &
                   " (" & colList & ") VALUES (" & paramList & ")"
        End Function

        ' ▼ UPDATE Table SET A=@A, B=@B WHERE Key1=@Key1 AND Key2=@Key2
        Public Shared Function BuildUpdate(def As TableDefinition) As String
            Dim setList As New List(Of String)

            For Each col In def.Columns
                If Not def.KeyColumns.Contains(col) Then
                    setList.Add(col & "=@" & col)
                End If
            Next

            Dim whereList As New List(Of String)
            For Each key In def.KeyColumns
                whereList.Add(key & "=@" & key)
            Next

            Return "UPDATE " & def.TableName &
                   " SET " & String.Join(", ", setList.ToArray()) &
                   " WHERE " & String.Join(" AND ", whereList.ToArray())
        End Function

        ' ▼ DELETE FROM Table WHERE Key1=@Key1 AND Key2=@Key2
        Public Shared Function BuildDelete(def As TableDefinition) As String
            Dim whereList As New List(Of String)

            For Each key In def.KeyColumns
                whereList.Add(key & "=@" & key)
            Next

            Return "DELETE FROM " & def.TableName &
                   " WHERE " & String.Join(" AND ", whereList.ToArray())
        End Function

    End Class
End Namespace