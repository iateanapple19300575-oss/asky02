''' <summary>
''' サービス基底クラス（トランザクション＋Audit）。
''' </summary>
Public MustInherit Class BaseService

    Protected Exec As SqlExecutor

    Protected Property CurrentEntity As BaseEntity
    Protected Property CurrentEntityBefore As BaseEntity
    Protected Property CurrentEntityAfter As BaseEntity

    Protected Sub ExecuteInTransaction(op As ServiceOperation)
        Exec = New SqlExecutor(AppEnvironment.ConnectionString)

        Try
            Exec.BeginTransaction()

            ApplyAudit(op)

            ExecuteBusinessLogic(Exec, op)

            Exec.Commit()

        Catch ex As Exception
            Try
                Exec.Rollback()
            Catch
            End Try

            LogService.WriteException(ex, Me.GetType().Name & ".ExecuteInTransaction")
            Throw

        Finally
            Exec.Dispose()
        End Try
    End Sub

    Private Sub ApplyAudit(op As ServiceOperation)
        Dim now As DateTime = DateTime.Now
        Dim user As String = Environment.UserName

        Select Case op
            Case ServiceOperation.Insert
                If CurrentEntity IsNot Nothing Then
                    CurrentEntity.Create_Date = now
                    CurrentEntity.Create_User = user
                End If
            Case ServiceOperation.Update
                If CurrentEntityAfter IsNot Nothing Then
                    CurrentEntityAfter.Update_Date = now
                    CurrentEntityAfter.Update_User = user
                End If
        End Select
    End Sub

    Protected MustOverride Sub ExecuteBusinessLogic(exec As SqlExecutor, op As ServiceOperation)

    Public Sub RunInsert(entity As BaseEntity)
        Me.CurrentEntity = entity
        ExecuteInTransaction(ServiceOperation.Insert)
    End Sub

    Public Sub RunUpdate(before As BaseEntity, after As BaseEntity)
        Me.CurrentEntityBefore = before
        Me.CurrentEntityAfter = after
        ExecuteInTransaction(ServiceOperation.Update)
    End Sub

    Public Sub RunDelete(entity As BaseEntity)
        Me.CurrentEntity = entity
        ExecuteInTransaction(ServiceOperation.Delete)
    End Sub

End Class

