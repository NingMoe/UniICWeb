<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveRoomList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindRoom %>ԤԼ״��</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <%if ((ConfigConst.GCSysKind & 1) > 0)
                      {%>
                    <a href="ReserveRoomList.aspx"><%=ConfigConst.GCSysKindRoom %>ԤԼ״��</a>
                    <%} %>
                    <%if ((ConfigConst.GCSysKind & 2) > 0)
                      {%>
                    <a href="ReservePCList.aspx"><%=ConfigConst.GCSysKindPC %>ԤԼ״��</a>
                    <%} %>
                    <%if ((ConfigConst.GCSysKind & 4) > 0)
                      {%>
                    <a href="ReserveLendList.aspx"><%=ConfigConst.GCSysKindLend %>ԤԼ״��</a>
                    <%} %>
                    <%if ((ConfigConst.GCSysKind & 8) > 0)
                      {%>
                    <a href="ReserveSeatList.aspx"><%=ConfigConst.GCSysKindSeat %>ԤԼ״��</a>
                    <%} %>
                </div>

            </div>

        </div>
         <div style="margin:10px;width:99%;">  
             <%if (ConfigConst.GCICTypeMode==1) {%>
             <a id="newAllResv" class="newResv">ȫ��ԤԼ</a> 
           <a id="newTeachAllResvImport" class="resvImport">����ȫ��ԤԼ</a>   
             <%} else { %>
           <a id="newAllResvImport" class="resvImport">����ԤԼ</a>   
             <%} %>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                    <tr>
                        <th>��ʼ����:</th>
                        <td>
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                        <th>��������:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>ѧ����:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th><%=ConfigConst.GCSysKindRoom %>����:</th>
                        <td>
                            <input type="text" name="devName" id="devName" /></td>

                    </tr>
                    <tr>
                        <th>״̬</th>
                        <td colspan="3">
                            <label>
                                <input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">ȫ��ԤԼ</label>
                            <label>
                                <input class="enum" value="1" type="radio" name="dwCheckStat">������Ա���</label>
                            <label>
                                <input class="enum" value="2" type="radio" name="dwCheckStat">���ͨ��</label>
                            <label>
                                <input class="enum" value="4" type="radio" name="dwCheckStat">��˲�ͨ��</label>
                            <label>
                                <input class="enum" value="512" type="radio" name="dwCheckStat">��Ч��</label>
                            <label>
                                <input class="enum" value="262144" type="radio" name="dwCheckStat">ΥԼ</label>
                            <label>
                                <input class="enum" value="1073741824" type="radio" name="dwCheckStat">�ѽ���</label>
                        </td>
                    </tr>
                       <tr>
                        <th style="width:100px;"><%=ConfigConst.GCKindName %>����</th>
                        <td colspan="3">
                          <%=szKindStr %>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="��ѯ" style="height: 25px" />
                            <input type="button" id="export" value="����" style="height: 25px" />
                        </th>
                    </tr>
                </table>

            </div>
        </div>
        <div class="content" style="margin-top: 10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ԤԼ��</th>
                        <th>������</th>
                        <th><%=ConfigConst.GCSysKindRoom %>����</th>
                        <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th>״̬</th>
                        <th name="dwOccurTime">�ύʱ��</th>
                        <th name="dwBeginTime">����ʱ��</th>
                        <th>ԤԼ��Ա</th>
                        <th>ʹ������</th>
                        <th>����</th>  
                        <th>����˵��</th>
                        <th>����</th>
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
                $("#newAllResv").button().click(function () {
                    $.lhdialog({
                        title: 'ȫ��ԤԼ',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/allresv.aspx?op=set'
                    });
                });
                $("#newAllResvImport").button().click(function () {
                    $.lhdialog({
                        title: '����ԤԼ',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newAllResvImport.aspx?op=set'
                    });
                });
                $("#newTeachAllResvImport").button().click(function () {
                    $.lhdialog({
                        title: '����ȫ��ԤԼ',
                        width: '750px',
                        height: '420px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newTeachAllResvImport.aspx?op=set'
                    });
                });
                $("#export").button().click(function () {
                    var dwCheckStat = $("input[name='dwCheckStat']:checked").val();
                    var szGetKey = $("#szGetKey").val();
                    var dwPID = $("#dwPID").val();
                    var dwDevKind = $("#dwDevKind").val();
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    $.lhdialog({
                        title: '������¼',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/RoomResvExport.aspx?op=set&dwCheckStat=' + dwCheckStat + '&szGetKey=' + szGetKey + '&dwPID=' + dwPID + '&dwDevKind=' + dwDevKind + '&dwStartDate=' + dwStartDate + '&dwEndDate=' + dwEndDate
                    });
                });

                
                
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $(".OPTDGet").html('<div class="OPTDBtn">\
                <a class="GetResv" title="�鿴"><img src="../../../themes/icon_s/17.png"/></a></a>\
                <a class="setResv" title="�޸�"><img src="../../../themes/icon_s/18.png"/></a>\
                <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a>\</div>');

                $(".OPTDCheck").html('<div class="OPTDBtn">\
                       <a class="OPTDCheck" title="���"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="GetResv" title="�鿴"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a class="setResv" title="�޸�"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a></div>');

                $(".OPTDCheckDel").html('<div class="OPTDBtn">\
                        <a class="OPTDCheck" title="���"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a class="GetResv" title="�鿴"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a class="setResv" title="�޸�"><img src="../../../themes/icon_s/18.png"/></a>\
                        <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a></div>');

                $(".OPTDDel").html('<div class="OPTDBtn">\
                       <a class="GetResv" title="�鿴"><img src="../../../themes/icon_s/17.png"/></a>\
                       <a class="OPTDCheck" title="���"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="setResv" title="�޸�"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="adminCredit" title="�˹�ΥԼ"><img src="../../../themes/icon_s/19.png"/></a></div>');
                $(".OPTDCheckok").html('<div class="OPTDBtn">\
                       <a class="GetResv" title="�鿴"><img src="../../../themes/icon_s/17.png"/></a>\
                       <a class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="setResv" title="�޸�"><img src="../../../themes/icon_s/18.png"/></a>\
                       <a class="adminCredit" title="�˹�ΥԼ"><img src="../../../themes/icon_s/19.png"/></a></div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDCheck").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CheckResv.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".GetResv").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�鿴',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvGet.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".delBtn").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwResvID);
                    }, '��ʾ', 1, function () { });
                });
                $(".adminCredit").click(function () {
                    var delBtn =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�˹�ΥԼ',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/resvOut.aspx?id=' + delBtn
                    });
                });
                $(".beforeDone").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '��ǰ����',
                        width: '350px',
                        height: '200px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvBeforeDone.aspx?op=beforeDone&delID=' + dwResvID
                    });
                });
                $(".resvRdit").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ����ǰ����?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=resvRdit&delID=" + dwResvID);
                    }, '��ʾ', 1, function () { });
                   });
                
                $("input[name='dwCheckStat']").click(function () {
                    // pForm.data("UniTab_tab1", "reserve.aspx?dwCheckStat=" + $("input[name='dwCheckStat']").val());
                   // TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
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
                $(".setResv").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetICResv.aspx?op=set&resvid=' + dwResvID
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
        <style>
            #tbSearch {
                border-width: 1px;
                border-color: #d1c1c1;
                cursor: hand;
            }

                #tbSearch td {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;
                }

            td input {
                margin-left: 20px;
            }
        </style>
    </form>
</asp:Content>
