''' <summary>
''' CSV 種類に応じて Reader / Validator / Processor を返す Factory。
''' </summary>
Public Class CsvImportFactory

    Public Function Create(csvType As String) As ICsvImportComponents

        Select Case csvType

            Case "A"
                Return New CsvImportComponents(
                    New CsvAReader(),
                    New CsvAValidator(),
                    New CsvAProcessor()
                )

            Case "B"
                Return New CsvImportComponents(
                    New CsvBReader(),
                    New CsvBValidator(),
                    New CsvBProcessor()
                )

            Case Else
                Throw New Exception("未対応の CSV 種類です。")
        End Select

    End Function

End Class