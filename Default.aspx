<%@ Page Title="" Language="VB" MasterPageFile="~/PRISM.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="TitleContent" ContentPlaceHolderID="PageTitle" runat="server">
    PRISM | Home
</asp:Content>
<asp:Content ID="Head" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1"
    runat="Server">
    <div>
        <br />
        <h2>Current Content</h2>
        <br />
        <asp:GridView ID="CurrentGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ContentID" DataSourceID="ObjectDataSource1" OnRowCommand="CurrentGV_RowCommand" AllowPaging="true" PageSize="10" class="GV" 
        EmptyDataText="No active content available">
            <Columns>
                <asp:ImageField DataImageUrlField="ContentID" HeaderText="Thumbnail" DataImageUrlFormatString="~/ImageHandler.ashx?type=1&id={0}" ReadOnly="true" InsertVisible="false" ControlStyle-Width="100" />
                <asp:BoundField DataField="ContentName" HeaderText="Name" SortExpression="ContentName" />
                <asp:BoundField DataField="ContentGoLiveDate" HeaderText="Go Live Date" SortExpression="ContentGoLiveDate" DataFormatString="{0:d}" />
                <asp:BoundField DataField="ContentExpirationDate" HeaderText="Expiration Date" SortExpression="ContentExpirationDate" DataFormatString="{0:d}" />
                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn" />
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                <asp:TemplateField HeaderText="Published?">
                    <ItemTemplate>
                        <asp:Label ID="PubLbl" runat="server" Text="<%# IIf(Boolean.Parse(Eval(“ContentPublished”).ToString()), “Yes”, “No”)%>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 25%">
                                    <asp:Button ID="PreviewBtn" runat="server" Text="Preview" Class="button black" CommandArgument='<%#Eval("ContentID")%>' CommandName="Preview" />
                                </td>
                                <td style="width: 25%">
                                    <asp:Button ID="EditBtn" runat="server" Text="Edit" Class="button black" CommandArgument='<%#Eval("ContentID")%>' CommandName="Edit" />
                                </td>
                                <td style="width: 25%">
                                    <asp:Button ID="UnPubBtn" runat="server" Text="Unpublish" Class="button black" Enabled='<%# Eval("ContentPublished") <> 0%>' CommandArgument='<%#Eval("ContentID")%>' CommandName="UnPub" />
                            <asp:ConfirmButtonExtender ID="UnPubBtnConf" runat="server" TargetControlID="UnPubBtn" 
                            ConfirmText="Are you sure you want to unpublish this content?" ConfirmOnFormSubmit="false"></asp:ConfirmButtonExtender>
                                </td>
                                <td style="width: 25%">
                                    <asp:Button ID="DeleteBtn" runat="server" Text="Delete" Class="button black" CommandArgument='<%#Eval("ContentID")%>' CommandName="Remove" />
                            <asp:ConfirmButtonExtender ID="delBtnConf" runat="server" TargetControlID="DeleteBtn" 
                            ConfirmText="WARNING: You are about to delete this content and its associated images. Continue?" ConfirmOnFormSubmit="false"></asp:ConfirmButtonExtender>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <br />
    <br />
    <div>
        <h2>Expired Content</h2>
        <br />
        <asp:Label runat="server" text="Search content by Name: "/><br />
            <asp:TextBox ID="SearchExp" runat="server" Width="200px" />&nbsp;&nbsp;<asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="button black" />&nbsp;&nbsp;<asp:Button ID="ClearBtn" runat="server" Text="Clear" CssClass="button black" /> 
        <br />
        <br />
        <asp:GridView ID="ExpiredGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ContentID" DataSourceID="ObjectDataSource2" OnRowCommand="ExpiredGV_RowCommand" AllowPaging="true" PageSize="10" class="GV"
        emptydatatext="No expired content available">
            <Columns>
                <asp:ImageField DataImageUrlField="ContentID" HeaderText="Thumbnail" DataImageUrlFormatString="~/ImageHandler.ashx?type=1&id={0}" ReadOnly="true" InsertVisible="false" ControlStyle-Width="100" />
                <asp:BoundField DataField="ContentName" HeaderText="Name" SortExpression="ContentName" />
                <asp:BoundField DataField="ContentGoLiveDate" HeaderText="Go Live Date" SortExpression="ContentGoLiveDate" DataFormatString="{0:d}" />
                <asp:BoundField DataField="ContentExpirationDate" HeaderText="Expiration Date" SortExpression="ContentExpirationDate" DataFormatString="{0:d}" />
                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn" />
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td style="width: 33%">
                                    <asp:Button ID="ExpPrevBtn" runat="server" Text="Preview" Class="button black" CommandArgument='<%#Eval("ContentID")%>' CommandName="Preview" />
                                </td>
                                <td style="width: 33%">
                                    <asp:Button ID="ExpEditBtn" runat="server" Text="Edit" Class="button black" CommandArgument='<%#Eval("ContentID")%>' CommandName="Edit" />
                                </td>
                                <td style="width: 33%">
                                    <asp:Button ID="ExpDelBtn" runat="server" Text="Delete" Class="button black" CommandArgument='<%#Eval("ContentID")%>' CommandName="Remove" />
                            <asp:ConfirmButtonExtender ID="delBtnConf" runat="server" TargetControlID="ExpDelBtn" 
                            ConfirmText="WARNING: You are about to delete this content and its associated images. Continue?" ConfirmOnFormSubmit="false"></asp:ConfirmButtonExtender>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllCurrentContent" TypeName="DataSet1TableAdapters.ContentTableAdapter"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllExpiredContent" TypeName="DataSet1TableAdapters.ContentTableAdapter" 
        FilterExpression="ContentName LIKE '%{0}%'"><FilterParameters><asp:ControlParameter ControlID="SearchExp" PropertyName="Text"/></FilterParameters></asp:ObjectDataSource>
</asp:Content>

