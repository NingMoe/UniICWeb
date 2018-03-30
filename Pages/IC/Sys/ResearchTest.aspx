<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResearchTest.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCReachTestName%>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
            <div class="tb_info"><!--������5����������5�����쳣��0����ʹ���У�1��--></div>
             
            <div class="FixBtn">
                <a id="btnDevCls">�½�<%=ConfigConst.GCReachTestName%></a>
            <a id="importResearch"> <%=ConfigConst.GCReachTestName%>����</a>
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
                        <th><%=ConfigConst.GCReachTestName %>����:</th>
                        <td>
                            <input type="text" name="szRTName" id="szRTName" /></td>
                        <th><%=ConfigConst.GCTutorName %>:</th>
                        <td>
                            <input type="text" name="szTrueName" id="szTrueName" /></td>
                        <th>����:</th>
                        <td>
                           <select id="dwRTLevel" name="dwRTLevel">
                               <%=szStatus %>
                           </select></td>
                        <th>
                            <input type="submit" id="btn" value="��ѯ" /></th>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szRTSN"><%=ConfigConst.GCReachTestName %>���</th>
                        <th name="szRTName"><%=ConfigConst.GCReachTestName %>����</th>
                        <th name="szTutorName"><%=ConfigConst.GCTutorName %></th>
                        <th ><%=ConfigConst.GCLeadName %></th>
                        <th >�е�<%=ConfigConst.GCDeptName %></th>
                        
                        <th >�´�ʱ��</th>
                        <th >�´�<%=ConfigConst.GCDeptName %></th>
                        <th >����</th>
                        <th width="25px">����</th>
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
                            <a  class="setDevkindBtn" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a class="DelbtnDevKind" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });             
                $("#btnDevCls").click(function () {
                    $.lhdialog({
                        title: '�½�',
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
                        title: '�޸�',
                        width: '780px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetResearchTest.aspx?op=set&id=' + dwDevKind
                    });
                });
                $("#importResearch").click(function () {
               
                    $.lhdialog({
                        title:'����',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/importResearchTest.aspx?op=new'
                });
                  });
                $(".DelbtnDevKind").click(function () {                    
                    var devKindID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                }, '��ʾ', 1, function () { });
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
