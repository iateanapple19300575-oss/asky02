''' <summary>
''' 予定側のドメインサービス。
''' </summary>
Public Class LecturePlanDomainService

    Private ReadOnly _repo As LecturePlanRepository

    Public Sub New(repo As LecturePlanRepository)
        _repo = repo
    End Sub

    Public Sub Validate(entity As LecturePlanEntity)
        If entity.LectureDate = DateTime.MinValue Then
            Throw New ApplicationException("講義日が設定されていません。")
        End If
        If String.IsNullOrEmpty(entity.TeacherCode) Then
            Throw New ApplicationException("講師コードが設定されていません。")
        End If
        If String.IsNullOrEmpty(entity.SubjectCode) Then
            Throw New ApplicationException("科目コードが設定されていません。")
        End If
    End Sub

    Public Sub ValidateBeforeInsert(entity As LecturePlanEntity)
        Validate(entity)
    End Sub

    Public Sub ValidateBeforeUpdate(before As LecturePlanEntity, after As LecturePlanEntity)
        Validate(after)
    End Sub

    Public Function SearchPlans([date] As Nullable(Of DateTime),
                                teacher As String,
                                subject As String) As List(Of LecturePlanEntity)

        Dim result As New List(Of LecturePlanEntity)()

        If Not [date].HasValue AndAlso String.IsNullOrEmpty(teacher) AndAlso String.IsNullOrEmpty(subject) Then
            Return _repo.FindAll()
        End If

        If [date].HasValue AndAlso Not String.IsNullOrEmpty(teacher) AndAlso Not String.IsNullOrEmpty(subject) Then
            Return _repo.FindByDateTeacherSubject([date].Value, teacher, subject)
        End If

        If [date].HasValue AndAlso Not String.IsNullOrEmpty(teacher) Then
            Dim list = _repo.FindByDate([date].Value)
            For Each p In list
                If p.TeacherCode = teacher Then
                    result.Add(p)
                End If
            Next
            Return result
        End If

        If [date].HasValue AndAlso Not String.IsNullOrEmpty(subject) Then
            Dim list = _repo.FindByDate([date].Value)
            For Each p In list
                If p.SubjectCode = subject Then
                    result.Add(p)
                End If
            Next
            Return result
        End If

        If Not String.IsNullOrEmpty(teacher) AndAlso Not String.IsNullOrEmpty(subject) Then
            Dim list = _repo.FindByTeacher(teacher)
            For Each p In list
                If p.SubjectCode = subject Then
                    result.Add(p)
                End If
            Next
            Return result
        End If

        If [date].HasValue Then Return _repo.FindByDate([date].Value)
        If Not String.IsNullOrEmpty(teacher) Then
            Return _repo.FindByTeacher(teacher)
        End If
        If Not String.IsNullOrEmpty(subject) Then
            Return _repo.FindBySubject(subject)
        End If

        Return result
    End Function

    Public Function ResetStatusByDate([date] As DateTime) As Integer
        Dim list As List(Of LecturePlanEntity) = _repo.FindByDate([date])
        Dim count As Integer = 0
        For Each plan As LecturePlanEntity In list
            plan.Status = 0
            plan.ActualHours = 0D
            _repo.Update(plan, plan)
            count += 1
        Next
        Return count
    End Function

    Public Function SearchPlans(cond As SearchCondition) As List(Of LecturePlanEntity)
        Return _repo.SearchDynamic(cond)
    End Function

End Class

