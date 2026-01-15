''' <summary>
''' CSV インポート処理の結果を表す DTO。
''' UI が結果を判断するための情報のみを保持する。
''' </summary>
Public Class ImportResult

    ''' <summary>処理が成功したかどうか</summary>
    Public ReadOnly Property Success As Boolean

    ''' <summary>エラー一覧（成功時は空）</summary>
    Public ReadOnly Property Errors As ErrorList

    ''' <summary>UI 表示用のメッセージ（任意）</summary>
    Public ReadOnly Property Message As String

    Public ReadOnly Property RowCount As Integer


    ''' <summary>
    ''' 成功時のコンストラクタ。
    ''' </summary>
    Public Sub New()
        Me.Success = True
        Me.Errors = New ErrorList()
        Me.Message = "インポートが正常に完了しました。"
    End Sub


    ''' <summary>
    ''' 失敗時のコンストラクタ（エラー一覧付き）。
    ''' </summary>
    Public Sub New(errors As ErrorList)
        Me.Success = False
        Me.Errors = errors
        Me.Message = "インポートに失敗しました。"
    End Sub


    ''' <summary>
    ''' 失敗時のコンストラクタ（メッセージ指定）。
    ''' </summary>
    Public Sub New(message As String)
        Me.Success = False
        Me.Errors = New ErrorList()
        Me.Message = message
    End Sub

End Class