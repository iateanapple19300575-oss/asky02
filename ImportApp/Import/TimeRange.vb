Namespace WindowsFormsApp2.Domain.ValueObjects

    Public NotInheritable Class TimeRange

        Public ReadOnly Property StartTime As DateTime
        Public ReadOnly Property EndTime As DateTime

        Public Sub New(startTime As DateTime, endTime As DateTime)
            If endTime <= startTime Then
                Throw New ArgumentException("終了時刻は開始時刻より後である必要があります。")
            End If

            Me.StartTime = startTime
            Me.EndTime = endTime
        End Sub

        Public Function Overlaps(other As TimeRange) As Boolean
            Return Me.StartTime < other.EndTime AndAlso other.StartTime < Me.EndTime
        End Function

        Public ReadOnly Property Duration As TimeSpan
            Get
                Return EndTime - StartTime
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("{0:HH:mm} - {1:HH:mm}", StartTime, EndTime)
        End Function

    End Class

End Namespace