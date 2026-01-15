''' <summary>
''' バリデーションエラーの一覧を管理するモデル。
''' </summary>
Public Class ErrorList

    Private ReadOnly _errors As New List(Of ErrorDetail)

    ''' <summary>エラー件数</summary>
    Public ReadOnly Property Count As Integer
        Get
            Return _errors.Count
        End Get
    End Property

    ''' <summary>エラーが存在するかどうか</summary>
    Public ReadOnly Property HasError As Boolean
        Get
            Return _errors.Count > 0
        End Get
    End Property

    ''' <summary>エラーを追加する</summary>
    Public Sub Add(lineNumber As Integer, fieldName As String, message As String)
        _errors.Add(New ErrorDetail(lineNumber, fieldName, message))
    End Sub

    ''' <summary>エラー一覧を取得する</summary>
    Public Function ToList() As IList(Of ErrorDetail)
        Return _errors
    End Function

End Class