Imports System.IO
Imports System.Reflection

Public Class CsvEntityMapper

    ' CSV → List(Of T) に変換する汎用メソッド
    Public Shared Function MapCsvToEntities(Of T As New)(csvPath As String) As List(Of T)

        Dim result As New List(Of T)()

        Dim lines() As String = File.ReadAllLines(csvPath)
        If lines.Length = 0 Then Return result

        ' 1行目：ヘッダ
        Dim headers() As String = lines(0).Split(","c)

        ' T のプロパティ一覧
        Dim t_Type As Type = GetType(T)
        Dim props As PropertyInfo() = t_Type.GetProperties()

        ' ヘッダ列番号 → プロパティ のマッピング辞書
        Dim map As New Dictionary(Of Integer, PropertyInfo)()

        For i As Integer = 0 To headers.Length - 1
            Dim header As String = headers(i).Trim()

            For Each p As PropertyInfo In props
                If String.Equals(p.Name, header, StringComparison.OrdinalIgnoreCase) Then
                    map(i) = p
                    Exit For
                End If
            Next
        Next

        ' データ行を処理
        For lineIndex As Integer = 1 To lines.Length - 1

            Dim line As String = lines(lineIndex)
            If String.IsNullOrEmpty(line) Then
                Continue For
            End If

            Dim cols() As String = line.Split(","c)
            Dim entity As New T()

            For i As Integer = 0 To cols.Length - 1

                If Not map.ContainsKey(i) Then
                    Continue For
                End If

                Dim prop As PropertyInfo = map(i)
                Dim rawValue As String = cols(i).Trim()

                If rawValue = "" Then
                    Continue For
                End If

                Dim converted As Object = ConvertValue(rawValue, prop.PropertyType)
                prop.SetValue(entity, converted, Nothing)

            Next

            result.Add(entity)
        Next

        Return result

    End Function

    ' 文字列 → プロパティ型への変換
    Private Shared Function ConvertValue(value As String, targetType As Type) As Object

        If targetType Is GetType(String) Then
            Return value

        ElseIf targetType Is GetType(Integer) OrElse targetType Is GetType(Integer?) Then
            Return Integer.Parse(value)

        ElseIf targetType Is GetType(Decimal) OrElse targetType Is GetType(Decimal?) Then
            Return Decimal.Parse(value)

        ElseIf targetType Is GetType(DateTime) OrElse targetType Is GetType(DateTime?) Then
            Return DateTime.Parse(value)

        ElseIf targetType Is GetType(Boolean) OrElse targetType Is GetType(Boolean?) Then
            Return Boolean.Parse(value)

        End If

        ' 未対応型はそのまま返す
        Return value

    End Function

End Class