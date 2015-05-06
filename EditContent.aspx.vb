Imports System.Data.SqlClient
Imports System.Data
Imports System.IO


Partial Class EditContent
    Inherits System.Web.UI.Page

    Protected Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("AuthPrismUser") = True Then
            Response.Redirect("NotAuth.aspx")
        End If

        If Not IsPostBack Then
            Try
                If Not Request.QueryString("id") Is Nothing Then
                    Dim ID As Integer = CInt(Request.QueryString("id"))
                    Dim Content As New DataSet1.ContentDataTable
                    Dim ContentBLL As New ContentBLL
                    Content = ContentBLL.GetContentByContentID(ID)
                    If Content IsNot Nothing Then
                        EDITtxtName.Text = Content(0).ContentName
                        EDITtxtTitle.Text = Content(0).ContentTitle
                        EDITCKBody.Text = Content(0).Content
                        EDITtxtLiveDate.Text = Content(0).ContentGoLiveDate

                        If Not Content(0).IsContentExpirationDateNull() Then
                            EDITtxtExpDate.Text = Content(0).ContentExpirationDate
                        End If

                        Dim HomeImg As String = RetrieveImages(ID, 1)
                        HomeImgPreview.Src = HomeImg

                        Dim LiteImg As String = RetrieveImages(ID, 2)
                        LiteImgPreview.Src = LiteImg

                        Dim BanrImg As String = RetrieveImages(ID, 3)
                        BanrImgPreview.Src = BanrImg
                    End If
                End If

            Catch ex As Exception
                LogIt.LogError(ex.Message, "EditContent.aspx", "Page_Load")
                valErrsDiv.Visible = True
                valLbl.Text = "Failed to load existing content. Please contact the I.S. department for additional assistance"
                valLbl.Focus()
            End Try
        End If
    End Sub

    Private Function RetrieveImages(ID As Integer, FileType As Integer) As String
        Dim base64Img As String = ""
        Dim ImgFile As New SiteFilesDS.SiteFilesDataTable
        Dim SiteFileBLL As New SiteFilesBLL
        ImgFile = SiteFileBLL.GetDatabyContentID_FileType(ID, FileType)
        If Not ImgFile Is Nothing Then
            Dim Img() As Byte = ImgFile(0).FileData
            base64Img = "data:image/png;base64," + Convert.ToBase64String(Img, 0, Img.Length)
        End If
        Return base64Img
    End Function

    Private Function ContentValid() As String
        Dim ValErrors As String = Nothing
        If DateTime.TryParse(EDITtxtLiveDate.Text, Nothing) = False Then ValErrors += "<br />Please select/enter a valid Go Live Date"
        If Not String.IsNullOrEmpty(EDITtxtExpDate.Text) Then
            If DateTime.TryParse(EDITtxtExpDate.Text, Nothing) = False Then ValErrors += "<br />Please select/enter a valid Expiration Date"
        End If
        If DateTime.TryParse(EDITtxtLiveDate.Text, Nothing) = True And DateTime.TryParse(EDITtxtExpDate.Text, Nothing) = True Then
            If CDate(EDITtxtExpDate.Text) <= CDate(EDITtxtLiveDate.Text) Then
                ValErrors += "<br />Expiration Date must be greater than Go Live Date"
            End If
        End If
        If EDITtxtName.Text.Trim.Length = 0 Then ValErrors += "<br />Please provide a Content Name"
        If EDITCKBody.Text.Trim.Length = 0 Then ValErrors += "<br />Please provide text for the Content Body"
        If HomeImgPreview.Src = "" Then ValErrors += "<br />A homepage image is required"
        If LiteImgPreview.Src = "" Then ValErrors += "<br />A content page image is required"
        If BanrImgPreview.Src = "" Then ValErrors += "<br />A banner image is required"
        Return ValErrors
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles EDITbtnSave.Click
        Try
            valLbl.Text = Nothing
            Dim ValErrs As String = ContentValid()
            If String.IsNullOrEmpty(ValErrs) Then

                Dim ContentBLL As New ContentBLL
                Dim ContentId As Integer = CInt(Request.QueryString("id"))
                Dim ExpDate As String = ""
                If Not EDITtxtExpDate.Text.Trim.Length = 0 Then
                    ExpDate = CDate(EDITtxtExpDate.Text)
                End If
                If ContentBLL.UpdateContent(ContentId, EDITtxtName.Text, EDITtxtTitle.Text, EDITCKBody.Text, 0, CDate(EDITtxtLiveDate.Text), ExpDate) Then

                    Dim ImgTbl As New DataTable
                    ImgTbl.Columns.Add("ContentID", Type.GetType("System.Int32"))
                    ImgTbl.Columns.Add("FileBytes", Type.GetType("System.Array"))
                    ImgTbl.Columns.Add("FileType", Type.GetType("System.Int32"))
                    If Not Session("EDITHomeImgFile") Is Nothing Then ImgTbl.Rows.Add(ContentId, Session("EDITHomeImgFile"), 1)
                    If Not Session("EDITLiteImgFile") Is Nothing Then ImgTbl.Rows.Add(ContentId, Session("EDITLiteImgFile"), 2)
                    If Not Session("EDITBanrImgFile") Is Nothing Then ImgTbl.Rows.Add(ContentId, Session("EDITBanrImgFile"), 3)

                    For Each dr As DataRow In ImgTbl.Rows
                        Dim bll As New SiteFilesBLL
                        If bll.DeleteSiteFile(dr("ContentID"), dr("FileType")) Then
                            bll.AddSiteFile(dr("ContentID"), dr("FileBytes"), dr("FileType"), Now, Session("UserFullName"))
                            ' All worked as expected
                        Else
                            ' The upload failed.
                        End If

                    Next
                    Session("HomeImgFile") = Nothing
                    Session("LiteImgFile") = Nothing
                    Session("BanrImgFile") = Nothing
                    Response.Redirect("~/Default.aspx")

                End If

            Else
                ' Article Failed Validation
                valLbl.Text = valLbl.Text + ValErrs
                valErrsDiv.Visible = True
                valLbl.Focus()
            End If
        Catch ex As Exception
            LogIt.LogError(ex.Message, "EditContent.aspx", "btnSave_Click")
            valErrsDiv.Visible = True
            valLbl.Text = "Failed to save content changes. Please contact the I.S. department for additional assistance"
            valLbl.Focus()
        End Try
    End Sub

    Protected Sub homeImgUploadBtn_Click(sender As Object, e As EventArgs)
        If homeImgFile.HasFile Then
            Dim Finfo As New FileInfo(homeImgFile.FileName)
            Dim Ext As String = Finfo.Extension.ToLower()
            If Ext <> ".gif" AndAlso Ext <> ".jpg" AndAlso Ext <> ".jpeg" AndAlso Ext <> ".png" Then
                homeImgLbl.Text = "Only .gif, .jpg & .png files are allowed"
                homeImgLbl.Visible = True
            Else
                Dim b As Byte() = homeImgFile.FileBytes
                Session("EDITHomeImgFile") = b
                Dim ms As New MemoryStream(b)
                Dim homeImg As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                If homeImg.Width = 1001 And homeImg.Height = 410 Then
                    homeImgLbl.Visible = False
                    Dim base64 As String = Convert.ToBase64String(b, 0, b.Length)
                    HomeImgPreview.Src = "data:image/png;base64," & base64
                Else
                    HomeImgPreview.Src = Nothing
                    homeImgLbl.Text = "Uploaded image dimensions are " & homeImg.Width.ToString() & " x " & homeImg.Height.ToString() & ". Homepage Header image dimensions must be 1001 x 410"
                    homeImgLbl.Visible = True
                End If
            End If
        End If
    End Sub

    Protected Sub LiteImgUploadBtn_Click(sender As Object, e As EventArgs)
        If liteImgFile.HasFile Then
            Dim Finfo As New FileInfo(liteImgFile.FileName)
            Dim Ext As String = Finfo.Extension.ToLower()
            If Ext <> ".gif" AndAlso Ext <> ".jpg" AndAlso Ext <> ".jpeg" AndAlso Ext <> ".png" Then
                LiteImgLbl.Text = "Only .gif, .jpg & .png files are allowed"
                LiteImgLbl.Visible = True
            Else
                Dim b As Byte() = liteImgFile.FileBytes
                Session("EDITLiteImgFile") = b
                Dim ms As New MemoryStream(b)
                Dim liteImg As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                If liteImg.Width = 1001 And liteImg.Height = 240 Then
                    LiteImgLbl.Visible = False
                    Dim base64 As String = Convert.ToBase64String(b, 0, b.Length)
                    LiteImgPreview.Src = "data:image/png;base64," & base64
                Else
                    LiteImgPreview.Src = Nothing
                    LiteImgLbl.Text = "Uploaded image dimensions are " & liteImg.Width.ToString() & " x " & liteImg.Height.ToString() & ". Lite Header image dimensions must be 1001 x 240"
                    LiteImgLbl.Visible = True
                End If
            End If
        End If
    End Sub

    Protected Sub BanrImgUploadBtn_Click(sender As Object, e As EventArgs)
        If BanrImgFile.HasFile Then
            Dim Finfo As New FileInfo(BanrImgFile.FileName)
            Dim Ext As String = Finfo.Extension.ToLower()
            If Ext <> ".gif" AndAlso Ext <> ".jpg" AndAlso Ext <> ".jpeg" AndAlso Ext <> ".png" Then
                BanrImgLbl.Text = "Only .gif, .jpg & .png files are allowed"
                BanrImgLbl.Visible = True
            Else
                Dim b As Byte() = BanrImgFile.FileBytes
                Session("EDITBanrImgFile") = b
                Dim ms As New MemoryStream(b)
                Dim banrImg As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                If banrImg.Width = 540 And banrImg.Height = 132 Then
                    BanrImgLbl.Visible = False
                    Dim base64 As String = Convert.ToBase64String(b, 0, b.Length)
                    BanrImgPreview.Src = "data:image/png;base64," & base64
                Else
                    BanrImgPreview.Src = Nothing
                    BanrImgLbl.Text = "Uploaded image dimensions are " & banrImg.Width.ToString() & " x " & banrImg.Height.ToString() & ". Banner image dimensions must be 540 x 132"
                    BanrImgLbl.Visible = True
                End If
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelBtn.Click
        Response.Redirect("~/Default.aspx")
    End Sub
End Class
