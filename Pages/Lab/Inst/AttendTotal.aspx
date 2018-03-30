<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="AttendTotal.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
      
        <h2>���ڹ���</h2>
       <div class="toolbar">
            <div class="tb_info"></div>
           <!-- <div class="FixBtn"><a id="newRule">�½����ڹ���</a></div>-->
             <div class="tb_info">
             <div class="UniTab" id="tabl">
                <a href="attendrule.aspx" id="attendrule">���ڹ���</a>                
                <a href="attendTotal.aspx" id="attendTotal">����ͳ��</a>
                 <a href="attendRec.aspx" id="attendRec">������ϸ</a>  
                </div> 
    </div>
        </div>
          <input type="hidden" name="dwAccNo" id="dwAccNo" />
         <div style="margin-top:15px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                   <th>���ڹ���</th>
                   <td><select id="attendid" name="attendid"><%=szAttendRule %></select></td>
                  <th>ѧ���ţ�</th>
                   <td><input  type="text" name="truename" id="truename" /></td>
               </tr>
                        <tr>
                             <th>��ʼ����</th>
                   <td>
                    <input type="text" name="dwStartDate" id="dwStartDate" runat="server" />   
                   </td>
                   <th>��������</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" runat="server"  /></td>
                        </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
                        <input type="submit" id="sub" value="��ѯ">
                        <input type="submit" id="import" value="����">
                    </td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ѧ����</th>
                        <th>����</th>
                         <th>�迼�ڴ���</th>
                        <th>���ڴ���</th>
                        <th>ȱ�ڴ���</th>
                        <th>�ٵ�����</th>
                        <th>���˴���</th>
                        <th>�ٵ�������</th>
                          <th>ʹ��ʱ�䲻������</th>
                        <th>�뿪δˢ������</th>
                        <th>������ʱ��</th>
                       
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
                AutoUserByLogonname($("#truename"), 1, $("#dwAccNo"), null, null, null);
                $("input,select").css("width", "150");
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="set" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="del" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a class="setRuleGroup" title="���ÿ�����Ա����"><img src="../../../themes/icon_s/11.png"/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#import").button().click(function () {
                    var vFrom = $("#formAdvOpts").serialize();
                    $.lhdialog({
                        title: '����',
                        width: '300px',
                        height: '100px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/AttendTotaOutExport.aspx?op=set&attendid=' + $("#attendid").val() + "&dwAccNo=" + $("#dwAccNo").val() + "&dwStartDate=" + $("#<%=dwStartDate.ClientID%>").val() + "&dwEndDate=" + $("#<%=dwEndDate.ClientID%>").val()
                    });
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
                $(".setRuleGroup").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '���ÿ�����Ա����',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setgroupmember.aspx?op=set&dwID=' + dwDevID
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
