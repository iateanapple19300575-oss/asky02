Imports System.Data.SqlClient

Public Class LessonWorkRepository

    Private ReadOnly _connectionString As String

    Public Sub New(conn As String)
        _connectionString = conn
    End Sub

    Public Function GetByTeacherAndDate(teacherId As Integer, workDate As DateTime) As List(Of LessonWork)

        Dim result As New List(Of LessonWork)()

        Dim sql As String = "
            SELECT Id, TeacherId, WorkDate, StartTime, EndTime
            FROM LessonWork
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
                        Dim lw As New LessonWork()
                        lw.Id = reader("Id")
                        lw.TeacherId = reader("TeacherId")
                        lw.WorkDate = reader("WorkDate")
                        lw.StartTime = reader("StartTime")
                        lw.EndTime = reader("EndTime")
                        result.Add(lw)
                    End While
                End Using
            End Using
        End Using

        Return result

    End Function

End Class