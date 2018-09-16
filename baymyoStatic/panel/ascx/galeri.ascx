<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="galeri.ascx.cs" Inherits="baymyoStatic.panel.ascx.galeri" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" runat="server" />
<div class="clearfix"></div>
<asp:Panel ID="jopSet" runat="server" CssClass="page-header">
    <h2 class="pull-left">Galeri Listesi</h2>
    <div class="form-group pull-right">
        <asp:Button ID="btnSaveChanges" runat="server" Text="Seçilenlere Uygula" CssClass="btn btn-warning pull-left"
            OnClick="btnSaveChanges_Click" />
        <asp:DropDownList ID="ddlIslemler" runat="server" CssClass="form-control pull-right" Width="200px">
        </asp:DropDownList>
        <div class="clearfix"></div>
    </div>
    <div class="clearfix"></div>
</asp:Panel>
<asp:GridView ClientIDMode="Static" ID="dataGrid1" runat="server" GridLines="None" CssClass="table table-condensed table-responsive"
    AutoGenerateColumns="False" DataKeyNames="id,resimurl">
    <RowStyle CssClass="rowStyle" />
    <AlternatingRowStyle CssClass="alternatingRowStyle" />
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:CheckBox runat="server" ID="chkAll" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="chkSelected" />
            </ItemTemplate>
            <HeaderStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Resim">
            <HeaderStyle Width="26%" HorizontalAlign="Left" VerticalAlign="Middle" />
            <ItemStyle Width="26%" HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemTemplate>
                <img src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "album/"+ Eval("albumid") + "/" + Eval("resimurl") : "admin-yok.png") %>"
                    style="width: 100%" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="aciklama" HeaderText="Açıklama" ReadOnly="True">
            <HeaderStyle Width="50%" />
            <ItemStyle Width="50%" />
        </asp:BoundField>
        <asp:BoundField DataField="kayittarihi" HeaderText="Tarih" ReadOnly="True">
            <HeaderStyle Width="20%" />
            <ItemStyle Width="20%" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="Kapak Resim Durumu">K</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateContent(Eval("kapak")) %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>

