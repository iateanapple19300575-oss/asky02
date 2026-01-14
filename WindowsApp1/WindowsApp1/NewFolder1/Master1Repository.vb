Imports WindowsApp1.Entities
Imports WindowsApp1.Framework
Imports WindowsApp1.Mappers

Public Class Master1Repository

    Private Shared ReadOnly Def As New TableDefinition(
        "Master1",
        New List(Of String) From {"School", "Koma", "WeekPattern", "StartTime", "EndTime"},
        New List(Of String) From {"School", "Koma", "WeekPattern"}
    )

    Public Function GetAll() As List(Of Master1Entity)
        Dim sql As String = QueryBuilder.BuildSelect(Def)
        Return SqlExecutor.Query(sql, AddressOf Master1Mapper.Map)
    End Function

    Public Sub Insert(e As Master1Entity)
        Dim sql As String = QueryBuilder.BuildInsert(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

    Public Sub Update(e As Master1Entity)
        Dim sql As String = QueryBuilder.BuildUpdate(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

    Public Sub Delete(e As Master1Entity)
        Dim sql As String = QueryBuilder.BuildDelete(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

End Class