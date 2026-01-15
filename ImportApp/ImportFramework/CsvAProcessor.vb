''' <summary>
''' CSV A 用の加工ロジック。
''' </summary>
Public Class CsvAProcessor
    Inherits AbstractProcessor(Of RawImportRow, ProcessedRow)

    Protected Overrides Function ConvertRow(row As RawImportRow,
                                            masterA As IList(Of MasterA),
                                            masterB As IList(Of MasterB)) As ProcessedRow

        Dim p As New ProcessedRow()

        ' 商品コード + カテゴリコード → KeyCode
        p.KeyCode = row.ColA & "-" & row.ColC

        ' 数量を Decimal に変換
        p.Amount = Decimal.Parse(row.ColB)

        ' マスタ参照（例：カテゴリ名）
        Dim m = masterB.FirstOrDefault(Function(x) x.Code = row.ColC)
        p.Category = If(m IsNot Nothing, m.Name, "不明")

        p.ProcessedDate = DateTime.Now

        Return p
    End Function

End Class