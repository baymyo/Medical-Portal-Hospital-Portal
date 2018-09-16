<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="index.ascx.cs" Inherits="baymyoStatic.common.ascx.index" %>
<script type="text/javascript">
    $(document).ready(function () {
        flashList();
        doctorList();
        newsList();
        healtList();
        linkList();
    });
</script>
<!--slider-section-->
<div class="main-slider">
    <div id="flashList" class="slider-inner owl-carousel">
    </div>
    <!--slider-inner end-->
</div>
<!--slider-section end-->
<!--main-group-->
<main id="main-content" class="main-group home-page">
    <div class="container">
        <div class="row first-row">
            <div class="col-md-6">
                <!--news-section-->
                <section class="news-section wow fadeInUp">
                    <div class="news-header">
                        <span class="icon-wrap">
                            <i class="fa fa-file-text-o"></i>
                        </span>
                        <h3 class="title">HABERLER &amp; DUYURULAR</h3>
                    </div>
                    <div id="newsList" class="news-content owl-carousel">
                    </div>
                </section>
            </div>
            <div class="col-md-6">
                <!--corporate-section-->
                <section class="corporate-section wow pulse">
                    <div class="corporate-content">
                        <h3 class="title">Kurumsal</h3>
                        <p>Tıp Merkezimizde, sterilizasyona büyük önem vermekte ve sizlere sağlanabilecek maksimum hijyenik ortamı sunmanın gayretindeyiz.</p>
                        <p>Sterilizasyonun çağdaş bir sağlık merkezinin en önemli unsuru olduğu bilinciyle hareket edilen Tıp Merkezimizde; </p>
                        <a href="/sayfa/2/kurumsal.html" class="btn btn-default">devam et</a>
                    </div>
                    <div class="logo-box hidden-md hidden-sm hidden-xs">
                        <img src="/common/images/logo-red.png" alt="">
                    </div>
                </section>
                <div class="row">
                    <div class="col-md-6">
                        <!--call-us-section-->
                        <section class="call-us-section wow bounceInUp" data-wow-duration="1s">
                            <div class="call-us-content">
                                <i class="fa fa-headphones"></i>
                                <h3>Poliklinik Randevusu<br>
                                    ALMAK İÇİN ARAYIN</h3>
                                <span>0326 643 33 01</span>
                                <a href="#!" class="btn btn-default">Hemen ara</a>
                            </div>
                        </section>
                    </div>
                    <div class="col-md-6">
                        <!--our-team-section-->
                        <section class="our-team-section wow bounceInUp" data-wow-duration="1.2s">
                            <h3 class="title">Ekibimiz</h3>
                            <div id="doctorList" class="our-team-content owl-carousel">
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
        <div class="row second-row">
            <div class="col-md-8">
                <!--health-section-->
                <section class="health-section wow fadeInUp">
                    <h3 class="title">Sağlık Köşesi</h3>
                    <div id="healthList" class="health-articles-group owl-carousel">
                    </div>
                </section>
            </div>
            <div class="col-md-4">
                <!--important-links-section-->
                <section class="important-links-section wow shake">
                    <h3>Önemli Linkler</h3>
                    <span class="divider"></span>
                    <div id="baglantiList" class="links-group owl-carousel">
                    </div>
                </section>
            </div>
        </div>
    </div>
    <!--ask-us-section-->
    <section class="ask-us-section wow fadeInUp">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="ask-us-inner">
                        <img class="woman-doctor" src="/common/images/women.png" alt="" />
                        <h2><span>Medica Arsuz</span> Doktorunuza Sorun</h2>
                        <p>Butona tıklayarak ilgili konuda doktora mail atabilirsiniz.</p>
                        <a href="/doktor" class="btn btn-primary btn-lg">Doktora sor</a>
                    </div>
                </div>
            </div>
        </div>
    </section>
</main>