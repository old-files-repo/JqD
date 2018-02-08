$(document).ready(function () {
    $("#owl-slide").owlCarousel({
        autoPlay: 3000,
        items: 2,
        itemsDesktop: [1199, 2],
        itemsDesktopSmall: [979, 1],
        itemsTablet: [768, 1],
        itemsMobile: [479, 1],
        navigation: true,
        navigationText: ['<i class="fa fa-chevron-left fa-5x"></i>', '<i class="fa fa-chevron-right fa-5x"></i>'],
        pagination: false
    });
});