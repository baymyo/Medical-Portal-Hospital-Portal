<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateTimeControl.ascx.cs" Inherits="baymyoStatic.DateTimeControl" %>
<asp:Panel ID="pnlDate" runat="server" CssClass="pull-left">
    <asp:DropDownList ID="ddlGun" runat="server" CssClass="form-control pull-left" Width="65">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlAy" runat="server" CssClass="form-control pull-left" Width="65">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlYil" runat="server" CssClass="form-control pull-left" Width="80">
    </asp:DropDownList>
    <div class="clearfix">
    </div>
</asp:Panel>
<asp:Panel ID="pnlTime" runat="server" CssClass="pull-left">
    <asp:DropDownList ID="ddlSaat" runat="server" CssClass="form-control pull-left" Width="65">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDakika" runat="server" CssClass="form-control pull-left" Width="65">
    </asp:DropDownList>
    <div class="clearfix">
    </div>
</asp:Panel>
<div class="clearfix">
</div>
