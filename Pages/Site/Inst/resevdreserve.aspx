<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="resevdreserve.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" name="szGetKey" id="szGetKey" />
        <h2>����Առ��</h2>
        <div class="toolbar">
            
        <div class="tb_info">
            <div class="UniTab" id="tabl">
               <a href="reserveYard.aspx" id="rtreserve">ԤԼ״��</a>
                 <a href="resevdreserve.aspx" id="resevdreserve">����Առ��</a>
            </div>
    
    </div>
         
        </div>
           <div style="float:right">
               <a id="btnNewResv">����Առ��<%=ConfigConst.GCDevName %></a></div>    
         <div>
            
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 99%">
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
                        <th><%=ConfigConst.GCDevName %>����:</th>
                        <td>
                            <input type="text" name="devName" id="devName" /></td>

                    </tr>
                    <tr>
                    <th>״̬</th>
                     <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">ȫ��</label>
                         
                            <label><input class="enum" id="idtest" value="512" type="radio" name="dwCheckStat">��Ч��</label>
                        
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">�ѽ���</label>
                           <label><input class="enum" value="4" type="radio" name="dwCheckStat">��ȡ��</label>
                        </td>
                        </tr>
                    <!--
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
                          -->
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="��ѯ" style="height: 25px" />
                            <input type="button" id="btnDelall" value="����ɾ��" style="height: 25px" />
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
                        <th>ռ����</th>
                        <th><%=ConfigConst.GCDevName %>����</th>
                       <th name="szLabName">����<%=ConfigConst.GCLabName %></th>
                        <th>״̬</th>
                        
                        <th name="dwBeginTime">ռ��ʱ��</th>
                        <th>ռ��ԭ��</th>
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
                $("#btnNewResv").button().click(function () {
                    $.lhdialog({
                        title: '����Առ��',
                        width: '750px',
                        height: '580px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewICResvList.aspx?op=set'
                    });
                });
          

                var tabl = $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $(".OPTD").html('<div class="OPTDBtn">\
                <a class="delBtn" title="����ɾ��"><img src="../../themes/iconpage/del.png"/></a>\
                    <a class="delListBtn" title="����ɾ��"><img src="../../themes/icon_s/del.png"/></a></div>');



                $(".OPTDCheck").html('<div class="OPTDBtn">\
                       <a class="OPTDCheck" title="���"><img src="../../themes/iconpage/edit.png"/></a>\
                       <a class="GetResv" title="�鿴"><img src="../../themes/icon_s/17.png"/></a>\
                       <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a></div>');

                $(".OPTDCheckDel").html('<div class="OPTDBtn">\
                        <a class="OPTDCheck" title="���"><img src="../../themes/iconpage/edit.png"/></a>\
                        <a class="GetResv" title="�鿴"><img src="../../themes/icon_s/17.png"/></a>\
                        <a class="delBtn" title="ɾ��"><img src="../../themes/iconpage/del.png""/></a>\
                        <a class="beforeDone" title="��ǰ����"><img src="../../../themes/icon_s/18.png"/></a></div>');

                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="delListBtn" title="����ɾ��"><img src="../../../themes/iconpage/del.png"/></a></div>');

                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
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
                $(".setResv").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���·���',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvSetOwner.aspx?op=set&id=' + dwResvID
                    });
                });
                
                $(".delBtn").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwResvID);
                    }, '��ʾ', 1, function () { });
                });
                $("#btnDelall").button().click(function () {
                   
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        var dwResvID = "";
                        $("input[name^='tblSelect']").each(function () {
                            debugger;
                            if ($(this).prop("checked") == true) {
                                dwResvID = dwResvID + $(this).parent().attr("data-id")+',';
                            }
                        });

                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=delall&delAllID=" + dwResvID);
                    }, '��ʾ', 1, function () { });
                });
                

                $(".delListBtn").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ�������ô�ɾ�������ܲ�ֹһ��", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=dellist&delID=" + dwResvID);
                    }, '��ʾ', 1, function () { });
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
                
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
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