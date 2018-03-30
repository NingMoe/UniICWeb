<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Room.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCRoomName%>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
            <div class="tb_info">
                 <div class="UniTab" id="tabl">
                    <a href="device.aspx" id="deviceTab"><%=ConfigConst.GCDevName %>管理</a>
                    <a href="devkind.aspx" id="devkindTab"><%=ConfigConst.GCKindName%>管理</a>
                <a href="room.aspx" id="roomTab"><%=ConfigConst.GCRoomName%>管理</a>
                <a href="lab.aspx" id="labTab"><%=ConfigConst.GCLabName%>管理</a>
                     <%if(ConfigConst.GroomNumMode==1) {%><a href="manGroup.aspx" id="A1">管理员组管理</a><%} %>
                </div>
            </div>
             
            <div class="FixBtn">
               
             <a id="btnNewRoom">新建<%=ConfigConst.GCRoomName%></a>
            </div>
            <div class="tb_btn">             
            </div>
        </div>  
          <div style="margin-top:8px;">
                <input type="button" value="批量修改开放规则" id="setOpenRUle" />
          
        </div>
              <div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                    <th><%=ConfigConst.GCLabName %>名称:</th>
                   <td> <select id="lab" name="lab">
                    <%=m_szLab %>
                </select></td>
                  
                   <th><%=ConfigConst.GCRoomName %>名称:</th>
                   <td><input type="text" id="szRoomName" name="szRoomName" /></td>
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
        </div>

        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px" name="szRoomNo">编号</th>
                        <th name="szRoomName"><%=ConfigConst.GCRoomName%>名称</th>
                        <th name="szLabName">所属<%=ConfigConst.GCLabName %></th>
                        <th><%=ConfigConst.GCDevName %>数</th>
                        <th>空闲<%=ConfigConst.GCDevName %>数</th>
                        <th name="szOpenRuleName">开放规则</th>
                        <th name="szRoomNo">门禁状态</th> 
                        <th name="dwManMode">控制方式</th>
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
                var tabl = $(".UniTab").UniTab();
                $("#btn").button();
                $(".opt").css({ width: "120px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $("#setOpenRUle").button();
                function fAllOp(op) {
                }

                $("#setOpenRUle").click(function () {
                    var vSelectName = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            var vid = $(this).parents("td").data("id");
                            vSelectName = vSelectName + vid + ",";
                        }
                    });

                    if (vSelectName == "") {
                        return;
                    }

                    $.lhdialog({
                        title: '批量修改开放规则',
                        width: '400px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setRoomOpenRuleList.aspx?op=set&id=' + vSelectName
                    });
                });
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
                        content: 'url:Dlg/NewOnlyRoom.aspx?op=new'
                    });
                });
                $(".InfoRoomBtn").click(function () {                  
                    var dwRoomID = $(this).parents("tr").children().first().children().first().val();                   
                    $.lhdialog({
                        title: '介绍',
                        width: '720px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwRoomID + "&type=RoomInfo"
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
                        content: 'url:Dlg/SetOnlyRoom.aspx?op=set&roomid=' + dwRoomID
                    });
                });

				 $(".manGroupList").click(function () {
                 var groupID = $(this).parents("tr").children().first().data("mangroupid");
                    $.lhdialog({
                        title: '管理员名单',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../inst/Dlg/SetUseGroup.aspx?op=set&id=' + groupID
                    });
                });
                $(".delRoomBtn").click(function () {
                    var dwRoomID = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwRoomID);
                }, '提示', 1, function () { });
                  });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false, });

            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
