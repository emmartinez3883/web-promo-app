
Partial Class ListContent
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, e As System.EventArgs) Handles Me.Load
        Dim ContentLogic As New ContentBLL()
        ListContentRepeater.DataSource = ContentLogic.GetAllPublishedContent()
        ListContentRepeater.DataBind()
    End Sub
End Class
