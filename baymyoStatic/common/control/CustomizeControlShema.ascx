<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomizeControlShema.ascx.cs" Inherits="baymyoStatic.CustomizeControlShema" %>
<div class="form-group row">
    <label for="inputText" class="col-sm-12 text-left">
        <asp:Literal ID="ltrTitle" runat="server" Text="Title"></asp:Literal></label>
    <div class="col-sm-12">
        <asp:Panel ID="pnlControl" runat="server">
        </asp:Panel>
        <asp:Literal ID="ltrExample" runat="server" Text=""></asp:Literal>
    </div>
</div>