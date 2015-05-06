Imports System.Data.SqlClient
Imports System.Data
Imports System.IO


Partial Class AddContent
    Inherits System.Web.UI.Page

    Dim SQLCon As New SqlConnection(ConfigurationManager.ConnectionStrings("PrismConn").ConnectionString)
    Protected Sub page_load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Session("AuthPrismUser") = True Then
            Response.Redirect("NotAuth.aspx")
        End If

        If Not IsPostBack Then
            Dim Phone As String = System.Configuration.ConfigurationManager.AppSettings("LStreamPhone")
            Dim DonateUrl As String = System.Configuration.ConfigurationManager.AppSettings("DonateUrl")
            Dim LocationsUrl As String = System.Configuration.ConfigurationManager.AppSettings("LocationsUrl")
            CKEditBody.Text = "<br /><br />Set your appointment TODAY by calling " & Phone & " or <a href='" & DonateUrl & "'>clicking here</a>." & _
                              "<br /><br />For donor center locations and hours, <a href='" & LocationsUrl & "'>click here."
        End If
    End Sub


    Private Function ContentValid() As String
        Dim ValErrors As String = Nothing
        If DateTime.TryParse(txtLiveDate.Text, Nothing) = False Then ValErrors += "<br />Please select/enter a valid Go Live Date"
        If Not String.IsNullOrEmpty(txtExpDate.Text) Then
            If DateTime.TryParse(txtExpDate.Text, Nothing) = False Then ValErrors += "<br />Please select/enter a valid Expiration Date"
        End If
        If DateTime.TryParse(txtLiveDate.Text, Nothing) = True And DateTime.TryParse(txtExpDate.Text, Nothing) = True Then
            If CDate(txtExpDate.Text) <= CDate(txtLiveDate.Text) Then
                ValErrors += "<br />Expiration Date must be greater than Go Live Date"
            End If
        End If
        If txtName.Text.Trim.Length = 0 Then ValErrors += "<br />Please provide a Content Name"
        If CKEditBody.Text.Trim.Length = 0 Then ValErrors += "<br />Please provide text for the Content Body"
        If HomeImgPreview.Src = "" Then ValErrors += "<br />A homepage image is required"
        If LiteImgPreview.Src = "" Then ValErrors += "<br />A content page image is required"
        If BanrImgPreview.Src = "" Then ValErrors += "<br />A banner image is required"
        Return ValErrors
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            valLbl.Text = Nothing
            Dim ValErrs As String = ContentValid()
            If String.IsNullOrEmpty(ValErrs) Then
                Dim SQLCmd As New SqlCommand("Insert_Content", SQLCon)
                SQLCmd.CommandType = CommandType.StoredProcedure

                SQLCmd.Parameters.Add(New SqlParameter("@Content", SqlDbType.NVarChar))
                SQLCmd.Parameters.Add(New SqlParameter("@ContentName", SqlDbType.NVarChar))
                SQLCmd.Parameters.Add(New SqlParameter("@ContentType", SqlDbType.NVarChar))
                SQLCmd.Parameters.Add(New SqlParameter("@ContentTitle", SqlDbType.NVarChar))
                SQLCmd.Parameters.Add(New SqlParameter("@CreatedBy", SqlDbType.NVarChar))
                SQLCmd.Parameters.Add(New SqlParameter("@ContentPublished", SqlDbType.Bit))
                SQLCmd.Parameters.Add(New SqlParameter("@ContentGoLiveDate", SqlDbType.Date))
                If String.IsNullOrEmpty(txtExpDate.Text) Then
                    SQLCmd.Parameters.Add(New SqlParameter("@ContentExpirationDate", DBNull.Value))
                Else
                    SQLCmd.Parameters.Add(New SqlParameter("@ContentExpirationDate", SqlDbType.Date))
                End If
                SQLCmd.Parameters.Add(New SqlParameter("@EntityID", SqlDbType.Int))
                Dim ReturnVal = SQLCmd.Parameters.Add(New SqlParameter("@RETURNVALUE", SqlDbType.Int))
                ReturnVal.Direction = ParameterDirection.ReturnValue

                SQLCmd.Parameters("@Content").Value = CKEditBody.Text
                SQLCmd.Parameters("@ContentName").Value = txtName.Text
                SQLCmd.Parameters("@ContentType").Value = "Promo"
                SQLCmd.Parameters("@ContentTitle").Value = txtTitle.Text
                SQLCmd.Parameters("@CreatedBy").Value = Session("UserFullName")
                SQLCmd.Parameters("@ContentPublished").Value = 0
                If Not String.IsNullOrEmpty(txtExpDate.Text) Then
                    SQLCmd.Parameters("@ContentExpirationDate").Value = CDate(txtExpDate.Text)
                End If
                SQLCmd.Parameters("@ContentGoLiveDate").Value = CDate(txtLiveDate.Text)
                SQLCmd.Parameters("@EntityID").Value = 1

                If SQLCmd.Connection.State <> ConnectionState.Open Then
                    SQLCmd.Connection.Open()
                End If

                SQLCmd.ExecuteNonQuery()

                Dim NewContentID = ReturnVal.Value

                Dim ImgTbl As New DataTable
                ImgTbl.Columns.Add("ContentID", Type.GetType("System.Int32"))
                ImgTbl.Columns.Add("FileBytes", Type.GetType("System.Array"))
                ImgTbl.Columns.Add("FileType", Type.GetType("System.Int32"))
                ImgTbl.Rows.Add(NewContentID, Session("HomeImgFile"), 1)
                ImgTbl.Rows.Add(NewContentID, Session("LiteImgFile"), 2)
                ImgTbl.Rows.Add(NewContentID, Session("BanrImgFile"), 3)

                For Each dr As DataRow In ImgTbl.Rows
                    Dim bll As New SiteFilesBLL
                    If bll.AddSiteFile(dr("ContentID"), dr("FileBytes"), dr("FileType"), Now, Session("UserFullName")) Then
                        ' All worked as expected
                    Else
                        ' The upload failed.
                    End If

                Next
                Session("HomeImgFile") = Nothing
                Session("LiteImgFile") = Nothing
                Session("BanrImgFile") = Nothing
                Response.Redirect("~/Default.aspx")
            Else
                ' Article Failed Validation
                valLbl.Text = valLbl.Text + ValErrs
                valErrsDiv.Visible = True
                valLbl.Focus()
            End If
        Catch ex As Exception
            LogIt.LogError(ex.Message, "AddContent.aspx", "btnSave_Click")
            valErrsDiv.Visible = True
            valLbl.Text = "Failed to create new content. Please contact the I.S. department for additional assistance"
            valLbl.Focus()
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CancelBtn.Click
        Response.Redirect("Default.aspx")
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
                Session("HomeImgFile") = b
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
                Session("LiteImgFile") = b
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
                Session("BanrImgFile") = b
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
End Class
