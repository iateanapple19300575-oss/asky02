'===========================================================
' SqlExecutor（最小実装）
'===========================================================
Imports System.Data.SqlClient

Public Class SqlExecutor
    Implements IDisposable

    Private ReadOnly _conn As SqlConnection
    Private _tran As SqlTransaction

    Public Sub New(connectionString As String)
        _conn = New SqlConnection(connectionString)
        _conn.Open()
    End Sub

    Public Function BeginTransaction() As SqlTransaction
        _tran = _conn.BeginTransaction()
        Return _tran
    End Function

    Public Sub Commit()
        If _tran IsNot Nothing Then
            _tran.Commit()
            _tran.Dispose()
            _tran = Nothing
        End If
    End Sub

    Public Sub Rollback()
        If _tran IsNot Nothing Then
            _tran.Rollback()
            _tran.Dispose()
            _tran = Nothing
        End If
    End Sub

    Public Function ExecuteReader(sql As String, parameters As List(Of SqlParameter)) As SqlDataReader
        Dim cmd As New SqlCommand(sql, _conn)
        cmd.Transaction = _tran
        If parameters IsNot Nothing Then
            cmd.Parameters.AddRange(parameters.ToArray())
        End If
        Return cmd.ExecuteReader()
    End Function

    Public Function ExecuteNonQuery(sql As String, parameters As List(Of SqlParameter)) As Integer
        Dim cmd As New SqlCommand(sql, _conn)
        cmd.Transaction = _tran
        If parameters IsNot Nothing Then
            cmd.Parameters.AddRange(parameters.ToArray())
        End If
        Return cmd.ExecuteNonQuery()
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        If _tran IsNot Nothing Then
            _tran.Dispose()
            _tran = Nothing
        End If
        If _conn IsNot Nothing Then
            _conn.Close()
            _conn.Dispose()
        End If
    End Sub

End Class


