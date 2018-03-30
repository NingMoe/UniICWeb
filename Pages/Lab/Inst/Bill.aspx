<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>����״��</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
        <div class="toolbar">
            <div class="tb_btn">
            </div>
        </div>
        <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
      
                <table style="margin: 15px; width: 99%">      
                 <!-- <tr>
                    
                    <td  style="width:20%; text-align:center">ԤԼ״̬</td>
                    <td style="width:75%;text-align:left">
                        <LABEL><INPUT class="enum" value="512" type="radio" name="dwCheckStat" > ������Ч�����ʱ��</LABEL>
                        <LABEL><INPUT class="enum" value="524288" type="radio" name="dwCheckStat" > ������</LABEL>    
                        <LABEL><INPUT class="enum" value="4194304" type="radio" name="dwCheckStat" > ��ӡУ��ת��ƾ֤</LABEL>                      
                    </td>
                   
                    </tr>
                 -->
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
                        <td colspan="3">
                            <input type="text" name="dwPID" id="dwPID" /></td>

                    </tr>
                <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="��ѯ" style="height: 25px" /></th>
                    </tr>
            </table>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ѧ����</th>
                        <th>����</th>
                        <th>����</th>
                        <th>ʹ��ʱ��</th>
                        <th>�Ʒ�ʱ��</th>
                        <th>����</th>
                          <th title="ÿ�����Ӽ�һ�η�">�Ʒ�����</th>
                        <th>�շѽ��</th>
                      
                        <th>�˵�ʱ��</th>
                        <th>״̬</th>
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
                $("#btnOK").button();
                $(".OPTD").html('<div class="OPTDBtn">\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                $("#btnDevKind").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevKind.aspx?op=new'
                    });
                });
                $(".InfoDevKindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '����',
                        width: '760px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwDevKind + "&type=DevKindInfo"
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevKind.aspx?op=set&id=' + dwDevKind
                    });
                });
                $(".DelbtnDevKind").click(function () {
                    var devKindID = $(this).parents("tr").children().first().children().first().val();
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                    }, '��ʾ', 1, function () { });
                });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
