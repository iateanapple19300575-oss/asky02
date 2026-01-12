Imports System.Data.SqlClient

Public Class WhereBuilder

    Private _conditions As New List(Of String)()
    Private _parameters As New List(Of SqlParameter)()

    Public Sub Add(condition As String, paramName As String, value As Object)
        If value Is Nothing OrElse (TypeOf value Is String AndAlso value.ToString() = "") Then
            Return
        End If

        _conditions.Add(condition)
        _parameters.Add(New SqlParameter(paramName, value))
    End Sub

    Public Function ToSql() As String
        If _conditions.Count = 0 Then
            Return ""
        End If
        Return " WHERE " & String.Join(" AND ", _conditions.ToArray())
    End Function

    Public Function GetParameters() As List(Of SqlParameter)
        Return _parameters
    End Function

End Class