''' <summary>
''' 講義実績エンティティ。
''' </summary>
Public Class LectureActualEntity
    Inherits BaseEntity

    Public Property LectureDate As DateTime
    Public Property TeacherCode As String
    Public Property SubjectCode As String
    Public Property LectureHours As Decimal
    Public Property Remarks As String

    Public Property RoomCode As String
    Public Property CourseCode As String

End Class

