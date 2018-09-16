<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="videogoster.ascx.cs" Inherits="baymyoStatic.common.ascx.videogoster" %>
<%@ Register Src="../../common/control/CommentControl.ascx" TagName="CommentControl"
    TagPrefix="baymyoCnt" %>
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<div class="clear">
</div>
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Yorum Yaz" />
<div class="clear">
</div>
<div id="videoGaleryList" class="twoListIMG">
    <ul>
        <asp:Repeater ID="rptOther" runat="server">
            <ItemTemplate>
                <li class="shadow">
                    <a itemprop="url" href="<%# baymyoStatic.Core.CreateLink("video",Eval("id"),Eval("baslik")) %>">
                        <figure>
                            <img alt="<%# Eval("baslik") %>" title="<%# Eval("baslik") %>" itemprop="image" data-original="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "video/" + Eval("resimurl") : "yok.png")%>"
                                src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "video/" + Eval("resimurl") : "yok.png")%>">
                            <span><span class="video"></span></span>
                            <figcaption class="gradian" itemprop="name">
                                <%# Eval("baslik") %>
                            </figcaption>
                        </figure>
                    </a>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
