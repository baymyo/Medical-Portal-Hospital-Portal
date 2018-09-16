<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="baglantiliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.baglantiliste" %>
<div class="page-header">
    <h2 class="pull-left">Bağlantı Listesi</h2>
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
                <HeaderStyle Width="10%" HorizontalAlign="Left" VerticalAlign="Middle" />
                <ItemStyle Width="10%" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemTemplate>
                    <img src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "firma/" + Eval("resimurl") : "admin-yok.png") %>"
                        style="width: 100%" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="adi" HeaderText="Başlık" ReadOnly="True">
                <HeaderStyle Width="26%" />
                <ItemStyle Width="26%" />
            </asp:BoundField>
            <asp:BoundField DataField="telefon1" HeaderText="Telefon" ReadOnly="True">
                <HeaderStyle Width="15%" />
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="gsm" HeaderText="GSM" ReadOnly="True">
                <HeaderStyle Width="15%" />
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="sehir" HeaderText="Şehir" ReadOnly="True">
                <HeaderStyle Width="12%" />
                <ItemStyle Width="12%" />
            </asp:BoundField>
            <asp:BoundField DataField="kayittarihi" HeaderText="Tarih" ReadOnly="True">
                <HeaderStyle Width="12%" />
                <ItemStyle Width="12%" />
            </asp:BoundField>
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
                <HeaderTemplate>
                    <span class="toolTip" title="Yönetici Onayı">A</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# baymyoStatic.Core.StateAdmin(Eval("yoneticionay"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# baymyoStatic.Core.CreateLink("firma",Eval("id"),Eval("adi")) %>' target="_blank">
                        <%# "<img class=\"toolTip left\" title='Bu bağlantı görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# baymyoStatic.Settings.PanelPath +"?go=baglanti&fid="+ Eval("id")  %>'>
                        <%# "<img class=\"toolTip left\" title='Bu bağlantı düzeltmek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
