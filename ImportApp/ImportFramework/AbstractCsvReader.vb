Imports System.IO

Imports System.Text

''' <summary>
''' CSV Reader の共通処理を提供する抽象クラス。
''' </summary>
Public MustInherit Class AbstractCsvReader(Of TRawRow)
    Implements ICsvReader(Of TRawRow)

    Public Function Read(filePath As String) As IList(Of TRawRow) _
        Implements ICsvReader(Of TRawRow).Read

        Dim list As New List(Of TRawRow)
        Dim lineNumber As Integer = 0

        Using sr As New StreamReader(filePath, Encoding.UTF8)
            While Not sr.EndOfStream
                Dim line = sr.ReadLine()
                lineNumber += 1

                ' 空行スキップ
                If String.IsNullOrEmpty(line) Then Continue While

                Dim cols = line.Split(","c)

                Dim row = ParseRow(cols, lineNumber)
                list.Add(row)
            End While
        End Using

        Return list
    End Function

    ''' <summary>
    ''' CSV の 1 行を DTO に変換する（サブクラスが実装）
    ''' </summary>
    Protected MustOverride Function ParseRow(cols As String(), lineNumber As Integer) As TRawRow

End Class