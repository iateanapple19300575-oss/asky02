Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.IO
Imports System.Text

'===========================================================
' 共通列挙体
'===========================================================
Public Enum ServiceOperation
    Insert
    Update
    Delete
    ComplexLogic
    BatchProcess
    Custom1
End Enum

'===========================================================
' 接続文字列（仮）
'===========================================================
Public Module AppEnvironment
    Public ReadOnly ConnectionString As String = "Data Source = DESKTOP-L98IE79;Initial Catalog = DeveloperDB;Integrated Security = SSPI"
End Module


