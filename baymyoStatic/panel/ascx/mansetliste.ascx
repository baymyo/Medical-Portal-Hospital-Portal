<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mansetliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.mansetliste" %>
<div class="page-header">
    <h2 class="pull-left">Manşet Listesi</h2>
    <div class="form-group pull-right">
        <asp:Button ID="btnSaveChanges" runat="server" Text="Seçilenlere Uygula" CssClass="btn btn-warning pull-left"
            OnClick="btnSaveChanges_Click" />
        <asp:DropDownList ID="ddlIslemler" runat="server" CssClass="form-control pull-right" Width="200px">
        </asp:DropDownList>
        <div class="clearfix">&nbsp;</div>
        <asp:DropDownList ID="ddlKategoriler" runat="server" CssClass="form-control pull-left" Width="200px" AutoPostBack="True"
            OnSelectedIndexChanged="ddlKategoriler_SelectedIndexChanged">
        </asp:DropDownList>
        <span class="pull-right" style="margin-top: 9px; margin-left: 10px; color: #b2b2b2; font-size: 10px;">
            <%= totalCount %></span>
    </div>
    <div class="clearfix"></div>
</div>
<asp:GridView ClientIDMode="Static" ID="dataGrid1" runat="server" GridLines="None" CssClass="table table-condensed table-responsive" 
    AutoGenerateColumns="False" DataKeyNames="id,modulid,resimbuyuk">
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
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" VerticalAlign="Top" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Resim">
            <HeaderStyle Width="13%" HorizontalAlign="Left" />
            <ItemStyle Width="13%" HorizontalAlign="Center" VerticalAlign="Top" />
            <ItemTemplate>
                <img src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimbuyuk").ToString()))? "manset/" + Eval("modulid") + "/" + Eval("resimbuyuk") : "admin-yok.png") %>"
                    style="width: 100%" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="baslik1" HeaderText="Başlık" ReadOnly="True">
            <HeaderStyle Width="35%" />
            <ItemStyle Width="35%" VerticalAlign="Top" />
        </asp:BoundField>
        <asp:BoundField DataField="aciklama" HeaderText="Açıklama" ReadOnly="True">
            <HeaderStyle Width="50%" />
            <ItemStyle Width="50%" VerticalAlign="Top" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderStyle Width="4%" HorizontalAlign="Center" />
            <ItemStyle Width="4%" HorizontalAlign="Center" VerticalAlign="Top" />
            <HeaderTemplate>
                <span class="toolTip" title="Manşetin bağlı olduğu modülü temsil eder.">Modül</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.GetModulIcon(Eval("modulid"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="Yayımlama Durumu">D</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateContent(Eval("aktif"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href='<%# Eval("baglanti") %>'
                    target="_blank">
                    <%# "<img class=\"toolTip left\" title='Manşete bağlı içeriği görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href='<%# baymyoStatic.Settings.PanelPath +"?go=manset&mid="+Eval("id")+"&mdl="+Eval("modulid")  %>'>
                    <%# "<img class=\"toolTip left\" title='Bu manşeti düzeltmek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
