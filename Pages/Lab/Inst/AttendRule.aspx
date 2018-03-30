<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="AttendRule.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>���ڹ���</h2>
       <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="newRule">�½����ڹ���</a></div>
             <div class="tb_info">
             <div class="UniTab" id="tabl">
               <a href="attendrule.aspx" id="attendrule">���ڹ���</a>                
                <a href="attendTotal.aspx" id="attendTotal">����ͳ��</a>
                 <a href="attendRec.aspx" id="attendRec">������ϸ</a>
                </div> 
    </div>
        </div>
          <div style="margin-top:15px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                   <th>���ƣ�</th>
                   <td colspan="3"><input type="text" name="szAttendName" id="szAttendName" /></td>
                  
               </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="sub" value="��ѯ"></td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>����</th>
                        <th>��ʼ����</th>
                         <th>��������</th>
                        <th>����ʱ��</th>
                        <th>�˳�ʱ��</th>
                        <th>����ͣ��ʱ�䣨���ӣ�</th>
                        <th>���ڷ���</th>
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
                $("#sub").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="del" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="setRuleGroup" title="���ÿ�����Ա����"><img src="../../../themes/icon_s/11.png"/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
              
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: "5", minWidth: "175", maxWidth: "175", minHeight: "25", maxHeight: "95", speed: 50
                });
                $(".UniTab").UniTab();
                $(".set").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newAttendRule.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".getAttendRuleDetail").click(function () {
                   // debugger;
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    var dwStartDate = $(this).parents("tr").children().first().attr("data-startdate");
                    var dwenddate = $(this).parents("tr").children().first().attr("data-enddate");
                    fdata = "attendid=" + dwDevID + '&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwenddate;
                    TabInJumpReload("l", fdata);
                });
                $(".setRuleGroup").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '���ÿ�����Ա����',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setusegroup.aspx?op=set&id=' + dwDevID
                    });
                });
               
                $(".del").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                }, '��ʾ', 1, function () { });
            });
                $("#newRule").click(function () {
                    $.lhdialog({
                        title: '�½����ڹ���',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newAttendRule.aspx?op=new'
                    });
                });
            });
            $(".ListTbl").UniTable({ HeaderIndex: false });
        </script>
    </form>
</asp:Content>
