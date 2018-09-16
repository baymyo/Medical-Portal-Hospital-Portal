<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PharmacyOnDuty.aspx.cs" Inherits="baymyoStatic.common.service.PharmacyOnDuty" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html id="bodyHtml" xmlns="http://www.w3.org/1999/xhtml" runat="server">
<head runat="server">
    <title>
        <%= "Tüm İller İçin Nöbetçi Eczaneler - " + baymyoStatic.Settings.Site.Title %></title>
    <meta name="description" content="Tüm illerin bugün nöbetçi eczanelerini görüntülemek için buraya tıklayınız." />
    <meta name="keywords" content="Adana,Adıyaman,Afyon,Ağrı,Amasya,Ankara,Antalya,Artvin,Aydın,Balıkesir,Bilecik,Bingöl,Bitlis,Bolu,Burdur,Bursa,Çanakkale,Çankırı,Çorum,Denizli,Diyarbakır,Edirne,Elazığ,Erzincan,Erzurum,Eskişehir,Gaziantep,Giresun,Gümüşhane,Hakkari,Hatay,Isparta,İçel,İstanbul,İzmir,Kars,Kastamonu,Kayseri,Kırklareli,Kırşehir,Kocaeli,Konya,Kütahya,Malatya,Manisa,Kahramanmaraş,Mardin,Muğla,Muş,Nevşehir,Niğde,Ordu,Rize,Sakarya,Samsun,Siirt,Sinop,Sivas,Tekirdağ,Tokat,Trabzon,Tunceli,Şanlıurfa,Uşak,Van,Yozgat,Zonguldak,Aksaray,Bayburt,Karaman,Kırıkkale,Batman,Şırnak,Bartın,Ardahan,Iğdır,Yalova,Karabük,Kilis,Osmaniye,Düzce" />
    <meta name="AUTHOR" content="baymyo">
    <meta name="OWNER" content="info@baymyo.com">
    <meta name="ROBOTS" content="nofollow">
    <meta http-equiv="EXPIRES" content="18-06-2018">
    <meta http-equiv="CONTENT-TYPE" content="text/html; charset=x-mac-turkish">
    <meta http-equiv="CONTENT-TYPE" content="text/html; charset=utf-8">
    <meta http-equiv="CONTENT-LANGUAGE" content="tr">
    <meta http-equiv="VW96.OBJECT TYPE" content="Other">
    <link id="page_favicon" href="~/images/favicon.ico" rel="icon" type="image/x-icon" />
    <link id="shortcut_favicon" href="~/images/favicon.ico" rel="shortcut icon" />
</head>
<frameset rows="<%= heigth %>,*">
    <frame src="/common/service/PharmacyTop.aspx" scrolling="no" noresize="noresize" frameborder="0" marginwidth="0" marginheight="0" />
    <frame src="<%= Core.GetNobetciEczane(BAYMYO.UI.Converts.NullToByte(Request.QueryString["plaka"])) %>"
        frameborder="0" marginwidth="0" marginheight="0" />
</frameset>
</html>