Public Class CsvBReader
    Inherits AbstractCsvReader(Of RawImportRow)

    Protected Overrides Function ParseRow(cols As String(), lineNumber As Integer) As RawImportRow

        Dim row As New RawImportRow()
        row.LineNumber = lineNumber

        ' CSV B は列数が違う例
        row.ColA = cols(0)
        row.ColB = cols(3)   ' 別の列位置
        row.ColC = cols(5)

        Return row
    End Function

End Class