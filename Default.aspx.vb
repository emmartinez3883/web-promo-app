Imports System.Web.HttpContext
Imports Util
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim msg As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, e As System.EventArgs) Handles Me.Load
        Dim aduser As String = String.Empty
        If Session("OperatorId") Is Nothing Then
            aduser = Current.Request.LogonUserIdentity.Name.Replace("HQ\", "")
            Session("OperatorId") = aduser
        Else
            aduser = Session("OperatorId")
        End If

        If Session("AuthPrismUser") Is Nothing Then
            If Not Page.IsPostBack Then
                Try
                    Util.SetSessionGroups(aduser)
                    Dim fullName As String = ""
                    Util.SetUserName(aduser, fullName)
                    Session("UserFullName") = fullName
                Catch ex As Exception
                    LogIt.LogError(ex.Message, "Default.aspx", "Authenticate User")
                    Response.Redirect("NotAuth.aspx")
                End Try
            End If
        End If

        If Not Session("AuthPrismUser") = True Then
            Response.Redirect("NotAuth.aspx")
        End If
    End Sub

    Protected Sub CurrentGV_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim ContentID As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim ContentBLL As New ContentBLL
        Dim FilesBLL As New SiteFilesBLL
        If (e.CommandName = "Edit") Then
            Response.Redirect("EditContent.aspx?id=" & ContentID)

        ElseIf (e.CommandName = "Remove") Then
            Try
                If ContentBLL.DeleteContent(ContentID) Then
                    'content deleted. now delete image files
                    FilesBLL.DeleteFilesByContentID(ContentID)
                Else
                    'failed to delete content
                End If
            Catch ex As Exception
                LogIt.LogError(ex.Message, "Default.aspx", "Delete Current Content")
                msg = "Failed to delete content.  Please contact the I.S. Department for additional assistance"
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Delete Content Error", _
                    "alert('" & msg & "');", True)
            End Try
            CurrentGV.DataBind()

        ElseIf (e.CommandName = "UnPub") Then
            Try
                If ContentBLL.UnpublishContent(ContentID) Then
                    'content successfully unpublished
                    CurrentGV.DataBind()
                Else
                    'failed to unpublish content
                End If
            Catch ex As Exception
                LogIt.LogError(ex.Message, "Default.aspx", "Unpublish Current Content")
                msg = "Failed to unpublish content.  Please contact the I.S. Department for additional assistance"
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Unpublish Content Error", _
                    "alert('" & msg & "');", True)
            End Try

        ElseIf (e.CommandName = "Preview") Then
            'link to preview content in vali environment
            Dim PreviewUrl As String = System.Configuration.ConfigurationManager.AppSettings("PreviewUrl")
            Response.Redirect(PreviewUrl & ContentID)
        End If
    End Sub

    Protected Sub ExpiredGV_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Dim ContentID As Integer = Integer.Parse(e.CommandArgument.ToString())
        Dim ContentBLL As New ContentBLL
        Dim FilesBLL As New SiteFilesBLL
        If (e.CommandName = "Edit") Then
            Response.Redirect("EditContent.aspx?id=" & ContentID)

        ElseIf (e.CommandName = "Remove") Then
            Try
                If ContentBLL.DeleteContent(ContentID) Then
                    'content deleted. now delete image files
                    FilesBLL.DeleteFilesByContentID(ContentID)
                Else
                    'failed to delete content
                End If
            Catch ex As Exception
                LogIt.LogError(ex.Message, "Default.aspx", "Delete Expired Content")
                msg = "Failed to delete content.  Please contact the I.S. Department for additional assistance"
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Delete Content Error", _
                    "alert('" & msg & "');", True)
            End Try
            ExpiredGV.DataBind()

        ElseIf (e.CommandName = "Preview") Then
            'link to preview content in vali environment
            Dim PreviewUrl As String = System.Configuration.ConfigurationManager.AppSettings("PreviewUrl")
            Response.Redirect(PreviewUrl & ContentID)
        End If
    End Sub

    Protected Sub ClearBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearBtn.Click
        SearchExp.Text = Nothing
    End Sub

End Class
