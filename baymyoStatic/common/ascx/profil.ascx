<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="profil.ascx.cs" Inherits="baymyoStatic.common.ascx.profil" %>
<%@ Register Src="~/common/ascx/contact.ascx" TagPrefix="baymyoCnt" TagName="contact" %>
<h1 style="text-align: center;"><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h1>
<hr />
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<asp:Panel ID="lastArticle" runat="server" CssClass="row" Visible="false">
    <div class="page-header">
        <h2 class="pull-left">Son Makaleleri</h2>
    </div>
    <div class="container" style="text-align: justify">
        <asp:Literal ID="ltrMessage" runat="server" Visible="false" />
        <ul class="single-list">
            <li class="first"></li>
            <asp:Repeater ID="rptMakaleler" runat="server">
                <ItemTemplate>
                    <li><a href="<%# baymyoStatic.Core.CreateLink("makale",Eval("id"),Eval("baslik")) %>">
                        <img title="<%# Eval("baslik") %>" class="toolTip images left" src="<%# baymyoStatic.Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "makale/"+Eval("resimurl") : "makale/yok.png") %>" /></a>
                        <div class="text left">
                            <h1 class="title">
                                <a href="<%# baymyoStatic.Core.CreateLink("makale",Eval("id"),Eval("baslik")) %>">
                                    <%# Eval("baslik") %></a></h1>
                            <span class="description">
                                <%# Eval("ozet")%></span>
                            <div class="info">
                                <span class="toolTip date left" title="<%# Eval("kayittarihi") %>">
                                    <%# baymyoStatic.Core.DateFormating(Eval("kayittarihi"))%></span>
                            </div>
                        </div>
                        <div class="clear">
                        </div>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Panel>
<div class="clear">
    &nbsp;
</div>
<baymyoCnt:contact runat="server" ID="contact1" />
