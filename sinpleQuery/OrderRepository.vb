' OrderRepository.vb
Imports System.Data
Imports System.Data.SqlClient

Public Class OrderRepository

    Private ReadOnly _cs As String

    Public Sub New(connectionString As String)
        _cs = connectionString
    End Sub

    Public Function GetOrdersByUserId(userId As Integer) As DataTable
        Dim O = Tables.Orders

        Dim sql As String =
$"SELECT
    O.{O.OrderId},
    O.{O.UserId},
    O.{O.OrderDate},
    O.{O.Amount}
FROM
    {O.TableName} AS O
WHERE
    O.{O.UserId} = @{O.UserId}
"

        Using con As New SqlConnection(_cs)
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@" & O.UserId, userId)

                Dim dt As New DataTable()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

End Class