Imports Import.WindowsFormsApp2.Domain.ValueObjects

Namespace WindowsFormsApp2.Domain.Factories

    Public NotInheritable Class TimeRangeFactory

        Public Shared Function FromLesson(work As LessonWork) As TimeRange
            Return New TimeRange(work.StartTime, work.EndTime)
        End Function

        Public Shared Function FromPreparation(work As PreparationWork) As TimeRange
            Return New TimeRange(work.StartTime, work.EndTime)
        End Function

    End Class

End Namespace