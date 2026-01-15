''' <summary>
''' CSV A 用の Reader。
''' </summary>
Public Class CsvAReader
    Inherits AbstractCsvReader(Of RawImportRow)

    Protected Overrides Function ParseRow(cols As String(), lineNumber As Integer) As RawImportRow

        Dim row As New RawImportRow()
        row.LineNumber = lineNumber

        row.ColA = cols(0)
        row.ColB = cols(1)
        row.ColC = cols(2)

        Return row
    End Function

End Class