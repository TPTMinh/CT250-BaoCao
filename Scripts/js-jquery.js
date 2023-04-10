$(document).ready(function ($) {

    $('[data-side-menu] >li').hover(function () {
        var subMenu = $(this).find('.sub-slide-menu');
        subMenu.slideDown();
    }, function () {
        var subMenu = $(this).find('.sub-slide-menu');
        subMenu.slideUp();
    });


    //slide
    $(".regular").slick({
        dots: true,
        prevArrow: false,
        nextArrow: false,
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 2000

        //slidesToShow: 1,
        //slidesToScroll: 1,
        ////centerMode: true,
        //easing: 'swing', // see http://api.jquery.com/animate/
        //speed: 700,
        //dots: true,
    });

    //backtop
    if ($(".btn-top").length > 0) {
        $(window).scroll(function () {
            var e = $(window).scrollTop();
            if (e > 300) {
                $(".btn-top").show()
            } else {
                $(".btn-top").hide()
            }
        });
        $(".btn-top").click(function () {
            $('body,html').animate({
                scrollTop: 0
            })
        })
    }







    // Scroll Menu
    var MenuoffSet = $(".wap_menu").offset().top;
    $(".wap_menu").addClass("fixed");
    $(window).scroll(function () {
        var ScrollPos = $(window).scrollTop();
        if (ScrollPos >= MenuoffSet)
            $(".wap_menu").addClass("fixed");
        else {
            $(".wap_menu").removeClass("fixed");
        }
    });

    //zoom
    var native_width = 0, native_height = 0;
    $('.magnify').mousemove(function (e) {



        if (!native_width && !native_height) {
            var image_object = new Image();
            image_object.src = $('.small').attr('src');
            native_width = image_object.width;
            native_height = image_object.height;
        }
        else {
            var magnify_offset = $(this).offset();
            var mx = e.pageX - magnify_offset.left;
            var my = e.pageY - magnify_offset.top;
            if (mx < $(this).width() && my < $(this).height() && mx > 0 && my > 0) {
                $('.large').fadeIn('fast');
            }
            else {
                $('.large').fadeOut('fast');
            }
            if ($('.large').is(':visible')) {
                rx = Math.round(mx / $('.small').width() * native_width - $('.large').width() / 2) * (-1);
                ry = Math.round(my / $('.small').height() * native_height - $('.large').height() / 2) * (-1);
                var bgp = rx + 'px ' + ry + 'px';
                var px = mx - $('.large').width() / 2;
                var py = my - $('.large').height() / 2;
                $('.large').css(
                    {
                        left: px,
                        top: py,
                        "background-position": bgp
                    })

            }
        }
    });
    //tin tuc




});
