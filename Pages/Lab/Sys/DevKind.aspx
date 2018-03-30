<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevKind.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCKindName%>管理</h2>
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
             
            <div id="btnDevKind" class="FixBtn"><a>新建<%=ConfigConst.GCKindName%></a></div>
            <div class="tb_btn">               
            </div>
        </div>  
          
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                  
                   <th><%=ConfigConst.GCKindName %>名称:</th>
                   <td><input type="text" name="szKindName" id="szKindName" /></td>
                   
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szKindName"><%=ConfigConst.GCKindName %>名称</th>
                     <!--   <th name="dwUsableNum" >开放数目</th>
                           <th name="dwMinUsers">最少使用人数</th>
                        <th name="dwMaxUsers">最多使用人数</th>
                         -->
                        <th><%=ConfigConst.GCDevName %>总数</th>
                         <th name="dwUsableNum" >开放数目</th>
                        <th name="szModel">型号</th>
                        <th name="szSpecification">规格</th>
                        <th name="dwNationCode">国别码</th>
                        <th name="szProducer">生产厂商</th>
                         <th name="dwProperty">属性</th>
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
                var tabl = $(".UniTab").UniTab();
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="setDevkindBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a class="DelbtnDevKind" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });             
                $("#btnDevKind").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevKind.aspx?op=new'
                    });
                });
                $(".InfoDevKindBtn").click(function () {                    
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '介绍',
                        width: '760px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwDevKind + "&type=DevKindInfo"
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '修改',
                        width: '780px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevKind.aspx?op=set&id=' + dwDevKind
                    });
                });
                $(".DelbtnDevKind").click(function () {                    
                    var devKindID = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                }, '提示', 1, function () { });
                 });
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
