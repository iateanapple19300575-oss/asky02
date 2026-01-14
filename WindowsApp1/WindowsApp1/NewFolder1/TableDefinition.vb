Namespace Framework
    Public Class TableDefinition

        Public Property TableName As String
        Public Property Columns As List(Of String)
        Public Property KeyColumns As List(Of String)

        Public Sub New(tableName As String, columns As List(Of String), keyColumns As List(Of String))
            Me.TableName = tableName
            Me.Columns = columns
            Me.KeyColumns = keyColumns
        End Sub

    End Class
End Namespace