<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="_Default" %>

<html>
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8">
    <title>研修间预约系统</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="stylesheet" type="text/css" media="screen" href="css.aspx" />
    <script type="text/javascript" src="zepto.min.js"></script>
    <script type="text/javascript" src="js.aspx"></script>
    <script type="text/javascript">
        function OnLoadResvForm() {

        }

        function myloginout() {
           // alert('t');
           window.location.href = "login.aspx?op=out";
        }
        function OnLoadMyResvList() {
            $(".btnCancel").on(szClickName, function (e) {
                e.preventDefault();
                var dwID = $(this).parents("tr").data("id");
                LoadContentByAjax2("tableDiv", "subMyResvList.aspx", null, "delID=" + dwID);
            });
            $("#btnSearch").on(szClickName, function (e) {
                e.preventDefault();
                var chenked ="";
                $('input[name="resvStatus"]:checked').each(function(){//遍历每一个名字为interest的复选框，其中选中的执行函数    
                    chenked+=($(this).val())+",";//将选中的值添加到数组chk_value中    
                });
                LoadContentByAjax2("tableDiv", "subMyResvList.aspx", OnLoadMyResvList, "resvDate=" + $("#searchDate").val() + "&resvStatus=" + chenked);
            });
            
        }
      
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
                    if ((devKind.uClassKind & 1) > 0) {
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
            $(".LBtn>button").on(szClickName, function (e) {
                e.preventDefault();
                var dwID = $(this).parents(".Item").data("id");
                var vKindID = $(this).parents(".Item").data("kindid");
                $.ajax({
                    type: 'GET',
                    url: 'ajax/getdevKindInfo.aspx',
                    data: { kindid: vKindID },
                    dataType: 'json',
                    timeout: 3000,
                    success: function (data) {
                        var devKind = eval(data);
                        if (devKind.uClassKind == 1) {
                            LoadContentByAjax2("resvDiv", "subResv.aspx?resvDate=" + $("#dwDate").val() + "&DevId=" + dwID, OnLoadResvForm);
                        }
                        else {
                            LoadContentByAjax2("resvDiv", "subResvKind.aspx?resvDate=" + $("#dwDate").val() + "&DevId=" + dwID, OnLoadResvForm);
                        }
                    },
                    error: function (xhr, type) {

                    }
                });
            });
        }

        function OnLoadSelectTimeList() {
            $(".LGraphics>canvas").each(function (i) {
                DrawBar(this, parseInt($(this).data("start")), parseInt($(this).data("end")), eval($(this).data("list")), OnRoomClickBar);
            });
            setTimeout(function () {
                $("#dwBeginTime").val($("#selectBeginTime").val());
                $("#dwEndTime").val($("#selectEndTime").val());
            }, 100);
            $(".TimedatePreSearch").on(szClickName, function (e) {
                var dateNow = new Date();
                dateNow = new Date(dateNow.getFullYear() + '-' + numToHour(dateNow.getMonth() + 1) + '-' + dateNow.getDate());
                var resvDate = new Date($("#TimeResvDateSearch").text());
                resvDate.setDate(resvDate.getDate() - 1);
                var diff = resvDate - dateNow;
                if ((diff / (1000 * 60 * 60 * 24)) < 0) {

                }
                else {
                    $("#TimeResvDateSearch").text(resvDate.getFullYear() + '-' + numToHour(resvDate.getMonth() + 1) + '-' + numToHour(resvDate.getDate()));

                }
            });
            $(".TimedateNextSearch").on(szClickName, function (e) {
                var dateNow = new Date();
                var resvDate = new Date($("#TimeResvDateSearch").text());
                resvDate.setDate(resvDate.getDate() + 1);
                var diff = resvDate - dateNow;
                if ((diff / (1000 * 60 * 60 * 24)) < 0) {
                }
                else {
                    $("#TimeResvDateSearch").text(resvDate.getFullYear() + '-' + numToHour(resvDate.getMonth() + 1) + '-' + numToHour(resvDate.getDate()));
                }
            });
            $(".btnTimeClass").on(szClickName, function (e) {
                e.preventDefault();
                var dwID = $(this).attr("id");
                var roomid = $(this).attr("roomid");
                var dwBeginTime = $(this).parents(".Item ").children("div").children("#dwBeginTime").val();
                var dwEndTime = $(this).parents(".Item ").children("div").children("#dwEndTime").val();
                var selectHour = 1;
                var vTimePart = dwEndTime * 10000 + dwBeginTime;// $(this).parents(".Item").attr("value");
            
                var selectDate = $("#TimeResvDateSearch").val();
                LoadContentByAjax2("postionResv", "postionResv.aspx?resvDate=" + selectDate + '&timePart=' + vTimePart + '&dwBeginTime=' + dwBeginTime + '&dwEndTime=' + dwEndTime + '&needHour=' + selectHour + "&roomid=" + roomid, postionResv);

            });
        }
        function postionResv() {
            hide();
            $("#dlgclose,#btnClose").on(szClickName, function (e) {
                hide();
            });
            $("#btnResv").on(szClickName, function (e) {
                var vResvID = $("#resvDevID").val();
                var beginTime = $("#selectBeginTime").val();
                var endtime = $("#selectEndTime").val();
                $.ajax({
                    url: 'Ajax/Reserve.aspx?resvDate=' + resvDate + '&devid=' + vResvID + '&beginTime=' + beginTime + '&endtime=' + endtime,
                    success: function (data) {
                        if (data == '预约成功') {
                            LoadContentByAjax2("tableDiv", "subMyResvList.aspx", OnLoadMyResvList);
                        }
                        else {
                            alert(data);
                        }
                    }
                });
            });
            var vAajxBeginTime = $("#dwBeginTime").val();
            var vAajxEndTime = $("#dwEndTime").val();
            var vAajxszLabID = $("#szLabID").val();
            var vNeedHour = $("#uNeedHour").val();
            var timePart = $("#timePart").val();
            var resvDate = $("#resvDate").val();
            $.ajax({
                url: "Ajax/AGetDevice.aspx?op=get&dwBeginTime=" + vAajxBeginTime + '&resvDate=' + resvDate + "&timePart=" + timePart + "&dwEndTime=" + vAajxEndTime + "&szLabID=" + vAajxszLabID + '&NeedHour=' + vNeedHour,
                success: function (data) {
                    var vDevList = eval(data);
                    var vDivContant = $("#dragContainer");
                    for (var i = 0; i < vDevList.length; i++) {
                        var x = Number(vDevList[i].x);
                        var y = Number(vDevList[i].y);
                        var vSigan = "cycleOrange";
                        if (vDevList[i].uStatus == 1) {
                            vSigan = "cycleOrange";
                        }
                        else {
                            vSigan = "cycleGreen";//有人
                        }
                        vSigan = "cycleOrange";
                        var isDrag = false;
                        var vDivTemp = $("<div opentime= " + vDevList[i].openTime + " devName='" + vDevList[i].DevName + "' style='text-align:center;' id='" + vDevList[i].devID + "'></div>");
                        // vDivTemp.css({'background': 'url(img/ResvStatePng/A" + "50" + ".png")' });
                        var vImage = "<img src='img/ResvStatePng/A" + vDevList[i].uStatus + ".png'  width='20px' height='20px' />";
                        vDivTemp.css({ 'position': 'absolute', 'left': x, 'top': y });
                        if (!isDrag) {
                            if (vSigan == "cycleOrange") {
                                vDivTemp.click(function () {
                                    var devidAjax = $(this).attr("id");
                                    $.ajax({
                                        url: 'Ajax/AGetDevice.aspx?op=getSingnal&resvDate=' + resvDate + '&devid=' + devidAjax,
                                        success: function (data) {
                                            var vList = data.split('，');
                                            var vRes = "";
                                            for (var i = 0; i < vList.length; i++)
                                            {
                                                vRes = vRes + vList[i] + "<br />";
                                            }
                                            $("#resvinfo").html(vRes);
                                        }
                                    });
                                    $("#resvDevID").val(devidAjax);
                                    $("#resvDevName").html($(this).attr("devName"));
                                    var openTimeList = $(this).attr("opentime");
                                    debugger;
                                    openTimeList = openTimeList.split(",");
                                    $("#selectBeginTime").empty();
                                    $("#selectEndTime").empty();
                                    for(var k=0;k<openTimeList.length;k++)
                                    {
                                        if(openTimeList[k]=="")
                                        {
                                            continue;
                                        }
                                        debugger;
                                        var opentime = Number(openTimeList[k]);
                                        var opentimeBegin = parseInt(opentime / 10000);
                                        var opentimeEnd = parseInt(opentime % 10000);
                                        
                                        for (var i = opentimeBegin; i <= opentimeEnd; i = i + 10) {
                                            if (parseInt(i % 100) == 60) {
                                                i = parseInt((i + 100) / 100) * 100;
                                                //continue;
                                            }
                                            var vMin=i%100;
                                            if(vMin<10)
                                            {
                                                vMin="0"+vMin;
                                            }
                                            selectTime = parseInt(i / 100) + ':' + vMin;
                                            $("#selectBeginTime").append("<option value='" + i + "'>" + selectTime + "</option>");
                                            $("#selectEndTime").append("<option value='" + i + "'>" + selectTime + "</option>");

                                        }
                                    }
                                    var pos = GetPostion(null);
                                    $("#win").css("left",pos.x);
                                    $("#win").css("top", pos.y);
                                    showWin();
                                });
                            }
                        }
                       // vSigan = "";
                        vDivTemp.append(vImage);
                           //vDivTemp.append("<div class='" + vSigan + "' id='div" + vDevList[i].devID + "'>" + '' + "</div><div>" + "" + "</div>");
                       //   vDivTemp.append("<div  id='div" + vDevList[i].devID + "'>" + '' + "</div>");
                        vDivContant.append(vDivTemp);
                    }
                }
            });
            function GetPostion(e) {
                var x = getX(e);
                var y = getY(e);
                return { "x": x-40, "y": y-30};
            }
            function getX(e) {
                e = e || window.event;
                return e.pageX || e.clientX + document.documentElement.scrollLeft;
            }
            function getY(e) {
                e = e || window.event;
                return e.pageY || e.clientY + document.documentElement.scrollTop;
            }
            function showWin() {
                /*找到div节点并返回*/
                var winNode = $("#win");

                var vBeginTime = $("#dwBeginTime").val();
                var vEndTime = $("#dwEndTime").val();
                $("#selectBeginTime").val(vBeginTime);
                $("#selectEndTime").val(vEndTime);

                winNode.css("display", "block");
            }
            function hide() {
                var winNode = $("#win");
                winNode.css("display", "none");
            }
        }
       
        function OnLoadKindList() {
            $(".actDevice").on(szClickName, function (e) {
                e.preventDefault();
                var dateNow = new Date();
                dateNow = dateNow.getFullYear() + '' + numToHour(dateNow.getMonth() + 1) + '' + numToHour(dateNow.getDate());
                LoadContentByAjax2("listDiv", "subDeviceList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateNow + "&KindId=" + $(this).data("id"), OnLoadDevList);
            });
        }
        function OnLoadTimeList() {
            $(".LGraphics>canvas").each(function (i) {
                DrawBar(this, parseInt($(this).data("start")), parseInt($(this).data("end")), eval($(this).data("list")), OnRoomClickBar);
            }); 
            $(".LBtn>button").on(szClickName, function (e) {
                
               // var dateNow = new Date();
                var dateNow = $("#dwDate").val();// dateNow.getFullYear() + '' + numToHour(dateNow.getMonth() + 1) + '' + numToHour(dateNow.getDate());
                e.preventDefault();
                begintime = 0;// $(this).attr("begintime");
                var dwID = $(this).parents(".Item").data("id");
                var vKindID = $(this).parents(".Item").data("kindid");
                LoadContentByAjax2("selectTimeList", "subtimelist.aspx?classKind=<%=szClassKind%>&dwDate=" + dateNow + "&roomid=" + dwID + '&begintime=' + begintime, OnLoadSelectTimeList);
            });
            $(".actRoom").on(szClickName, function (e) {
                e.preventDefault();
                var dateNow = new Date();
            
                dateNow = dateNow.getFullYear() + '' + numToHour(dateNow.getMonth() + 1) + '' + numToHour(dateNow.getDate());
                LoadContentByAjax2("selectTimeList", "subtimelist.aspx?classKind=<%=szClassKind%>&dwDate=" + dateNow + "&roomid=" + $(this).data("id"), OnLoadSelectTimeList);
            });
        }
        function OnRoomClickBar(selectValue, obj) {
            var dwID = $(obj).parents(".Item").data("id");
            // var dateNow = new Date();
            var dateNow = $("#dwDate").val();// dateNow.getFullYear() + '' + numToHour(dateNow.getMonth() + 1) + '' + numToHour(dateNow.getDate());
            LoadContentByAjax2("selectTimeList", "subtimelist.aspx?classKind=<%=szClassKind%>&dwDate=" + dateNow + "&roomid=" + dwID + '&begintime=' + selectValue, OnLoadSelectTimeList);
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
                LoadContentByAjax2("selectTimeList", "subDeviceList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateTemp + "&KindId=" + $(this).data("id"), OnLoadDevList);
            }
        }
        function resvRoomStateNextDay() {
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
                LoadContentByAjax2("selectTimeList", "subSeatRoomList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateTemp, OnLoadTimeList);
            }
        }
        function resvRoomStatePreDay() {
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
                LoadContentByAjax2("selectTimeList", "subSeatRoomList.aspx?classKind=<%=szClassKind%>&dwDate=" + dateTemp, OnLoadTimeList);
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
            <span class="curUser"><%=curUser.szTrueName %></span> 
            
            <div class="Content Menu">
                <div id="menu">
             
                       <a tid="kindlistDiv" href="subKindList.aspx?classKind=<%=szClassKind%>" onload="OnLoadKindList()">研修间 </a>
                    <!--临时注释，研修间预约-->
                      <a tid="selectTimeList" href="subSeatRoomList.aspx?classKind=<%=szClassKind%>" onload="OnLoadTimeList()">座位</a> 
                    <a tid="tableDiv" href="subMyResvList.aspx" onload="OnLoadMyResvList()">预约记录 </a>

                </div>
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
