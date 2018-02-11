$(document).ready(function () {
    $.ajaxSetup({
        cache: false,
        error: function (xhr) {
            if (xhr) {
                Ewin.alert({ message: jQuery.parseJSON(xhr.responseText).Message });
            }
        }
    });
});
