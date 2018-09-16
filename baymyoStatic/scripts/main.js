$(function () {
    $('ul.nav li.dropdown').hover(function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(0).fadeIn(200);
    }, function () {
        $(this).find('.dropdown-menu').stop(true, true).delay(200).fadeOut(100);
    });

    new WOW().init();
});
function flashList() {
    $.ajax({
        type: "GET",
        url: "/common/json/manset1.js",
        dataType: "json",
        async: true,
        cache: true,
        timeout: 50000,
        contentType: "application/json",
        success: function (data) {
            var slideName = '.main-slider #doctorList';
            $(slideName).text("");
            var slideItem = "";
            $.each(data, function (i, item) {
                slideItem = '<div class="slider-item">';
                slideItem += '<div class="container">';
                slideItem += '<div class="slider-content">';
                slideItem += '<h1 class="heading">' + item.Baslik1 + '</h1>';
                slideItem += '<h2 class="heading">' + item.Baslik2 + '</h2>';
                slideItem += '<p>' + item.Aciklama + '</p>';
                if (item.Dugme != "")
                    slideItem += '<a href="' + item.Baglanti + '" class="btn btn-danger btn-lg">' + item.Dugme + '</a>';
                slideItem += '</div>';
                slideItem += '<img class="hasta-doktor hidden-md hidden-sm hidden-xs" src="/common/images/manset/' + item.ModulID + '/' + item.ResimBuyuk + '" alt="">';
                slideItem += '</div>';
                slideItem += '</div>';
                $(slideName).append(slideItem);
                slideItem = "";
            });

            $('.slider-inner').owlCarousel({
                loop: true,
                margin: 30,
                nav: true,
                items: 1,
                navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
                autoplayHoverPause: true,
                autoplay: true,
                autoplay: true,
                autoplayTimeout: 5000,
                autoplaySpeed: 1500,
            });
        },
        error: function (xhr, status, error) {
            /*alert(xhr.status);*/
        }
    });
}
function doctorList() {
    $.ajax({
        type: "GET",
        url: "/common/json/doktorlar.js",
        dataType: "json",
        async: true,
        cache: true,
        timeout: 50000,
        contentType: "application/json",
        success: function (data) {
            var slideName = '.our-team-section #doctorList';
            $(slideName).text("");
            $.each(data, function (i, item) {
                $(slideName).append('<div class="team-item"><img src="/common/images/profil/' + item.Resim + '" alt=""><span class="person-desc"><a style="color:#fff;" href="' + item.Link + '">' + item.Baslik + '</a></span></div>');
            });

            $('.our-team-content').owlCarousel({
                loop: true,
                margin: 10,
                nav: false,
                items: 1,
                dots: true,
                autoplay: true,
                autoplayTimeout: 1500,
                autoplaySpeed: 1000,
            });
        },
        error: function (xhr, status, error) {
            /*alert(xhr.status);*/
        }
    });
}
function newsList() {
    $.ajax({
        type: "GET",
        url: "/common/json/haberler.js",
        dataType: "json",
        async: true,
        cache: true,
        timeout: 50000,
        contentType: "application/json",
        success: function (data) {
            var slideName = '.news-section #newsList';
            $(slideName).text("");
            $.each(data, function (i, item) {
                $(slideName).append('<article class="news-article"><h4><a href="' + item.Link + '">' + item.Baslik + '</a></h4><p>' + item.Ozet + '</p></article>');
            });

            $('.news-content').owlCarousel({
                loop: true,
                margin: 10,
                nav: false,
                items: 1,
                dots: true,
                autoplay: true,
                autoplayTimeout: 5000,
                autoplaySpeed: 1500,
            });
        },
        error: function (xhr, status, error) {
            /*alert(xhr.status);*/
        }
    });
}
function healtList() {
    $.ajax({
        type: "GET",
        url: "/common/json/haberler1.js",
        dataType: "json",
        async: true,
        cache: true,
        timeout: 50000,
        contentType: "application/json",
        success: function (data) {
            var slideName = '.health-section #healthList';
            $(slideName).text("");
            $.each(data, function (i, item) {
                $(slideName).append('<article class="health-article"><div class="thumbnail"><img src="/common/images/haber/' + item.Resim + '" alt=""><div class="caption"><h4><a href="' + item.Link + '">' + item.Baslik + '</a></h4></div></div></article>');
            });

            $('.health-articles-group').owlCarousel({
                loop: true,
                margin: 20,
                nav: false,
                items: 3,
                dots: true,
                autoplay: true,
                autoplayTimeout: 2000,
                autoplaySpeed: 1500,
                responsive: {
                    0: {
                        items: 1
                    },
                    768: {
                        items: 3
                    }
                }
            });
        },
        error: function (xhr, status, error) {
            /*alert(xhr.status);*/
        }
    });
}
function linkList() {
    $.ajax({
        type: "GET",
        url: "/common/json/baglantilar.js",
        dataType: "json",
        async: true,
        cache: true,
        timeout: 50000,
        contentType: "application/json",
        success: function (data) {
            var slideName = '.important-links-section #baglantiList';
            $(slideName).text("");
            $.each(data, function (i, item) {
                $(slideName).append('<div class="link-wrap"><a href="/baglantilar" class="link-item"><img src="/common/images/firma/b/' + item.Resim + '" alt=""></a></div>');
            });

            $('.links-group').owlCarousel({
                loop: true,
                margin: 20,
                nav: true,
                items: 1,
                navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
                autoplay: true,
                autoplayTimeout: 2000,
                autoplaySpeed: 1000,
            });
        },
        error: function (xhr, status, error) {
            /*alert(xhr.status);*/
        }
    });
}