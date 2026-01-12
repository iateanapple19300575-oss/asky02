Imports System.IO

Public Class CSVImportDomainService

    Private ReadOnly _repo As LecturePlanRepository
    Private ReadOnly _historyRepo As ImportHistoryRepository

    Public Sub New(repo As LecturePlanRepository, historyRepo As ImportHistoryRepository)
        _repo = repo
        _historyRepo = historyRepo
    End Sub

    Public Function Process(csvPath As String) As CSVImportResult

        Dim result As New CSVImportResult()

        ' CSV → Entity
        Dim entities = CsvEntityMapper.MapCsvToEntities(Of LecturePlanEntity)(csvPath)
        Dim allLines() As String = File.ReadAllLines(csvPath)

        Try
            ' ① バリデーション
            Dim lineNo As Integer = 2

            For Each entity In entities

                Dim vr = Validate(entity)

                If Not vr.IsValid Then
                    result.ValidationErrors.Add(New ValidationErrorRow With {
                    .LineNo = lineNo,
                    .ErrorMessage = String.Join(" / ", vr.Errors.ToArray()),
                    .RawData = allLines(lineNo - 1)
                })
                End If

                lineNo += 1
            Next

            ' バリデーションエラーがあれば終了
            If result.ValidationErrors.Count > 0 Then
                Return result
            End If

        Catch ex As LectpayAppException
            Throw

        Catch ex As Exception
            result.ImportError = ex.Message
            Throw New ImportServiceException("データインポート処理でエラーが発生しました。", ex)

        End Try

        Try
            ' ② 取り込み
            For Each entity In entities
                _repo.Insert(entity)
            Next

            ' ③ 履歴書き込み
            _historyRepo.InsertHistory("LecturePlan", csvPath, entities.Count)

        Catch ex As LectpayAppException
            Throw

        Catch ex As Exception
            result.ImportError = ex.Message
            Throw New ImportServiceException("データインポート処理でエラーが発生しました。", ex)
        End Try

        Return result

    End Function

    ' ★ バリデーション本体
    Private Function Validate(entity As LecturePlanEntity) As ValidationResult

        Dim vr As New ValidationResult()

        If entity.LectureDate = DateTime.MinValue Then
            vr.AddError("LectureDate が不正")
        End If

        If String.IsNullOrEmpty(entity.TeacherCode) Then
            vr.AddError("TeacherCode が空")
        End If

        If String.IsNullOrEmpty(entity.SubjectCode) Then
            vr.AddError("SubjectCode が空")
        End If

        If entity.Status <> 0 AndAlso entity.Status <> 1 Then
            vr.AddError("Status が不正（0 or 1）")
        End If

        If entity.ActualHours < 0 Then
            vr.AddError("ActualHours が負の値")
        End If

        Return vr

    End Function

End Class