$(document).ready(function () {

    var Edit = window.wangEditor;
    var editor = new Edit("#content");
    editor.customConfig.menus = [
        "head", // 标题
        "bold", // 粗体
        "italic", // 斜体
        "underline", // 下划线
        "strikeThrough", // 删除线
        "foreColor", // 文字颜色
        "backColor", // 背景颜色
        "link", // 插入链接
        "list", // 列表
        "justify", // 对齐方式
        "quote", // 引用
        "emoticon", // 表情
        "image", // 插入图片
        "table", // 表格
        //"video",  // 插入视频
        "code", // 插入代码
        "undo", // 撤销
        "redo" // 重复
    ];
    editor.customConfig.uploadImgServer = "/Infrustruct/AddImages";
    // 将图片大小限制为 5M
    editor.customConfig.uploadImgMaxSize = 5 * 1024 * 1024;
    // 限制一次最多上传 1 张图片
    editor.customConfig.uploadImgMaxLength = 1;
    // 隐藏“网络图片”tab
    editor.customConfig.showLinkImg = false;
    editor.customConfig.debug = true;
    editor.create();

    var optionstring = "";
    for (var item in window.CategoryType) {
        optionstring += "<option value=\"" + item.type + "\" >" + item.name + "</option>";
    }
    $("#addCategory").html(optionstring);

    $("#add").off().on("click", function () {
        $("#newModal").modal("show");
        $("#saveNew").show();
        $("#saveUpdate").hide();
    });

    $("#saveNew").off().on("click", function () {
        var datas = form2js($("#addForm").get(0), ".", true);
        var content = editor.txt.html();
        datas["Content"] = content;
        $.ajax({
            url: "/Work/Add",
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
        window.Ewin.confirm({ message: "deleted" }).on(function (e) {
            if (!e) {
                return;
            } else {
                $.ajax({
                    url: "/Work/Delete",
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
            url: "/Work/Get",
            method: "POST",
            data: JSON.stringify({ id: operateid }),
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (result) {
            window.js2form($("#addForm")[0], result);
            editor.txt.clear();
            editor.txt.html(result.Content);
        });
        $("#newModal").modal("show");
        $("#saveNew").hide();
        $("#saveUpdate").show();
    });

    $("#saveUpdate").off().on("click", function () {
        var datas = form2js($("#addForm").get(0), ".", true);
        var content = editor.txt.html();
        datas["Content"] = content;
        $.ajax({
            url: "/Work/Update",
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