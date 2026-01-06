Imports System.Reflection

''' <summary>
''' WinForms のイベント内部構造にアクセスするための Reflection キャッシュクラス。
''' EventInfo からイベントキー（EventHandlerList のキー）を取得したり、
''' EventHandlerList の Item プロパティ情報をキャッシュして再利用する。
''' </summary>
''' <remarks>
''' WinForms のイベントは内部的に EventHandlerList によって管理されており、
''' 各イベントは "EventXXX" という非公開の静的フィールドをキーとして保持している。
''' このクラスはそのキーを Reflection で取得し、パフォーマンス向上のためキャッシュする。
''' </remarks>
Public NotInheritable Class EventReflectionCache

    ''' <summary>
    ''' インスタンス生成を禁止するためのプライベートコンストラクタ。
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' EventInfo と、そのイベントに対応する内部キー（EventHandlerList のキー）をキャッシュする辞書。
    ''' </summary>
    Private Shared ReadOnly _eventKeyCache As New Dictionary(Of EventInfo, Object)()

    ''' <summary>
    ''' EventHandlerList のインデクサ（Item プロパティ）をキャッシュするための PropertyInfo。
    ''' </summary>
    Private Shared _eventListItemProperty As PropertyInfo = Nothing

    ''' <summary>
    ''' 指定された EventInfo に対応する WinForms 内部イベントキーを取得する。
    ''' </summary>
    ''' <param name="ev">対象の EventInfo。</param>
    ''' <returns>
    ''' イベントキー（通常は Object 型の静的フィールド値）。
    ''' 対応するキーが存在しない場合は Nothing を返す。
    ''' </returns>
    ''' <remarks>
    ''' WinForms のイベントは "Event" &lt;イベント名&gt; の形式で非公開静的フィールドとして定義されている。
    ''' このメソッドはそのフィールドを Reflection で取得し、キャッシュして再利用する。
    ''' </remarks>
    Public Shared Function GetEventKey(ev As EventInfo) As Object

        If _eventKeyCache.ContainsKey(ev) Then
            Return _eventKeyCache(ev)
        End If

        Dim keyField As FieldInfo =
            ev.DeclaringType.GetField("Event" & ev.Name,
                                      BindingFlags.NonPublic Or BindingFlags.Static)

        If keyField Is Nothing Then
            _eventKeyCache(ev) = Nothing
            Return Nothing
        End If

        Dim key As Object = keyField.GetValue(Nothing)
        _eventKeyCache(ev) = key
        Return key
    End Function

    ''' <summary>
    ''' EventHandlerList の内部インデクサ（Item プロパティ）を取得する。
    ''' </summary>
    ''' <param name="eventList">EventHandlerList インスタンス。</param>
    ''' <returns>
    ''' 非公開の Item プロパティを表す PropertyInfo。
    ''' </returns>
    ''' <remarks>
    ''' EventHandlerList は内部的に Item(key) によりイベントハンドラを管理している。
    ''' このプロパティは Reflection で一度取得した後キャッシュされる。
    ''' </remarks>
    Public Shared Function GetEventListItemProperty(eventList As Object) As PropertyInfo

        If _eventListItemProperty IsNot Nothing Then
            Return _eventListItemProperty
        End If

        _eventListItemProperty =
            eventList.GetType().GetProperty("Item",
                BindingFlags.NonPublic Or BindingFlags.Instance)

        Return _eventListItemProperty
    End Function

End Class