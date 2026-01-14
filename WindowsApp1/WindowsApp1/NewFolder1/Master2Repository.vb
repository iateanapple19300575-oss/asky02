Imports WindowsApp1.Entities
Imports WindowsApp1.Framework
Imports WindowsApp1.Mappers

Public Class Master2Repository

    Private Shared ReadOnly Def As New TableDefinition(
        "Master2",
        New List(Of String) From {"School", "Grade", "Class", "Koma", "StartTime", "EndTime"},
        New List(Of String) From {"School", "Grade", "Class", "Koma"}
    )

    Public Function GetAll() As List(Of Master2Entity)
        Dim sql As String = QueryBuilder.BuildSelect(Def)
        Return SqlExecutor.Query(sql, AddressOf Master2Mapper.Map)
    End Function

    Public Sub Insert(e As Master2Entity)
        Dim sql As String = QueryBuilder.BuildInsert(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

    Public Sub Update(e As Master2Entity)
        Dim sql As String = QueryBuilder.BuildUpdate(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

    Public Sub Delete(e As Master2Entity)
        Dim sql As String = QueryBuilder.BuildDelete(Def)
        SqlExecutor.Execute(sql, e)
    End Sub

End Class