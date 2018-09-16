<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="videoliste.ascx.cs" Inherits="baymyoStatic.common.ascx.videoliste" %>
<div id="videoGaleryList" class="threeList">
    <ul>
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <li><a itemprop="url" href="<%# baymyoStatic.Core.CreateLink("video",Eval("id"),Eval("baslik")) %>">
                    <figure>
                        <img alt="<%# Eval("baslik") %>" title="<%# Eval("baslik") %>" itemprop="image" data-original="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "video/" + Eval("resimurl") : "yok.png")%>"
                            src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "video/" + Eval("resimurl") : "yok.png")%>">
                        <span><span class="video"></span></span>
                        <figcaption class="gradian" itemprop="name"><%# Eval("baslik") %></figcaption>
                    </figure>
                </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
<div class="row">
    <div class="clear">
    </div>
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <center>
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
        </center>
    </asp:Panel>
</div>
<div class="clear">
</div>
