<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomGroup.aspx.cs" Inherits="Sub_Lab"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2><%=ConfigConst.GCRoomName %>组合管理</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNew">新建<%=ConfigConst.GCRoomName %>组合</a></div>
        <div class="tb_btn">
               
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>名称</th><th>房间数目</th><th>房间</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
      
    </div>
    <script type="text/javascript">
      
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delBtn"  href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".delBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                debugger;
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '提示', 1, function () { });
             });
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '新建房间组合' ,
                    width: '660px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewRoomGroup.aspx?op=new'
                });
            });
          
            
        });
    </script>
</form>
</asp:Content>