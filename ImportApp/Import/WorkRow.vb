Imports Import.WindowsFormsApp2.Domain.ValueObjects

Public Class WorkRow
    Public Property Id As Integer
    Public Property WorkType As String
    Public Property StartTime As DateTime
    Public Property EndTime As DateTime
    Public Property TimeRange As TimeRange

    ' ★ 重複相手の情報を表示する列
    Public Property OverlapInfo As String
End Class