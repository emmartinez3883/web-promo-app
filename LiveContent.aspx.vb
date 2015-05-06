Imports ContentBLL
Imports System.Data
Imports System.Data.SqlClient

Partial Class LiveContent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Session("AuthPrismUser") = True Then
            Response.Redirect("NotAuth.aspx")
        End If

        Dim Content As New ContentBLL
        Dim dt As New DataTable
        dt = Content.GetAllPublishedContent()
        BannerRptr.DataSource = dt
        BannerRptr.DataBind()

        Dim SubHeader As New SubHeaderBLL
        Dim dt2 As New DataTable
        dt2 = SubHeader.GetAllSubHeaders()
        SubHdrRptr.DataSource = dt2
        SubHdrRptr.DataBind()

    End Sub

End Class
