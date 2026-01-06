Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormLogWrapper.WrapFormLifecycle(Me)
        FormLogWrapper.WrapAllEvents(Me)
    End Sub
End Class