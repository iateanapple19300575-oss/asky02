Public Class CSVImportService

    Public Function Execute(csvPath As String) As CSVImportResult

        Using exec As New SqlExecutor(AppEnvironment.ConnectionString)
            Using tran = exec.BeginTransaction()

                Dim repo As New LecturePlanRepository(exec)
                Dim historyRepo As New ImportHistoryRepository(exec)
                Dim domain As New CSVImportDomainService(repo, historyRepo)

                Dim result = domain.Process(csvPath)

                ' バリデーションエラー or 取り込みエラーならロールバック
                If result.ValidationErrors.Count > 0 OrElse result.ImportError IsNot Nothing Then
                    tran.Rollback()
                    Return result
                End If

                ' 正常ならコミット
                tran.Commit()
                Return result

            End Using
        End Using

    End Function

End Class