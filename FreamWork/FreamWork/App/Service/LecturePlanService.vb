'===========================================================
' LecturePlanService（検索 API 付き）
'===========================================================
Public Class LecturePlanService
    Inherits BaseService

    Public Function SearchPlans(cond As SearchCondition) As List(Of LecturePlanEntity)
        Using exec As New SqlExecutor(AppEnvironment.ConnectionString)
            Dim repo As New LecturePlanRepository(exec)
            Return repo.SearchDynamic(cond)
        End Using
    End Function

    'Public Function SearchPlans(cond As SearchCondition) As List(Of LecturePlanEntity)

    '    Dim repo As New LecturePlanRepository(New SqlExecutor(AppEnvironment.ConnectionString))
    '    Dim domain As New LecturePlanDomainService(repo)

    '    Return domain.SearchPlans([DateTime], teacher, subject)
    'End Function

    Protected Overrides Sub ExecuteBusinessLogic(exec As SqlExecutor, op As ServiceOperation)

        Dim repo As New LecturePlanRepository(exec)
        Dim domain As New LecturePlanDomainService(repo)
        Dim actualRepo As New LectureActualRepository(exec)
        Dim cross As New LecturePlanActualDomainService(repo, actualRepo)

        Select Case op

            Case ServiceOperation.Insert
                Dim entity As LecturePlanEntity =
                    DirectCast(Me.CurrentEntity, LecturePlanEntity)

                domain.ValidateBeforeInsert(entity)
                repo.Insert(entity)

            Case ServiceOperation.Update
                Dim before As LecturePlanEntity =
                    DirectCast(Me.CurrentEntityBefore, LecturePlanEntity)
                Dim after As LecturePlanEntity =
                    DirectCast(Me.CurrentEntityAfter, LecturePlanEntity)

                domain.ValidateBeforeUpdate(before, after)
                cross.ValidatePlanUpdate(before, after)
                repo.Update(before, after)

            Case ServiceOperation.Delete
                Dim entity As LecturePlanEntity =
                    DirectCast(Me.CurrentEntity, LecturePlanEntity)

                repo.Delete(entity)

            Case ServiceOperation.ComplexLogic
                domain.ResetStatusByDate(DateTime.Today)

            Case Else
                Throw New InvalidOperationException("未定義の ServiceOperation です。")

        End Select

    End Sub

End Class

