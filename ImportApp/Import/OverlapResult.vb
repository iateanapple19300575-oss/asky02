Namespace WindowsFormsApp2.Domain.ValueObjects

    Public NotInheritable Class OverlapResult

        Public ReadOnly Property Source As TimeRange
        Public ReadOnly Property Conflict As TimeRange

        Public Sub New(source As TimeRange, conflict As TimeRange)
            Me.Source = source
            Me.Conflict = conflict
        End Sub

    End Class

End Namespace