Public NotInheritable Class ErrorFacade

    Private Sub New()
    End Sub

    Public Shared Sub Report(ex As Exception, context As ErrorContext)
        System.Diagnostics.Debug.WriteLine(
            $"[Error] {context}: {ex.Message}"
        )
    End Sub

End Class

Public Enum ErrorContext
    Manual
    System
End Enum