<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�γ̹���</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnResvRule">�½��γ�</a></div>
            <div class="tb_btn">
            </div>
        </div>
        
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                  
                   <th>�γ�����:</th>
                   <td><input type="text" name="szCourseName" id="szCourseName" /></td>
                   
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szCourseCode">�γ̱���</th>
                        <th name="szCourseName">�γ�����</th>
                        <th name="dwCourseProperty">�γ�����</th>
                        <th name="dwCreditHour">ѧ��</th>
                        <th name="dwTestNum">ʵ�����</th>
                        <th name="dwTestHour">ʵ��ѧʱ��</th>
                        <th name="dwTheoryHour">����ѧʱ��</th>
                        <th name="dwPracticeHour">ʵ��ѧʱ��</th>
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
                //$(".ListTbl").UniTable();

                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '�½��γ�',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewCourse.aspx?op=new'
                    });
                });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸Ŀγ�',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetCourse.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
            });
        });
        </script>
    </form>
</asp:Content>
