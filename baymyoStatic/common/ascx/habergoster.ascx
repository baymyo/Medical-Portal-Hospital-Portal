<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="habergoster.ascx.cs" Inherits="baymyoStatic.common.ascx.habergoster" %>
<%@ Register Src="../../common/control/CommentControl.ascx" TagName="CommentControl"
    TagPrefix="baymyoCnt" %>
<asp:Literal ID="ltrContent" runat="server"></asp:Literal>
<div class="clear">
    &nbsp;
</div>
<baymyoCnt:CommentControl ID="CommentControl1" runat="server" FormTitle="Yorum Yaz" />