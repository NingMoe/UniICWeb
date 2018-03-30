<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="manGroup.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>管理员组管理</h2>
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
        <div class="FixBtn"><a id="btnResvRule">新建管理员组</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
                <tr>
            <th>租名称</th>
                <td> <input type="text" name="szName" id="szName" /></td>
                    <td><input type="submit" id="btn" value="查询" /></td>
                    </tr>
           </table>
               </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>组名称</th><th>成员</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>   
          <uc1:PageCtrl runat="server" ID="PageCtrl" />    
    </div>
    <script type="text/javascript">
        $(function () {
            $(".UniTab").UniTab();
            $("#btn").button();
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="manGroupList" title="修改/添加成员"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnResvRule").click(function () {
                $.lhdialog({
                    title: '新建管理员组',
                    width: '620px',
                    height: '200px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewManGroup.aspx?op=new'
                });
            });
            $(".setResvRuleBtn").click(function () {
                
                var dwGroup = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改开放对象',
                    width: '620px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/setopenGroup.aspx?op=set&dwID=' + dwGroup
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
            $(".delResvRuleBtn").click(function () {
                var groupID = $(this).parents("tr").children().first().data("mangroupid");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + groupID);
                }, '提示', 1, function () { });
              });
        });
    </script>
</form>
</asp:Content>