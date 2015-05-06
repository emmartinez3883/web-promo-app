Imports Microsoft.VisualBasic
Imports SubHeaderDSTableAdapters

Public Class SubHeaderBLL

    Private SubHeaderAdapter As SubHeadersTableAdapter = Nothing
    Protected ReadOnly Property Adapter() As SubHeadersTableAdapter
        Get
            If SubHeaderAdapter Is Nothing Then
                SubHeaderAdapter = New SubHeadersTableAdapter()
            End If
            Return SubHeaderAdapter
        End Get
    End Property

    'Get all SubHeaders function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetAllSubHeaders() As SubHeaderDS.SubHeadersDataTable
        Return Adapter.GetData()
    End Function

    'Get SubHeader by SubHeaderID function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, True)>
    Public Function GetDataBySubHeaderID(ByVal SubHeaderID As Integer) As SubHeaderDS.SubHeadersDataTable
        Return Adapter.GetDataBySubHeaderID(SubHeaderID)
    End Function

    'Add SubHeader function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, True)>
    Public Function AddSubHeader(ByVal Title As String, ByVal Excerpt As String, ByVal HyperLink As String, ByVal ImageBytes As Byte(), _
                                 ByVal LastUpdated As DateTime, ByVal UpdatedBy As String) As Boolean
        Dim SubHeader As New SubHeaderDS.SubHeadersDataTable()
        Dim SubHeaderRow As SubHeaderDS.SubHeadersRow = SubHeader.NewSubHeadersRow()
        SubHeaderRow.Title = Title
        SubHeaderRow.Excerpt = Excerpt
        SubHeaderRow.HyperLink = HyperLink
        SubHeaderRow.ImageBytes = ImageBytes
        SubHeaderRow.LastUpdated = LastUpdated
        SubHeaderRow.UpdatedBy = UpdatedBy
        SubHeader.AddSubHeadersRow(SubHeaderRow)
        Dim rowsAffected As Integer = Adapter.Update(SubHeaderRow)
        Return rowsAffected = 1
    End Function

    'Update SubHeader function
    <System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, True)>
    Public Function UpdateSubHeader(ByVal SubHeaderID As Integer, ByVal Title As String, ByVal Excerpt As String, ByVal HyperLink As String, ByVal ImageBytes As Byte(), _
                               ByVal LastUpdated As DateTime, ByVal UpdatedBy As String) As Boolean
        Dim ChgSubHeader As SubHeaderDS.SubHeadersDataTable = Adapter.GetDataBySubHeaderID(SubHeaderID)
        If ChgSubHeader.Count = 0 Then
            'return false if no matching record is found
            Return False
        End If
        Dim SubHeaderRow As SubHeaderDS.SubHeadersRow = ChgSubHeader(0)
        SubHeaderRow.Title = Title
        SubHeaderRow.Excerpt = Excerpt
        SubHeaderRow.HyperLink = HyperLink
        SubHeaderRow.ImageBytes = ImageBytes
        SubHeaderRow.LastUpdated = LastUpdated
        SubHeaderRow.UpdatedBy = UpdatedBy
        Dim rowsAffected As Integer = Adapter.Update(SubHeaderRow)

        Return rowsAffected = 1
    End Function
End Class
