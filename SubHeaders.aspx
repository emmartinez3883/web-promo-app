<%@ Page Title="" Language="VB" MasterPageFile="~/PRISM.master" AutoEventWireup="false" CodeFile="SubHeaders.aspx.vb" Inherits="EditSubHeaders" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="PageTitle" runat="server">
    PRISM | Sub-Headers
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="//code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <table>
        <tr>
            <td colspan="2">
                <h2>
                    Sub-Header 1</h2>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Title Heading:
            </td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="500px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Excerpt:
            </td>
            <td>
                <asp:TextBox ID="txtExcerpt" runat="server" Width="500px" Height="50px" MaxLength="500"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                HyperLink URL:
            </td>
            <td>
                <asp:TextBox ID="txtHyperLink" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Image:
            </td>
            <td>
                <asp:FileUpload ID="Sub1File" runat="server" /><br />
                <asp:Button ID="Sub1UploadBtn" runat="server" Text="Upload" OnClick="Sub1ImgUploadBtn_Click"
                    class="button black" /><br />
                <asp:Label ID="Sub1ImgLbl" runat="server" Text="" Visible="false" ForeColor="red" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Preview:
            </td>
            <td colspan="2">
                <img id="Sub1ImgPreview" runat="server" height="108" width="169" src="" style="border-width: 0px" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="Sub1ValLbl" runat="server" ForeColor="white" BackColor="Red" Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="btnSub1Save" runat="server" Text="Save Changes" class="button black" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td colspan="2">
                <h2>
                    Sub-Header 2</h2>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Title Heading:
            </td>
            <td>
                <asp:TextBox ID="txtTitle2" runat="server" Width="500px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Excerpt:
            </td>
            <td>
                <asp:TextBox ID="txtExcerpt2" runat="server" Width="500px" Height="50px" MaxLength="500"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                HyperLink URL:
            </td>
            <td>
                <asp:TextBox ID="txtHyperLink2" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Image:
            </td>
            <td>
                <asp:FileUpload ID="Sub2File" runat="server" /><br />
                <asp:Button ID="Sub2UploadBtn" runat="server" Text="Upload" OnClick="Sub2ImgUploadBtn_Click"
                    class="button black" /><br />
                <asp:Label ID="Sub2ImgLbl" runat="server" Text="" Visible="false" ForeColor="red" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Preview:
            </td>
            <td colspan="2">
                <img id="Sub2ImgPreview" runat="server" height="108" width="169" src="" style="border-width: 0px" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="Sub2ValLbl" runat="server" ForeColor="white" BackColor="Red" Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="btnSub2Save" runat="server" Text="Save Changes" class="button black" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td colspan="2">
                <h2>
                    Sub-Header 3</h2>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Title Heading:
            </td>
            <td>
                <asp:TextBox ID="txtTitle3" runat="server" Width="500px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Excerpt:
            </td>
            <td>
                <asp:TextBox ID="txtExcerpt3" runat="server" Width="500px" Height="50px" MaxLength="500"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                HyperLink URL:
            </td>
            <td>
                <asp:TextBox ID="txtHyperLink3" runat="server" Width="500px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Image:
            </td>
            <td>
                <asp:FileUpload ID="Sub3File" runat="server" /><br />
                <asp:Button ID="Sub3UploadBtn" runat="server" Text="Upload" OnClick="Sub3ImgUploadBtn_Click"
                    class="button black" /><br />
                <asp:Label ID="Sub3ImgLbl" runat="server" Text="" Visible="false" ForeColor="red" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">
                Preview:
            </td>
            <td colspan="2">
                <img id="Sub3ImgPreview" runat="server" height="108" width="169" src="" style="border-width: 0px" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Label ID="Sub3ValLbl" runat="server" ForeColor="white" BackColor="Red" Visible="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="btnSub3Save" runat="server" Text="Save Changes" class="button black" />&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

