'===========================================================
' ベースエンティティ
'===========================================================
Public MustInherit Class BaseEntity
    Public Property ID As Integer
    Public Property RowVersion As Byte()
    Public Property Create_Date As DateTime
    Public Property Create_User As String
    Public Property Update_Date As DateTime
    Public Property Update_User As String
End Class

