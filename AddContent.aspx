<%@ Page Title="" Language="VB" MasterPageFile="~/PRISM.master" AutoEventWireup="false" CodeFile="AddContent.aspx.vb" Inherits="AddContent" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="PageTitle" runat="server">
    PRISM | Add Content
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="//code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <table>
        <tr>
            <td valign="top" align="right">Content Name:</td>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top" align="right">Title Heading:</td>
            <td>
                <asp:TextBox ID="txtTitle" runat="server" Width="500px" MaxLength="100"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top" align="right">Content Body:</td>
            <td>
                <CKEditor:CKEditorControl ID="CKEditBody" BasePath="./ckeditor/" runat="server"></CKEditor:CKEditorControl>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Go Live Date:</td>
            <td>
                <asp:TextBox ID="txtLiveDate" runat="server" Width="100px"></asp:TextBox>
                <asp:ImageButton ID="liveDtCalBtn" runat="server" ImageUrl="~/Images/Calendar.png" />
                <asp:CalendarExtender ID="liveDtCalExt" runat="server" Enabled="true" PopupButtonID="liveDtCalBtn" TargetControlID="txtLiveDate"></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Expiration Date:</td>
            <td>
                <asp:TextBox ID="txtExpDate" runat="server" Width="100px"></asp:TextBox>
                <asp:ImageButton ID="expDtCalBtn" runat="server" ImageUrl="~/Images/Calendar.png" />
                <asp:CalendarExtender ID="expDtCalExt" runat="server" Enabled="true" PopupButtonID="expDtCalBtn" TargetControlID="txtExpDate"></asp:CalendarExtender>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Homepage Header Image:</td>
            <td>
                <asp:FileUpload ID="homeImgFile" runat="server" /><br />
                <asp:Button ID="homeImgUploadBtn" runat="server" Text="Upload" OnClick="homeImgUploadBtn_Click" class="button black" /><br />
                <asp:Label ID="homeImgLbl" runat="server" Text="" Visible="false" ForeColor="red" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Preview:</td>
            <td colspan="2">
                <img id="HomeImgPreview" runat="server" height="410" width="1001" src="" style="border-width: 0px" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Content Page Header Image:</td>
            <td>
                <asp:FileUpload ID="liteImgFile" runat="server" /><br />
                <asp:Button ID="LiteImgUploadBtn" runat="server" Text="Upload" OnClick="LiteImgUploadBtn_Click" class="button black" />
                <asp:Label ID="LiteImgLbl" runat="server" Text="" Visible="false" ForeColor="red" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Preview:</td>
            <td>
                <img id="LiteImgPreview" runat="server" height="240" width="1001" src="" style="border-width: 0px" />
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Content Banner Image:</td>
            <td>
                <asp:FileUpload ID="BanrImgFile" runat="server" /><br />
                <asp:Button ID="BanrImgUploadBtn" runat="server" Text="Upload" OnClick="BanrImgUploadBtn_Click" class="button black" />
                <asp:Label ID="BanrImgLbl" runat="server" Text="" Visible="false" ForeColor="red" />
            </td>
        </tr>
        <tr>
            <td valign="top" align="right">Preview:</td>
            <td>
                <img id="BanrImgPreview" runat="server" height="132" width="540" src="" style="border-width: 0px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="valErrsDiv" runat="server" style="text-align: center; background-color: red" visible="false">
                    <asp:Label runat="server" Text="ERRORS:" ForeColor="White" /><br />
                    <asp:Label ID="valLbl" runat="server" ForeColor="White" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: right">
                <asp:Button ID="btnSave" runat="server" Text="Save Content" class="button black" />&nbsp;&nbsp;
                <asp:Button ID="CancelBtn" runat="server" Text="Cancel" Class="button black" />
            </td>
        </tr>
    </table>
</asp:Content>

