<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="remember.ascx.cs" Inherits="baymyoStatic.common.ascx.remember" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl" TagPrefix="oviCnt" %>
<div class="row">
    <div class="col-xs-12 col-sm-6 col-md-5">
        <oviCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" SubmitText="Gönder" RemoveVisible="false" runat="server" />
        <div class="clearfix">&nbsp;</div>
        <div class="text-left">
            <a href="/?go=login">Oturum Aç!</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="/?go=register&type=2">Yeni Kayıt Ol!</a>
        </div>
    </div>
</div>
<div class="clearfix">&nbsp;</div>
