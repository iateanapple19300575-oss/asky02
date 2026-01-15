''' <summary>
''' バリデーションエラー 1 件を表す DTO。
''' </summary>
Public Class ErrorDetail

    ''' <summary>CSV の行番号</summary>
    Public Property LineNumber As Integer

    ''' <summary>エラーの項目名（ColA など）</summary>
    Public Property FieldName As String

    ''' <summary>エラーメッセージ</summary>
    Public Property Message As String

    Public Sub New(lineNumber As Integer, fieldName As String, message As String)
        Me.LineNumber = lineNumber
        Me.FieldName = fieldName
        Me.Message = message
    End Sub

End Class