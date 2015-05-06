Imports System.Web.HttpContext
Partial Class PRISM
    Inherits System.Web.UI.MasterPage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserLbl.Text = Session("UserFullName")
    End Sub

End Class

