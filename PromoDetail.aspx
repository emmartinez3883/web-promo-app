<%@ Page Title="" Language="VB" MasterPageFile="~/PRISM.master" AutoEventWireup="false" CodeFile="PromoDetail.aspx.vb" Inherits="PromoDetail" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Image ID="HeaderLiteImg" runat="server" ImageUrl="" AlternateText="Header Lite Image" />
    <h1 id="promoTitle" runat="server">
    </h1>
    <div id="promoDetail" runat="server">
    </div>
    <div style="text-align:center">
        <div style="background-color:red">
        <asp:Label ID="PubLbl" runat="server" style="font-weight:bold;color:white" Text=""/>
                    <br /><br />
        <asp:Button ID="PublishBtn" runat="server" Text="Publish Content" OnClick="PublishBtn_Click" visible="false"/>
            </div>
    </div>
</asp:Content>

