''' <summary>
''' 講義予定エンティティ。
''' </summary>
Public Class LecturePlanEntity
    Inherits BaseEntity

    Public Property LectureDate As DateTime
    Public Property TeacherCode As String
    Public Property SubjectCode As String

    ''' <summary>
    ''' 0=未実施, 1=完了
    ''' </summary>
    Public Property Status As Integer

    ''' <summary>
    ''' 予定／実績時間
    ''' </summary>
    Public Property ActualHours As Decimal

End Class

