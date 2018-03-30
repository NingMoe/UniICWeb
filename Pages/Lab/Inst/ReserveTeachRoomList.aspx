<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveTeachRoomList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��ѧԤԼ״��</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <input type="hidden" id="szRoomNo" name="szRoomNo" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="ReserveTeachRoomList.aspx">��ѧԤԼ״��</a>
                    <a href="ReservePersonRoomList.aspx">����ԤԼ״��</a>
                </div>

            </div>

        </div>
        <div style="margin-top:8px;width:99%;">  
             <a class="button" id="outplan">����</a>
                <a class="button" id="outWeekResv">����������</a>
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 95%">
                    <tr>
                        <th>��ʼ����:</th>
                        <td>
                            <input type="text" name="dwBeginDate" id="dwBeginDate" runat="server" /></td>
                        <th>��������:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>ѧ����:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th><%=ConfigConst.GCRoomName %>����:</th>
                        <td>
                            <input type="text" name="szRoomName" id="szRoomName" /></td>

                    </tr>
                    <tr>
                        <th>״̬</th>
                        <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">ȫ��</label>
                            <label><input class="enum" value="512" type="radio" name="dwCheckStat">��Ч��</label>
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">�ѽ���</label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="��ѯ" style="height: 25px" />
                        <!--     <input type="button" id="btnCheckCLSName" value="У���Ͽΰ༶����" style="height: 25px" />-->
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
                        <th>��Ŀ����</th>
                        <th>�Ͽν�ʦ</th>
                        <th><%=ConfigConst.GCRoomName %>����</th>
                        <th>�༶</th>
                        <th>״̬</th>
                        <th name="dwBeginTime">�Ͽ�ʱ��</th>
                         <th name="dwBeginTime">����ʱ��</th>
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
                $("#btnOK,#outplan,#btnCheckCLSName").button();
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwBeginDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
              //  AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $("#outWeekResv").button().click(function () {
                    $.lhdialog({
                        title: '����������',
                        width: '450px',
                        height: '220px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/weekCaloutport.aspx'
                    });
                    
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="set" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDDel").html('<div class="OPTDBtn">\
                       <a class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                AutoRoom($("#szRoomName"), 1, $("#szRoomNo"), null, null);
                $("#szRoomName").on("autocompleteselect", function (event, ui) {
                    setTimeout(function () {
                        $("#szRoomNo").val(ui.item.szRoomNo);
                    }, 200);
                });
                $(".set").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    var dwResvDate = $(this).parents("tr").children().first().attr("data-resvDate");
                    $.lhdialog({
                        title: '�޸�ԤԼ',
                        width: '700px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ResvFormSet.aspx?op=set&id=' + dwResvID + '&date=' + dwResvDate
                    });
                });
                $("#btnCheckCLSName").click(function () {
                    var vResvID = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            vResvID = vResvID + $(this).parent().data("id")+',';
                        }
                    });
                    if (vResvID == "") {
                        return;
                    }

                    $.lhdialog({
                        title: 'У���Ͽΰ༶����',
                        width: '450px',
                        height: '220px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CheckClsName.aspx?op=set&id=' + vResvID
                    });
                });

                $(".classgroup").click(function () {
                    var dwID = $(this).data("groupid");
                    $.lhdialog({
                        title: '�鿴�Ͽ�����',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?op=set&id=' + dwID
                    });
                });
                $("#outplan").click(function () {
                    var dwStartDate = $("#<%=dwBeginDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var szRoomNo = $("#szRoomNo").val();
                    var dwCheckStat = $("#dwCheckStat").val();
                    var szGetKey = $("#szGetKey").val();
                    var szPid = $("#szPid").val();
                    $.lhdialog({
                        title: '����',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/resvTeachExport.aspx?op=set&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwEndDate + "&szRoomNo=" + szRoomNo + "&szPid=" + szPid
                    });

                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                    }, '��ʾ', 1, function () { });
                });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: true });

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
            .classgroup {
            text-decoration:underline;
            }
        </style>
    </form>
</asp:Content>
