Imports System.Data.SqlClient

Public Class ImportHistoryRepository
    Private ReadOnly _exec As SqlExecutor

    Public Sub New(exec As SqlExecutor)
        _exec = exec
    End Sub

    Public Sub InsertHistory(tableName As String, filePath As String, count As Integer)

        Dim sql As String =
            "INSERT INTO ImportHistory (TableName, FilePath, ImportCount, ImportDate) " &
            "VALUES (@TableName, @FilePath, @ImportCount, GETDATE())"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@TableName", tableName))
        p.Add(New SqlParameter("@FilePath", filePath))
        p.Add(New SqlParameter("@ImportCount", count))

        _exec.ExecuteNonQuery(sql, p)

    End Sub

End Class