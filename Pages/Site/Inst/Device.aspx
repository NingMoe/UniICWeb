<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Device.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>管理</h2>
        <div class="toolbar">
            <div class="tb_info">总数：30台，使用中：5台，报修：1台</div>
            <div id="OPTDCTRL" class="OPTD"></div>
            <div class="FixBtn"><a>新建<%=ConfigConst.GCDevName %></a></div>
            <div class="tb_btn">
                <div class="AdvOpts" width="350" height="250" page="DeviceAdvOpts.aspx">
                    <div class="AdvLab">高级选项</div>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">编号</th>
                        <th>计算机名</th>
                         <th>IP地址</th>
                        <th name="szKindName">所属<%=ConfigConst.GCKindName %></th>
                        <th>型号</th>
                        <th>规格</th>
                        <th>所属<%=ConfigConst.GCRoomName %></th>
                        <th>所属<%=ConfigConst.GCLabName %></th>
                        <th>使用者</th>
                        <th>状态</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a href="#" title="发消息"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="设置使用人员"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" title="锁定"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="解锁"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" title="远程开机"><img src="../../../themes/icon_s/17.png"/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#OPTDCTRL").html('<div class="OPTDBtnSet">\
                            <a href="javascript:fAllOp("open")" title="批量新建"><img src="../../../themes/icon_s/6.png"/></a>\
                            <a href="javascript:fAllOp("shutdown")" title="免登陆"><img src="../../../themes/icon_s/15.png"/></a>\
                            <a href="javascript:fAllOp("restart")" title="需要登陆"><img src="../../../themes/icon_s/13.png"/></a>\
                            <a href="javascript:fAllOp("nologin")" title="设置状态"><img src="../../../themes/icon_s/21.png"/></a>\
                            <a href="javascript:fAllOp("neddlogin")" title="设置使用人员"><img src="../../../themes/icon_s/3.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="设备卸载客户端"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="发送消息"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="开机"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="关机"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="重启"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="U盘锁定"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="U盘解锁"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="光驱锁定"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="光驱解锁"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="卸载客户端"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="锁定"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="解锁"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: "5", minWidth: "175", maxWidth: "175", minHeight: "25", maxHeight: "95", speed: 50
                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().children().first().val();
                  
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevice.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                }, '提示', 1, function () { });
            });
                $(".FixBtn").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevice.aspx?op=new'
                    });
                });
            });
            $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
        </script>
    </form>
</asp:Content>
