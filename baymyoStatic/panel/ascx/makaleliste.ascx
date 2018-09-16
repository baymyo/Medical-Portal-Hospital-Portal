<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="makaleliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.makaleliste" %>
<div class="page-header">
    <h2 class="pull-left">Makale Listesi</h2>
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
        <asp:TemplateField HeaderText="Resim">
            <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemTemplate>
                <img src="<%# baymyoStatic.Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "makale/" + Eval("resimurl") : "admin-yok.png") %>"
                    style="width: 100%" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="baslik" HeaderText="Başlık" ReadOnly="True">
            <HeaderStyle Width="30%" />
            <ItemStyle Width="30%" />
        </asp:BoundField>
        <asp:BoundField DataField="ozet" HeaderText="Özet" ReadOnly="True">
            <HeaderStyle Width="51%" />
            <ItemStyle Width="51%" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <ItemTemplate>
                <div class="vertical-img-set">
                    <a href='<%# baymyoStatic.Core.CreateLink("makale",Eval("id"),Eval("baslik")) %>' target="_blank">
                        <%# "<img class=\"toolTip left\" title='Makaleyi görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
                    <a href='<%# baymyoStatic.Settings.PanelPath +"?go=makale&mklid="+ Eval("id")  %>'>
                        <%# "<img class=\"toolTip\" title='Makaleyi düzeltmek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
                    <a href='<%# baymyoStatic.Settings.PanelPath +"?go=manset&mdl=makale&mcid="+ Eval("id")  %>'>
                        <%# "<img class=\"toolTip\" title='Makaleyi manşete eklemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/manset.png\"/>"%></a>
                    <%# baymyoStatic.Core.StateAdmin(Eval("yoneticionay")) + baymyoStatic.Core.StateContent(Eval("aktif")) %>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
