<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtSchool = New System.Windows.Forms.TextBox()
        Me.txtGrade = New System.Windows.Forms.TextBox()
        Me.txtClass = New System.Windows.Forms.TextBox()
        Me.txtKoma = New System.Windows.Forms.TextBox()
        Me.txtPeriodType = New System.Windows.Forms.TextBox()
        Me.txtPeriodValue = New System.Windows.Forms.TextBox()
        Me.txtStartTime = New System.Windows.Forms.TextBox()
        Me.txtEndTime = New System.Windows.Forms.TextBox()
        Me.tabMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtWeekPattern = New System.Windows.Forms.TextBox()
        Me.pnlInput = New System.Windows.Forms.Panel()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.tabMain.SuspendLayout()
        Me.pnlInput.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(662, 12)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(126, 34)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(662, 52)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(126, 34)
        Me.btnEdit.TabIndex = 2
        Me.btnEdit.Text = "編集"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(662, 92)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(126, 34)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(662, 132)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(126, 34)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(662, 172)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(126, 34)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "キャンセル"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtSchool
        '
        Me.txtSchool.Location = New System.Drawing.Point(25, 18)
        Me.txtSchool.Name = "txtSchool"
        Me.txtSchool.Size = New System.Drawing.Size(238, 19)
        Me.txtSchool.TabIndex = 6
        '
        'txtGrade
        '
        Me.txtGrade.Location = New System.Drawing.Point(25, 47)
        Me.txtGrade.Name = "txtGrade"
        Me.txtGrade.Size = New System.Drawing.Size(238, 19)
        Me.txtGrade.TabIndex = 7
        '
        'txtClass
        '
        Me.txtClass.Location = New System.Drawing.Point(25, 76)
        Me.txtClass.Name = "txtClass"
        Me.txtClass.Size = New System.Drawing.Size(238, 19)
        Me.txtClass.TabIndex = 8
        '
        'txtKoma
        '
        Me.txtKoma.Location = New System.Drawing.Point(25, 105)
        Me.txtKoma.Name = "txtKoma"
        Me.txtKoma.Size = New System.Drawing.Size(238, 19)
        Me.txtKoma.TabIndex = 9
        '
        'txtPeriodType
        '
        Me.txtPeriodType.Location = New System.Drawing.Point(25, 134)
        Me.txtPeriodType.Name = "txtPeriodType"
        Me.txtPeriodType.Size = New System.Drawing.Size(238, 19)
        Me.txtPeriodType.TabIndex = 10
        '
        'txtPeriodValue
        '
        Me.txtPeriodValue.Location = New System.Drawing.Point(25, 163)
        Me.txtPeriodValue.Name = "txtPeriodValue"
        Me.txtPeriodValue.Size = New System.Drawing.Size(238, 19)
        Me.txtPeriodValue.TabIndex = 11
        '
        'txtStartTime
        '
        Me.txtStartTime.Location = New System.Drawing.Point(25, 192)
        Me.txtStartTime.Name = "txtStartTime"
        Me.txtStartTime.Size = New System.Drawing.Size(238, 19)
        Me.txtStartTime.TabIndex = 12
        '
        'txtEndTime
        '
        Me.txtEndTime.Location = New System.Drawing.Point(25, 221)
        Me.txtEndTime.Name = "txtEndTime"
        Me.txtEndTime.Size = New System.Drawing.Size(238, 19)
        Me.txtEndTime.TabIndex = 13
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.TabPage1)
        Me.tabMain.Controls.Add(Me.TabPage2)
        Me.tabMain.Controls.Add(Me.TabPage3)
        Me.tabMain.Location = New System.Drawing.Point(12, 12)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(635, 51)
        Me.tabMain.TabIndex = 14
        '
        'TabPage1
        '
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(627, 25)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TabPage1"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(627, 610)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'txtWeekPattern
        '
        Me.txtWeekPattern.Location = New System.Drawing.Point(350, 18)
        Me.txtWeekPattern.Name = "txtWeekPattern"
        Me.txtWeekPattern.Size = New System.Drawing.Size(164, 19)
        Me.txtWeekPattern.TabIndex = 15
        '
        'pnlInput
        '
        Me.pnlInput.Controls.Add(Me.txtPeriodValue)
        Me.pnlInput.Controls.Add(Me.txtWeekPattern)
        Me.pnlInput.Controls.Add(Me.txtEndTime)
        Me.pnlInput.Controls.Add(Me.txtPeriodType)
        Me.pnlInput.Controls.Add(Me.txtStartTime)
        Me.pnlInput.Controls.Add(Me.txtSchool)
        Me.pnlInput.Controls.Add(Me.txtGrade)
        Me.pnlInput.Controls.Add(Me.txtClass)
        Me.pnlInput.Controls.Add(Me.txtKoma)
        Me.pnlInput.Location = New System.Drawing.Point(19, 396)
        Me.pnlInput.Name = "pnlInput"
        Me.pnlInput.Size = New System.Drawing.Size(624, 258)
        Me.pnlInput.TabIndex = 16
        '
        'TabPage3
        '
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(627, 610)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "TabPage3"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Location = New System.Drawing.Point(19, 65)
        Me.dgv.Name = "dgv"
        Me.dgv.RowTemplate.Height = 21
        Me.dgv.Size = New System.Drawing.Size(624, 325)
        Me.dgv.TabIndex = 18
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 669)
        Me.Controls.Add(Me.pnlInput)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.tabMain)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.tabMain.ResumeLayout(False)
        Me.pnlInput.ResumeLayout(False)
        Me.pnlInput.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents txtSchool As TextBox
    Friend WithEvents txtGrade As TextBox
    Friend WithEvents txtClass As TextBox
    Friend WithEvents txtKoma As TextBox
    Friend WithEvents txtPeriodType As TextBox
    Friend WithEvents txtPeriodValue As TextBox
    Friend WithEvents txtStartTime As TextBox
    Friend WithEvents txtEndTime As TextBox
    Friend WithEvents tabMain As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents txtWeekPattern As TextBox
    Friend WithEvents pnlInput As Panel
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents dgv As DataGridView
End Class
