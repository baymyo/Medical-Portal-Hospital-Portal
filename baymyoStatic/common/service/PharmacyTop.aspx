<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PharmacyTop.aspx.cs" Inherits="baymyoStatic.common.service.PharmacyTop" %>

<%@ Register Assembly="BAYMYO.AdRotatorExtention" Namespace="BAYMYO.AdRotatorExtention"
    TagPrefix="baymyoAdv" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tüm İllerin Nöbetçi Eczaneler Listesi</title>
    <meta name="description" content="Tüm illerin bugün nöbetçi eczanelerini görüntülemek için buraya tıklayınız." />
    <meta name="keywords" content="Adana,Adıyaman,Afyon,Ağrı,Amasya,Ankara,Antalya,Artvin,Aydın,Balıkesir,Bilecik,Bingöl,Bitlis,Bolu,Burdur,Bursa,Çanakkale,Çankırı,Çorum,Denizli,Diyarbakır,Edirne,Elazığ,Erzincan,Erzurum,Eskişehir,Gaziantep,Giresun,Gümüşhane,Hakkari,Hatay,Isparta,İçel,İstanbul,İzmir,Kars,Kastamonu,Kayseri,Kırklareli,Kırşehir,Kocaeli,Konya,Kütahya,Malatya,Manisa,Kahramanmaraş,Mardin,Muğla,Muş,Nevşehir,Niğde,Ordu,Rize,Sakarya,Samsun,Siirt,Sinop,Sivas,Tekirdağ,Tokat,Trabzon,Tunceli,Şanlıurfa,Uşak,Van,Yozgat,Zonguldak,Aksaray,Bayburt,Karaman,Kırıkkale,Batman,Şırnak,Bartın,Ardahan,Iğdır,Yalova,Karabük,Kilis,Osmaniye,Düzce" />
    <meta name="ROBOTS" content="index,follow">
    <meta http-equiv="EXPIRES" content="18-06-2018">
    <meta http-equiv="CONTENT-TYPE" content="text/html; charset=x-mac-turkish">
    <meta http-equiv="CONTENT-TYPE" content="text/html; charset=utf-8">
    <meta http-equiv="CONTENT-LANGUAGE" content="tr">
    <meta http-equiv="VW96.OBJECT TYPE" content="Other">
    <link id="page_favicon" href="~/images/favicon.ico" rel="icon" type="image/x-icon" />
    <link id="shortcut_favicon" href="~/images/favicon.ico" rel="shortcut icon" />
    <style type="text/css">
        html { background: #c60000; }
        body { background: #c60000; margin: 0px !important; padding: 0px !important; }
        .menu { display: block; text-align: center; padding: 2px !important; margin: 0px !important; width: 99%; }
        ol, ul { list-style: none; margin: 0px !important; padding: 0px !important; width: 99%; }
        .nobet li { margin: 2px 3px; padding: 2px; float: left; }
        .nobet li a { font-size: 11px; font-family: Arial; display: block; color: #fff; text-decoration: none; }
        .nobet li a:hover { color: #F5F5F5; text-decoration: underline; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="menu">
        <ul class="nobet">
            <li><a rel="dofollow" target="_parent" href="/" target="_self">[Ana Sayfa]</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/1/adana-nobetcileri.html">Adana</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/2/adiyaman-nobetcileri.html">Adıyaman</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/3/afyon-nobetcileri.html">Afyon</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/4/agri-nobetcileri.html">Ağrı</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/5/amasya-nobetcileri.html">Amasya</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/6/ankara-nobetcileri.html">Ankara</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/7/antalya-nobetcileri.html">Antalya</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/8/artvin-nobetcileri.html">Artvin</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/9/aydin-nobetcileri.html">Aydın</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/10/balikesir-nobetcileri.html">Balıkesir</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/11/bilecik-nobetcileri.html">Bilecik</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/12/bingol-nobetcileri.html">Bingöl</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/13/bitlis-nobetcileri.html">Bitlis</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/14/bolu-nobetcileri.html">Bolu</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/15/burdur-nobetcileri.html">Burdur</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/16/bursa-nobetcileri.html">Bursa</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/17/canakkale-nobetcileri.html">Çanakkale</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/18/cankiri-nobetcileri.html">Çankırı</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/19/corum-nobetcileri.html">Çorum</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/20/denizli-nobetcileri.html">Denizli</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/21/diyarbakır-nobetcileri.html">Diyarbakır</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/22/edirne-nobetcileri.html">Edirne</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/23/elazig-nobetcileri.html">Elazığ</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/24/erzincan-nobetcileri.html">Erzincan</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/25/erzurum-nobetcileri.html">Erzurum</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/26/eskisehir-nobetcileri.html">Eskişehir</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/27/gaziantep-nobetcileri.html">Gaziantep</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/28/giresun-nobetcileri.html">Giresun</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/29/gumushane-nobetcileri.html">Gümüşhane</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/30/hakkari-nobetcileri.html">Hakkari</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/31/hatay-nobetcileri.html">Hatay</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/32/isparta-nobetcileri.html">Isparta</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/33/mersin-nobetcileri.html">İçel</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/34/istanbul-nobetcileri.html">İstanbul</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/35/izmir-nobetcileri.html">İzmir</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/36/kars-nobetcileri.html">Kars</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/37/kastamonu-nobetcileri.html">Kastamonu</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/38/kayseri-nobetcileri.html">Kayseri</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/39/kirklareli-nobetcileri.html">Kırklareli</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/40/kirsehir-nobetcileri.html">Kırşehir</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/41/kocaeli-nobetcileri.html">Kocaeli</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/42/konya-nobetcileri.html">Konya</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/43/kutahya-nobetcileri.html">Kütahya</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/44/malatya-nobetcileri.html">Malatya</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/45/manisa-nobetcileri.html">Manisa</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/46/kahramanmaras-nobetcileri.html">Kahramanmaraş</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/47/mardin-nobetcileri.html">Mardin</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/48/mugla-nobetcileri.html">Muğla</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/49/mus-nobetcileri.html"> Muş</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/50/nevsehir-nobetcileri.html">Nevşehir</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/51/nigde-nobetcileri.html">Niğde</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/52/ordu-nobetcileri.html">Ordu</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/53/rize-nobetcileri.html">Rize</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/54/sakarya-nobetcileri.html">Sakarya</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/55/samsun-nobetcileri.html">Samsun</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/56/siirt-nobetcileri.html">Siirt</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/57/sinop-nobetcileri.html">Sinop</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/58/sivas-nobetcileri.html">Sivas</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/59/tekirdag-nobetcileri.html">Tekirdağ</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/60/tokat-nobetcileri.html">Tokat</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/61/trabzon-nobetcileri.html">Trabzon</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/62/tunceli-nobetcileri.html">Tunceli</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/63/sanliurfa-nobetcileri.html">Şanlıurfa</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/64/uşak-nobetcileri.html">Uşak</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/65/van-nobetcileri.html">Van</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/66/yozgat-nobetcileri.html">Yozgat</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/67/zonguldak-nobetcileri.html">Zonguldak</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/68/aksaray-nobetcileri.html">Aksaray</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/69/bayburt-nobetcileri.html">Bayburt</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/70/karaman-nobetcileri.html">Karaman</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/71/kirikkale-nobetcileri.html">Kırıkkale</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/72/batman-nobetcileri.html">Batman</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/73/sirnak-nobetcileri.html">Şırnak</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/74/bartin-nobetcileri.html">Bartın</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/75/ardahan-nobetcileri.html">Ardahan</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/76/igdir-nobetcileri.html">Iğdır</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/77/yalova-nobetcileri.html">Yalova</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/78/karabuk-nobetcileri.html">Karabük</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/79/kilis-nobetcileri.html">Kilis</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/80/osmaniye-nobetcileri.html">Osmaniye</a></li>
            <li><a rel="nofollow" target="_parent" href="/eczane/81/düzce-nobetcileri.html">Düzce</a></li>
        </ul>
    </div>
    <div style="display: block; clear: both">
    </div>
    <center>
        <asp:Literal ID="ltrAdv790_Top" runat="server" />
        <baymyoAdv:AdRotatorExtention ID="adv790_top" runat="server" Target="_blank" />
    </center>
    <div style="display: block; clear: both">
        &nbsp;
    </div>
    <div style="display: block; clear: both">
        <asp:Literal ID="ltrGoogleAnalytics" runat="server" />
    </div>
    </form>
</body>
</html>
