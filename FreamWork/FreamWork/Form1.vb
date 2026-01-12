Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LogService.WriteInfo("アプリ起動")

        Dim planService As New LecturePlanService()
        Dim actualService As New LectureActualService()

        ' 予定登録
        Dim plan As New LecturePlanEntity() With {
            .LectureDate = DateTime.Today,
            .TeacherCode = "T001",
            .SubjectCode = "SUB01",
            .Status = 0,
            .ActualHours = 2D
        }
        planService.RunInsert(plan)

        ' 実績登録（予定と整合性チェック＋予定を完了）
        Dim actual As New LectureActualEntity() With {
            .LectureDate = DateTime.Today,
            .TeacherCode = "T001",
            .SubjectCode = "SUB01",
            .LectureHours = 2D
        }
        actualService.RunInsert(actual)

        ' 検索
        Dim cond As New SearchCondition
        cond.Items.Add("LectureDate", DateTime.Today)
        cond.Items.Add("TeacherCode", "T001")
        cond.Items.Add("SubjectCode", "SUB01")
        Dim List = planService.SearchPlans(cond)
        Console.WriteLine("検索結果件数: " & list.Count)

        LogService.WriteInfo("アプリ終了")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim service As New CSVImportService()
            Dim result = service.Execute(txtCsvPath.Text)

            If result.ValidationErrors.Count > 0 Then
                ' バリデーションエラーを画面に表示
                DataGridView1.DataSource = result.ValidationErrors
                MessageBox.Show("CSVにエラーがあります。修正してください。")
                Exit Sub
            End If

            If result.ImportError IsNot Nothing Then
                MessageBox.Show("取り込み中にエラーが発生しました: " & result.ImportError)
                Exit Sub
            End If

            MessageBox.Show("取り込みが正常に完了しました。")

        Catch ex As LectpayAppException
            MessageBox.Show(ex.Message)

        Catch ex As Exception

        End Try

    End Sub
End Class
