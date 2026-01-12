Public Class ValidationResult
    Public Property IsValid As Boolean = True
    Public Property Errors As New List(Of String)

    Public Sub AddError(msg As String)
        IsValid = False
        Errors.Add(msg)
    End Sub
End Class