<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SysKindSeat.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindSeat %>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />

        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    
                   <a href="syskindSeat.aspx" id="syskindSeat"><%=ConfigConst.GCSysKindSeat%>管理</a>
                    <a href="SeatRoom.aspx" id="SeatRoom"><%=ConfigConst.GCSysKindSeat%>所在区域</a>
                    <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="seatLab.aspx" id="seatLab">区域所在<%=ConfigConst.GCLabName %></a>
                    <%} %>
                    <a href="SeatKind.aspx" id="SeatKind"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="SeatClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleSeat.aspx?kind=64" id="resvrule">预约规则</a>
                </div>
            </div>

            <div class="FixBtn">               
                <a id="export">导出</a>
                    <a id="btnNew">新建<%=ConfigConst.GCSysKindSeat %></a>
                <a id="newList">批量新建</a>
                <!--
                 <select class="opt" id="lab" name="lab">
                    <%=m_szLab %>
                </select>
                --> 
                <select class="opt" id="room" name="room">
                    <%=m_szRoom %>
                </select>
                 <select class="opt" id="kind" name="kind">
                    <%=m_szDevKind %>
                </select>
            </div>

        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>         
                        <th name="dwDevSN">座位序号</th>                       
                        <th name="szDevName">座位名称</th> 
                        <th name="dwDevStat">状态</th>                       
                        <th name="szKindName">所属<%=ConfigConst.GCKindName %></th>                                              
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>            
                       <!-- <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>-->
                        <th name="szAssertSN">资产号</th>      
                        <th>智能卡绑定</th>
                        <th width="25px"></th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">

            $(function () {
                $("#export").button().click(function () {
                    $.lhdialog({
                        title: '导出',
                        width: '150px',
                        height: '70px',
                        lock: true,
                        content: 'url:Dlg/DevListExport.aspx?op=new'
                    });
                });
                var tabl = $(".UniTab").UniTab();
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="print" href="#" title="查看二维码"><img src="../../../themes/iconpage/film.png"/></a>\
                    <a class="setUnUse" href="#" title="禁用"><img src="../../../themes/iconpage/psrest.png"/></a>\
                    <a class="setUable" href="#" title="启用"><img src="../../../themes/iconpage/record.png""/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".print").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var labid = $(this).parents("tr").children().first().data("labid");
                    var name = $(this).parents("tr").children().first().data("name");
                    window.open("http://update.unifound.net/wxnotice/qrcode.aspx?pcid=" + labid + "&id=" + dwDevID + "&session=Seat&msg=" + name)

                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetSeatDev.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".setUnUse").click(function () {
                    var devid = $(this).parents("tr").children().first().data("id");
                    var devid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定禁用?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=unable&id=" + devid);
                    }, '提示', 1, function () { }); c

                  });
                $(".setUable").click(function () {
                    var devid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定启用?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=usable&id=" + devid);
                    }, '提示', 1, function () { });

                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var dwLabID = $(this).parents("tr").children().first().next().data("labid");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID + "&delParentID=" + dwLabID);
                    }, '提示', 1, function () { });
                });
                $("#btnNew").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewSeatDev.aspx?op=new'
                    });
                });
                $("#newList").click(function () {
                    $.lhdialog({
                        title: '批量新建',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDeviceSeatList.aspx?op=new'
                    });
                });

            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false });

        </script>
    </form>
</asp:Content>
