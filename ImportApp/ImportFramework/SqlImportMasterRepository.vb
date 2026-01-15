Imports System.Data.SqlClient

Public Class SqlImportMasterRepository
    Implements IImportMasterRepository

    Private ReadOnly _connection As SqlConnection

    Public Sub New(conn As SqlConnection)
        _connection = conn
    End Sub

    Public Function GetMasterA() As IList(Of MasterA) _
        Implements IImportMasterRepository.GetMasterA

        Const sql As String = "
            SELECT Code, Name
            FROM MasterA
        "

        Dim list As New List(Of MasterA)

        Using cmd As New SqlCommand(sql, _connection)
            Using reader = cmd.ExecuteReader()
                While reader.Read()
                    list.Add(New MasterA With {
                        .Code = reader("Code").ToString(),
                        .Name = reader("Name").ToString()
                    })
                End While
            End Using
        End Using

        Return list
    End Function

End Class