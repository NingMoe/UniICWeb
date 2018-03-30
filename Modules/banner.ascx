
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="banner.ascx.cs" Inherits="banner" %>
<div class="banner">
    <div>
        <div class="logo">
            <table style="width:99%"> 
                <tr>
                    <td style="text-align:center">
                        <div class="LogoTitle" style="margin:10px auto;"> 

                <label id="logolabel" style="font-size:45px;"> <%=ConfigConst.GCSysName %></label>
               
               <!--  <img id="logoimg" src="<%=MyVPath %>themes/img/titlename.png" width="800" />-->
                    </div></td>
               <td style="text-align:right;width:190px;">
          
            <div class="KindBtn" style="text-align:right">
                <%if (nCurPage > 0)
                  { %>
                <table border="0" style="width:180px;">
                    <tr>
                        <td><a href="<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame %>/Inst/Main.aspx">
                            <p>
                                <img src="<%=MyVPath %>themes/img/f<%=szInstImg %>.png" /></p>
                            <p>日常管理</p>
                        </a></td>
                        <%//if (ConfigConst.GCSysFrame.ToLower() != "site")
                          { %>
                        <td><a href="<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame  %>/Report/Main.aspx">
                            <p>
                                <img src="<%=MyVPath %>themes/img/a<%=szReportImg %>.png" /></p>
                            <p>统计报表</p>
                        </a></td>
                        <%} %>
                        <% {%>
                        <td><a href="<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame  %>/Sys/Main.aspx">
                            
                            <p>
                                <img src="<%=MyVPath %>themes/img/e<%=szSysImg %>.png" /></p>
                            <p>管理设置</p>
                            
                        </a></td>
                        <%} %>
                    </tr>
                </table>
                <%} %>
            </div>
                    </td>
                     </tr>
            </table>
        </div>
    </div>
    <div id="SearchBtn"></div>

    <%if (nCurPage == 1)
      { %>
      <%if (nCurPage == 1 && ConfigConst.GCRemoveCtrl == "1")
              { %>
    <div id="QuickBtn">
      
        <div id="QuickBtnList">
            
            <a href="#" id="sendMessage" title="发消息">
                <img src="<%=MyVPath %>themes/icon_s/9.png" /></a>
            <a href="#" id="noLogin" title="<%=ConfigConst.GCDevName %>免登录">
                <img src="<%=MyVPath %>themes/iconPage/Nologin.png" /></a>
            <a href="#" id="needLogin" title="<%=ConfigConst.GCDevName %>需要登录">
                <img src="<%=MyVPath %>themes/iconpage/needLoginIn.png" /></a>
            <a href="#" id="powerON" title="<%=ConfigConst.GCDevName %>开机">
                <img src="<%=MyVPath %>themes/iconpage/poweron.png" /></a>
            <a href="#" id="powerON2" title="<%=ConfigConst.GCDevName %>关机">
                <img src="<%=MyVPath %>themes/iconpage/poweroff.png" /></a>
            <a href="#" id="restart" title="<%=ConfigConst.GCDevName %>重启">
                <img src="<%=MyVPath %>themes/iconpage/powerrest.png" /></a>
            <a href="#" id="uLock" title="U盘锁定">
                <img src="<%=MyVPath %>themes/iconpage/ULOCK.png" /></a>
            <a href="#" id="uRes" title="U盘解锁">
                <img src="<%=MyVPath %>themes/iconpage/UUNLOCK.png" /></a>
            <a href="#" id="gLock" title="光驱禁用">
                <img src="<%=MyVPath %>themes/iconpage/CDLOCK.png" /></a>
            <a href="#" id="gRes" title="光驱解禁">
                <img src="<%=MyVPath %>themes/iconpage/CDUNLOCK.png" /></a>
              <a href="#" id="screenLock" title="屏幕锁定">
                <img src="<%=MyVPath %>themes/iconpage/lock.png" /></a>
            <a href="#" id="screenunLock" title="屏幕解禁">
                <img src="<%=MyVPath %>themes/iconpage/unlock.png" /></a>
				 <!--    <a href="#" id="gSubSidy" title="加减补助"><img src="<%=MyVPath %>themes/icon_s/4.png"/></a>
           <a href="#" id="devNeedRepair" title="禁用"> <img src="<%=MyVPath %>themes/icon_s/9.png" /></a>-->
   
        <a href="#" id="noLoginResv" title="免登陆预约"><img src="<%=MyVPath %>themes/iconpage/edit.png"/></a>
            <a href="#" id="noLoginResvLimiTime" title="限时免登陆"><img src="<%=MyVPath %>themes/icon_s/11.png"/></a>
            <a href="#" id="AllResv" title="全体人员预约"><img src="<%=MyVPath %>themes/icon_s/5.png"/></a>
               <%if (szUnStall == 1)
                 { %>
               <a href="#" id="unstall" title="卸载客户端"><img src="<%=MyVPath %>themes/iconpage/del.png""/></a>
              
            <%}%>
            <%} %>
        </div>
      <div id="divWaringInfo">

      </div>
    </div>
       
    <%} %>
   
    <div class="myInfo">
        <a href="../../../clientweb/default.aspx" style="color: #ffffff">返回预约端</a>
        <img class="UserIcon" src="<%=MyVPath%>themes/img/user.png" /><span><%=szTrueName %> <a id="btnLogout" href="<%=MyVPath %>loginall.aspx?op=Logout">退出</a></span><img id="stationInfoIcon" class="UserIcon" title="点击查看详情" />
    </div>
    <style>
      
    </style>
    <script>
        $(function () {
            var vMutRoom = "";
            <%if(ConfigConst.GCSysFrame.ToLower()=="lab"&&ConfigConst.GroomNumMode==1) {%>
            vMutRoom = "mroom";
            <%}%>
            var vImg = 2;//1表示图片，2表示文字;
            if (vImg == 1) {
                $("#logolabel").hide();
            }
            else {
                $("#logoimg").hide();
            }
            SetStationIco();
            function SetStationIco() {
                $.get(
                       "<%=MyVPath %>ajaxdata/getstationwarninfo.aspx",
                       { },
                       function (data) {
                           var vCamp = eval(data);
                           vCamp = vCamp[0];
                           $("#stationInfoIcon").attr("src", "<%=MyVPath %>themes/img/" + vCamp.dwStatus + ".png");
                           var szTitleList = vCamp.szStatInfo;
                           $("#divWaringInfo").html(szTitleList);
                       }
                     );
            }
            $("#divWaringInfo").dialog({
                autoOpen: false,
                height: 250,
                width: 300,
                modal: true,
                show: {
                    effect: "blind",
                    duration: 1000
                },
                hide: {
                    effect: "blind",
                    duration: 1000
                }
            });
            $("#stationInfoIcon").click(function () {
                $("#divWaringInfo").dialog("open");
            });
            
            $("#sendMessage").click(function (event) {

                $.lhdialog({
                    title: '发消息',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/sendMessage'+vMutRoom+'.aspx?op=set'
                });
            });
            $("#noLoginResv").click(function (event) {

                $.lhdialog({
                    title: '免登陆预约',
                    width: '750px',
                    height: '600px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/NoLoginResv.aspx?op=set'
                });
            });
            $("#noLoginResvLimiTime").click(function (event) {
                $.lhdialog({
                    title: '限时免登陆',
                    width: '750px',
                    height: '600px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/NoLoginResvLimitTime.aspx?op=set'
                });
            });
            
            $("#AllResv").click(function (event) {

                $.lhdialog({
                    title: '全体人员预约',
                    width: '750px',
                    height: '520px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/allresv'+vMutRoom+'.aspx?op=set'
                });
            });
            
            $("#powerON2").click(function (event) {
                $.lhdialog({
                    title: '关机',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=12'
                });
            });
            $("#powerON").click(function (event) {
                $.lhdialog({
                    title: '开机',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=11'
                });
            });
            $("#restart").click(function (event) {
                $.lhdialog({
                    title: '重启',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=13'
                })
            });
            $("#unstall").click(function (event) {
                $.lhdialog({
                    title: '卸载客户端',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=23'
                });
            });

            $("#noLogin").click(function (event) {

                $.lhdialog({
                    title: '<%=ConfigConst.GCDevName %>免登录',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=52'
                });
            });
			  $("#gSubSidy").click(function (event) {
                $.lhdialog({
                    title: '加减补助',
                    width: '850px',
                    height: '550px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/SubSidy'+vMutRoom+'.aspx?op=set&type=23'
                });
            });
            $("#uLock").click(function (event) {
                $.lhdialog({
                    title: 'U盘锁定',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=45'
                });
            });
            $("#uRes").click(function (event) {
                $.lhdialog({
                    title: 'U盘解锁',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=46'
                });
            });
           
            $("#screenLock").click(function (event) {
                $.lhdialog({
                    title: '屏幕锁定',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl' + vMutRoom + '.aspx?op=set&type=41'
                });
            });
            $("#screenunLock").click(function (event) {
                $.lhdialog({
                    title: '屏幕解锁',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl' + vMutRoom + '.aspx?op=set&type=42'
                });
            });
            $("#gLock").click(function (event) {
                $.lhdialog({
                    title: '光驱禁用',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl' + vMutRoom + '.aspx?op=set&type=43'
                });
            });
            $("#gRes").click(function (event) {
                $.lhdialog({
                    title: '光驱解禁',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=44'
                });
            });
            $("#needLogin").click(function (event) {

                $.lhdialog({
                    title: '<%=ConfigConst.GCDevName %>需要登录',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/RomoveCtrl'+vMutRoom+'.aspx?op=set&type=51'
                });
            });

            $("#devNeedRepair").click(function (event) {
                $.lhdialog({
                    title: '仪器禁用',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/DevNeedRepair'+vMutRoom+'.aspx?op=set'
                });
            });
            $("#HolidDayShift").click(function (event) {

                $.lhdialog({
                    title: '调课',
                    width: '500px',
                    height: '320px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/HolidDayShift'+vMutRoom+'.aspx?op=set'
                });
            });
            $("#DoorCardRec").click(function (event) {
                $.lhdialog({
                    title: '刷卡记录',
                    width: '750px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../Dlg/DoorCardRec'+vMutRoom+'.aspx?op=set'
                });
            });

        });
    </script>
