<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Room.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCRoomName%>列表</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
          <div class="toolbar">
               <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="room.aspx" id="room">实验室列表</a>
                    <a href="LabCodeTable.aspx?dwCodeType=1" id="LabCodeTable1">实验室类型管理</a>
                    <a href="LabCodeTable.aspx?dwCodeType=2" id="LabCodeTable2">实验室经费来源管理</a>
                    <a href="LabCodeTable.aspx?dwCodeType=3" id="LabCodeTable3">实验室建设水平管理</a>
                </div>
            </div>

            <div class="FixBtn"> <a id="btnNewRoom">新建<%=ConfigConst.GCRoomName%></a></div>
           
        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRoomNo">编号</th>
                        <th name="szRoomName"><%=ConfigConst.GCRoomName%>名称</th>
                        <th name="dwCreateDate">建成时间</th>
                        <th name="szLabKindCode">类型</th>
                        <th name="szFloorNo">位置</th>
                        <th name="szLabFromCode">经费来源</th>
                        <th name="szAcademicSubjectCode">学科</th>
                        <th name="szLabLevelCode">建设水平</th>
                        <th name="szDeptName">单位</th>
                        <th>负责人</th>
                        <th>平面图</th>
                        <th>插图</th>
                        <th width="25px" title="设置开放规则" style="text-align:center"></th>
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <style>
            .InfoLabBtn2 {
                cursor:pointer;
            }
            .InfoLabBtn{
                cursor:pointer;
            }
        </style>
        <script type="text/javascript">
           
            $(function () {
                var tabl = $(".UniTab").UniTab();

                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });

                function fAllOp(op) {
                }

                $(".class1").html('<div class="OPTDBtn">\
                            <a href="#"  class="setResvRuleBtn" title="设置开放规则"><img src="../../../themes/icon_s/6.png"/></a>\</div>');
                $(".class2").html('<div class="OPTDBtn">\
                            <a  class="setRoomBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="delRoomBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                            <a href="#"  class="manGroupList" title="管理员名单"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#OPTDCTRL").html('<div class="OPTDBtnSet">\
                            <a href="javascript:fAllOp("open")" title="远程开门"><img src="../../../themes/icon_s/6.png"/></a>\
                            <a href="javascript:fAllOp("shutdown")" title="设备关机"><img src="../../../themes/icon_s/15.png"/></a>\
                            <a href="javascript:fAllOp("restart")" title="设备重启"><img src="../../../themes/icon_s/13.png"/></a>\
                            <a href="javascript:fAllOp("nologin")" title="设备免登陆"><img src="../../../themes/icon_s/21.png"/></a>\
                            <a href="javascript:fAllOp("neddlogin")" title="设备需要登陆"><img src="../../../themes/icon_s/3.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="设备卸载客户端"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: 10, minWidth: "80", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnNewRoom").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewRoom.aspx?op=new'
                    });
                });
                $(".InfoLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + dwLabID + "&type=hard")
                });
                $(".InfoLabBtn2").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + dwLabID + "&type=hard2")
                });
                
                $(".setResvRuleBtn").click(function () {
                    var openruleSN = $(this).parents("tr").children().first().data("openid");
                    $.lhdialog({
                        title: '修改开放规则',
                        width: '920px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewOpenRule.aspx?op=set&dwID=' + openruleSN
                    });
                });
               
                $(".setRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewRoom.aspx?op=set&roomid=' + dwRoomID
                    });
                });
                $(".manGroupList").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("mangroupid");
                    $.lhdialog({
                        title: '管理员名单',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwRoomID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                }, '提示', 1, function () { });
                  });
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false});
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
