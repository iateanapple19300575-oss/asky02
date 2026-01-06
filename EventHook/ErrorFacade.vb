''' <summary>
''' アプリケーション内で発生した例外を一元的に報告するためのファサードクラス。
''' ログ出力や通知処理など、例外処理の共通化ポイントとして利用する。
''' </summary>
''' <remarks>
''' 現状は Debug 出力のみを行うが、将来的にログファイル保存や
''' エラーダイアログ表示などへ拡張しやすい構造として設計されている。
''' </remarks>
Public NotInheritable Class ErrorFacade

    ''' <summary>
    ''' インスタンス生成を禁止するためのプライベートコンストラクタ。
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' 指定された例外とエラー発生コンテキストを報告する。
    ''' </summary>
    ''' <param name="ex">発生した例外。</param>
    ''' <param name="context">例外が発生した状況を示すコンテキスト。</param>
    ''' <remarks>
    ''' 現在は Debug.WriteLine による簡易ログ出力のみを行う。
    ''' 出力形式は「[Error] コンテキスト: メッセージ」。
    ''' </remarks>
    Public Shared Sub Report(ex As Exception, context As ErrorContext)

        System.Diagnostics.Debug.WriteLine(
            $"[Error] {context}: {ex.Message}"
        )

    End Sub

End Class

''' <summary>
''' エラー発生時の状況（コンテキスト）を表す列挙体。
''' </summary>
Public Enum ErrorContext

    ''' <summary>
    ''' ユーザー操作（手動処理）中に発生したエラー。
    ''' </summary>
    Manual

    ''' <summary>
    ''' システム処理（自動処理）中に発生したエラー。
    ''' </summary>
    System

End Enum