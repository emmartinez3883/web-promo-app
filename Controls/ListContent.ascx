<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ListContent.ascx.vb" Inherits="ListContent" %>

<asp:Repeater ID="ListContentRepeater" runat="server">
  <ItemTemplate>
    <h2>
      <asp:Label ID="ContentName" runat="server" Text='<%# Eval("ContentName")%>' />
    </h2>
      <asp:Label ID="Content" runat="server" Text='<%# Eval("Content")%>' />
    <hr />
  </ItemTemplate>
</asp:Repeater>