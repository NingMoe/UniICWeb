<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Room.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCRoomName%>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
            <div class="tb_info">总数：5个，正常：5个，异常：0个，使用中：1个</div>
             
            <div id="btnNewRoom" class="FixBtn"><a>新建<%=ConfigConst.GCRoomName%></a></div>
            <div class="tb_btn">
                <div class="AdvOpts" page="RoomAdvOpts.aspx">
                    <div class="AdvLab">高级选项</div>
                </div>
                 
            </div>
        </div>  
         
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">编号</th>
                        <th><%=ConfigConst.GCRoomName%>名称</th>
                        <th>所属<%=ConfigConst.GCLabName %></th>
                        <th><%=ConfigConst.GCDevName %>数</th>
                        <th>空闲<%=ConfigConst.GCDevName %>数</th>
                        <th>开放规则</th>
                        <th>门禁状态</th> 
                        <th>控制方式</th>
                        <th width="25px">操作</th>
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
                function fAllOp(op) {
                   
                }                              
                $(".class2").html('<div class="OPTDBtn">\
                            <a  class="setRoomBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                            <a href="#" title="管理员名单"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="#" title="刷卡记录"><img src="../../../themes/icon_s/11.png"/></a>\
                            <a href="#" title="视频记录"><img src="../../../themes/icon_s/17.png"/></a>\
                            <a class="InfoRoomBtn" href="#" title="修改介绍"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#OPTDCTRL").html('<div class="OPTDBtnSet">\
                            <a href="javascript:fAllOp("open")" title="远程开门"><img src="../../../themes/icon_s/6.png"/></a>\
                            <a href="javascript:fAllOp("shutdown")" title="设备关机"><img src="../../../themes/icon_s/15.png"/></a>\
                            <a href="javascript:fAllOp("restart")" title="设备重启"><img src="../../../themes/icon_s/13.png"/></a>\
                            <a href="javascript:fAllOp("nologin")" title="设备免登录"><img src="../../../themes/icon_s/21.png"/></a>\
                            <a href="javascript:fAllOp("neddlogin")" title="设备需要登录"><img src="../../../themes/icon_s/3.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="设备卸载客户端"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNewRoom").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewRoom.aspx?op=new'
                    });
                });
                $(".InfoRoomBtn").click(function () {                  
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();                   
                    $.lhdialog({
                        title: '介绍',
                        width: '760px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwRoomID + "&type=RoomInfo"
                    });
                });
                $(".setRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetRoom.aspx?op=set&roomid=' + dwRoomID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                }, '提示', 1, function () { });
                  });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
