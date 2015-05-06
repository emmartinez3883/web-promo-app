<%@ WebHandler Language="VB" Class="ImageHandler" %>

Imports System
Imports System.Web

Public Class ImageHandler : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If Not context.Request.QueryString("id") Is Nothing AndAlso Not context.Request.QueryString("type") Is Nothing Then
            Dim ID As Integer = CInt(context.Request.QueryString("id"))
            Dim FileType As Integer = CInt(context.Request.QueryString("type"))
            If FileType = 4 Then
                Dim SubHeader As New SubHeaderDS.SubHeadersDataTable
                Dim SubHeaderBLL As New SubHeaderBLL
                SubHeader = SubHeaderBLL.GetDataBySubHeaderID(ID)
                Dim b() As Byte = SubHeader(0).ImageBytes
                context.Response.BinaryWrite(b)
                context.Response.[End]()
            Else
                Dim SiteFile As New SiteFilesDS.SiteFilesDataTable
                Dim SiteFilesBLL As New SiteFilesBLL
                SiteFile = SiteFilesBLL.GetDatabyContentID_FileType(ID, FileType)
                Dim b() As Byte = SiteFile(0).FileData
                context.Response.BinaryWrite(b)
                context.Response.[End]()
            End If

        End If
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class