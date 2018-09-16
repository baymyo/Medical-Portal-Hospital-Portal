<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="haberliste.ascx.cs" Inherits="baymyoStatic.common.ascx.haberliste" %>
<div>
    <h2 class="pull-left">Haber Listesi</h2>
    <div class="form-group pull-right">
        <asp:DropDownList ID="ddlKategoriler" runat="server" CssClass="form-control pull-left" Width="200px" AutoPostBack="True"
            OnSelectedIndexChanged="ddlKategoriler_SelectedIndexChanged">
        </asp:DropDownList>
        <span class="pull-right" style="margin-top: 9px; margin-left: 10px; color: #b2b2b2; font-size: 10px;">
            <%= totalCount %></span>
    </div>
    <div class="clearfix"></div>
</div>
<asp:GridView ClientIDMode="Static" ID="dataGrid1" runat="server" GridLines="None" CssClass="table table-condensed table-responsive"
    AutoGenerateColumns="False" DataKeyNames="id" ShowHeader="false">
    <RowStyle CssClass="rowStyle" />
    <AlternatingRowStyle CssClass="alternatingRowStyle" />
    <Columns>
        <asp:TemplateField HeaderText="">
            <HeaderStyle Width="15%" HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle Width="15%" HorizontalAlign="Right" VerticalAlign="Middle" />
            <ItemTemplate>
                <img src="<%# baymyoStatic.Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "haber/" + Eval("resimurl") : "admin-yok.png") %>"
                    style="width: 100%;" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="">
            <HeaderStyle Width="85%" HorizontalAlign="Left" VerticalAlign="Middle" />
            <ItemStyle Width="85%" HorizontalAlign="Left" VerticalAlign="Top" />
            <ItemTemplate>
                <a href='<%# baymyoStatic.Core.CreateLink("haber",Eval("id"),Eval("baslik")) %>'>
                <%#"<b>"+Eval("baslik") + "</b>&nbsp;<br/>"+Eval("ozet")%></a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<p>
    <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
</p>
<div class="clearfix">&nbsp;</div>
