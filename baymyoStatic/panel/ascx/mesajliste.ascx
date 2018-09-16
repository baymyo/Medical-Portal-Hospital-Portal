<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mesajliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.mesajliste" %>
<div class="page-header">
    <h2 class="pull-left">Mesaj Listesi</h2>
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
            <asp:TemplateField HeaderText="Bilgi">
                <HeaderStyle Width="25%" />
                <ItemStyle Width="25%" />
                <ItemTemplate>
                    <h3 class="titleFormat1">
                        Ad Soyad:</h3>
                    <%# Eval("adi")%>
                    <h3 class="titleFormat1">
                        Mail:</h3>
                    <%# Eval("mail")%>
                    <h3 class="titleFormat1">
                        Telefon:</h3>
                    <%# Eval("telefon")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mesaj">
                <HeaderStyle Width="65%" />
                <ItemStyle Width="65%" />
                <ItemTemplate>
                    <h3 class="titleFormat1">
                        Konu:&nbsp;<%# Eval("konu")%></h3>
                    <var class="textFormat1">
                        <%# DataBinder.Eval(Container, "DataItem.icerik") %></var>
                    <div class="clear">
                    </div>
                    <h3 class="titleFormat2">
                        --- Yanıt ---</h3>
                    <div class="textFormat2">
                        <%# DataBinder.Eval(Container, "DataItem.yanit") %>.. .
                    </div>
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
            <asp:TemplateField>
                <HeaderStyle Width="2%" HorizontalAlign="Center" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# baymyoStatic.Core.CreateLink("mesaj",Eval("id"),Eval("konu")) %>' target="_blank">
                        <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "adi") + " isimli kullanıcının sorusunu görüntülemek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/link.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderStyle Width="2%" />
                <ItemStyle Width="2%" HorizontalAlign="Center" />
                <ItemTemplate>
                    <a href='<%# baymyoStatic.Settings.PanelPath +"?go=mesaj&mid="+ Eval("id")  %>'>
                        <%# "<img class=\"toolTip\" title='" + DataBinder.Eval(Container.DataItem, "adi") + " isimli kullanıcının sorusunu yanıtlamak için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>