''' <summary>
''' CSV の生データ 1 行を表す DTO。
''' 加工前の値をそのまま保持する。
''' </summary>
Public Class RawImportRow

    ''' <summary>CSV の列 A（例：商品コード）</summary>
    Public Property ColA As String

    ''' <summary>CSV の列 B（例：数量）</summary>
    Public Property ColB As String

    ''' <summary>CSV の列 C（例：カテゴリコード）</summary>
    Public Property ColC As String

    ''' <summary>CSV の行番号（エラー表示用）</summary>
    Public Property LineNumber As Integer

End Class