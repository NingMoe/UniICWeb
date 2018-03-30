<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Reserve.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>ԤԼ�����</h2>
        <div class="toolbar">
            
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="reserve.aspx" id="devList">ԤԼ�����</a>
                <a href="reserveRec.aspx" id="devUseRec">ԤԼ��¼</a>
            </div>
    
    </div>
         
        </div>
         <div>
            <table id="tbSearch" style="border:1px solid #d1c1c1">             
                 <tr>
                    <td  style="width:20%; text-align:center">ԤԼ״̬</td>
                    <td style="width:75%;text-align:left">
                        <LABEL><INPUT class="enum" value="8" type="radio" name="dwCheckStat" checked="checked" > ������Ա���</LABEL>
                        <!--<LABEL><INPUT class="enum" value="1024" type="radio" name="dwCheckStat" > ��Ԥ�շ�</LABEL>-->
                        <LABEL><INPUT class="enum" value="512" type="radio" name="dwCheckStat" > ������Ч�����ʱ��</LABEL>
                        <LABEL><INPUT class="enum" value="524288" type="radio" name="dwCheckStat" > ������</LABEL>    
                        <LABEL><INPUT class="enum" value="1048576" type="radio" name="dwCheckStat" > ��ӡУ��ת��ƾ֤</LABEL>                      
                     <!--   <LABEL><INPUT class="enum" value="2097152" type="radio" name="dwCheckStat" > ������</LABEL>-->
                    </td>
                    </tr>
               
            </table>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ʵ������</th>
                        <th>����������</th>
                        <th><%=ConfigConst.GCTutorName %>����</th>
                        <th><%=ConfigConst.GCDevName %></th>
                      
                        <th>����<%=ConfigConst.GCLabName %></th>
                        <th>״̬</th>
                        <th name="dwOccurTime">�ύʱ��</th>
                        <th>����ʱ��</th>
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
         <!--   <uc1:PageCtrl runat="server" ID="PageCtrl" />-->
        </div>
        <script type="text/javascript">

            $(function () {
                var tabl = $(".UniTab").UniTab();
                $(".OPTD1").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="���"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTD512").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="��������ʱ��"><img src="../../../themes/icon_s/18.png"/></a></div>');
                $(".OPTD524288").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="����"><img src="../../../themes/icon_s/19.png"/></a></div>');
                $(".OPTD1048576").html('<div class="OPTDBtn">\
                 <a class="btnPrit" title="��ӡ���㵥 "><img src="../../../themes/icon_s/11.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
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
                $(".btnRuZhang").click(function () {
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
                $(".btnPrit").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '��ӡ',
                        width: '700px',
                        height: '600px',
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
