Imports System.Threading

Public Class FrmSample
    Inherits BaseForm
    Implements IDisableEventWrap

    Public ReadOnly Property DisableEventWrap As Boolean _
        Implements IDisableEventWrap.DisableEventWrap
        Get
            Return True   ' ← この画面はラップしない
        End Get
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ProcessingService.RunAsync(Me,
        Sub()
            Thread.Sleep(5000) ' ← 重い処理
        End Sub,
        "集計中..."
    )

    End Sub

    Private Sub HeavyWork()
        Thread.Sleep(5000) ' ← 時間のかかる処理
    End Sub
End Class
