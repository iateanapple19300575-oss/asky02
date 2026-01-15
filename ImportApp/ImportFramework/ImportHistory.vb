''' <summary>
''' インポート処理の履歴を表す DTO。
''' 成功・失敗、件数、ファイル名などを保持する。
''' </summary>
Public Class ImportHistory

    ''' <summary>インポートしたファイル名</summary>
    Public Property FileName As String

    ''' <summary>インポートした行数</summary>
    Public Property RowCount As Integer

    ''' <summary>処理が成功したかどうか</summary>
    Public Property Success As Boolean

    ''' <summary>処理日時</summary>
    Public Property ExecDate As DateTime

    ''' <summary>エラーメッセージ（失敗時のみ）</summary>
    Public Property ErrorMessage As String

End Class