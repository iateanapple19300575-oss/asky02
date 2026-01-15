''' <summary>
''' 加工後のデータ 1 行を表す DTO。
''' マスタ参照や変換を終えた最終形。
''' </summary>
Public Class ProcessedRow

    ''' <summary>キーコード（例：商品コード + カテゴリ）</summary>
    Public Property KeyCode As String

    ''' <summary>数量（数値に変換済み）</summary>
    Public Property Amount As Decimal

    ''' <summary>カテゴリ名（マスタ参照後の名称）</summary>
    Public Property Category As String

    ''' <summary>加工日時</summary>
    Public Property ProcessedDate As DateTime

End Class