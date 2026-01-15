Imports Import.WindowsFormsApp2.Domain.Services
Imports Import.WindowsFormsApp2.Domain.ValueObjects

Public Class Form1
    Inherits Form

    Private DataGridView1 As DataGridView

    Public Sub New()
        InitializeComponent()

        Me.DataGridView1 = New DataGridView()
        Me.DataGridView1.Dock = DockStyle.Fill
        Me.Controls.Add(Me.DataGridView1)

        ' ★ VB.NET では AddHandler を使う
        AddHandler Me.Load, AddressOf Form1_Load
    End Sub


    '    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '        ' ★ 1. ダミーデータ作成（Lesson / Prep 混在）
    '        Dim rows As New List(Of WorkRow)()

    '        ' 2025/01/01 のダミーデータ
    '        Dim baseDate As DateTime = #2025/01/01#

    '        ' Lesson 10:00-11:00
    '        rows.Add(New WorkRow() With {
    '            .Id = 1,
    '            .WorkType = "Lesson",
    '            .StartTime = baseDate.AddHours(10),
    '            .EndTime = baseDate.AddHours(11),
    '            .TimeRange = New TimeRange(baseDate.AddHours(10), baseDate.AddHours(11))
    '        })

    '        ' Prep 10:30-11:30（上と重複）
    '        rows.Add(New WorkRow() With {
    '            .Id = 2,
    '            .WorkType = "Prep",
    '            .StartTime = baseDate.AddHours(10.5),
    '            .EndTime = baseDate.AddHours(11.5),
    '            .TimeRange = New TimeRange(baseDate.AddHours(10.5), baseDate.AddHours(11.5))
    '        })

    '        ' Lesson 12:00-13:00（重複なし）
    '        rows.Add(New WorkRow() With {
    '            .Id = 3,
    '            .WorkType = "Lesson",
    '            .StartTime = baseDate.AddHours(12),
    '            .EndTime = baseDate.AddHours(13),
    '            .TimeRange = New TimeRange(baseDate.AddHours(12), baseDate.AddHours(13))
    '        })

    '        ' Prep 12:30-13:30（上と重複）
    '        rows.Add(New WorkRow() With {
    '            .Id = 4,
    '            .WorkType = "Prep",
    '            .StartTime = baseDate.AddHours(12.5),
    '            .EndTime = baseDate.AddHours(13.5),
    '            .TimeRange = New TimeRange(baseDate.AddHours(12.5), baseDate.AddHours(13.5))
    '        })

    '        ' ★ 2. 重複チェック
    '        Dim service As New TimeRangeOverlapService()
    '        Dim ranges As List(Of TimeRange) = rows.Select(Function(r) r.TimeRange).ToList()
    '        Dim overlaps As IList(Of OverlapResult) = service.FindOverlaps(ranges)

    '        ' ★ 3. OverlapInfo を埋める
    '        For Each o In overlaps
    '            Dim src As WorkRow = rows.First(Function(r) r.TimeRange Is o.Source)
    '            Dim tgt As WorkRow = rows.First(Function(r) r.TimeRange Is o.Conflict)

    '            src.OverlapInfo &= String.Format("{0} {1:HH:mm}-{2:HH:mm}; ",
    '                                             tgt.WorkType, tgt.StartTime, tgt.EndTime)

    '            tgt.OverlapInfo &= String.Format("{0} {1:HH:mm}-{2:HH:mm}; ",
    '                                             src.WorkType, src.StartTime, src.EndTime)
    '        Next

    '        ' ★ 4. DataGridView にバインド
    '        Me.DataGridView1.AutoGenerateColumns = True
    '        Me.DataGridView1.DataSource = rows

    '        ' ★ 5. 重複行をハイライト
    '        For Each dgRow As DataGridViewRow In Me.DataGridView1.Rows
    '            Dim wr As WorkRow = TryCast(dgRow.DataBoundItem, WorkRow)
    '            If wr IsNot Nothing AndAlso Not String.IsNullOrEmpty(wr.OverlapInfo) Then
    '                dgRow.DefaultCellStyle.BackColor = Color.MistyRose
    '            End If
    '        Next

    '    End Sub




    'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    '    '-----------------------------------------
    '    ' 0. Repository の準備
    '    '-----------------------------------------
    '    Dim connStr As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"

    '    Dim lessonRepo As New LessonWorkRepository(connStr)
    '    Dim prepRepo As New PreparationWorkRepository(connStr)

    '    Dim teacherId As Integer = 1
    '    'Dim targetDate As DateTime = DateTime.Today
    '    Dim targetDate As DateTime = New Date(2025, 1, 1)

    '    '-----------------------------------------
    '    ' 1. Repository からデータ取得
    '    '-----------------------------------------
    '    Dim lessonList As List(Of LessonWork) =
    '    lessonRepo.GetByTeacherAndDate(teacherId, targetDate)

    '    Dim prepList As List(Of PreparationWork) =
    '    prepRepo.GetByTeacherAndDate(teacherId, targetDate)

    '    '-----------------------------------------
    '    ' 2. WorkRow に変換（UI 専用 DTO）
    '    '-----------------------------------------
    '    Dim rows As New List(Of WorkRow)()

    '    For Each lw In lessonList
    '        rows.Add(New WorkRow() With {
    '        .Id = lw.Id,
    '        .WorkType = "Lesson",
    '        .StartTime = lw.StartTime,
    '        .EndTime = lw.EndTime,
    '        .TimeRange = New TimeRange(lw.StartTime, lw.EndTime)
    '    })
    '    Next

    '    For Each pw In prepList
    '        rows.Add(New WorkRow() With {
    '        .Id = pw.Id,
    '        .WorkType = "Prep",
    '        .StartTime = pw.StartTime,
    '        .EndTime = pw.EndTime,
    '        .TimeRange = New TimeRange(pw.StartTime, pw.EndTime)
    '    })
    '    Next

    '    '-----------------------------------------
    '    ' 3. 重複チェック
    '    '-----------------------------------------
    '    Dim service As New TimeRangeOverlapService()
    '    Dim overlaps As IList(Of OverlapResult) =
    '    service.FindOverlaps(rows.Select(Function(r) r.TimeRange).ToList())

    '    '-----------------------------------------
    '    ' 4. OverlapInfo を埋める
    '    '-----------------------------------------
    '    For Each o In overlaps
    '        Dim src = rows.First(Function(r) r.TimeRange Is o.Source)
    '        Dim tgt = rows.First(Function(r) r.TimeRange Is o.Conflict)

    '        src.OverlapInfo &= String.Format("{0} {1:HH:mm}-{2:HH:mm}; ",
    '                                     tgt.WorkType, tgt.StartTime, tgt.EndTime)

    '        tgt.OverlapInfo &= String.Format("{0} {1:HH:mm}-{2:HH:mm}; ",
    '                                     src.WorkType, src.StartTime, src.EndTime)
    '    Next

    '    '-----------------------------------------
    '    ' 5. DataGridView にバインド
    '    '-----------------------------------------
    '    DataGridView1.AutoGenerateColumns = True
    '    DataGridView1.DataSource = rows

    '    '-----------------------------------------
    '    ' 6. 重複行をハイライト
    '    '-----------------------------------------
    '    For Each dgRow As DataGridViewRow In DataGridView1.Rows
    '        Dim wr As WorkRow = TryCast(dgRow.DataBoundItem, WorkRow)
    '        If wr IsNot Nothing AndAlso Not String.IsNullOrEmpty(wr.OverlapInfo) Then
    '            dgRow.DefaultCellStyle.BackColor = Color.MistyRose
    '        End If
    '    Next

    'End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs)

        Dim connStr As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"
        Dim repo As New OverlapRepository(connStr)

        ' 1か月分（例：2025年1月）
        Dim startDate As DateTime = #2025/01/01#
        Dim endDate As DateTime = #2025/02/01#

        ' SQL だけで重複ペアを抽出
        'Dim dt As DataTable = repo.GetMonthlyOverlaps(startDate, endDate)

        Dim dt As DataTable = repo.GetMonthlyWorkWithOverlap(startDate, endDate)

        ' DataGridView に表示
        DataGridView1.AutoGenerateColumns = True
        'DataGridView1.DataSource = dt
        DataGridView1.DataSource = dt

        '' 重複行をハイライト（Source 側だけ）
        'For Each row As DataGridViewRow In DataGridView1.Rows
        '    row.DefaultCellStyle.BackColor = Color.MistyRose
        'Next

        ' ★ 重複がある行だけ色付け
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim overlap As Object = row.Cells("OverlapInfo").Value

            If overlap IsNot Nothing AndAlso Not String.IsNullOrEmpty(overlap.ToString()) Then
                row.DefaultCellStyle.BackColor = Color.MistyRose
            End If
        Next


    End Sub




End Class

