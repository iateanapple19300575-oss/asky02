Imports Import.WindowsFormsApp2.Domain.ValueObjects

Namespace WindowsFormsApp2.Domain.Services

    Public NotInheritable Class TimeRangeOverlapService

        Public Function FindOverlaps(ranges As IList(Of TimeRange)) As IList(Of OverlapResult)
            Dim results As New List(Of OverlapResult)()

            For i As Integer = 0 To ranges.Count - 1
                For j As Integer = i + 1 To ranges.Count - 1

                    Dim a As TimeRange = ranges(i)
                    Dim b As TimeRange = ranges(j)

                    If a.Overlaps(b) Then
                        results.Add(New OverlapResult(a, b))
                    End If

                Next
            Next

            Return results
        End Function

    End Class

End Namespace