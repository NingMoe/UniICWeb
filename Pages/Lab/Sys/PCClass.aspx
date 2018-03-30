<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PCClass.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCClassName %>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
          <div class="tb_info">
                <div class="UniTab" id="tabl">
                     <a href="syskindPC.aspx" id="syskindPC"><%=ConfigConst.GCSysKindPC%>����</a>
                    <a href="PCRoom.aspx" id="PCRoom"><%=ConfigConst.GCSysKindPC%>��������</a>
                    <a href="PCKind.aspx" id="PCKind"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="PCClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                </div>
            </div>
            <div id="btnDevCls" class="FixBtn"><a>�½�<%=ConfigConst.GCClassName%></a></div>         
        </div>  
         
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="70%">����</th>
                        <th >��ע</th>
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
                var tabl = $(".UniTab").UniTab();
                function fAllOp(op) {
                   
                }                              
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
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevCLS.aspx?op=new&dwKind=2'
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '780px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevCLS.aspx?op=set&kind=2&id=' + dwDevKind
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
        </script>
    </form>
</asp:Content>