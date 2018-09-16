<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="galerigoster.ascx.cs" Inherits="baymyoStatic.common.ascx.galerigoster" %>
<%@ Register Src="../../common/control/CommentControl.ascx" TagName="CommentControl"
    TagPrefix="baymyoCnt" %>
<asp:Panel ID="pnlGaleri" runat="server">
    <article id="titleBox" class="contentView" itemscope itemtype="http://schema.org/CreativeWork">
        <div class="content">
            <asp:Repeater ID="rptListe" runat="server">
                <ItemTemplate>
                    <center>
                    <a itemprop="url" href="<%= SayfaURL %>">
                        <img itemprop="image" alt="<%# Eval("aciklama") %>" src="<%# baymyoStatic.Settings.ImagesPath+((!string.IsNullOrEmpty(Eval("resimurl").ToString()))? "album/" + Eval("albumid") + "/" + Eval("resimurl") : "yok.png") %>" />
                     </a>
                     </center>
                    <div class="clear">
                        &nbsp;
                    </div>
                    <header>
                        <h1 itemprop="headline" style="margin-top: 10px; margin-bottom: 10px;">
                            <%= AlbumBilgi.Adi %></h1>
                        <div class="clear">
                        </div>
                        <h2 itemprop="description" style="margin-top: 10px; margin-bottom: 10px;">
                            <%# Eval("aciklama") %>
                        </h2>
                        <div class="clear">
                        </div>
                    </header>
                    <footer>
                    <div>
                        <strong id="newsCategory" style="padding: 5px 8px;color:#454545">
                            <%# "<a itemprop=\"url\" href=\"" + baymyoStatic.Core.CreateLink("galerikategori", KategoriBilgi.ID, KategoriBilgi.Adi) + "\"><strong itemprop=\"genre\">" + KategoriBilgi.Adi + "</strong></a>"%></strong>&nbsp;&nbsp;<time
                                pubdate="dateCreated" datetime="<%= string.Format("yyyy-MM-ddTHH:mm:ss", AlbumBilgi.KayitTarihi) %>"><%= AlbumBilgi.KayitTarihi %></time>
                    </div>
                    <div class="clear">
                    &nbsp;
                    </div>
<%--                    <b>Etiketler;</b>&nbsp;<span itemprop="keywords"><%# Etiketler %></strong>
                    <br />--%>
                    </footer>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clear">
            </div>
        </div>
    </article>
    <!-- Go to www.addthis.com/dashboard to customize your tools -->
    <div class="addthis_sharing_toolbox">
    </div>
    <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=baymyo"
        async="async"></script>
</asp:Panel>
<div class="row">
    <asp:Panel ID="jobSet" runat="server" class="job-set">
        <center>
            <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." /></center>
    </asp:Panel>
</div>
<div class="clear">
    &nbsp;
</div>
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Yorum Yaz" />
<div class="clear">
</div>
<div id="fotoGaleryList" class="twoList">
    <ul>
        <asp:Repeater ID="rptOther" runat="server">
            <ItemTemplate>
                <li class="shadow" itemscope itemtype="http://schema.org/Thing"><a itemprop="url" href="<%# baymyoStatic.Core.CreateLink("galeri",Eval("id"),Eval("adi")) %>">
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