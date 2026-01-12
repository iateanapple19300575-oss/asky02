'===========================================================
' ReaderMapper（反射で DataReader → Entity 変換）
'===========================================================
Imports System.Data.SqlClient
Imports System.Reflection

Public NotInheritable Class ReaderMapper

    Private Sub New()
    End Sub

    Public Shared Function Map(Of T As New)(reader As SqlDataReader) As T
        Dim entity As New T()
        Dim t_type As Type = GetType(T)

        For i As Integer = 0 To reader.FieldCount - 1
            Dim colName As String = reader.GetName(i)
            Dim prop As PropertyInfo = t_type.GetProperty(colName)

            If prop IsNot Nothing AndAlso Not reader.IsDBNull(i) Then
                Dim value As Object = reader.GetValue(i)
                prop.SetValue(entity, value, Nothing)
            End If
        Next

        Return entity
    End Function



End Class

