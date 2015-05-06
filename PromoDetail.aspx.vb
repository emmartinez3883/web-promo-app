Imports ContentBLL
Imports System.Data
Partial Class PromoDetail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request.QueryString("id") Is Nothing Then
            Dim ID As Integer = CInt(Request.QueryString("id"))
            Dim ws As New PRISM_Service.ServiceSoapClient
            Dim Content As New DataTable
            Content = ws.getContentById(ID)
            Dim ContentDr As DataRow = Content.Rows(0)
            Dim ContentTitle As String = ContentDr("ContentTitle")
            Dim ContentTxt As String = ContentDr("Content")
            promoTitle.InnerHtml = ContentTitle
            promoDetail.InnerHtml = ContentTxt

            If ContentDr("ContentPublished") = 0 Then
                PublishBtn.Visible = True
                PubLbl.Text = "This is an unpublished draft"
            Else
                PubLbl.Text = "This content has already been published"
            End If

            If Not IsDBNull(ContentDr("ContentExpirationDate")) Then
                If ContentDr("ContentExpirationDate") <= Today Then
                    PublishBtn.Visible = False
                    PubLbl.Text = "This content is currently expired.  You must edit the Expiration Date in order to republish it"
                End If
            End If

            Dim ImgFile As New DataTable
            'Grab the Header Lite image file for the ContentID from query string
            ImgFile = ws.getFilesById_Type(ID, 2)
            Dim ImgDr As DataRow = ImgFile.Rows(0)
            Dim Img() As Byte = ImgDr("FileData")
            Dim base64Img As String = Convert.ToBase64String(Img, 0, Img.Length)
            HeaderLiteImg.ImageUrl = "data:image/png;base64," & base64Img
        End If
    End Sub

    Protected Sub PublishBtn_Click(sender As Object, e As EventArgs)
        Dim ws As New PRISM_Service.ServiceSoapClient
        If ws.publishContent(CInt(Request.QueryString("id"))) Then
            PublishBtn.Visible = False
            PubLbl.Text = "This content has already been published"
        Else
            'failed to publish content
        End If

    End Sub
End Class
