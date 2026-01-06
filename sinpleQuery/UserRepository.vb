' UserRepository.vb
Imports System.Data
Imports System.Data.SqlClient

Public Class UserRepository

    Private ReadOnly _cs As String

    Public Sub New(connectionString As String)
        _cs = connectionString
    End Sub

    Public Function GetUserById(id As Integer) As DataTable
        Dim U = Tables.Users

        Dim sql As String =
$"SELECT
    U.{U.UserId},
    U.{U.UserName},
    U.{U.Email}
FROM
    {U.TableName} AS U
WHERE
    U.{U.UserId} = @{U.UserId}
"

        Using con As New SqlConnection(_cs)
            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@" & U.UserId, id)

                Dim dt As New DataTable()
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                Return dt
            End Using
        End Using
    End Function

    '-----------------------------------------
    ' ユーザー追加
    '-----------------------------------------
    Public Sub InsertUser(userName As String, email As String)
        Dim U = Tables.Users

        Dim sql As String =
$"INSERT INTO {U.TableName} (
    {U.UserName},
    {U.Email}
)
VALUES (
    @{U.UserName},
    @{U.Email}
)
"

        Using con As New SqlConnection(_cs)
            con.Open()

            Using cmd As New SqlCommand(sql, con)
                cmd.Parameters.AddWithValue("@" & U.UserName, userName)
                cmd.Parameters.AddWithValue("@" & U.Email, email)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub



    Public Function GetUserOrdersByUserId(userId As Integer) As DataTable
        Dim U = Tables.Users
        Dim O = Tables.Orders

        Dim sql As String =
$"SELECT
    U.{U.UserName},
    O.{O.OrderDate},
    O.{O.Amount}
FROM
    {U.TableName} AS U
INNER JOIN
    {O.TableName} AS O
        ON U.{U.UserId} = O.{O.UserId}
WHERE
    U.{U.UserId} = @{U.UserId}
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