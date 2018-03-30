﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editContent.aspx.cs" Inherits="ClientWeb_md_ueditor_net_editContent" ValidateRequest="false"%>
<%@ Register TagPrefix="Uni" TagName="include" Src="include.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>信息编辑</title>
    <Uni:include runat="server" />
    <script charset="utf-8" src="<%=url %>md/ueditor/ueditor.config.js" type="text/javascript"></script>
    <script charset="utf-8" src="<%=url %>md/ueditor/ueditor.all.min.js" type="text/javascript"></script>
    <script charset="utf-8" src="<%=url %>md/ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <link href="<%=url %>md/ueditor/themes/default/css/ueditor.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-dialog { z-index: 9999; }
    </style>
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
        function ajaxSubmit() {
            var id = $("#infoId").val();
            var type = $("#infoType").val();
            var postData = "\"" + UE.getEditor("editor").getContent() + "\"";
            pro.j.art.saveXmlArticle(postData, id, type, function () {
                uni.confirm("保存成功，是否关闭？", function () {
                    window.close();
                });
            });
        }
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
