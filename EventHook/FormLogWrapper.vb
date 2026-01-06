Imports System.Reflection

''' <summary>
''' フォームおよびコントロールのイベントを横断的にラップし、
''' 各イベントの開始・終了・例外をログとして記録するユーティリティクラス。
''' </summary>
''' <remarks>
''' ・対象イベントは <see cref="TargetEvents"/> にて型ごとに定義される。<br/>
''' ・同一イベントを二重にラップしないよう内部セットで管理する。<br/>
''' ・フォームのライフサイクルイベント（Load/Shown/FormClosing/FormClosed）も自動で記録する。<br/>
''' ・CheckBox / RadioButton の CheckedChanged では ON/OFF 状態もログに含める。
''' </remarks>
Public Class FormLogWrapper

    ''' <summary>
    ''' すでにラップ済みの「コントロール名.イベント名」を保持するセット。
    ''' 二重ラップを防止するために使用する。
    ''' </summary>
    Private Shared ReadOnly _wrapped As New HashSet(Of String)()

    ''' <summary>
    ''' コントロール型ごとにラップ対象とするイベント名の一覧。
    ''' </summary>
    Private Shared ReadOnly TargetEvents As New Dictionary(Of Type, String()) From {
        {GetType(Button), New String() {"Click"}},
        {GetType(ComboBox), New String() {"SelectedIndexChanged"}},
        {GetType(RadioButton), New String() {"CheckedChanged"}},
        {GetType(CheckBox), New String() {"CheckedChanged"}},
        {GetType(TextBox), New String() {"LostFocus"}}
    }

    ' ------------------------------------------------------------
    ' フォームのライフサイクル
    ' ------------------------------------------------------------

    ''' <summary>
    ''' 指定したフォームの主要ライフサイクルイベント（Load、Shown、FormClosing、FormClosed）をラップし、
    ''' 各イベントの開始・終了をログに記録する。
    ''' </summary>
    ''' <param name="form">ラップ対象のフォーム。</param>
    Public Shared Sub WrapFormLifecycle(form As Form)

        AddHandler form.Load,
            Sub(sender, e)
                LogService.Write(form.Text, "Form.Load", "開始")
            End Sub

        AddHandler form.Shown,
            Sub(sender, e)
                LogService.Write(form.Text, "Form.Shown", "表示完了")
            End Sub

        AddHandler form.FormClosing,
            Sub(sender, e)
                LogService.Write(form.Text, "Form.FormClosing", "終了開始")
            End Sub

        AddHandler form.FormClosed,
            Sub(sender, e)
                LogService.Write(form.Text, "Form.FormClosed", "終了完了")
            End Sub

    End Sub

    ' ------------------------------------------------------------
    ' コントロールイベントのラップ
    ' ------------------------------------------------------------

    ''' <summary>
    ''' 指定したルートコントロール配下のすべてのコントロールに対して、
    ''' 対象イベントのラップ処理を再帰的に適用する。
    ''' </summary>
    ''' <param name="root">ラップ対象のルートコントロール。</param>
    Public Shared Sub WrapAllEvents(root As Control)
        WrapControl(root)

        For Each child As Control In root.Controls
            WrapAllEvents(child)
        Next
    End Sub

    ''' <summary>
    ''' 単一コントロールに対して、対象イベントをラップする。
    ''' </summary>
    ''' <param name="ctrl">ラップ対象のコントロール。</param>
    Private Shared Sub WrapControl(ctrl As Control)

        Dim t As Type = ctrl.GetType()

        ' 対象外の型は無視
        If Not TargetEvents.ContainsKey(t) Then
            Return
        End If

        For Each evName In TargetEvents(t)

            Dim ev As EventInfo = t.GetEvent(evName)
            If ev Is Nothing Then
                Continue For
            End If

            ' EventHandler 型以外は対象外
            If ev.EventHandlerType IsNot GetType(EventHandler) Then
                Continue For
            End If

            Dim key As String = ctrl.Name & "." & evName

            ' 二重ラップ防止
            If _wrapped.Contains(key) Then
                Continue For
            End If
            _wrapped.Add(key)

            ' ラップハンドラを追加
            AddHandlerDirect(ctrl, ev, key)
        Next

    End Sub

    ''' <summary>
    ''' 指定されたイベントに対してログ出力を行うラップハンドラを追加する。
    ''' CheckBox および RadioButton の場合は、Checked 状態（ON/OFF）もログに含める。
    ''' </summary>
    ''' <param name="ctrl">イベントを持つ対象コントロール。</param>
    ''' <param name="ev">ラップ対象のイベント情報。</param>
    ''' <param name="key">「コントロール名.イベント名」を表す識別キー。</param>
    ''' <remarks>
    ''' ・すべてのイベントラップはこのメソッドを経由するため、追加情報（ON/OFF）もここで一元的に処理できる。<br/>
    ''' ・元のイベントハンドラは AddEventHandler により自動的に呼び出される。<br/>
    ''' ・ログは開始と終了の 2 回出力される。
    ''' </remarks>
    Private Shared Sub AddHandlerDirect(ctrl As Control, ev As EventInfo, key As String)

        Dim form As Form = ctrl.FindForm()
        Dim eventName As String = key

        Dim wrapper As EventHandler =
            Sub(sender As Object, e As EventArgs)

                Try
                    ' ----------------------------------------------------
                    ' CheckBox / RadioButton の ON/OFF 状態を取得
                    ' ----------------------------------------------------
                    Dim stateInfo As String = ""

                    If TypeOf ctrl Is CheckBox Then
                        Dim cb = DirectCast(ctrl, CheckBox)
                        stateInfo = If(cb.Checked, "ON", "OFF")

                    ElseIf TypeOf ctrl Is RadioButton Then
                        Dim rb = DirectCast(ctrl, RadioButton)
                        stateInfo = If(rb.Checked, "ON", "OFF")

                        ' ComboBox の選択表示項目
                    ElseIf TypeOf ctrl Is ComboBox Then
                        Dim cmb = DirectCast(ctrl, ComboBox)
                        Dim selectedText As String = If(cmb.Text, "")
                        stateInfo = $"選択: {selectedText}"

                        ' TextBox の入力内容（LostFocus 時のみ）
                    ElseIf TypeOf ctrl Is TextBox AndAlso ev.Name = "LostFocus" Then
                        Dim tb = DirectCast(ctrl, TextBox)
                        stateInfo = $"入力: {tb.Text}"

                    End If

                    ' ----------------------------------------------------
                    ' ログ出力（開始）
                    ' ----------------------------------------------------
                    If stateInfo <> "" Then
                        'LogService.Write(form.Text, eventName, $"開始 ({stateInfo})")
                        LogService.Write(form.Text, eventName, $" ({stateInfo})")
                    Else
                        'LogService.Write(form.Text, eventName, "開始")
                        LogService.Write(form.Text, eventName, "")
                    End If

                    ' 元のハンドラは AddEventHandler により自動で呼ばれる

                    ' ----------------------------------------------------
                    ' ログ出力（終了）
                    ' ----------------------------------------------------
                    If stateInfo <> "" Then
                        'LogService.Write(form.Text, eventName, $"終了 ({stateInfo})")
                    Else
                        'LogService.Write(form.Text, eventName, "終了")
                    End If

                Catch ex As Exception
                    LogService.Write(form.Text, eventName, "例外: " & ex.Message)
                End Try

            End Sub

        ev.AddEventHandler(ctrl, wrapper)
    End Sub

End Class