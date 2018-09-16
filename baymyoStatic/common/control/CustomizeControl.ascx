<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomizeControl.ascx.cs" Inherits="baymyoStatic.CustomizeControl" %>
<%--<style type="text/css">
    .form-horizontal .titles {
        padding: 1%;
        border-bottom: 2px solid #ccd2d5;
        background-color: rgba(208, 213, 215, 0.20);
        color: #34495e;
    }
</style>--%>
<asp:Panel ID="CustomControls" runat="server" DefaultButton="CustomSubmit">
    <%= (this.FormTitleVisible) ? this.FormTitle : "" %>
    <asp:Literal ID="CustomMessage" runat="server" Text="" />
    <asp:Panel ID="CustomPanel" runat="server" CssClass="form-horizontal">
    </asp:Panel>
    <div class="form-group row">
        <div class="col-sm-12">
            <asp:Label ID="CustomStatus" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="text-left has-warning">
        <asp:Image ID="ValidateImage" runat="server" Style="margin-right: 5px; border: 1px solid #fff;" CssClass="pull-left" ToolTip="Resimdeki 5 karakteri birleşik olarak yandaki kutuya yazınız!" />
        <asp:TextBox ID="ValidateCode" runat="server" CssClass="form-control form-control-warning pull-left" Font-Bold="true" Font-Size="10pt" MaxLength="5" Width="80" Text=""></asp:TextBox>
    </div>
    <div class="text-right">
        <asp:Button ID="CustomSubmit" runat="server" Text="Kaydet" CssClass="btn btn-default" OnClick="CustomSubmit_Click" />
        <asp:Button ID="CustomUpdate" runat="server" Text="Güncelle" CssClass="btn btn-success" OnClick="CustomUpdate_Click" Visible="false" />
        <asp:Button ID="CustomRemove" runat="server" Text="Sil" CssClass="btn btn-danger" OnClick="CustomRemove_Click" />
    </div>
</asp:Panel>
<div class="clearfix"></div>
