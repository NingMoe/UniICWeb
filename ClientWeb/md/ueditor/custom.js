//AJAX保存数据
function ajaxSubmit() {
    var id = $("#infoId").val();
    var type = $("#infoType").val();
    var postData = "\"" + UE.getEditor("editor").getContent() + "\"";
    var url = location.pathname;
    url = url.replace("aspx", "ashx");
    $.ajax({
        type: "POST",
        url: url,
        dataType: 'json',
        data: { "content": postData, "id": id, "type": type },
        success: function (msg) {
            if (msg != null && msg.ret == 1) {
                uni.confirm("内容已保存,是否关闭？", function () {
                    window.close();
                });
            }
            else {
                uni.msgBox("保存失败！");
            }
        },
        error: function (er) {
            uni.msgBox("服务器异常，保存不成功！");
        }
    });
}
