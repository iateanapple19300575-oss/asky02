Imports System.Reflection

Public Class FormEventWrapper

    Private ReadOnly _form As Form
    Private ReadOnly _formTitle As String
    Private Shared ReadOnly _wrapped As New HashSet(Of String)()

    ' 対象イベント一覧
    Private Shared ReadOnly TargetEvents As New Dictionary(Of Type, String()) From {
        {GetType(Button), New String() {"Click"}},
        {GetType(ComboBox), New String() {"SelectedIndexChanged"}},
        {GetType(RadioButton), New String() {"CheckedChanged"}},
        {GetType(CheckBox), New String() {"CheckedChanged"}},
        {GetType(TextBox), New String() {"TextChanged", "LostFocus"}}
    }

    Public Sub New(form As Form)
        _form = form
        _formTitle = form.Text
    End Sub

    Public Sub Start()
        WrapFormLifecycle()
        WrapAllEvents(_form)
        AddHandler _form.FormClosed, AddressOf OnFormClosed
    End Sub

    ' ------------------------------------------------------------
    ' フォームのライフサイクルイベント
    ' ------------------------------------------------------------
    Private Sub WrapFormLifecycle()

        AddHandler _form.Load,
            Sub(sender, e)
                LogService.Write(_formTitle, "Form.Load", "開始")
            End Sub

        AddHandler _form.Shown,
            Sub(sender, e)
                LogService.Write(_formTitle, "Form.Shown", "表示完了")
            End Sub

        AddHandler _form.FormClosing,
            Sub(sender, e)
                LogService.Write(_formTitle, "Form.FormClosing", "終了開始")
            End Sub

        AddHandler _form.FormClosed,
            Sub(sender, e)
                LogService.Write(_formTitle, "Form.FormClosed", "終了完了")
            End Sub

    End Sub

    ' ------------------------------------------------------------
    ' コントロールイベントのラップ
    ' ------------------------------------------------------------
    Private Sub WrapAllEvents(root As Control)
        WrapControl(root)

        For Each child As Control In root.Controls
            WrapAllEvents(child)
        Next
    End Sub

    Private Sub WrapControl(ctrl As Control)

        Dim t As Type = ctrl.GetType()

        If Not TargetEvents.ContainsKey(t) Then Return

        For Each evName In TargetEvents(t)

            Dim ev As EventInfo = t.GetEvent(evName)
            If ev Is Nothing Then Continue For

            If ev.EventHandlerType IsNot GetType(EventHandler) Then Continue For

            Dim key As String = ctrl.Name & "." & evName
            If _wrapped.Contains(key) Then Continue For
            _wrapped.Add(key)

            AddHandlerDirect(ctrl, ev, key)
        Next

    End Sub

    Private Sub AddHandlerDirect(ctrl As Control, ev As EventInfo, key As String)

        Dim eventName As String = key

        Dim wrapper As EventHandler =
            Sub(sender As Object, e As EventArgs)

                Try
                    LogService.Write(_formTitle, eventName, "開始")
                    ' 元のハンドラは AddHandler により自動で呼ばれる
                    LogService.Write(_formTitle, eventName, "終了")

                Catch ex As Exception
                    LogService.Write(_formTitle, eventName, "例外: " & ex.Message)
                End Try

            End Sub

        ev.AddEventHandler(ctrl, wrapper)
    End Sub

    Private Sub OnFormClosed(sender As Object, e As FormClosedEventArgs)
        ' 必要なら後処理
    End Sub

End Class