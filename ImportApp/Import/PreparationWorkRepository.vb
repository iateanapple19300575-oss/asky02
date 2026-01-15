Imports System.Data.SqlClient

Public Class PreparationWorkRepository

    Private ReadOnly _connectionString As String

    Public Sub New(conn As String)
        _connectionString = conn
    End Sub

    Public Function GetByTeacherAndDate(teacherId As Integer, workDate As DateTime) As List(Of PreparationWork)

        Dim result As New List(Of PreparationWork)()

        Dim sql As String = "
            SELECT Id, TeacherId, WorkDate, StartTime, EndTime
            FROM PreparationWork
            WHERE TeacherId = @TeacherId
              AND WorkDate = @WorkDate
            ORDER BY StartTime
        "

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@TeacherId", teacherId)
                cmd.Parameters.AddWithValue("@WorkDate", workDate.Date)

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim pw As New PreparationWork()
                        pw.Id = reader("Id")
                        pw.TeacherId = reader("TeacherId")
                        pw.WorkDate = reader("WorkDate")
                        pw.StartTime = reader("StartTime")
                        pw.EndTime = reader("EndTime")
                        result.Add(pw)
                    End While
                End Using
            End Using
        End Using

        Return result

    End Function

End Class