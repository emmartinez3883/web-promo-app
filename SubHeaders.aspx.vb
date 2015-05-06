Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
'Imports System.Net
'Imports System.Net.Sockets


Partial Class EditSubHeaders
    Inherits System.Web.UI.Page

    Protected Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("AuthPrismUser") = True Then
            Response.Redirect("NotAuth.aspx")
        End If
        If Not IsPostBack Then
                Dim SubHeaders As SubHeaderDS.SubHeadersDataTable
                Dim BLL As New SubHeaderBLL
                SubHeaders = BLL.GetAllSubHeaders()
                'Load data & image for subheader 1
                txtTitle.Text = SubHeaders(0).Title
                txtExcerpt.Text = SubHeaders(0).Excerpt
                txtHyperLink.Text = SubHeaders(0).HyperLink
                Session("Sub1Img") = SubHeaders(0).ImageBytes
                Dim Sub1Img As String = ConvertImages(SubHeaders(0).ImageBytes)
                Sub1ImgPreview.Src = Sub1Img
                'for subheader 2
                txtTitle2.Text = SubHeaders(1).Title
                txtExcerpt2.Text = SubHeaders(1).Excerpt
                txtHyperLink2.Text = SubHeaders(1).HyperLink
                Session("Sub2Img") = SubHeaders(1).ImageBytes
                Dim Sub2Img As String = ConvertImages(SubHeaders(1).ImageBytes)
                Sub2ImgPreview.Src = Sub2Img
                'for subheader 3
                txtTitle3.Text = SubHeaders(2).Title
                txtExcerpt3.Text = SubHeaders(2).Excerpt
                txtHyperLink3.Text = SubHeaders(2).HyperLink
                Session("Sub3Img") = SubHeaders(2).ImageBytes
                Dim Sub3Img As String = ConvertImages(SubHeaders(2).ImageBytes)
                Sub3ImgPreview.Src = Sub3Img
        End If

    End Sub

    Private Function ConvertImages(Img As Byte()) As String
        Dim base64Img As String = ""
        base64Img = "data:image/png;base64," + Convert.ToBase64String(Img, 0, Img.Length)
        Return base64Img
    End Function

    'Private Function UrlIsValid(ByVal url As String) As Boolean
    '    Dim is_valid As Boolean = False
    '    If url.ToLower().StartsWith("www.") Then url = _
    '        "http://" & url
    '    Dim web_response As HttpWebResponse = Nothing
    '    Try
    '        Dim web_request As HttpWebRequest = _
    '            HttpWebRequest.Create(url)
    '        web_response = _
    '            DirectCast(web_request.GetResponse(),  _
    '            HttpWebResponse)
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    Finally
    '        If Not (web_response Is Nothing) Then _
    '            web_response.Close()
    '    End Try
    'End Function

    Private Function ContentValid(ByVal Title As String, ByVal Excerpt As String, ByVal HyperLink As String, ByVal ImgSrc As String) As String
        Dim ValErrors As String = Nothing
        If Title.Trim.Length = 0 Then ValErrors += "<br />Please provide a Sub-Header Title"
        If Excerpt.Trim.Length = 0 Then ValErrors += "<br />Please provide text for the Sub-Header excerpt"
        If HyperLink.Trim.Length = 0 Then ValErrors += "<br />Please provide a hyperlink for this Sub-Header"
        If ImgSrc = "" Then ValErrors += "<br />A sub-Header image is required"
        Return ValErrors
    End Function

    Protected Sub btnSub1Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSub1Save.Click
        Try
            Sub1ValLbl.Visible = False
            Sub1ValLbl.Text = Nothing
            Dim ValErrs As String = ContentValid(txtTitle.Text, txtExcerpt.Text, txtHyperLink.Text, Sub1ImgPreview.Src)
            If String.IsNullOrEmpty(ValErrs) Then
                Dim bll As New SubHeaderBLL
                If bll.UpdateSubHeader(1, txtTitle.Text, txtExcerpt.Text, txtHyperLink.Text, Session("Sub1Img"), Today, Session("UserFullName")) Then
                    'all worked as expected
                    Sub1ValLbl.Visible = True
                    Sub1ValLbl.Text = "Sub-Header 1 was updated successfully"
                Else
                    'failed to save SubHeader
                End If
            Else
                'SubHeader failed validation
                Sub1ValLbl.Visible = True
                Sub1ValLbl.Text = "ERRORS:<br />" & ValErrs
                Sub1ValLbl.Focus()
            End If
        Catch ex As Exception
            LogIt.LogError(ex.Message, "SubHeaders.aspx", "btnSub1Save_Click")
            Sub1ValLbl.Visible = True
            Sub1ValLbl.Text = "ERRORS:<br />Failed to save changes to Sub-Header 1. Please contact the I.S. department for additional assistance"
            Sub1ValLbl.Focus()
        End Try
    End Sub

    Protected Sub btnSub2Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSub2Save.Click
        Try
            Sub2ValLbl.Visible = False
            Sub2ValLbl.Text = Nothing
            Dim ValErrs As String = ContentValid(txtTitle2.Text, txtExcerpt2.Text, txtHyperLink2.Text, Sub2ImgPreview.Src)
            If String.IsNullOrEmpty(ValErrs) Then
                Dim bll As New SubHeaderBLL
                If bll.UpdateSubHeader(2, txtTitle2.Text, txtExcerpt2.Text, txtHyperLink2.Text, Session("Sub2Img"), Today, Session("UserFullName")) Then
                    'all worked as expected
                    Sub2ValLbl.Visible = True
                    Sub2ValLbl.Text = "Sub-Header 2 was updated successfully"
                Else
                    'failed to save SubHeader
                End If
            Else
                'SubHeader failed validation
                Sub2ValLbl.Visible = True
                Sub2ValLbl.Text = "ERRORS:<br />" & ValErrs
                Sub2ValLbl.Focus()
            End If
        Catch ex As Exception
            LogIt.LogError(ex.Message, "SubHeaders.aspx", "btnSub2Save_Click")
            Sub2ValLbl.Visible = True
            Sub2ValLbl.Text = "ERRORS:<br />Failed to save changes to Sub-Header 2. Please contact the I.S. department for additional assistance"
            Sub2ValLbl.Focus()
        End Try
    End Sub

    Protected Sub btnSub3Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSub3Save.Click
        Try
            Sub3ValLbl.Visible = False
            Sub3ValLbl.Text = Nothing
            Dim ValErrs As String = ContentValid(txtTitle3.Text, txtExcerpt3.Text, txtHyperLink3.Text, Sub3ImgPreview.Src)
            If String.IsNullOrEmpty(ValErrs) Then
                Dim bll As New SubHeaderBLL
                If bll.UpdateSubHeader(3, txtTitle3.Text, txtExcerpt3.Text, txtHyperLink3.Text, Session("Sub3Img"), Today, Session("UserFullName")) Then
                    'all worked as expected
                    Sub3ValLbl.Visible = True
                    Sub3ValLbl.Text = "Sub-Header 3 was updated successfully"
                Else
                    'failed to save SubHeader
                End If
            Else
                'SubHeader failed validation
                Sub3ValLbl.Visible = True
                Sub3ValLbl.Text = "ERRORS:<br />" & ValErrs
                Sub3ValLbl.Focus()
            End If
        Catch ex As Exception
            LogIt.LogError(ex.Message, "SubHeaders.aspx", "btnSub3Save_Click")
            Sub3ValLbl.Visible = True
            Sub3ValLbl.Text = "ERRORS:<br />Failed to save changes to Sub-Header 3. Please contact the I.S. department for additional assistance"
            Sub3ValLbl.Focus()
        End Try
    End Sub


    Protected Sub Sub1ImgUploadBtn_Click(sender As Object, e As EventArgs)
        If Sub1File.HasFile Then
            Dim Finfo As New FileInfo(Sub1File.FileName)
            Dim Ext As String = Finfo.Extension.ToLower()
            If Ext <> ".gif" AndAlso Ext <> ".jpg" AndAlso Ext <> ".jpeg" AndAlso Ext <> ".png" Then
                Sub1ImgLbl.Text = "Only .gif, .jpg & .png files are allowed"
                Sub1ImgLbl.Visible = True
            Else
                Dim b As Byte() = Sub1File.FileBytes
                Session("Sub1Img") = b
                Dim ms As New MemoryStream(b)
                Dim Sub1Img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                If Sub1Img.Width = 169 And Sub1Img.Height = 108 Then
                    Sub1ImgLbl.Visible = False
                    Dim base64 As String = Convert.ToBase64String(b, 0, b.Length)
                    Sub1ImgPreview.Src = "data:image/png;base64," & base64
                Else
                    Sub1ImgLbl.Text = "Uploaded image dimensions are " & Sub1Img.Width.ToString() & " x " & Sub1Img.Height.ToString() & ". Sub-Header image dimensions must be 169 x 108"
                    Sub1ImgLbl.Visible = True
                    Sub1ImgPreview.Src = Nothing
                End If
            End If
        End If
    End Sub

    Protected Sub Sub2ImgUploadBtn_Click(sender As Object, e As EventArgs)
        If Sub2File.HasFile Then
            Dim Finfo As New FileInfo(Sub2File.FileName)
            Dim Ext As String = Finfo.Extension.ToLower()
            If Ext <> ".gif" AndAlso Ext <> ".jpg" AndAlso Ext <> ".jpeg" AndAlso Ext <> ".png" Then
                Sub2ImgLbl.Text = "Only .gif, .jpg & .png files are allowed"
                Sub2ImgLbl.Visible = True
            Else
                Dim b As Byte() = Sub2File.FileBytes
                Session("Sub2Img") = b
                Dim ms As New MemoryStream(b)
                Dim Sub2Img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                If Sub2Img.Width = 169 And Sub2Img.Height = 108 Then
                    Sub2ImgLbl.Visible = False
                    Dim base64 As String = Convert.ToBase64String(b, 0, b.Length)
                    Sub2ImgPreview.Src = "data:image/png;base64," & base64
                Else
                    Sub2ImgLbl.Text = "Uploaded image dimensions are " & Sub2Img.Width.ToString() & " x " & Sub2Img.Height.ToString() & ". Sub-Header image dimensions must be 169 x 108"
                    Sub2ImgLbl.Visible = True
                    Sub2ImgPreview.Src = Nothing
                End If
            End If
        End If
    End Sub

    Protected Sub Sub3ImgUploadBtn_Click(sender As Object, e As EventArgs)
        If Sub3File.HasFile Then
            Dim Finfo As New FileInfo(Sub3File.FileName)
            Dim Ext As String = Finfo.Extension.ToLower()
            If Ext <> ".gif" AndAlso Ext <> ".jpg" AndAlso Ext <> ".jpeg" AndAlso Ext <> ".png" Then
                Sub3ImgLbl.Text = "Only .gif, .jpg & .png files are allowed"
                Sub3ImgLbl.Visible = True
            Else
                Dim b As Byte() = Sub3File.FileBytes
                Session("Sub3Img") = b
                Dim ms As New MemoryStream(b)
                Dim Sub3Img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                If Sub3Img.Width = 169 And Sub3Img.Height = 108 Then
                    Sub3ImgLbl.Visible = False
                    Dim base64 As String = Convert.ToBase64String(b, 0, b.Length)
                    Sub3ImgPreview.Src = "data:image/png;base64," & base64
                Else
                    Sub3ImgLbl.Text = "Uploaded image dimensions are " & Sub3Img.Width.ToString() & " x " & Sub3Img.Height.ToString() & ". Sub-Header image dimensions must be 169 x 108"
                    Sub3ImgLbl.Visible = True
                    Sub3ImgPreview.Src = Nothing
                End If
            End If
        End If
    End Sub

End Class
