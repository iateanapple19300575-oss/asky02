''' <summary>
''' 加工ロジックの共通処理を提供する抽象クラス。
''' </summary>
Public MustInherit Class AbstractProcessor(Of TRawRow, TProcessedRow)

    ''' <summary>
    ''' 加工処理の Template Method。
    ''' </summary>
    Public Function Process(rows As IList(Of TRawRow),
                            masterA As IList(Of MasterA),
                            masterB As IList(Of MasterB)) As IList(Of TProcessedRow)

        ' マスタの事前チェック（任意）
        ValidateMaster(masterA, masterB)

        Dim list As New List(Of TProcessedRow)

        For Each r In rows
            Dim processed = ConvertRow(r, masterA, masterB)
            list.Add(processed)
        Next

        Return list
    End Function

    ''' <summary>
    ''' マスタの事前チェック（必要な場合のみオーバーライド）
    ''' </summary>
    Protected Overridable Sub ValidateMaster(masterA As IList(Of MasterA),
                                             masterB As IList(Of MasterB))
        ' デフォルトは何もしない
    End Sub

    ''' <summary>
    ''' Raw → Processed の変換（サブクラスが実装）
    ''' </summary>
    Protected MustOverride Function ConvertRow(row As TRawRow,
                                               masterA As IList(Of MasterA),
                                               masterB As IList(Of MasterB)) As TProcessedRow

End Class