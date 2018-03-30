<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveSeatList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindSeat %>ԤԼ״��</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                 <%if((ConfigConst.GCSysKind&1)>0) {%>
         <a href="ReserveRoomList.aspx"><%=ConfigConst.GCSysKindRoom %>ԤԼ״��</a>
         <%} %>
        <%if((ConfigConst.GCSysKind&2)>0) {%>
         <a href="ReservePCList.aspx"><%=ConfigConst.GCSysKindPC %>ԤԼ״��</a>
         <%} %>
        <%if((ConfigConst.GCSysKind&4)>0) {%>
         <a href="ReserveLendList.aspx"><%=ConfigConst.GCSysKindLend %>ԤԼ״��</a>
         <%} %>
        <%if((ConfigConst.GCSysKind&8)>0) {%>
         <a href="ReserveSeatList.aspx"><%=ConfigConst.GCSysKindSeat %>ԤԼ״��</a>
         <%} %>             
            </div>
    
    </div>
         
        </div> 
            <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>��ʼ����:</th>
                            <td><input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>��������:</th>
                            <td> <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>ѧ����:</th>
                            <td><input type="text" name="dwPID" id="dwPID" /></td>
                            <th>��λ����:</th>
                            <td><input type="text" name="devName" id="devName" /></td>
                          
                        </tr>
                         <tr>
                               <th>״̬</th>
                        <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">ȫ��</label>                           
                            <label><input class="enum" value="512" type="radio" name="dwCheckStat">��Ч��</label>
                         <label>
                                <input class="enum" value="262144" type="radio" name="dwCheckStat">ΥԼ</label>
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">�ѽ���</label>
                        </td>
                        </tr>
                        <tr>
                            <th colspan="4">
                                <input type="submit" id="btnOK" value="��ѯ" style="height:25px" />
                                 <input type="button" id="export" value="����" style="height: 25px" />
                            </th>
                        </tr>
                    </table>
             
            </div>
       </div>   
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ԤԼ��</th>
                        <th>������</th>                        
                        <th>��λ����</th>                      
                        <th>����<%=ConfigConst.GCLabName %></th>
                        <th>״̬</th>
                        <th name="dwOccurTime">�ύʱ��</th>
                        <th>����ʱ��</th>
                        <th width="25px"></th>
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
                $("#btnOK").button();
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#devName"), 1, $("#szGetKey"), 8, null, null, null);
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="adminCredit" title="�˹�ΥԼ"><img src="../../../themes/icon_s/19.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                     $("#export").button().click(function () {
                    var dwCheckStat = $("input[name='dwCheckStat']:checked").val();
                    var szGetKey = $("#szGetKey").val();
                    var dwPID = $("#dwPID").val();
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    $.lhdialog({
                        title: '������¼',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SeatResvExport.aspx?op=set&dwCheckStat=' + dwCheckStat + '&szGetKey=' + szGetKey + '&dwPID=' + dwPID  + '&dwStartDate=' + dwStartDate + '&dwEndDate=' + dwEndDate
                    });
                });
                $(".adminCredit").click(function () {
                    var delBtn = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�˹�ΥԼ',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/resvOut.aspx?id=' + delBtn
                    });
                });
                $(".delLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
                });
                $("input[name='dwCheckStat']").click(function () {
                   // pForm.data("UniTab_tab1", "reserve.aspx?dwCheckStat=" + $("input[name='dwCheckStat']").val());
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                  });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewLab.aspx?op=new'
                });
            });
            $(".ListTbl").UniTable();

        });
        </script>
          <style>
            #tbSearch
            {
                border-width: 1px;                
                border-color: #d1c1c1;                
                cursor: hand;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:20px;
            }

        </style>
    </form>
</asp:Content>
