<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResearchTest.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCReachTestName%>管理</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
            <div class="tb_info"><!--总数：5个，正常：5个，异常：0个，使用中：1个--></div>
             
            <div class="FixBtn">
                <a id="btnDevCls">新建<%=ConfigConst.GCReachTestName%></a>
            <a id="importResearch"> <%=ConfigConst.GCReachTestName%>导入</a>
            </div>

            <div class="tb_btn">               
            </div>
        </div>  
           <div class="toolbar" style="margin: 10px">
            <div class="tb_infoInLine">
                <input type="hidden" id="dwTutorID" name="dwTutorID" />
                <input type="hidden" id="dwDeptID" name="dwDeptID" />
                <table style="width: 99%">
                    <tr>
                        <th><%=ConfigConst.GCReachTestName %>名称:</th>
                        <td>
                            <input type="text" name="szRTName" id="szRTName" /></td>
                        <th><%=ConfigConst.GCTutorName %>:</th>
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                        <th>级别:</th>
                        <td>
                           <select id="dwRTLevel" name="dwRTLevel">
                               <%=szStatus %>
                           </select></td>
                        <th>
                            <input type="submit" id="btn" value="查询" /></th>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRTSN"><%=ConfigConst.GCReachTestName %>编号</th>
                        <th name="szRTName"><%=ConfigConst.GCReachTestName %>名称</th>
                        <th name="szTutorName"><%=ConfigConst.GCTutorName %></th>
                        <th ><%=ConfigConst.GCLeadName %></th>
                        <th >承担<%=ConfigConst.GCDeptName %></th>
                        
                        <th >下达时间</th>
                        <th >下达<%=ConfigConst.GCDeptName %></th>
                        <th >级别</th>
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
                AutoUserByIdent($("#szTrueName"), 1, $("#dwTutorID"));
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="setDevkindBtn" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a class="DelbtnDevKind" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });             
                $("#btnDevCls").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '720px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewResearchTest.aspx?op=new'
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改',
                        width: '780px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetResearchTest.aspx?op=set&id=' + dwDevKind
                    });
                });
                $("#importResearch").click(function () {
               
                    $.lhdialog({
                        title:'导入',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/importResearchTest.aspx?op=new'
                });
                  });
                $(".DelbtnDevKind").click(function () {                    
                    var devKindID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                }, '提示', 1, function () { });
                 });
                $(".ListTbl").UniTable();
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
        <style>
            .tb_infoInLine table td input{
            margin-left:10px;
            }
            .tb_infoInLine table td select{
            margin-left:10px;
            }
            
        </style>
    </form>
</asp:Content>
