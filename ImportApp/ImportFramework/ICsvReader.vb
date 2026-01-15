''' <summary>
''' CSV を読み込み、RawImportRow のリストに変換する Strategy。
''' </summary>
Public Interface ICsvReader(Of TRawRow)

    ''' <summary>
    ''' CSV ファイルを読み込み、DTO のリストに変換する。
    ''' </summary>
    Function Read(filePath As String) As IList(Of TRawRow)

End Interface