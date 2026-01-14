Imports System.Data.SqlClient
Imports WindowsApp1.Entities
Imports WindowsApp1.Framework

Namespace Mappers
    Public NotInheritable Class Master2Mapper

        Private Sub New()
        End Sub

        Public Shared Function Map(r As SqlDataReader) As Master2Entity
            Dim e As New Master2Entity()
            e.School = SafeGet.Str(r, "School")
            e.Grade = SafeGet.Int(r, "Grade")
            e.Class = SafeGet.Int(r, "Class")
            e.Koma = SafeGet.Int(r, "Koma")
            e.StartTime = SafeGet.Time(r, "StartTime")
            e.EndTime = SafeGet.Time(r, "EndTime")
            Return e
        End Function

    End Class
End Namespace