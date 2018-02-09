﻿$(document).ready(function () {

    $("#add").on("click", function () {
        $("#newModal").modal("show");
    });

    $("#saveNew").off("click").on("click", function () {
        var datas = form2js($("#addForm").get(0), ".", true);
        $.ajax({
            url: "/SystemUser/Add",
            method: "POST",
            data: JSON.stringify(datas),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (result) {
            if (result.Success) {
                window.location.reload();
            }
        });
    });

});