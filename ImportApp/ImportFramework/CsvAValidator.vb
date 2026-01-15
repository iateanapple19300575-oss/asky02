''' <summary>
''' CSV A 用のバリデーション。
''' </summary>
Public Class CsvAValidator
    Inherits AbstractValidator(Of RawImportRow)

    Protected Overrides Sub ValidateRow(row As RawImportRow, errors As ErrorList)

        If String.IsNullOrEmpty(row.ColA) Then
            errors.Add(row.LineNumber, "ColA", "ColA は必須です。")
        End If

        If Not IsNumeric(row.ColB) Then
            errors.Add(row.LineNumber, "ColB", "ColB は数値である必要があります。")
        End If

        If row.ColC.Length > 10 Then
            errors.Add(row.LineNumber, "ColC", "ColC は 10 文字以内である必要があります。")
        End If

    End Sub

End Class