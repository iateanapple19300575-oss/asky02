Imports System.Drawing
Imports System.Runtime.InteropServices

Public Class ProcessingForm
    Inherits BaseForm
    Implements IDisableEventWrap

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTCAPTION As Integer = 2

    Private lbl As Label
    Private borderWidth As Integer = 4

    Public ReadOnly Property DisableEventWrap As Boolean Implements IDisableEventWrap.DisableEventWrap
        Get
            Return True
        End Get
    End Property

    Public Sub New()
        InitializeComponent()

        Me.FormBorderStyle = FormBorderStyle.None
        Me.ControlBox = False
        Me.ShowInTaskbar = False
        Me.Width = 260
        Me.Height = 80
        Me.BackColor = Color.FromArgb(255, 255, 240)

        lbl = New Label()
        lbl.Text = "処理中..."
        lbl.Font = New Font("Meiryo", 14, FontStyle.Bold)
        lbl.ForeColor = Color.Black
        lbl.BackColor = Color.Transparent
        lbl.AutoSize = True
        Me.Controls.Add(lbl)

        AddHandler Me.Resize, AddressOf ProcessingForm_Resize
        AddHandler Me.MouseDown, AddressOf Form_MouseDown
        AddHandler lbl.MouseDown, AddressOf Form_MouseDown
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim offset As Integer = borderWidth \ 2
        Dim rect As New Rectangle(offset, offset, Me.Width - borderWidth, Me.Height - borderWidth)

        Using pen As New Pen(Color.Black, borderWidth)
            e.Graphics.DrawRectangle(pen, rect)
        End Using
    End Sub

    Private Sub ProcessingForm_Resize(sender As Object, e As EventArgs)
        lbl.Left = (Me.ClientSize.Width - lbl.Width) \ 2
        lbl.Top = (Me.ClientSize.Height - lbl.Height) \ 2
    End Sub

    Private Sub Form_MouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0)
        End If
    End Sub

    Public Sub SetMessage(msg As String)
        lbl.Text = msg
        lbl.Left = (Me.ClientSize.Width - lbl.Width) \ 2
        lbl.Top = (Me.ClientSize.Height - lbl.Height) \ 2
    End Sub
End Class