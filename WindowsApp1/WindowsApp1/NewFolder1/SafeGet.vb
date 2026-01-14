Imports System.Data.SqlClient

Namespace Framework
    Public NotInheritable Class SafeGet

        Private Sub New()
        End Sub

        Public Shared Function Str(r As SqlDataReader, col As String) As String
            Dim i As Integer = r.GetOrdinal(col)
            If r.IsDBNull(i) Then Return ""
            Return r.GetString(i)
        End Function

        Public Shared Function Int(r As SqlDataReader, col As String) As Integer
            Dim i As Integer = r.GetOrdinal(col)
            If r.IsDBNull(i) Then Return 0
            Return r.GetInt32(i)
        End Function

        Public Shared Function Dec(r As SqlDataReader, col As String) As Decimal
            Dim i As Integer = r.GetOrdinal(col)
            If r.IsDBNull(i) Then Return 0D
            Return r.GetDecimal(i)
        End Function

        Public Shared Function Dt(r As SqlDataReader, col As String) As DateTime
            Dim i As Integer = r.GetOrdinal(col)
            If r.IsDBNull(i) Then Return DateTime.MinValue
            Return r.GetDateTime(i)
        End Function

        Public Shared Function Time(r As SqlDataReader, col As String) As TimeSpan
            Dim i As Integer = r.GetOrdinal(col)
            If r.IsDBNull(i) Then Return TimeSpan.Zero
            Return CType(r.GetValue(i), TimeSpan)
        End Function

        Public Shared Function Bool(r As SqlDataReader, col As String) As Boolean
            Dim i As Integer = r.GetOrdinal(col)
            If r.IsDBNull(i) Then Return False
            Return r.GetBoolean(i)
        End Function

    End Class
End Namespace