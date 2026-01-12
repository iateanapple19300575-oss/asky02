''' <summary>
''' 予定＋実績の整合性チェックを行う複合ドメインサービス。
''' </summary>
Public Class LecturePlanActualDomainService

    Private ReadOnly _planRepo As LecturePlanRepository
    Private ReadOnly _actualRepo As LectureActualRepository

    Public Sub New(planRepo As LecturePlanRepository, actualRepo As LectureActualRepository)
        _planRepo = planRepo
        _actualRepo = actualRepo
    End Sub

    Public Sub ValidateActualInsert(actual As LectureActualEntity)
        Dim plan As LecturePlanEntity =
            _planRepo.FindByDateAndTeacher(actual.LectureDate, actual.TeacherCode)

        If plan Is Nothing Then
            Throw New ApplicationException("対応する講義予定が存在しません。")
        End If

        If plan.SubjectCode <> actual.SubjectCode Then
            Throw New ApplicationException("予定と実績の科目が一致しません。")
        End If

        If plan.Status = 1 Then
            Throw New ApplicationException("この講義予定はすでに完了しています。")
        End If

        If actual.LectureHours > plan.ActualHours AndAlso plan.ActualHours > 0D Then
            Throw New ApplicationException("実績時間が予定時間を超えています。")
        End If
    End Sub

    Public Sub ValidateActualDelete(actual As LectureActualEntity)
        Dim plan As LecturePlanEntity =
            _planRepo.FindByDateAndTeacher(actual.LectureDate, actual.TeacherCode)

        If plan Is Nothing Then
            Throw New ApplicationException("対応する講義予定が存在しません。")
        End If

        If plan.Status = 0 Then
            Throw New ApplicationException("予定はすでに未実施状態です。")
        End If
    End Sub

    Public Sub ValidatePlanUpdate(before As LecturePlanEntity, after As LecturePlanEntity)
        Dim actualList As List(Of LectureActualEntity) =
            _actualRepo.FindByDate(before.LectureDate)

        For Each actual In actualList
            If before.TeacherCode <> after.TeacherCode Then
                Throw New ApplicationException("実績が存在するため、講師コードを変更できません。")
            End If
            If before.SubjectCode <> after.SubjectCode Then
                Throw New ApplicationException("実績が存在するため、科目コードを変更できません。")
            End If
            If before.LectureDate <> after.LectureDate Then
                Throw New ApplicationException("実績が存在するため、講義日を変更できません。")
            End If
        Next
    End Sub

End Class

