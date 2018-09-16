<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="myaccount.ascx.cs" Inherits="baymyoStatic.common.ascx.myaccount" %>
<%@ Register Src="~/common/control/CustomizeControl.ascx" TagName="CustomizeControl"
    TagPrefix="baymyoCnt" %>
<%@ Register Src="~/common/control/DateTimeControl.ascx" TagName="DateTimeControl"
    TagPrefix="baymyoCnt" %>
<div class="row">
    <div class="col-xs-12 col-sm-6 col-md-6">
        <baymyoCnt:CustomizeControl ID="CustomizeControl1" FormTitleVisible="false" runat="server" />
        <div class="clearfix">&nbsp;</div>
    </div>
    <div class="col-xs-12 col-sm-6 col-md-6">
    </div>
</div>
<div class="clearfix">&nbsp;</div>
