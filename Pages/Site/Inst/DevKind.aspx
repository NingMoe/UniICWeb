<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevKind.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCKindName%>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
            <div class="tb_info">总数：5个，正常：5个，异常：0个，使用中：1个</div>
             
            <div id="btnDevKind" class="FixBtn"><a>新建<%=ConfigConst.GCKindName%></a></div>
            <div class="tb_btn">               
            </div>
        </div>  
         
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">名称</th>
                        <th>开放数目</th>
                        <th>总数</th>
                        <th>最少使用人数</th>
                        <th>最多使用人数</th>
                        <th>型号</th>
                        <th>规格</th>
                         <th>属性</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="setDevkindBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a class="DelbtnDevKind" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                            <a class="InfoDevKindBtn" href="#" title="修改介绍"><img src="../../../themes/icon_s/11.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });             
                $("#btnDevKind").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
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
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevKind.aspx?op=set&id=' + dwDevKind
                    });
                });
                $(".DelbtnDevKind").click(function () {                    
                    var devKindID = $(this).parents("tr").children().first().children().first().val();                  
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
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
