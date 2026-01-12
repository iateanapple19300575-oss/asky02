'===========================================================
' LectureActualService
'===========================================================
Public Class LectureActualService
    Inherits BaseService

    Protected Overrides Sub ExecuteBusinessLogic(exec As SqlExecutor, op As ServiceOperation)

        Dim actualRepo As New LectureActualRepository(exec)
        Dim planRepo As New LecturePlanRepository(exec)
        Dim domain As New LectureActualDomainService(actualRepo, planRepo)
        Dim cross As New LecturePlanActualDomainService(planRepo, actualRepo)

        Select Case op

            Case ServiceOperation.Insert
                Dim entity As LectureActualEntity =
                    DirectCast(Me.CurrentEntity, LectureActualEntity)

                domain.ValidateBeforeInsert(entity)
                cross.ValidateActualInsert(entity)

                actualRepo.Insert(entity)
                domain.ApplyActualToPlan(entity)

            Case ServiceOperation.Update
                Dim before As LectureActualEntity =
                    DirectCast(Me.CurrentEntityBefore, LectureActualEntity)
                Dim after As LectureActualEntity =
                    DirectCast(Me.CurrentEntityAfter, LectureActualEntity)

                domain.ValidateBeforeUpdate(before, after)
                actualRepo.Update(before, after)

            Case ServiceOperation.Delete
                Dim entity As LectureActualEntity =
                    DirectCast(Me.CurrentEntity, LectureActualEntity)

                cross.ValidateActualDelete(entity)
                actualRepo.Delete(entity)
                domain.RevertPlanStatus(entity)

            Case Else
                Throw New InvalidOperationException("未定義の ServiceOperation です。")

        End Select

    End Sub

End Class

