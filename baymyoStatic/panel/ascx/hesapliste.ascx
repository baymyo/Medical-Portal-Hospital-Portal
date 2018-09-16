<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="hesapliste.ascx.cs" Inherits="baymyoStatic.panel.ascx.hesapliste" %>
<div class="page-header">
    <h2 class="pull-left">Kullanıcı Hesapları</h2>
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
        <asp:BoundField DataField="adi" HeaderText="Adı" ReadOnly="True">
            <HeaderStyle Width="15%" />
            <ItemStyle Width="15%" />
        </asp:BoundField>
        <asp:BoundField DataField="soyadi" HeaderText="Soyadı" ReadOnly="True">
            <HeaderStyle Width="14%" />
            <ItemStyle Width="14%" />
        </asp:BoundField>
        <asp:BoundField DataField="mail" HeaderText="Mail">
            <HeaderStyle Width="30%" />
            <ItemStyle Width="30%" />
        </asp:BoundField>
        <asp:BoundField DataField="roller" HeaderText="Roller" ReadOnly="True">
            <HeaderStyle Width="20%" />
            <ItemStyle Width="20%" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Türü">
            <HeaderStyle Width="8%" HorizontalAlign="Center" />
            <ItemStyle Width="8%" HorizontalAlign="Center" />
            <ItemTemplate>
                <%# baymyoStatic.Core.GetAccountType(BAYMYO.MultiSQLClient.MConvert.NullToByte(Eval("tipi")))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="Yorum yazma durumu">Y</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateComment(Eval("yorum")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="e-Mail abonelik durumu">E</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateFollowMe(Eval("abonelik")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="Mail aktivasyon durumu">M</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateActivation(Eval("aktivasyon")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle Width="2%" HorizontalAlign="Center" />
            <ItemStyle Width="2%" HorizontalAlign="Center" />
            <HeaderTemplate>
                <span class="toolTip" title="Hesap durumu">D</span>
            </HeaderTemplate>
            <ItemTemplate>
                <%# baymyoStatic.Core.StateContent(Eval("aktif")) %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemStyle Width="3%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href='<%# baymyoStatic.Settings.PanelPath +"?go=hesap&uid="+ Eval("id")  %>'>
                    <%# "<img class=\"toolTip left\" title='" + DataBinder.Eval(Container.DataItem, "adi") + " isimli üyenin bilgilerini düzeltmek için tıkla!' src=\"" + baymyoStatic.Settings.IconsPath + "admin/edit.png\"/>"%></a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