</div>
<script type="text/javascript">
    var g_TimeoutHandle = null;
    function checkTimeout() {
        $.get("<%=MyVPath%>Pages/<%=ConfigConst.GCSysFrame%>/Data/checkTimeout.aspx", function (data) {
            if (data != "OK") {
                if (g_TimeoutHandle) {
                    clearInterval(g_TimeoutHandle);
                    g_TimeoutHandle = null;
                }
                MessageBox("登录超时", "登录超时", 1, function () {
                    location.href = "<%=MyVPath%>Pages/default.aspx";
                });
            }
        });
    }
    var g_PasswdHandle = null;
    var vCount = 0;
    function CheckPdNull() {
        var vSysFrame="<%=ConfigConst.GCSysFrame%>";
        
        if (vSysFrame.toUpperCase() == "IC")
        {
            $.get("<%=MyVPath%>Pages/<%=ConfigConst.GCSysFrame%>/Data/checkpasdnull.aspx", function (data) {
                if (data != "OK") {
                    if (g_TimeoutHandle) {
                        clearInterval(CheckPdNull);
                        CheckPdNull = null;

                        $(function () {
                            if (vCount > 0) {
                                return;
                            }
                            $.lhdialog({
                                title: '修改密码',
                                width: '660px',
                                height: '450px',
                                max: false,
                                min: false,
                                extendDrag: false,
                                close: function () {
                                    vCount = vCount - 1;
                                    return;
                                },
                                drag: false,
                                esc: true,
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath%>Pages/<%=ConfigConst.GCSysFrame%>/Dlg/changePaawd.aspx?op=new'
                            });
                            vCount = vCount + 1;
                        }
                 );
                    }
                }
            });
        }
    }
    $(function () { 
        g_TimeoutHandle = setInterval(checkTimeout, 10000);
        g_PasswdHandle = setInterval(CheckPdNull, 1000); 
    });
    
</script>
