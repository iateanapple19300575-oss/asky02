Imports System.IO
Imports System.Text

''' <summary>
''' アプリケーション全体で使用するログ出力サービス。
''' </summary>
Public Class LogService

    Private Shared ReadOnly BaseDir As String = "C:\Logs"
    Private Shared ReadOnly Enc As Encoding = Encoding.GetEncoding("Shift_JIS")

    Private Shared Sub EnsureDir()
        If Not Directory.Exists(BaseDir) Then
            Directory.CreateDirectory(BaseDir)
        End If
    End Sub

    Public Shared Sub WriteOperation(ByVal operation As String, ByVal message As String, Optional ByVal context As String = "")
        EnsureDir()
        Dim filePath = Path.Combine(BaseDir, "Operation_" & DateTime.Now.ToString("yyyyMMdd") & ".log")

        Dim msg As String = String.Format(
            "[{0}] OPERATION  Operation={1}, Message={2}, PC={3}, User={4}",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            operation,
            message,
            Environment.MachineName,
            Environment.UserName
        )

        File.AppendAllText(filePath, msg, Enc)
    End Sub

    Public Shared Sub WriteException(ByVal ex As Exception, ByVal message As String, Optional ByVal context As String = "")
        EnsureDir()
        Dim filePath = Path.Combine(BaseDir, "Error_" & DateTime.Now.ToString("yyyyMMdd") & ".log")

        Dim sb As New StringBuilder()
        sb.AppendLine("[" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "]" & vbTab & "EXCEPTION")
        If context <> "" Then sb.AppendLine("Context: " & context)
        sb.AppendLine("Message: " & ex.Message)
        sb.AppendLine("Type: " & ex.GetType().FullName)
        sb.AppendLine("StackTrace:")
        sb.AppendLine(ex.StackTrace)

        If ex.InnerException IsNot Nothing Then
            sb.AppendLine("---- InnerException ----")
            sb.AppendLine(ex.InnerException.Message)
            sb.AppendLine(ex.InnerException.StackTrace)
        End If
        If context <> "" Then
            sb.AppendLine("Context: " & context)
        End If
        sb.AppendLine("")

        File.AppendAllText(filePath, sb.ToString(), Enc)
    End Sub

    Public Shared Sub WriteSql(ByVal sql As String, Optional ByVal context As String = "")
        EnsureDir()
        Dim filePath = Path.Combine(BaseDir, "Sql_" & DateTime.Now.ToString("yyyyMMdd") & ".log")

        Dim sb As New StringBuilder()
        sb.Append("[" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "]" & vbTab)
        sb.AppendLine("[SQL]:" & vbTab & sql)
        If context <> "" Then
            sb.AppendLine("Context: " & context)
        End If

        File.AppendAllText(filePath, sb.ToString(), Enc)
    End Sub

    Public Shared Sub WriteInfo(ByVal message As String, Optional ByVal context As String = "")
        EnsureDir()
        Dim filePath = Path.Combine(BaseDir, "Sql_" & DateTime.Now.ToString("yyyyMMdd") & ".log")

        Dim msg As String = String.Format(
            "[{0}] INFO       {1}",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            message
        )

        File.AppendAllText(filePath, message, Enc)
    End Sub

End Class