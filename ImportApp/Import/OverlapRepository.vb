Imports System.Data.SqlClient

Public Class OverlapRepository

    Private ReadOnly _connectionString As String

    Public Sub New(conn As String)
        _connectionString = conn
    End Sub

    Public Function GetMonthlyOverlaps(startDate As DateTime, endDate As DateTime) As DataTable

        Dim sql As String = "
            WITH Work AS (
                SELECT *
                FROM vwWork
                WHERE WorkDate >= @StartDate
                  AND WorkDate <  @EndDate
            )
            SELECT 
                w1.WorkType AS SourceType,
                w1.Id AS SourceId,
                w1.TeacherId,
                w1.WorkDate,
                w1.StartTime AS SourceStart,
                w1.EndTime AS SourceEnd,
                w2.WorkType AS ConflictType,
                w2.Id AS ConflictId,
                w2.StartTime AS ConflictStart,
                w2.EndTime AS ConflictEnd
            FROM Work w1
            JOIN Work w2
                ON w1.TeacherId = w2.TeacherId
                AND w1.WorkDate = w2.WorkDate
                AND NOT (w1.WorkType = w2.WorkType AND w1.Id = w2.Id)
                AND w1.StartTime <= w2.StartTime
                AND w1.StartTime < w2.EndTime
                AND w2.StartTime < w1.EndTime
            ORDER BY w1.TeacherId, w1.WorkDate, w1.StartTime;
        "

        Dim dt As New DataTable()

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt

    End Function

    Public Function GetMonthlyWorkWithOverlap(startDate As DateTime, endDate As DateTime) As DataTable

        Dim sql As String = "
        WITH Work AS (
            SELECT *
            FROM vwWork
            WHERE WorkDate >= @StartDate
              AND WorkDate <  @EndDate
        )
        SELECT 
            w1.WorkType,
            w1.Id,
            w1.TeacherId,
            w1.WorkDate,
            w1.StartTime,
            w1.EndTime,
            STUFF((
                SELECT 
                    '; ' + w2.WorkType 
                    + ' ' + CONVERT(VARCHAR(5), w2.StartTime, 108)
                    + '-' + CONVERT(VARCHAR(5), w2.EndTime, 108)
                FROM Work w2
                WHERE w1.TeacherId = w2.TeacherId
                  AND w1.WorkDate = w2.WorkDate
                  AND NOT (w1.WorkType = w2.WorkType AND w1.Id = w2.Id)
                  AND w1.StartTime < w2.EndTime
                  AND w2.StartTime < w1.EndTime
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)'), 1, 2, '') AS OverlapInfo
        FROM Work w1
        ORDER BY w1.TeacherId, w1.WorkDate, w1.StartTime;
    "

        Dim dt As New DataTable()

        Using conn As New SqlConnection(_connectionString)
            Using cmd As New SqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@StartDate", startDate)
                cmd.Parameters.AddWithValue("@EndDate", endDate)

                conn.Open()
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        End Using

        Return dt

    End Function


End Class