<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index2.aspx.cs" Inherits="_Default" %>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <title>研修间预约系统</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="stylesheet" type="text/css" media="screen" href="../css.aspx" />
    <script type="text/javascript" src="../zepto.min.js"></script>
    <script type="text/javascript" src="../js.aspx"></script>
    <script type="text/javascript">

        function OnLoadResvForm() {

        }
        function myloginout() {
           window.location.href = "login.aspx?op=out";
        }
        function OnLoadMyResvList() {
            $(".btnCancel").on(szClickName, function (e) {
                e.preventDefault();

                var dwID = $(this).parents("tr").data("id");
                LoadContentByAjax2("tableDiv", "subMyResvList.aspx", OnLoadResvForm, "delID=" + dwID);
            });
        }
        $(".actDevice").on(szClickName, function (e) {
            LoadContentByAjax2("tableDiv", "subdevicelist.aspx", OnLoadResvForm, "");
        });
        function OnClickBar(selectValue, obj) {
            var dwID = $(obj).parents(".Item").data("id");
            var vKindID = $(obj).parents(".Item").data("kindid");
            $.ajax({
                type: 'GET',
                url: 'ajax/getdevKindInfo.aspx',
                data: { kindid: vKindID },
                dataType: 'json',
                timeout: 3000,
                success: function (data) {
                    var devKind = eval(data);
                    if (devKind.uClassKind == 1) {
                        LoadContentByAjax2("resvDiv", "subResv.aspx?resvDate=" + $("#dwDate").val() + "&DevId=" + dwID + "&starttime=" + selectValue, OnLoadResvForm);
                    }
                    else {
                        LoadContentByAjax2("resvDiv", "subResvKind.aspx?resvDate=" + $("#dwDate").val() + "&DevId=" + dwID + "&starttime=" + selectValue, OnLoadResvForm);
                    }
                },
                error: function (xhr, type) {

                }
            });

           
        }

        function OnLoadDevList() {
            $(".LGraphics>canvas").each(function (i) {
                DrawBar(this, parseInt($(this).data("start")), parseInt($(this).data("end")), eval($(this).data("list")), OnClickBar);
            });
            $(".test").on(szClickName, function (e) {
                e.preventDefault();
                LoadContentByAjax2("resvDiv", "test2.aspx", OnLoadResvForm);
            });
        }
        function OnLoadKindList() {
            $(".actDevice").on(szClickName, function (e) {
                e.preventDefault();
                var dateNow = new Date();
                dateNow = dateNow.getFullYear() + '' + numToHour(dateNow.getMonth() + 1) + '' + numToHour(dateNow.getDate());
                LoadContentByAjax2("listDiv", "subDeviceList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateNow + "&KindId=" + $(this).data("id"), OnLoadDevList);
            });
        }
        function addUser() {
            var LogonName = $("#logonName").val();
            if (LogonName != "") {
                $.ajax({
                    type: 'GET',
                    url: 'ajax/searchAccount.aspx',
                    data: { logonName: LogonName },
                    dataType: 'json',
                    timeout: 3000,
                    success: function (data) {
                        var accno = eval(data);
                        var accnohtml = $("#szAccno").val();
                        if (accnohtml != "" && (accnohtml.indexOf("," + accno[0].id + ",") > -1)) {
                            return;
                        }
                        if (accnohtml == "") {
                            accnohtml = "," + accno[0].id + ",";
                        }
                        else {
                            accnohtml = accnohtml + accno[0].id + ",";
                        }
                        $("#szAccno").val(accnohtml);
                        var text = "";
                        if ($("#divTrueName").html() != "") {
                            text = $("#divTrueName").html() + "," + accno[0].szTrueName;
                        }
                        else {
                            text = accno[0].szTrueName;
                        }
                        $("#divTrueName").text(text);
                        $("#logonName").val("");
                    },
                    error: function (xhr, type) {

                    }
                })
            }
        }
        function resvPreDay(){
            var dateNow = new Date();
            dateNow = new Date(dateNow.getFullYear() + '-' + numToHour(dateNow.getMonth() + 1) + '-' + dateNow.getDate());
            var resvDate = new Date($("#resvDate").text());
            resvDate.setDate(resvDate.getDate() - 1);
            var diff = resvDate - dateNow;
            if ((diff / (1000 * 60 * 60 * 24)) < 0) {

            }
            else {
                $("#resvDate").text(resvDate.getFullYear() + '-' + numToHour(resvDate.getMonth() + 1) + '-' + numToHour(resvDate.getDate()));
                $("#date").val(resvDate.getFullYear() + '' + numToHour(resvDate.getMonth() + 1) + '' + numToHour(resvDate.getDate()));
            }
        }
        function resvNextDay(){
            var dateNow = new Date();
            var resvDate = new Date($("#resvDate").text());
            resvDate.setDate(resvDate.getDate() + 1);
            var diff = resvDate - dateNow;
            if ((diff / (1000 * 60 * 60 * 24)) < 0) {

            }
            else {

                $("#resvDate").text(resvDate.getFullYear() + '-' + numToHour(resvDate.getMonth() + 1) + '-' + numToHour(resvDate.getDate()));
                $("#date").val(resvDate.getFullYear() + '' + numToHour(resvDate.getMonth() + 1) + '' + numToHour(resvDate.getDate()));
            }
        }
        function resvStatePreDay() {
           
            var dateNow = new Date();
            dateNow = new Date(dateNow.getFullYear() + '-' + numToHour(dateNow.getMonth() + 1) + '-' + dateNow.getDate());
            var resvDate = new Date($("#ResvDateSearch").text());
            resvDate.setDate(resvDate.getDate() - 1);
            var diff = resvDate - dateNow;
            if ((diff / (1000 * 60 * 60 * 24)) < 0) {

            }
            else {
                $("#ResvDateSearch").text(resvDate.getFullYear() + '-' + numToHour(resvDate.getMonth() + 1) + '-' + numToHour(resvDate.getDate()));
                var dateTemp = resvDate.getFullYear() + '' + numToHour(resvDate.getMonth() + 1) + '' + numToHour(resvDate.getDate());
                $("#dwDate").val(dateTemp);
                LoadContentByAjax2("resvDiv", "subDeviceList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateTemp + "&KindId=" + $(this).data("id"), OnLoadDevList);
            }
        }
        function resvStateNextDay() {
            var dateNow = new Date();
            var resvDate = new Date($("#ResvDateSearch").text());
            resvDate.setDate(resvDate.getDate() + 1);
            var diff = resvDate - dateNow;
            if ((diff / (1000 * 60 * 60 * 24)) < 0) {

            }
            else {
                $("#ResvDateSearch").text(resvDate.getFullYear() + '-' + numToHour(resvDate.getMonth() + 1) + '-' + (resvDate.getDate()));
                var dateTemp = resvDate.getFullYear() + '' + numToHour(resvDate.getMonth() + 1) + '' + numToHour(resvDate.getDate());
                $("#dwDate").val(dateTemp);
                LoadContentByAjax2("resvDiv", "subDeviceList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateTemp + "&KindId=" + $(this).data("id"), OnLoadDevList);

            }
        }
        function numToHour(hour) {
            if (hour < 10) {
                return "0" + hour;
            }
            return hour;
        }
        function BtnResv(){
            $("#__VIEWSTATE").attr("disabled", true);
            var dataForm = $("form").serialize();
            var dwID = $(this).parents(".Item").data("id");
            LoadContentByAjax2("resvDiv", "subResv.aspx?resvSubmit=" + 'ok', OnLoadResvForm, dataForm);
        }
      
    </script>
</head>
<body>
    <form runat="server">

        <div class="Placeholder"></div>

        <div id="headDiv">
          <a style="text-decoration:none;font-size:12px" href="#" id="myloginouta" onclick="myloginout()">退出</a>
            <span class="curUser">测试账号</span> 
            
            <div class="Content Menu">
                <div id="menu"><a tid="kindlistDiv" href="subKindList.aspx?classKind=<%=szClassKind%>" onload="OnLoadKindList()">座位</a><a tid="tableDiv" href="subMyResvList.aspx" onload="OnLoadMyResvList()">预约记录 </a></div>
            </div>
        </div>

        <div id="DContent">
        </div>

        <div class="msg">
            <%=szMsg %>
        </div>

    </form>

    <div class="copyright">Unifound</div>
</body>
</html>
