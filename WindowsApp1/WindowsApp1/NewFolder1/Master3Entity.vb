Namespace Entities
    Public Class Master3Entity
        Public Property School As String
        Public Property Grade As Integer
        Public Property [Class] As Integer
        Public Property Koma As Integer
        Public Property PeriodType As Integer   ' 1=年, 2=年月, 3=年月日
        Public Property PeriodValue As String   ' "2024", "2024-04", "2024-04-15"
        Public Property StartTime As TimeSpan
        Public Property EndTime As TimeSpan
    End Class
End Namespace