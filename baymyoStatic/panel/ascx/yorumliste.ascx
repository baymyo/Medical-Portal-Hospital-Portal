<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="yorumliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.yorumliste" %>
<div class="page-header">
    <h2 class="pull-left">Yorum Listesi</h2>
    <div class="form-group pull-right">
        <asp:Button ID="btnSaveChanges" runat="server" Text="Seçilenlere Uygula" CssClass="btn btn-warning pull-left"
            OnClick="btnSaveChanges_Click" />
        <asp:DropDownList ID="ddlIslemler" runat="server" CssClass="form-control pull-right" Width="200px">
        </asp:DropDownList>
    </div>
    <div class="clearfix"></div>
</div>
<asp:GridView ClientIDMode="Static" ID="dataGrid1" runat="server" GridLines="None" CssClass="table table-condensed table-responsive"
    AutoGenerateColumns="False" DataKeyNames="id,modulid,icerikid">
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
                <ItemStyle Width="2%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="ip" HeaderText="IP" SortExpression="ip">
                <HeaderStyle Width="14%" />
                <ItemStyle Width="14%" />
            </asp:BoundField>
            <asp:BoundField DataField="mail" HeaderText="Mail" SortExpression="mail">
                <HeaderStyle Width="22%" />
                <ItemStyle Width="22%" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderStyle Width="56%" HorizontalAlign="Center" />
                <ItemStyle Width="56%" />
                <HeaderTemplate>
                    <span class="toolTip" title="Yazılan Yorumlar">Yazan ve Yorum İçeriği</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# "<b>" + Eval("adi") + "</b><br />" + Eval("icerik") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="kayittarihi" HeaderText="Tarih" SortExpression="kayittarihi">
                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# baymyoStatic.Core.CreateLink(Eval("modulid").ToString(),Eval("icerikid"),Eval("adi")) %>'
                        target="_blank">
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "adi") + " isimli kişinin yorum yaptığı içeriği görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
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
                <HeaderTemplate>
                    <span class="toolTip" title="Yönetici Onayı">A</span>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# baymyoStatic.Core.StateAdmin(Eval("yoneticionay"))%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
