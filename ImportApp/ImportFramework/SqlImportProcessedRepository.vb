Imports System.Data.SqlClient

Public Class SqlImportProcessedRepository
    Implements IImportProcessedRepository

    Private ReadOnly _connection As SqlConnection

    Public Sub New(conn As SqlConnection)
        _connection = conn
    End Sub

    Public Sub InsertProcessedData(rows As IList(Of ProcessedRow), tran As SqlTransaction) _
        Implements IImportProcessedRepository.InsertProcessedData

        Const sql As String = "
            INSERT INTO ProcessedTable
            (KeyCode, Amount, Category, ProcessedDate)
            VALUES
            (@KeyCode, @Amount, @Category, @ProcessedDate)
        "

        Using cmd As New SqlCommand(sql, _connection, tran)
            cmd.Parameters.Add("@KeyCode", SqlDbType.VarChar)
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal)
            cmd.Parameters.Add("@Category", SqlDbType.VarChar)
            cmd.Parameters.Add("@ProcessedDate", SqlDbType.DateTime)

            For Each r In rows
                cmd.Parameters("@KeyCode").Value = r.KeyCode
                cmd.Parameters("@Amount").Value = r.Amount
                cmd.Parameters("@Category").Value = r.Category
                cmd.Parameters("@ProcessedDate").Value = DateTime.Now

                cmd.ExecuteNonQuery()
            Next
        End Using
    End Sub

End Class