Public Class FormMasterMaintenance

    Private _repo1 As New Master1Repository()
    Private _repo2 As New Master2Repository()
    Private _repo3 As New Master3Repository()

    Private _mode As EditMode = EditMode.None

    Private Enum EditMode
        None
        Add
        Edit
    End Enum

End Class