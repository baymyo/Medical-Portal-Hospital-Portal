<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="baymyoStatic.common.ascx.loginCnt" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<div class="row">
    <div class="col-xs-12 col-sm-6 col-md-5">
        <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="true" FormTitle="Kullanıcı Girişi"
            SubmitText="Giriş Yap" RemoveVisible="false" runat="server" />
        <div class="clearfix">&nbsp;</div>
        <div class="text-left">
            <a href="/?go=remember&r=sifre">Şifremi Unuttum?</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="/?go=register&type=2">Yeni Kayıt Ol!</a>
        </div>
        <div class="clearfix">&nbsp;</div>
    </div>
</div>
<div class="clearfix">&nbsp;</div>

