Imports System.Data.SqlClient
Imports WindowsApp1.Entities
Imports WindowsApp1.Framework

Namespace Mappers
    Public NotInheritable Class Master1Mapper

        Private Sub New()
        End Sub

        Public Shared Function Map(r As SqlDataReader) As Master1Entity
            Dim e As New Master1Entity()
            e.School = SafeGet.Str(r, "School")
            e.Koma = SafeGet.Int(r, "Koma")
            e.WeekPattern = SafeGet.Int(r, "WeekPattern")
            e.StartTime = SafeGet.Time(r, "StartTime")
            e.EndTime = SafeGet.Time(r, "EndTime")
            Return e
        End Function

    End Class
End Namespace