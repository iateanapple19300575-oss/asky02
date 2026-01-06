Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormLogWrapper.WrapFormLifecycle(Me)
        FormLogWrapper.WrapAllEvents(Me)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New Form2
        f.MdiParent = Me
        f.Show()
    End Sub
End Class