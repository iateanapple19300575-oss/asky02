Imports System.Data.SqlClient

Imports System.IO

''' <summary>
''' CSV インポート処理を統括するアプリケーションサービス。
''' </summary>
Public Class ImportApplicationService

    Private ReadOnly _connectionString As String
    Private ReadOnly _factory As CsvImportFactory

    Private ReadOnly _rawRepo As IImportRawRepository
    Private ReadOnly _processedRepo As IImportProcessedRepository
    Private ReadOnly _masterRepo As IImportMasterRepository
    Private ReadOnly _maintenanceRepo As IImportMaintenanceRepository

    Public Sub New(connectionString As String,
                   rawRepo As IImportRawRepository,
                   processedRepo As IImportProcessedRepository,
                   masterRepo As IImportMasterRepository,
                   maintenanceRepo As IImportMaintenanceRepository)

        _connectionString = connectionString
        _factory = New CsvImportFactory()

        _rawRepo = rawRepo
        _processedRepo = processedRepo
        _masterRepo = masterRepo
        _maintenanceRepo = maintenanceRepo
    End Sub


    ''' <summary>
    ''' CSV インポートのメイン処理。
    ''' </summary>
    Public Function ImportCsv(filePath As String, csvType As String) As ImportResult

        ' 1. Reader / Validator / Processor のセットを取得
        Dim comp = _factory.Create(csvType)

        ' 2. CSV 読み込み（Domain）
        Dim rows = comp.Reader.Read(filePath)

        ' 3. バリデーション（Domain）
        Dim errors = comp.Validator.Validate(rows)
        If errors.HasError Then
            Return New ImportResult(False, errors)
        End If

        ' 4. マスタ取得（Repository）
        Dim masterA = _masterRepo.GetMasterA()
        Dim masterB = _masterRepo.GetMasterB()

        ' 5. 加工（Domain）
        Dim processed = comp.Processor.Process(rows, masterA, masterB)

        ' 6. DB への登録（Repository）
        Using conn As New SqlConnection(_connectionString)
            conn.Open()
            Dim tran = conn.BeginTransaction()

            Try
                ' 6-1. 過去データ削除（1か月分）
                Dim targetDate As DateTime = DateTime.Now.AddMonths(-1)
                _maintenanceRepo.DeleteOldData(targetDate, tran)

                ' 6-2. 生データ INSERT
                _rawRepo.InsertRawData(rows, tran)

                ' 6-3. 加工後データ INSERT
                _processedRepo.InsertProcessedData(processed, tran)

                ' 6-4. 履歴 INSERT
                Dim history As New ImportHistory()
                history.FileName = Path.GetFileName(filePath)
                history.RowCount = rows.Count
                history.Success = True
                history.ExecDate = DateTime.Now

                _maintenanceRepo.InsertHistory(history, tran)

                tran.Commit()

                Return New ImportResult(True)

            Catch ex As Exception
                tran.Rollback()

                ' 履歴（失敗）を残す
                Dim history As New ImportHistory()
                history.FileName = Path.GetFileName(filePath)
                history.RowCount = rows.Count
                history.Success = False
                history.ExecDate = DateTime.Now
                history.ErrorMessage = ex.Message

                ' 失敗履歴はトランザクション外で書く
                Using conn2 As New SqlConnection(_connectionString)
                    conn2.Open()
                    Dim tran2 = conn2.BeginTransaction()
                    Try
                        _maintenanceRepo.InsertHistory(history, tran2)
                        tran2.Commit()
                    Catch
                        tran2.Rollback()
                    End Try
                End Using

                Throw
            End Try
        End Using

    End Function

End Class