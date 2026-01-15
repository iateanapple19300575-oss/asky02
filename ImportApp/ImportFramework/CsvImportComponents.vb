Public Class CsvImportComponents
    Implements ICsvImportComponents

    Public Sub New(reader As ICsvReader(Of RawImportRow),
                   validator As AbstractValidator(Of RawImportRow),
                   processor As AbstractProcessor(Of RawImportRow, ProcessedRow))

        Me.Reader = reader
        Me.Validator = validator
        Me.Processor = processor
    End Sub

    Public ReadOnly Property Reader As ICsvReader(Of RawImportRow) _
        Implements ICsvImportComponents.Reader

    Public ReadOnly Property Validator As AbstractValidator(Of RawImportRow) _
        Implements ICsvImportComponents.Validator

    Public ReadOnly Property Processor As AbstractProcessor(Of RawImportRow, ProcessedRow) _
        Implements ICsvImportComponents.Processor

End Class