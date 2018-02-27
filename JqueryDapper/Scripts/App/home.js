$(document).ready(function () {

    var Edit = window.wangEditor;
    var editor = new Edit("#content");
    initializeDate();
    editor.$textElem.attr('contenteditable', false);

    $(".moreDetail").off().on("click", function () {
        var id = $(this).attr("data-operateid");
        $.ajax({
            url: "/Work/Get",
            method: "POST",
            data: JSON.stringify({ id: id }),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (result) {
            window.js2form($("#form")[0], result);
            editor.txt.clear();
            editor.txt.html(result.Content);
        });
        $("#moreDetailModal").modal("show");
    });

    function initializeDate() {
        editor.customConfig.menus = [
        ];
        editor.create();
        for (var i = 0; i < window.CategoryGroup.length; i++) {
            $(`<option value='${window.CategoryGroup[i].Id}'>${window.CategoryGroup[i].EnumString}</option>`).appendTo($("#category"));
        }
    }

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