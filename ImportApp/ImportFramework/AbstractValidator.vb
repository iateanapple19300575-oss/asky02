''' <summary>
''' バリデーションの共通処理を提供する抽象クラス。
''' </summary>
Public MustInherit Class AbstractValidator(Of TRawRow)

    ''' <summary>
    ''' 全行バリデーション（Template Method）
    ''' </summary>
    Public Function Validate(rows As IList(Of TRawRow)) As ErrorList
        Dim errors As New ErrorList()

        For Each row In rows
            ValidateRow(row, errors)
        Next

        Return errors
    End Function

    ''' <summary>
    ''' 行単位のバリデーション（サブクラスが実装）
    ''' </summary>
    Protected MustOverride Sub ValidateRow(row As TRawRow, errors As ErrorList)

End Class