Imports WindowsApp1.Entities
Imports WindowsApp1.Framework
Imports WindowsApp1.Mappers

Public Class Master3Repository

    Private Shared ReadOnly Def As New TableDefinition(
        "Master3",
        New List(Of String) From {
            "School", "Grade", "Class", "Koma",
            "PeriodType", "PeriodValue",
            "StartTime", "EndTime"
        },
        New List(Of String) From {
            "School", "Grade", "Class", "Koma", "PeriodType", "PeriodValue"
        }
    )

    Public Function GetAll() As List(Of Master3Entity)
        Dim sql As String = QueryBuilder.BuildSelect(Def)
        Return SqlExecutor.Query(sql, AddressOf Master3Mapper.Map)
    End Function

    Public Sub Insert(e As Master3Entity)
        Dim sql As String = QueryBuilder.BuildInsert(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

    Public Sub Update(e As Master3Entity)
        Dim sql As String = QueryBuilder.BuildUpdate(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

    Public Sub Delete(e As Master3Entity)
        Dim sql As String = QueryBuilder.BuildDelete(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

End Class