Imports System.Threading

Public Class ProcessingService

    Private Shared processingForm As ProcessingForm
    Private Shared ownerForm As Form

    ' ★ どの画面からでも使える Show
    Public Shared Sub Show(owner As Form, Optional message As String = "処理中...")
        If processingForm IsNot Nothing AndAlso Not processingForm.IsDisposed Then Return

        ownerForm = owner
        ownerForm.Enabled = False

        processingForm = New ProcessingForm()
        processingForm.SetMessage(message)

        Dim x As Integer = owner.Left + (owner.Width - processingForm.Width) \ 2
        Dim y As Integer = owner.Top + (owner.Height - processingForm.Height) \ 2

        processingForm.StartPosition = FormStartPosition.Manual
        processingForm.Left = x
        processingForm.Top = y

        processingForm.Show(owner)
        processingForm.Refresh()
    End Sub

    ' ★ 完全共通化：重い処理を渡すだけでOK
    Public Shared Sub RunAsync(owner As Form, work As Action, Optional message As String = "処理中...")

        Show(owner, message)

        Dim t As New Thread(Sub()

                                Try
                                    work() ' ← 重い処理を実行
                                Finally
                                    owner.Invoke(New Action(Sub()
                                                                Close()
                                                            End Sub))
                                End Try

                            End Sub)

        t.IsBackground = True
        t.Start()
    End Sub

    ' ★ 閉じる
    Public Shared Sub Close()
        Try
            If processingForm IsNot Nothing AndAlso Not processingForm.IsDisposed Then
                processingForm.Close()
            End If
        Catch
        Finally
            processingForm = Nothing
            If ownerForm IsNot Nothing Then ownerForm.Enabled = True
            ownerForm = Nothing
        End Try
    End Sub

End Class