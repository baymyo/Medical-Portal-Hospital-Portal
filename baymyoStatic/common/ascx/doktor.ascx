<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="doktor.ascx.cs" Inherits="baymyoStatic.common.ascx.doktor" %>
<h1 style="text-align: center;">Medica Arsuz Doktorlarımız</h1>
<hr />
<div class="row">
    <asp:Repeater ID="rptListe" runat="server">
        <ItemTemplate>
            <div class="col-lg-3 col-sm-6 text-center">
                <img class="img-circle img-responsive img-center"
                    src="<%# baymyoStatic.Settings.ImagesPath + ((!string.IsNullOrEmpty(Eval("resimurl").ToString())) ? "profil/" + Eval("resimurl") : "http://placehold.it/200x200") %>"
                    alt="">
                <br/>
                <a href="#">
                    <h3><%# Eval("adi") + " " + Eval("soyadi")%>
                    <br/><small><%# Eval("meslek") %></small>
                    </h3>
                </a>
                <div class="profile-userbuttons">
                    <a href="<%# baymyoStatic.Settings.VirtualPath + Eval("url") %>" class="btn btn-success btn-sm">Bilgiler</a>
                    <a href="<%# baymyoStatic.Core.CreateLink("iletisim", Eval("url"), "go") %>" class="btn btn-danger btn-sm">Mesaj</a>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="clearfix"></div>
    <p>
        <asp:Literal ID="pageNumberLiteral" runat="server" Text="..." />
    </p>
    <div class="clearfix">&nbsp;</div>
</div>
