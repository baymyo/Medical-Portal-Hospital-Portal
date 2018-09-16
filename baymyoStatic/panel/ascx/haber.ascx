<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="haber.ascx.cs" Inherits="baymyoStatic.panel.ascx.haber" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<%@ Register Src="~/common/control/DateTimeControl.ascx" TagName="DateTimeControl"
    TagPrefix="baymyoCnt" %>
<baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" runat="server" />
<br />
<br />
<div id="youtubeHTML" style="display:block">...</div>
<asp:HiddenField ID="youtubeImage" Value="" ClientIDMode="Static" runat="server" />