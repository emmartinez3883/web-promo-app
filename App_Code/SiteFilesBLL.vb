Imports Microsoft.VisualBasic
Imports SiteFilesDSTableAdapters

Public Class SiteFilesBLL

    Private SiteFilesAdapter As SiteFilesTableAdapter = Nothing
    Protected ReadOnly Property Adapter() As SiteFilesTableAdapter
        Get
            If SiteFilesAdapter Is Nothing Then
                SiteFilesAdapter = New SiteFilesTableAdapter()
            End If
            Return SiteFilesAdapter
        End Get
    End Property

    'Get all site files function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetFiles() As SiteFilesDS.SiteFilesDataTable
        Return Adapter.GetData()
    End Function

    'Update file function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, True)>
    Public Function UpdateSiteFile(ByVal FileID As Integer, ByVal ContentID As Integer, ByVal FileData As Byte(), ByVal FileType As Integer, ByVal CreatedOn As DateTime, ByVal CreatedBy As String) As Boolean
        Dim SiteFiles As SiteFilesDS.SiteFilesDataTable = Adapter.GetDataByFileID(FileID)
        If SiteFiles.Count = 0 Then
            'if no matching record, return false
            Return False
        End If
        Dim SiteFilesRow As SiteFilesDS.SiteFilesRow = SiteFiles(0)
        SiteFilesRow.CreatedOn = CreatedOn
        SiteFilesRow.CreatedBy = CreatedBy
        SiteFilesRow.FileData = FileData
        SiteFilesRow.FileType = FileType
        SiteFilesRow.ContentID = ContentID
        'update site file record
        Dim rowsAffected As Integer = Adapter.Update(SiteFilesRow)
        Return rowsAffected = 1
    End Function

    'Get images function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetImages() As SiteFilesDS.SiteFilesDataTable
        Return Adapter.GetImages()
    End Function

    'Add file function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, True)>
    Public Function AddSiteFile(ByVal ContentID As Integer, ByVal FileData As Byte(), ByVal FileType As Integer, ByVal CreatedOn As DateTime, ByVal CreatedBy As String) As Boolean
        Dim SiteFiles As New SiteFilesDS.SiteFilesDataTable()
        Dim SiteFilesRow As SiteFilesDS.SiteFilesRow = SiteFiles.NewSiteFilesRow()
        SiteFilesRow.CreatedOn = CreatedOn
        SiteFilesRow.CreatedBy = CreatedBy
        SiteFilesRow.FileData = FileData
        SiteFilesRow.FileType = FileType
        SiteFilesRow.ContentID = ContentID
        'insert site file record
        SiteFiles.AddSiteFilesRow(SiteFilesRow)
        Dim rowsAffected As Integer = Adapter.Update(SiteFilesRow)
        Return rowsAffected = 1
    End Function

    'Get files by FileID
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, False)>
    Public Function GetDatabyFileID(ByVal FileID As Integer) As SiteFilesDS.SiteFilesDataTable
        Return Adapter.GetDataByFileID(FileID)
    End Function

    'Get files by ContentID and FileType
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, False)>
    Public Function GetDatabyContentID_FileType(ByVal ContentID As Integer, ByVal FileType As Integer) As SiteFilesDS.SiteFilesDataTable
        Return Adapter.GetDataByContentID_FileType(ContentID, FileType)
    End Function

    'Delete single file function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, True)>
    Public Function DeleteSiteFile(ByVal ContentID As Integer, ByVal FileType As Integer) As Boolean
        Dim rowsAffected As Integer = Adapter.DeleteSiteFile(ContentID, FileType)
        Return rowsAffected = 1
    End Function

    'Delete all files function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, True)>
    Public Function DeleteFilesByContentID(ByVal ContentID As Integer) As Boolean
        Dim rowsAffected As Integer = Adapter.DeleteFilesByContentID(ContentID)
        Return rowsAffected > 0
    End Function
End Class
