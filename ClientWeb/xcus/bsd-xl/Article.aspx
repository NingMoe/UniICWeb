<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Article.aspx.cs" Inherits="Article" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function iframeResizeHeight(frame_name, body_name, offset) {
            parent.document.getElementById(frame_name).height = document.getElementById(body_name).offsetHeight + offset;
        }

        function Resize() {
            var tbls = document.getElementsByTagName("table");
            for (var i = 0; i < tbls.length; i++) {
                tbls[i].border = "1";
            }
            var frame_name = "myContent";
            var body_name = "myBody";
            if (parent.document.getElementById(frame_name)) {
                return iframeResizeHeight(frame_name, body_name, 30);
            }
        }
        window.onload = Resize;
    </script>
    <style type="text/css">
        table { border-collapse: collapse; }
        th, td { border-width: 1px; border-style: solid; }
        .info_title { text-align:center;font-size:16px;font-family:"微软雅黑";font-weight:bold;height:24px;line-height:24px;margin:10px 0;}
        .info_date {text-align:right;font-size:12px;height:14px;line-height:14px;margin:3px;color:#999;border-bottom:1px solid #eee;}
    </style>
</head>
<body id="myBody">
    <div runat="server" id="divContent"></div>
</body>
</html>
