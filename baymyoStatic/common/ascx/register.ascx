<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="register.ascx.cs" Inherits="baymyoStatic.common.ascx.register" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagPrefix="oviCnt" TagName="CustomizeControl" %>
<%@ Register Src="~/common/control/DateTimeControl.ascx" TagPrefix="oviCnt" TagName="DateTimeControl" %>
<div class="row">
    <div class="col-xs-12 col-sm-9 col-md-6">
        <oviCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" RemoveVisible="false" runat="server" />
        <div class="clearfix">&nbsp;</div>
        <div class="text-left">
            <a href="/?go=remember&r=sifre">Şifremi Unuttum?</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="/?go=login">Zaten Üyeyim!</a>
        </div>
        <div class="clearfix">&nbsp;</div>
    </div>
</div>
<div class="clearfix">&nbsp;</div>
