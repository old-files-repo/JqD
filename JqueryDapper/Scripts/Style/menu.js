//(function ($) {
//    $(document).ready(function () {
//        $("#cssmenu").prepend('<div id="menu-button">Menu</div>');
//        $("#cssmenu #menu-button").on("click", function () {
//            var menu = $(this).next("ul");
//            if (menu.hasClass("open")) {
//                menu.removeClass("open");
//            }
//            else {
//                menu.addClass("open");
//            }
//        });
//    });
//})(jQuery);

(function ($) {
    window.Ewin = function () {
        var html = '<div id="[Id]" class="modal fade" role="dialog" aria-labelledby="modalLabel">' +
                              '<div class="modal-dialog modal-sm auto_widthPercert30">' +
                                  '<div class="modal-content">' +
                                      '<div class="modal-header">' +
                                          '<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>' +
                                          '<h4 class="modal-title" id="modalLabel">[Title]</h4>' +
                                      "</div>" +
                                      '<div class="modal-body">' +
                                      "<p>[Message]</p>" +
                                      "</div>" +
                                       '<div class="modal-footer">' +
                                        '<button type="button" class="btn btn-default cancel" data-dismiss="modal">[BtnCancel]</button>' +
                                        '<button type="button" class="btn btn-primary ok" data-dismiss="modal">[BtnOk]</button>' +
                                    "</div>" +
                                  "</div>" +
                              "</div>" +
                          "</div>";
        var reg = new RegExp("\\[([^\\[\\]]*?)\\]", "igm");
        var generateId = function () {
            var date = new Date();
            return "mdl" + date.valueOf();
        }
        var initalert = function (options) {
            options = $.extend({}, {
                title: "警告信息",
                message: "",
                btnok: "确定",
                btncl: "取消",
                width: 100,
                auto: false
            }, options || {});
            var modalId = generateId();
            var content = html.replace(reg, function (node, key) {
                return {
                    Id: modalId,
                    Title: options.title,
                    Message: options.message,
                    BtnOk: options.btnok,
                    BtnCancel: options.btncl
                }[key];
            });
            $("body").append(content);
            $("#" + modalId).modal({
                width: options.width,
                backdrop: "static"
            });
            $("#" + modalId).on("hide.bs.modal", function (e) {
                $("body").find("#" + modalId).remove();
            });
            return modalId;
        }
        var initconfirm = function (options) {
            options = $.extend({}, {
                title: "提示信息",
                message: "",
                btnok: "确定",
                btncl: "取消",
                width: 100,
                auto: false
            }, options || {});
            var modalId = generateId();
            var content = html.replace(reg, function (node, key) {
                return {
                    Id: modalId,
                    Title: options.title,
                    Message: options.message,
                    BtnOk: options.btnok,
                    BtnCancel: options.btncl
                }[key];
            });
            $("body").append(content);
            $("#" + modalId).modal({
                width: options.width,
                backdrop: "static"
            });
            $("#" + modalId).on("hide.bs.modal", function (e) {
                $("body").find("#" + modalId).remove();
            });
            return modalId;
        }
        return {
            alert: function (options) {
                if (typeof options == "string") {
                    options = {
                        message: options
                    };
                }
                var id = initalert(options);
                var modal = $("#" + id);
                modal.find(".ok").removeClass("btn-success").addClass("btn-primary");
                modal.find(".cancel").hide();

                return {
                    id: id,
                    on: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.find(".ok").click(function () { callback(true); });
                        }
                    },
                    hide: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.on("hide.bs.modal", function (e) {
                                callback(e);
                            });
                        }
                    }
                };
            },
            confirm: function (options) {
                if (options.message === "deleted") {
                    options.message = "确认删除吗？";
                }
                var id = initconfirm(options);
                var modal = $("#" + id);
                modal.find(".ok").removeClass("btn-primary").addClass("btn-success");
                modal.find(".cancel").show();
                return {
                    id: id,
                    on: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.find(".ok").click(function () { callback(true); });
                            modal.find(".cancel").click(function () { callback(false); });
                        }
                    },
                    hide: function (callback) {
                        if (callback && callback instanceof Function) {
                            modal.on("hide.bs.modal", function (e) {
                                callback(e);
                            });
                        }
                    }
                };
            }
        }
    }();
})(jQuery);
