''' <summary>
''' アプリケーション全体で使用するログ出力サービス。
''' シンプルな静的メソッドを提供し、画面名・イベント名・メッセージを一元的に記録する。
''' </summary>
''' <remarks>
''' 現状は Debug 出力のみを行うが、将来的にファイル出力やDB保存などへ
''' 拡張しやすい構造として NotInheritable（継承禁止）で定義している。
''' </remarks>
Public NotInheritable Class LogService

    ''' <summary>
    ''' インスタンス生成を禁止するためのプライベートコンストラクタ。
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' 指定された画面名・イベント名・メッセージをログとして出力する。
    ''' </summary>
    ''' <param name="screen">ログ対象となる画面名（フォームタイトルなど）。</param>
    ''' <param name="eventName">発生したイベント名。</param>
    ''' <param name="message">ログに記録するメッセージ。</param>
    ''' <remarks>
    ''' 現在は Debug.WriteLine による出力のみを行う。
    ''' ログ形式は「時刻 [画面名] イベント名 - メッセージ」。
    ''' </remarks>
    Public Shared Sub Write(screen As String, eventName As String, message As String)

        System.Diagnostics.Debug.WriteLine(
            $"{DateTime.Now:HH:mm:ss} [{screen}] {eventName} - {message}"
        )

    End Sub

End Class