'===========================================================
' LecturePlanRepository
'===========================================================
Imports System.Data.SqlClient

Public Class LecturePlanRepository
    Inherits BaseRepository(Of LecturePlanEntity)

    Public Sub New(exec As SqlExecutor)
        MyBase.New(exec)
    End Sub

    Protected Overrides ReadOnly Property TableName As String
        Get
            Return "LecturePlan"
        End Get
    End Property

    Protected Overrides ReadOnly Property PrimaryKey As String
        Get
            Return "ID"
        End Get
    End Property

    'Public Sub Insert(entity As LecturePlanEntity)

    '    Dim sql As String =
    '        "INSERT INTO LecturePlan " &
    '        "(LectureDate, TeacherCode, SubjectCode, Status, ActualHours, " &
    '        "Create_Date, Create_User, Update_Date, Update_User) " &
    '        "VALUES (@LectureDate, @TeacherCode, @SubjectCode, @Status, @ActualHours, " &
    '        "GETDATE(), SYSTEM_USER, GETDATE(), SYSTEM_USER)"

    '    Dim p As New List(Of SqlParameter)()
    '    p.Add(New SqlParameter("@LectureDate", entity.LectureDate))
    '    p.Add(New SqlParameter("@TeacherCode", entity.TeacherCode))
    '    p.Add(New SqlParameter("@SubjectCode", entity.SubjectCode))
    '    p.Add(New SqlParameter("@Status", entity.Status))
    '    p.Add(New SqlParameter("@ActualHours", entity.ActualHours))

    '    Exec.ExecuteNonQuery(sql, p)

    'End Sub

    Public Function FindByDate([date] As DateTime) As List(Of LecturePlanEntity)
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE LectureDate = @LectureDate"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@LectureDate", [date]))

        Dim list As New List(Of LecturePlanEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LecturePlanEntity)(reader))
            End While
        End Using

        Return list
    End Function

    Public Function FindByTeacher(teacherCode As String) As List(Of LecturePlanEntity)
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE TeacherCode = @TeacherCode"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@TeacherCode", teacherCode))

        Dim list As New List(Of LecturePlanEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LecturePlanEntity)(reader))
            End While
        End Using

        Return list
    End Function

    Public Function FindBySubject(subjectCode As String) As List(Of LecturePlanEntity)
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE SubjectCode = @SubjectCode"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@SubjectCode", subjectCode))

        Dim list As New List(Of LecturePlanEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LecturePlanEntity)(reader))
            End While
        End Using

        Return list
    End Function

    Public Function FindByDateTeacherSubject([date] As DateTime, teacher As String, subject As String) As List(Of LecturePlanEntity)
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE LectureDate = @LectureDate" & vbCrLf &
"  AND TeacherCode = @TeacherCode" & vbCrLf &
"  AND SubjectCode = @SubjectCode"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@LectureDate", [date]))
        p.Add(New SqlParameter("@TeacherCode", teacher))
        p.Add(New SqlParameter("@SubjectCode", subject))

        Dim list As New List(Of LecturePlanEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LecturePlanEntity)(reader))
            End While
        End Using

        Return list
    End Function

    Public Function FindByDateAndTeacher([date] As DateTime, teacher As String) As LecturePlanEntity
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE LectureDate = @LectureDate" & vbCrLf &
"  AND TeacherCode = @TeacherCode"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@LectureDate", [date]))
        p.Add(New SqlParameter("@TeacherCode", teacher))

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            If reader.Read() Then
                Return ReaderMapper.Map(Of LecturePlanEntity)(reader)
            End If
        End Using

        Return Nothing
    End Function

    Public Function SearchDynamic(cond As SearchCondition) As List(Of LecturePlanEntity)

        Dim wb As New WhereBuilder()

        For Each kv In cond.Items
            Dim column As String = kv.Key
            Dim value As Object = kv.Value

            If value Is Nothing Then Continue For
            If TypeOf value Is String AndAlso value.ToString() = "" Then Continue For

            Dim paramName As String = "@" & column
            wb.Add(column & " = " & paramName, paramName, value)
        Next

        Dim sql As String = "SELECT * FROM LecturePlan" & wb.ToSql()

        Dim list As New List(Of LecturePlanEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, wb.GetParameters())
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LecturePlanEntity)(reader))
            End While
        End Using

        Return list
    End Function
End Class

