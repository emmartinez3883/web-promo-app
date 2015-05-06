Imports Microsoft.VisualBasic
Imports DataSet1TableAdapters


Public Class ContentBLL

    Private ContentAdapter As ContentTableAdapter = Nothing
    Protected ReadOnly Property Adapter() As ContentTableAdapter
        Get
            If ContentAdapter Is Nothing Then
                ContentAdapter = New ContentTableAdapter()
            End If
            Return ContentAdapter
        End Get
    End Property
    'Get all content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetContent() As DataSet1.ContentDataTable
        Return Adapter.GetData()
    End Function

    'Get all current content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetAllCurrentContent() As DataSet1.ContentDataTable
        Return Adapter.GetAllCurrentContent()
    End Function

    'Get all expired content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetAllExpiredContent() As DataSet1.ContentDataTable
        Return Adapter.GetAllExpiredContent()
    End Function

    'Get all published content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetAllPublishedContent() As DataSet1.ContentDataTable
        Return Adapter.GetAllPublishedContent()
    End Function

    'Get all content by ContentID function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetContentByContentID(ByVal ContentID As Integer) As DataSet1.ContentDataTable
        Return Adapter.GetDataByContentID(ContentID)
    End Function

    'Get all content by ContentName function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetContentByContentName(ByVal ContentName As String) As DataSet1.ContentDataTable
        Return Adapter.GetDataByContentName(ContentName)
    End Function

    'Get all content by ContentType function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetContentByContentType(ByVal ContentType As String) As DataSet1.ContentDataTable
        Return Adapter.GetDataByContentType(ContentType)
    End Function

    'Update content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, True)>
    Public Function UpdateContent(ByVal ContentID As Integer, ByVal ContentName As String, ByVal ContentTitle As String, ByVal Content As String, _
                               ByVal ContentPublished As Boolean, ByVal ContentGoLiveDate As DateTime, ByVal ContentExpirationDate As String) As Boolean
        Dim ChgContent As DataSet1.ContentDataTable = Adapter.GetDataByContentID(ContentID)
        If ChgContent.Count = 0 Then
            'return false if no matching record is found
            Return False
        End If
        Dim ContentRow As DataSet1.ContentRow = ChgContent(0)
        If Content Is Nothing Then ContentRow.SetContentNull() Else ContentRow.Content = Content
        If ContentName Is Nothing Then ContentRow.SetContentNameNull() Else ContentRow.ContentName = ContentName
        If ContentTitle Is Nothing Then ContentRow.SetContentTitleNull() Else ContentRow.ContentTitle = ContentTitle
        ContentRow.ContentPublished = ContentPublished
        ContentRow.ContentGoLiveDate = ContentGoLiveDate
        If String.IsNullOrEmpty(ContentExpirationDate) Then ContentRow.SetContentExpirationDateNull() Else ContentRow.ContentExpirationDate = CDate(ContentExpirationDate)
        Dim rowsAffected As Integer = Adapter.Update(ContentRow)

        Return rowsAffected = 1
    End Function

    'Unpublish content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, True)>
    Public Function UnpublishContent(ByVal ContentID As Integer) As Boolean
        Dim ChgContent As DataSet1.ContentDataTable = Adapter.GetDataByContentID(ContentID)
        If ChgContent.Count = 0 Then
            'return false if no matching record is found
            Return False
        End If
        Dim rowsAffected As Integer = Adapter.UnPublishContent(ContentID)
        Return rowsAffected = 1
    End Function

    'Delete content function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, True)>
    Public Function DeleteContent(ByVal ContentId As Integer) As Boolean
        Dim rowsAffected As Integer = Adapter.DeleteContentByContentID(ContentId)
        Return rowsAffected = 1
    End Function

    Public Shared Function Validate(ByVal _expDate As Date) As Boolean
        Dim _isValid As Boolean = True
        If Not _expDate > Today.AddDays(1) Then _isValid = False
        Return _isValid
    End Function

End Class
