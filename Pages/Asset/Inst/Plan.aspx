<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Sub_Plan" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="courseid" name="courseid" />
        <h2>�γ�ʵ��ƻ�</h2>
         <div class="toolbar">
            <div class="FixBtn">
                <a id="planImport">����ʵ��ƻ�</a>
                <a id="newPlan">�ֶ����ʵ��ƻ�</a>

            </div>
          
        </div>
         <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                  
                   <th>ѧ��:</th>


                   <td><select id="dwYearTerm" name="dwYearTerm">
                       <%=m_TermList %>
                       </select></td>
                   <th>��ʦ����</th>
                   <td><input type="text" id="pid" name="pid" /></td>
                   <th>�γ�����</th>
                   <td><input type="text" id="courseName" name="courseName" /> </td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>
        <div class="content">
          
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th >���</th>
                        <th>�ƻ�����</th>
                        <th>ѧ��</th>
                        <th name="szTeacherName">��ʦ</th>
                        <th>�༶</th>
                        <th>�γ�</th>
                        <th>��Ŀ��</th>
                        <th name="dwTotalTestHour">ѧʱ</th>
                      <th>���ſ�ѧʱ</th>
                        <th>�����ѧʱ</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="DoItemSet" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="DoItemDel" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                AutoCourseName($("#courseName"), 1, $("#courseid"), 1, false);

                $("#btn").button();
                $(".DoItemDel").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: 'ɾ��ʵ��ƻ�',
                        width: '250px',
                        height: '100px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/DelPlan.aspx?op=del&id=' + dwID
                    });
                });


                $(".DoItemSet").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: '�޸�ʵ��ƻ�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=set&id=' + dwID
                    });
                });
                
                $("#planImport").click(function () {
                    $.lhdialog({
                        title: '����ʵ��ƻ�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importtestplan.aspx?op=new'
                    });
                });
                $("#newPlan").click(function () {
                    $.lhdialog({
                        title: '�ֶ����ʵ��ƻ�',
                        width: '1000px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
