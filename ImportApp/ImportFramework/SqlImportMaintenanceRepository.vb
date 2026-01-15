Imports System.Data.SqlClient

Public Class SqlImportMaintenanceRepository
    Implements IImportMaintenanceRepository

    Private ReadOnly _connection As SqlConnection

    Public Sub New(conn As SqlConnection)
        _connection = conn
    End Sub

    Public Sub DeleteOldData(targetDate As DateTime, tran As SqlTransaction) _
        Implements IImportMaintenanceRepository.DeleteOldData

        Const sql As String = "
            DELETE FROM ProcessedTable
            WHERE ProcessedDate < @TargetDate
        "

        Using cmd As New SqlCommand(sql, _connection, tran)
            cmd.Parameters.Add("@TargetDate", SqlDbType.DateTime).Value = targetDate
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub InsertHistory(history As ImportHistory, tran As SqlTransaction) _
        Implements IImportMaintenanceRepository.InsertHistory

        Const sql As String = "
            INSERT INTO ImportHistory
            (FileName, RowCount, Success, ExecDate)
            VALUES
            (@FileName, @RowCount, @Success, @ExecDate)
        "

        Using cmd As New SqlCommand(sql, _connection, tran)
            cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = history.FileName
            cmd.Parameters.Add("@RowCount", SqlDbType.Int).Value = history.RowCount
            cmd.Parameters.Add("@Success", SqlDbType.Bit).Value = history.Success
            cmd.Parameters.Add("@ExecDate", SqlDbType.DateTime).Value = DateTime.Now

            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Class