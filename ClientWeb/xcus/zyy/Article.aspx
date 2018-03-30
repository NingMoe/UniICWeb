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
        table {
            border-collapse: collapse;
        }
        body {
            border-bottom: dashed 1px #808080;
        }
    </style>
</head>
<body id="myBody">
    <div runat="server" class="div_content" id="divContent">
        <p>9月21日至22日，教育部高等学校中医学类专业教学指导委员会第一次全体委员工作会议、首届“中医药社杯”全国高等中医药院校教师发展论坛暨青年教师教学基本功竞赛在兰州举行。我校校长范永升、副校长李俊伟等一行9人参加会议。
会上，范永升被聘为教育部高等学校中医学类专业教学指导委员会副主任委员，并作题为《以专业认证为抓手，推进教育质量的全面提升》的报告。他在报告中详细阐述了专业认证在健全内部教学质量保障体系、促进学校规范办学等方面的重要作用，介绍了我校以专业认证为抓手，全面提升教育质量的具体实践以及取得的成效。李俊伟在首届“中医药社杯”全国高等中医药院校教师发展论坛上作了题为《医部门校教师基本素质与能力的培养与发展》的报告。</p>
        <img alt="" src="Theme/images/temp05.jpg" />
        <img alt="" src="Theme/images/temp06.jpg" />
    </div>
</body>
</html>
