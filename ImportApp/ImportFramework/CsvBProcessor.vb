Public Class CsvBProcessor
    Inherits AbstractProcessor(Of RawImportRow, ProcessedRow)

    Protected Overrides Function ConvertRow(row As RawImportRow,
                                            masterA As IList(Of MasterA),
                                            masterB As IList(Of MasterB)) As ProcessedRow

        Dim p As New ProcessedRow()

        ' CSV B は KeyCode の作り方が違う例
        p.KeyCode = row.ColA

        ' 数量は ColC に入っている例
        p.Amount = Decimal.Parse(row.ColC)

        ' マスタ参照（例：商品名）
        Dim m = masterA.FirstOrDefault(Function(x) x.Code = row.ColA)
        p.Category = If(m IsNot Nothing, m.Name, "不明")

        p.ProcessedDate = DateTime.Now

        Return p
    End Function

End Class