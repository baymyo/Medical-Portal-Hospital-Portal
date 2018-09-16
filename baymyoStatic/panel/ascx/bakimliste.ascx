<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bakimliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.bakimliste" %>
<div class="page-header">
    <h2 class="pull-left">Veritabanı Bakımı</h2>
    <div class="form-group pull-right">
        <asp:Button ID="btnSaveChanges" runat="server" Text="Seçilenlere Uygula" CssClass="btn btn-warning pull-left"
            OnClick="btnSaveChanges_Click" />
        <asp:DropDownList ID="ddlIslemler" runat="server" CssClass="form-control pull-right" Width="200px">
        </asp:DropDownList>
    </div>
    <div class="clearfix"></div>
    <asp:Literal ID="infoLiteral" runat="server" Text="..." />
</div>
<asp:GridView ClientIDMode="Static" ID="dataGrid1" runat="server" GridLines="None" CssClass="table table-condensed table-responsive"
    AutoGenerateColumns="False" DataKeyNames="table">
    <RowStyle CssClass="rowStyle" />
    <AlternatingRowStyle CssClass="alternatingRowStyle" />
    <Columns>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="center" />
            <ItemStyle Width="2%" HorizontalAlign="center" />
            <ItemTemplate>
                <%# "<img src=\""+ baymyoStatic.Settings.IconsPath + "admin/database.png\" />"%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="table" HeaderText="Tablo" SortExpression="table" ItemStyle-Font-Bold="true"
            FooterStyle-Font-Size="12">
            <HeaderStyle Width="25%" />
            <ItemStyle Width="25%" />
        </asp:BoundField>
        <asp:BoundField DataField="op" HeaderText="Opsiyon" SortExpression="op">
            <HeaderStyle Width="15%" />
            <ItemStyle Width="15%" />
        </asp:BoundField>
        <asp:BoundField DataField="msg_type" HeaderText="Tipi" SortExpression="msg_type">
            <HeaderStyle Width="15%" />
            <ItemStyle Width="15%" />
        </asp:BoundField>
        <asp:BoundField DataField="msg_text" HeaderText="Mesaj" SortExpression="msg_text">
            <HeaderStyle Width="43%" />
            <ItemStyle Width="43%" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
