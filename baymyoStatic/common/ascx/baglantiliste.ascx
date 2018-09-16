<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="baglantiliste.ascx.cs" Inherits="baglantiliste" %>
<h1 style="text-align: center;">Önemli Bağlantılar ve Detayları</h1>
<hr />
<div id="card-view">
    <ul>
        <asp:Repeater ID="rptListe" runat="server">
            <ItemTemplate>
                <div itemscope itemtype="http://schema.org/PostalAddress">
                    <li class="title">
                        <img class="icons" src="/common/images/icons/16/home.png" /><%# "<a itemprop=\"url\" href=\"#!\"><strong itemprop=\"name\">"+ Eval("adi")+ "</strong></a>"%>
                    </li>
                    <li>
                        <h1>
                            <img src="/common/images/icons/10/phone.png" />Telefonlar
                        </h1>
                        <span>:</span>
                        <div>
                            <%# "<strong itemprop=\"telephone\">"+ Eval("telefon1") +"</strong> / "+ Eval("telefon2") + " / " + Eval("gsm") %>
                        </div>
                        <div class="clear">
                        </div>
                    </li>
                    <li>
                        <h1>
                            <img src="/common/images/icons/10/mail.png" />Adres
                        </h1>
                        <span>:</span>
                        <div itemprop="streetAddress">
                            <%# Eval("adres")%>
                        </div>
                        <div class="clear">
                        </div>
                    </li>
                    <li>
                        <h1>
                            <img src="/common/images/icons/10/work.png" />Sehir (İL)
                        </h1>
                        <span>:</span>
                        <div>
                            <%# "<a itemprop=\"url\" href=\"" + baymyoStatic.Core.CreateLink("firmasehir", Eval("sehir"), "go") + "\"><strong itemprop=\"addressLocality\">" + Eval("sehir") + "</strong></a>"%>
                        </div>
                        <div class="clear">
                        </div>
                    </li>
                    <li>
                        <h1>
                            <img src="/common/images/icons/10/arrow_ne.png" />Web
                        </h1>
                        <span>:</span>
                        <div>
                            <a rel="nofollow" href="http://<%# Eval("web") %>" target="_blank">
                                <%# Eval("web")%></a>
                        </div>
                        <div class="clear">
                        </div>
                    </li>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<div class="clear"></div>
<asp:Panel ID="jobSet" runat="server" class="job-set">
    <div class="left">
        <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
    </div>
</asp:Panel>
<div class="clear"></div>
