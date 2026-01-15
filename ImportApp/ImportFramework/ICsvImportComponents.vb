''' <summary>
''' CSV インポートに必要なコンポーネントのセット。
''' </summary>
Public Interface ICsvImportComponents
    ReadOnly Property Reader As ICsvReader(Of RawImportRow)
    ReadOnly Property Validator As AbstractValidator(Of RawImportRow)
    ReadOnly Property Processor As AbstractProcessor(Of RawImportRow, ProcessedRow)
End Interface