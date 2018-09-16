<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="habereditorliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.habereditorliste" %>
<div class="page-header">
    <h2 class="pull-left">Haber Editör Listesi</h2>
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
    <asp:Literal ID="infoLiteral" runat="server" Text="..." />
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
            <ItemStyle Width="15%" HorizontalAlign="Right" VerticalAlign="Middle" />
            <ItemTemplate>
                <img src="<%# baymyoStatic.Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "haber/" + Eval("resimurl") : "admin-yok.png") %>"
                    style="width: 100%;" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Başlık">
            <HeaderStyle Width="30%" HorizontalAlign="Left" VerticalAlign="Middle" />
            <ItemStyle Width="30%" HorizontalAlign="Left" VerticalAlign="Top" />
            <ItemTemplate>
                <%#"<b>"+Eval("baslik") + "</b>&nbsp;"+ baymyoStatic.Core.StatePhotoGaleri(Eval("galeri")) + "&nbsp;" + baymyoStatic.Core.StateVideoGaleri(Eval("video"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="ozet" HeaderText="Özet" ReadOnly="True">
            <HeaderStyle Width="51%" HorizontalAlign="Left" VerticalAlign="Middle" />
            <ItemStyle Width="51%" HorizontalAlign="Left" VerticalAlign="Top" />
        </asp:BoundField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <ItemTemplate>
                <div class="vertical-img-set">
                    <a href='<%# baymyoStatic.Core.CreateLink("haber",Eval("id"),Eval("baslik")) %>'>
                        <%# "<img class=\"toolTip left\" title='Haberi görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
                    <a href='<%# baymyoStatic.Settings.PanelPath +"?go=haber&hid="+ Eval("id")  %>'>
                        <%# "<img class=\"toolTip\" title='Haberi düzeltmek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
                    <a href='<%# baymyoStatic.Settings.PanelPath +"?go=manset&mdl=haber&mcid="+ Eval("id")  %>'>
                        <%# "<img class=\"toolTip\" title='Haberi manşete eklemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/manset.png\"/>"%></a>
                    <%# baymyoStatic.Core.StateContent(Eval("aktif"))%>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
