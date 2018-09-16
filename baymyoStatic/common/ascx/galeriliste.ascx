<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="galeriliste.ascx.cs" Inherits="baymyoStatic.common.ascx.galeriliste" %>
<div id="fotoGaleryList" class="threeList">
    <ul>
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li><a itemprop="url" href="<%# baymyoStatic.Core.CreateLink("galeri",Eval("id"),Eval("adi")) %>">
                    <figure>
                        <img alt="<%# Eval("adi") %>" title="<%# Eval("adi") %>" src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "album/" + Eval("id") + "/" + Eval("resimurl") : "yok.png")%>" itemprop="image" data-original="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "album/" + Eval("id") + "/" + Eval("resimurl") : "yok.png")%>">
                        <span><span class="galeri"></span></span>
                        <figcaption class="gradian" itemprop="name"><%# Eval("adi")%></figcaption>
                    </figure>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
<div class="row">
    <div class="clear"></div>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <center>
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
        </center>
    </asp:Panel>
</div>
<div class="clear"></div>
