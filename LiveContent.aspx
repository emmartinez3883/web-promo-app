<%@ Page Title="" Language="VB" MasterPageFile="~/PRISM.master" AutoEventWireup="false" CodeFile="LiveContent.aspx.vb" Inherits="LiveContent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="PageTitle" runat="server">
    PRISM | Live Content
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<br />
<div class="GV">
<asp:Accordion ID="CntAccordion" runat="server" Height="175px" 
        FadeTransitions="True" RequireOpenedPane="False" HeaderCssClass="accordionHeader">
<Panes>
<asp:AccordionPane ID="PromoPane" runat="server">
<header><h2>Promotions</h2></header>
    <Content>
        <div class="GV">
            <asp:Repeater ID="BannerRptr" runat="server">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td style="text-align: left">
                                <%# DataBinder.Eval(Container.DataItem, "ContentGoLiveDate","{0:d}")%>
                                &#166;
                                <%# DataBinder.Eval(Container.DataItem, "ContentName")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                        <% Dim promoLnk As String = System.Configuration.ConfigurationManager.AppSettings("PromoDtlUrl")%>
                                <a href='<%=promoLnk %><%# String.Format("{0}", DataBinder.Eval(Container.DataItem, "ContentID"))%>'target="_blank">
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/ImageHandler.ashx?type=3&id={0}", DataBinder.Eval(Container.DataItem, "ContentID"))%>'
                                        Width="540px" />
                                </a>
                            </td>
                        </tr>
                        <br />
                        <br />
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <br />
        </div>
    </Content>
    </asp:AccordionPane>
    <asp:AccordionPane ID="SubHdrPane" runat="server">
        <Header>
            <h2>
                Sub-Headers</h2>
        </Header>
        <Content>
            <div id="SubHeaders" class="GV" style="margin-left: 5px">
                <br />
                <asp:Repeater ID="SubHdrRptr" runat="server">
                    <ItemTemplate>
                        <div id="main<%# DataBinder.Eval(Container.DataItem, "SubHeaderID")%>">
                            <asp:Image ID="img1" ImageUrl='<%# String.Format("~/ImageHandler.ashx?type=4&id={0}", DataBinder.Eval(Container.DataItem, "SubHeaderID"))%>'
                                runat="server" />
                            <h3>
                                <%# DataBinder.Eval(Container.DataItem, "Title")%></h3>
                            <%# DataBinder.Eval(Container.DataItem, "Excerpt")%>
                            <h3>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "HyperLink")%>'>Read More &#187</asp:HyperLink></h3>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </Content>
    </asp:AccordionPane>
    </Panes>
    </asp:Accordion>
    </div>
</asp:Content>

