Public Class BaseForm
    Inherits Form

    Protected _wrapper As FormEventWrapper

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        ' ★ サブクラスがラップ無効を宣言している場合は何もしない
        If TypeOf Me Is IDisableEventWrap Then
            If DirectCast(Me, IDisableEventWrap).DisableEventWrap Then
                Return
            End If
        End If

        ' ★ ラップ開始
        _wrapper = New FormEventWrapper(Me)
        _wrapper.Start()
    End Sub

End Class