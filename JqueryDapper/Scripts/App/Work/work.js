$(document).ready(function () {

    $("#add").off().on("click", function () {
        $("#newModal").modal("show");
        $("#saveNew").show();
        $("#saveUpdate").hide();
    });

    $("#saveNew").off().on("click", function () {
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

    $("a[data-operation='delete']").off().on("click", function () {
        var $this = $(this);
        var operateid = $this.attr("data-operateid");
        Ewin.confirm({ message: "deleted" }).on(function (e) {
            if (!e) {
                return;
            } else {
                $.ajax({
                    url: "/SystemUser/Delete",
                    method: "POST",
                    data: JSON.stringify({ id: operateid }),
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (result) {
                    if (result.Success) {
                        window.location.reload();
                    }
                });
            }
        });
    });

    $("a[data-operation='edit']").off().on("click", function () {
        var $this = $(this);
        var operateid = $this.attr("data-operateid");
        $.ajax({
            url: "/SystemUser/Get",
            method: "POST",
            data: JSON.stringify({ id: operateid }),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (result) {
            window.js2form($("#addForm")[0], result);
        });
        $("#newModal").modal("show");
        $("#addLoginName").attr("disabled", true);
        $("#saveNew").hide();
        $("#saveUpdate").show();
    });

    $("#saveUpdate").off().on("click", function () {
        var datas = form2js($("#addForm").get(0), ".", true);
        $.ajax({
            url: "/SystemUser/Update",
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