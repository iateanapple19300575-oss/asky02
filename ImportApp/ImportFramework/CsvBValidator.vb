Public Class CsvBValidator
    Inherits AbstractValidator(Of RawImportRow)

    Protected Overrides Sub ValidateRow(row As RawImportRow, errors As ErrorList)

        If String.IsNullOrEmpty(row.ColA) Then
            errors.Add(row.LineNumber, "ColA", "ColA は必須です。")
        End If

        If row.ColB <> "OK" AndAlso row.ColB <> "NG" Then
            errors.Add(row.LineNumber, "ColB", "ColB は OK または NG である必要があります。")
        End If

    End Sub

End Class