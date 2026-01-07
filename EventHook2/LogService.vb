Public NotInheritable Class LogService

    Private Sub New()
    End Sub

    Public Shared Sub Write(screen As String, eventName As String, message As String)
        System.Diagnostics.Debug.WriteLine(
            $"{DateTime.Now:HH:mm:ss} [{screen}] {eventName} - {message}"
        )
    End Sub

End Class