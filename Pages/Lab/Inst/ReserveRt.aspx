<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveRt.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>ʹ��ԤԼ����</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
         
        </div>
         <div>
           
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ʵ������</th>
                        <th>����������</th>
                        <th><%=ConfigConst.GCTutorName%>����</th>
                        <th><%=ConfigConst.GCDevName %></th>
                          <th>����Ա</th>
                        <th>����<%=ConfigConst.GCLabName %></th>
                        <th>״̬</th>
                        <th name="dwOccurTime">�ύʱ��</th>
                        <th>����ʱ��</th>
                       
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
                $(".OPTD1").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="���"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTD1024").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="Ԥ�շ�"><img src="../../../themes/icon_s/17.png"/></a></div>');
                $(".OPTD512").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="��������ʱ��"><img src="../../../themes/icon_s/18.png"/></a></div>');
                $(".OPTD524288").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="����"><img src="../../../themes/icon_s/19.png"/></a></div>');
                $(".OPTD2097152").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="����"><img src="../../../themes/icon_s/6.png"/></a></div>');
                $(".OPTD4194304").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="��ӡ"><img src="../../../themes/icon_s/11.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "25", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTD1").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���',
                        width: '700px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkbg.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".OPTD1024").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: 'Ԥ�շ�',
                        width: '600px',
                        height: '350px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkbill.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".OPTD524288").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '����',
                        width: '700px',
                        height: '680px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkPayBill.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".OPTD2097152").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '����',
                        width: '650px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/BillRecevie.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".OPTD4194304").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '��ӡ',
                        width: '650px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/printBill.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".OPTD512").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '��������ʱ��',
                        width: '600px',
                        height: '350px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/changendtime.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".InfoLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '����',
                        width: '760px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwLabID + "&type=LabInfo"
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
            //$(".ListTbl").UniTable();

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
