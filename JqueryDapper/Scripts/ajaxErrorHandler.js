$(document).ready(function () {
    $.ajaxSetup({
        cache: false,
        error: function (xhr) {
            if (xhr) {
                ErrorTip.confirm({ message: jQuery.parseJSON(xhr.responseText).Message })
                    .on(function () { });
            }
        }
    });
});
