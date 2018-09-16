<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="makalegoster.ascx.cs" Inherits="baymyoStatic.common.ascx.makalegoster" %>
<%@ Register Src="../../common/control/CommentControl.ascx" TagName="CommentControl"
    TagPrefix="baymyoCnt" %>
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<div class="clear">
    &nbsp;
</div>
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Yorum Yaz" />