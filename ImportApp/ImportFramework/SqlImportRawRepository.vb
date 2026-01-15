Imports System.Data.SqlClient

Public Class SqlImportRawRepository
    Implements IImportRawRepository

    Private ReadOnly _connection As SqlConnection

    Public Sub New(conn As SqlConnection)
        _connection = conn
    End Sub

    Public Sub InsertRawData(rows As IList(Of RawImportRow), tran As SqlTransaction) _
        Implements IImportRawRepository.InsertRawData

        Const sql As String = "
            INSERT INTO RawImportTable
            (ColA, ColB, ColC, ImportDate)
            VALUES
            (@ColA, @ColB, @ColC, @ImportDate)
        "

        Using cmd As New SqlCommand(sql, _connection, tran)
            cmd.Parameters.Add("@ColA", SqlDbType.VarChar)
            cmd.Parameters.Add("@ColB", SqlDbType.VarChar)
            cmd.Parameters.Add("@ColC", SqlDbType.VarChar)
            cmd.Parameters.Add("@ImportDate", SqlDbType.DateTime)

            For Each r In rows
                cmd.Parameters("@ColA").Value = r.ColA
                cmd.Parameters("@ColB").Value = r.ColB
                cmd.Parameters("@ColC").Value = r.ColC
                cmd.Parameters("@ImportDate").Value = DateTime.Now

                cmd.ExecuteNonQuery()
            Next
        End Using
    End Sub

End Class