<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editContent.aspx.cs" Inherits="ClientWeb_md_ueditor_net_editContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>信息编辑</title>
    <script charset="utf-8" src="../../../fm/jquery/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script charset="utf-8"  type="text/javascript" src="../../../fm/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../fm/jquery-ui/start/jquery-ui-1.10.3.custom.min.css" />
    <script charset="utf-8"  type="text/javascript" src="../../../fm/uni.lib.js"></script>
    <link rel="stylesheet" type="text/css" href="../../../fm/uni.css" />

    <script charset="utf-8" src="../ueditor.cfg.js" type="text/javascript"></script>
    <script charset="utf-8" src="../ueditor.all.min.js" type="text/javascript"></script>
    <script charset="utf-8" src="../lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <script charset="utf-8" src="../custom.js" type="text/javascript"></script>
    <link href="../themes/default/css/ueditor.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            var req = uni.getReq();
            var width = req["w"];//宽
            if (width) $("#editor").width(width);
            var height = req["h"];//高
            if (height) $("#editor").height(height);
            var edit = UE.getEditor('editor');
            var con = $("#editContent").val();
            if (con != null || con != "") {
                edit.ready(function () {
                    this.setContent(con);
                })
            }
        });
    </script>
    <form runat="server" id="myForm">
        <div>
            <div runat="server" id="divTitle" style="font-size: 20px; color: #366092; font-weight: bold; padding: 2px; text-align: center;">内容编辑</div>
            <input type="hidden" runat="server" id="editContent" />
            <input type="hidden" runat="server" id="infoId" />
            <input type="hidden" runat="server" id="infoType" />
            <div>
                <script id="editor" type="text/plain" style="width: 700px; height: 420px; margin: 0 auto;">
                </script>
            </div>
            <div style="margin-top: 3px; text-align: center;">
                <input style="height: 30px; width: 80px; padding: 0 3px;" type="button" id="save" value="提交" onclick="ajaxSubmit()" />
                <input style="height: 30px; width: 80px; padding: 0 3px;" type="button" id="back" value="关闭" onclick="javascript: window.close();" />
            </div>
        </div>
    </form>
</body>
</html>
