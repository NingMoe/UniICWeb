<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SeatClass.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC%><%=ConfigConst.GCClassName %>����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
          <div class="tb_info">
                <div class="UniTab" id="tabl">
                   <a href="syskindSeat.aspx" id="syskindSeat"><%=ConfigConst.GCSysKindSeat%>����</a>
                    <a href="SeatRoom.aspx" id="SeatRoom"><%=ConfigConst.GCSysKindSeat%>��������</a>
                    <%if(ConfigConst.GCICLabRoom==1) {%>
                    <a href="seatLab.aspx" id="seatLab">��������<%=ConfigConst.GCLabName %></a>
                    <%} %>
                    <a href="SeatKind.aspx" id="SeatKind"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCKindName %></a>                    
                    <%if (ConfigConst.GCKindAndClass == 0)
                      {%>
                    <a href="SeatClass.aspx" id="PCClass"><%=ConfigConst.GCSysKindSeat%><%=ConfigConst.GCClassName %></a>
                    <%} %>
                     <a href="resvruleSeat.aspx?kind=64" id="resvrule">ԤԼ����</a>
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
                        content: 'url:Dlg/NewDevCLS.aspx?op=new&dwKind=8'
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
                        content: 'url:Dlg/SetDevCLS.aspx?op=set&kind=8&id=' + dwDevKind
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
