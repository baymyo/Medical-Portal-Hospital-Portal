<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="sayfaliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.sayfaliste" %>
<div class="page-header">
    <h2 class="pull-left">Sayfa Bilgileri</h2>
    <div class="form-group pull-right">
        <asp:Button ID="btnSaveChanges" runat="server" Text="Seçilenlere Uygula" CssClass="btn btn-warning pull-left"
            OnClick="btnSaveChanges_Click" />
        <asp:DropDownList ID="ddlIslemler" runat="server" CssClass="form-control pull-right" Width="200px">
        </asp:DropDownList>
    </div>
    <div class="clearfix"></div>
</div>
<asp:GridView ClientIDMode="Static" ID="dataGrid1" runat="server" GridLines="None" CssClass="table table-condensed table-responsive"
    AutoGenerateColumns="False" DataKeyNames="id">
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
        <asp:BoundField DataField="baslik" HeaderText="Başlık" ReadOnly="True">
            <HeaderStyle Width="75%" />
            <ItemStyle Width="75%" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Gösterim">
            <HeaderStyle Width="17%" HorizontalAlign="Center" />
            <ItemStyle Width="17%" HorizontalAlign="Center" />
            <ItemTemplate>
                <%# baymyoStatic.Core.GetMenuType(BAYMYO.MultiSQLClient.MConvert.NullToByte(Eval("yerlesim")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="Yayımlama Durumu">D</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateContent(Eval("aktif")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href='<%# baymyoStatic.Core.CreateLink("sayfa",Eval("id"),Eval("baslik")) %>' target="_blank">
                    <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "baslik") + " sayfasını görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href='<%# baymyoStatic.Settings.PanelPath +"?go=sayfa&sid="+ Eval("id")  %>'>
                    <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "baslik") + " sayfasını düzeltmek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
