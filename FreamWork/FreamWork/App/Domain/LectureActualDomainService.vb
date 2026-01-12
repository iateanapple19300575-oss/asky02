''' <summary>
''' 実績側のドメインサービス。
''' </summary>
Public Class LectureActualDomainService

    Private ReadOnly _actualRepo As LectureActualRepository
    Private ReadOnly _planRepo As LecturePlanRepository

    Public Sub New(actualRepo As LectureActualRepository, planRepo As LecturePlanRepository)
        _actualRepo = actualRepo
        _planRepo = planRepo
    End Sub

    Public Sub Validate(entity As LectureActualEntity)
        If entity.LectureHours <= 0D Then
            Throw New ApplicationException("講義時間が 0 以下のため登録できません。")
        End If

        If entity.LectureDate = DateTime.MinValue Then
            Throw New ApplicationException("講義日が設定されていません。")
        End If
    End Sub

    Public Sub ValidateBeforeInsert(entity As LectureActualEntity)
        Validate(entity)
    End Sub

    Public Sub ValidateBeforeUpdate(before As LectureActualEntity, after As LectureActualEntity)
        Validate(after)
    End Sub

    Public Sub ApplyActualToPlan(actual As LectureActualEntity)
        Dim plan As LecturePlanEntity =
            _planRepo.FindByDateAndTeacher(actual.LectureDate, actual.TeacherCode)

        If plan Is Nothing Then
            Throw New ApplicationException("対応する講義予定が存在しません。")
        End If

        plan.ActualHours = actual.LectureHours
        plan.Status = 1

        _planRepo.Update(plan, plan)
    End Sub

    Public Sub RevertPlanStatus(actual As LectureActualEntity)
        Dim plan As LecturePlanEntity =
            _planRepo.FindByDateAndTeacher(actual.LectureDate, actual.TeacherCode)

        If plan Is Nothing Then
            Throw New ApplicationException("対応する講義予定が存在しません。")
        End If

        plan.Status = 0
        plan.ActualHours = 0D

        _planRepo.Update(plan, plan)
    End Sub

End Class

