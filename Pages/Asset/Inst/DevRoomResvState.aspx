<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevRoomResvState.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=szDevNameURL %>预约状况</h2>       
        <div class="tb_info">
             <div class="UniTab" id="tabl">
                <a href="DevRoomList.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomList"><%=szDevNameURL %>使用状况</a>
                <a href="DevRoomResvState.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomResvState"><%=szDevNameURL %>预约状况</a>
                <a href="DevRoomDoorCardRec.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomDoorCardRec"><%=szDevNameURL %>刷卡记录</a>
                <a href="DevRoomUseRec.aspx?dwClassKind=<%=uClassKind %>" id="DevRoomUseRec"><%=szDevNameURL %>使用记录</a>
                </div> 
                <div class="FixBtn">
            </div>
    </div>
        <div style="margin-top:30px;width:99%;">  
             <!--<a id="newResv" class="newResv">管理员新建预约</a>     -->
        </div>
        <div class="content">
            <div style="width:99%">
            <div style="margin-top:10px;text-align:center; MARGIN-RIGHT: auto; MARGIN-LEFT: 50px; ">
          <div id="roomResvState" style=""></div>
                </div>
                </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#roomResvState").ResvState({
                    "url": "../data/devResvstate.aspx",
                    "devClassKind": <%=uClassKind%>-1,
                    "purpose":11

                });
                 var tabl= $(".UniTab").UniTab();
                $("#Back").button().click(function () {                  
                    TabJump("Device/Stat.aspx");
                });
                $("#sub").button();
                $("#newResv").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="发消息"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="免登陆"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="需要登陆"><img src="../../../themes/icon_s/8.png"/></a>\
                    <a href="#" class="devCtrlPowerup" title="远程开机"><img src="../../../themes/icon_s/7.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtnSet").html('<div class="OPTDBtn">\
                    <a href="#" class="DevFix" title="报修"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtnRec").html('<div class="OPTDBtn">\
                    <a href="#" class="DevUseGroup" title="免预约使用人员"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a class="openDoor" href="#" title="远程开门"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="devDoorCard" title="刷卡记录"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("input[name='lab'],input[name='szRoom'],input[name='szDevKinds'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $(".video").click(function () {
                    var roomno =$(this).parents("tr").children().first().attr("data-roomno");
                    $.lhdialog({
                        title: '查看视频',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:video/videoplay.aspx?op=new&roomno=' + roomno
                    });
                });
                $(".devDoorCard").click(function () {
                    var roomid =$(this).parents("tr").children().first().attr("data-roomid");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    fdata = "szGetKey=" + roomid + '&roomName=' + devName;
                    TabInJumpReload("DevRoomDoorCardRec", fdata);
                });
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '免预约使用人员',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetGroupMember.aspx?op=set&dwID=' + groupID
                    });
                });
                $(".newResv").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '管理员新建预约',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewICResv.aspx?op=set&dwID=' + groupID
                    });
                });
                
                $("table").delegate(".DevUseGroup", "click", function () {
                   
                });
                
                $(".DevUseRec").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                  
                    fdata = "szGetKey=" + devID + '&devName=' + devName;
                    TabInJumpReload("devUseRec", fdata);
                });
                $(".DevTestData").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    urlPar = [["szGetKey", devID], ['devName', (devName)]];
                    fdata = "szGetKey=" + devID + '&devName=' + devName;
                    TabInJumpReload("devTestData", fdata);
                });
                $(".ListTbl").UniTable();               
            });
        </script>
        <style>
            #tbSearch
            {
                border-width: 1px;                
                border-color: #d1c1c1;                
                cursor: hand;
            }
            .thCenter
            {
                text-align:center;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:8px;
            }

        </style>
    </form>
</asp:Content>
