<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentControl.ascx.cs" Inherits="baymyoStatic.common.control.CommentControl" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<asp:Panel runat="server" ClientIDMode="Static" id="siteComments" class="shadow" style="background-color: #fff; margin-bottom: 10px; padding: 5px 5px">
    <div id="pageLink" style="display: block; border-bottom: 1px solid #f0f0f0">
        <a id="0" href="#shared">
            <h3 style="float: left; padding: 5px 10px; margin-right: 15px; display: block; border-bottom: 2px solid #CF0A0A">
                <strong>SİTE YORUMLARI</strong></h3>
        </a><a id="1" href="#shared">
            <h3 style="float: left; padding: 5px 10px; margin-right: 15px; display: block; border-bottom: 2px solid #CF0A0A">
                <strong>FACEBOOK YORUMLARI</strong></h3>
        </a>
        <div class="clear">
        </div>
    </div>
    <div class="clear">
    </div>
    <div id="pages" style="float: left; display: block; width: 100%; padding: 5px 5px">
        <div style="display: block; width: 100%">
            <div id="comment">
                <asp:Literal ID="ltrOpenComment" runat="server" Visible="false" />
                <div id="writeBox" class="writeBox" runat="server">
                    <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" FormTitle=""
                        SubmitText="Gönder" RemoveVisible="false" runat="server" />
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="modul-main-box">
                <asp:Repeater ID="rptComments" runat="server" OnItemCommand="rptComments_ItemCommand">
                    <ItemTemplate>
                        <div class="comments">
                            <div class="<%# baymyoStatic.Core.GetAccountTypeName(Eval("tipi")) %>">
                                <span class="name">
                                    <%# Eval("adi") %></span><span class="job" <%#  (!IsCommandActive) ? "style=\"display:none;visibility:hidden;\"" : "" %>>
                                        <asp:ImageButton CssClass="toolTip" ToolTip="Yorumu pasif etmek için tıkla" ImageUrl="~/images/icons/12/content-1.png"
                                            runat="server" ID="imgActive" CommandName="pasif" Visible='<%#  (IsCommandActive & BAYMYO.UI.Converts.NullToBool(Eval("aktif"))) %>' /><asp:ImageButton
                                                CssClass="toolTip" ToolTip="Yorumu aktif etmek için tıkla" ImageUrl="~/images/icons/12/content-0.png"
                                                runat="server" ID="imgPassive" CommandName="aktif" Visible='<%#  (IsCommandActive & !BAYMYO.UI.Converts.NullToBool(Eval("aktif"))) %>' /><asp:ImageButton
                                                    CssClass="toolTip" ToolTip="Yorumu kaldırmak için tıkla!" ImageUrl="~/images/icons/12/remove.png"
                                                    runat="server" ID="imgRemove" CommandName="remove" Visible="<%#  IsCommandActive %>" />
                                    </span><span class="toolTip date" title="<%# Eval("kayittarihi") %>">
                                        <%# baymyoStatic.Core.DateFormating(Eval("kayittarihi"))%></span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="content">
                                <%# (!string.IsNullOrEmpty(Eval("resimurl").ToString()) ? "<a href=\"" + baymyoStatic.Settings.VirtualPath + Eval("url") + "\" class=\"toolTip\" alt=\"" + Eval("adi") + "\" title=\"Sayfasını görüntülemek için tıkla.\"><img src=\"" + baymyoStatic.Settings.ImagesPath + "profil/" + Eval("resimurl") + "\"/></a>" : "") + Eval("icerik")%>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("id") %>' />
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clear">
                </div>
                <div>
                    <asp:Literal ID="pageNumberLiteral" runat="server" Text="" />
                </div>
                <%--<script type="text/javascript">
        $("#comment .job-set span.writeOpen").click(function () {
            $("#comment .writeBox").show("slow");
        });
    </script>--%>
            </div>
        </div>
        <div style="display: block; width: 100%">
            <asp:Literal ID="facebookComment" runat="server" />
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Panel>
<script type="text/javascript">
    (function ($) {
        $('#siteComments > #pages > div').hide();
        $('#siteComments > #pages > div:eq(0)').show();
        $('#siteComments > #pageLink > a').click(function () {
            $('#siteComments > #pages > div').hide();
            $('#siteComments > #pages > div:eq(' + this.id + ')').fadeIn(1000);
        });
    })(jQuery);
</script>
