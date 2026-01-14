Imports System.Runtime.InteropServices
Imports WindowsApp1.Entities
Imports WindowsApp1.Framework

Public Class Form1
    ' ▼ ここで宣言する（フォーム全体で使えるようにする）
    Private _repo1 As New Master1Repository()
    Private _repo2 As New Master2Repository()
    Private _repo3 As New Master3Repository()

    Private _mode As EditMode = EditMode.None

    Private Enum EditMode
        None
        Add
        Edit
    End Enum


    Private Sub FormMasterMaintenance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 接続文字列初期化
        SqlExecutor.Initialize("Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI")

        tabMain.SelectedIndex = 0
        LoadCurrentMaster()
        SetEditMode(EditMode.None)
    End Sub

    Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged
        LoadCurrentMaster()
        SetEditMode(EditMode.None)
    End Sub

    Private Sub LoadCurrentMaster()
        Select Case tabMain.SelectedIndex
            Case 0
                LoadMaster1()
            Case 1
                LoadMaster2()
            Case 2
                LoadMaster3()
        End Select
    End Sub

    Private Sub LoadMaster1()
        Dim list = _repo1.GetAll()
        dgv.DataSource = list
    End Sub

    Private Sub LoadMaster2()
        Dim list = _repo2.GetAll()
        dgv.DataSource = list
    End Sub

    Private Sub LoadMaster3()
        Dim list = _repo3.GetAll()
        dgv.DataSource = list
    End Sub

    Private Sub SetEditMode(mode As EditMode)
        _mode = mode

        btnAdd.Enabled = (mode = EditMode.None)
        btnEdit.Enabled = (mode = EditMode.None AndAlso dgv.CurrentRow IsNot Nothing)
        btnDelete.Enabled = (mode = EditMode.None AndAlso dgv.CurrentRow IsNot Nothing)
        btnSave.Enabled = (mode <> EditMode.None)
        btnCancel.Enabled = (mode <> EditMode.None)

        ' 入力欄の Enabled 切り替えはここで
        pnlInput.Enabled = (mode <> EditMode.None)
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        ClearInput()
        SetEditMode(EditMode.Add)
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If dgv.CurrentRow Is Nothing Then Return
        BindRowToInput()
        SetEditMode(EditMode.Edit)
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgv.CurrentRow Is Nothing Then Return

        If MessageBox.Show("削除しますか？", "確認", MessageBoxButtons.YesNo) = DialogResult.No Then Return

        Select Case tabMain.SelectedIndex
            Case 0
                Dim e1 = CType(dgv.CurrentRow.DataBoundItem, Master1Entity)
                _repo1.Delete(e1)
            Case 1
                Dim e2 = CType(dgv.CurrentRow.DataBoundItem, Master2Entity)
                _repo2.Delete(e2)
            Case 2
                Dim e3 = CType(dgv.CurrentRow.DataBoundItem, Master3Entity)
                _repo3.Delete(e3)
        End Select

        LoadCurrentMaster()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Select Case tabMain.SelectedIndex
                Case 0
                    SaveMaster1()
                Case 1
                    SaveMaster2()
                Case 2
                    SaveMaster3()
            End Select

            LoadCurrentMaster()
            SetEditMode(EditMode.None)

        Catch ex As Exception
            MessageBox.Show("保存に失敗しました。" & Environment.NewLine & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        SetEditMode(EditMode.None)
    End Sub

    Private Sub ClearInput()
        txtSchool.Text = ""
        txtGrade.Text = ""
        txtClass.Text = ""
        txtKoma.Text = ""
        txtPeriodType.Text = ""
        txtPeriodValue.Text = ""
        txtStartTime.Text = ""
        txtEndTime.Text = ""
    End Sub

    Private Sub BindRowToInput()
        Select Case tabMain.SelectedIndex
            Case 0
                Dim e1 = CType(dgv.CurrentRow.DataBoundItem, Master1Entity)
                txtSchool.Text = e1.School
                txtKoma.Text = e1.Koma.ToString()
                txtWeekPattern.Text = e1.WeekPattern.ToString()
                txtStartTime.Text = e1.StartTime.ToString()
                txtEndTime.Text = e1.EndTime.ToString()

            Case 1
                Dim e2 = CType(dgv.CurrentRow.DataBoundItem, Master2Entity)
                txtSchool.Text = e2.School
                txtGrade.Text = e2.Grade.ToString()
                txtClass.Text = e2.Class.ToString()
                txtKoma.Text = e2.Koma.ToString()
                txtStartTime.Text = e2.StartTime.ToString()
                txtEndTime.Text = e2.EndTime.ToString()

            Case 2
                Dim e3 = CType(dgv.CurrentRow.DataBoundItem, Master3Entity)
                txtSchool.Text = e3.School
                txtGrade.Text = e3.Grade.ToString()
                txtClass.Text = e3.Class.ToString()
                txtKoma.Text = e3.Koma.ToString()
                txtPeriodType.Text = e3.PeriodType.ToString()
                txtPeriodValue.Text = e3.PeriodValue
                txtStartTime.Text = e3.StartTime.ToString()
                txtEndTime.Text = e3.EndTime.ToString()
        End Select
    End Sub
    Private Sub SaveMaster1()
        Dim e As New Master1Entity()
        e.School = txtSchool.Text
        e.Koma = Integer.Parse(txtKoma.Text)
        e.WeekPattern = Integer.Parse(txtWeekPattern.Text)
        e.StartTime = TimeSpan.Parse(txtStartTime.Text)
        e.EndTime = TimeSpan.Parse(txtEndTime.Text)

        If _mode = EditMode.Add Then
            _repo1.Insert(e)
        Else
            _repo1.Update(e)
        End If
    End Sub

    Private Sub SaveMaster2()
        Dim e As New Master2Entity()
        e.School = txtSchool.Text
        e.Grade = Integer.Parse(txtGrade.Text)
        e.Class = Integer.Parse(txtClass.Text)
        e.Koma = Integer.Parse(txtKoma.Text)
        e.StartTime = TimeSpan.Parse(txtStartTime.Text)
        e.EndTime = TimeSpan.Parse(txtEndTime.Text)

        If _mode = EditMode.Add Then
            _repo2.Insert(e)
        Else
            _repo2.Update(e)
        End If
    End Sub

    Private Sub SaveMaster3()
        Dim e As New Master3Entity()
        e.School = txtSchool.Text
        e.Grade = Integer.Parse(txtGrade.Text)
        e.Class = Integer.Parse(txtClass.Text)
        e.Koma = Integer.Parse(txtKoma.Text)
        e.PeriodType = Integer.Parse(txtPeriodType.Text)
        e.PeriodValue = txtPeriodValue.Text
        e.StartTime = TimeSpan.Parse(txtStartTime.Text)
        e.EndTime = TimeSpan.Parse(txtEndTime.Text)

        If _mode = EditMode.Add Then
            _repo3.Insert(e)
        Else
            _repo3.Update(e)
        End If
    End Sub


End Class
