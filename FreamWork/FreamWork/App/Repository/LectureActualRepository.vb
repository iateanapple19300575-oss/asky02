'===========================================================
' LectureActualRepository
'===========================================================
Imports System.Data.SqlClient

Public Class LectureActualRepository
    Inherits BaseRepository(Of LectureActualEntity)

    Public Sub New(exec As SqlExecutor)
        MyBase.New(exec)
    End Sub

    Protected Overrides ReadOnly Property TableName As String
        Get
            Return "LectureActual"
        End Get
    End Property

    Protected Overrides ReadOnly Property PrimaryKey As String
        Get
            Return "ID"
        End Get
    End Property

    Public Function FindByDate([date] As DateTime) As List(Of LectureActualEntity)
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE LectureDate = @LectureDate"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@LectureDate", [date]))

        Dim list As New List(Of LectureActualEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LectureActualEntity)(reader))
            End While
        End Using

        Return list
    End Function

    Public Function FindByTeacher(teacherCode As String) As List(Of LectureActualEntity)
        Dim sql As String =
"SELECT * FROM " & TableName & vbCrLf &
"WHERE TeacherCode = @TeacherCode"

        Dim p As New List(Of SqlParameter)()
        p.Add(New SqlParameter("@TeacherCode", teacherCode))

        Dim list As New List(Of LectureActualEntity)()

        Using reader As SqlDataReader = Exec.ExecuteReader(sql, p)
            While reader.Read()
                list.Add(ReaderMapper.Map(Of LectureActualEntity)(reader))
            End While
        End Using

        Return list
    End Function

End Class

