<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewTestPlan.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwCourseID" name="dwCourseID" type="hidden" />
            <input id="dwGroupID" name="dwGroupID" type="hidden" />
            <input id="dwTeacherID" name="dwTeacherID" type="hidden" />
            <table>
                <tr>
                    <th>ѧ�ڣ�</th>
                    <td>
                        <select id="dwYearTerm" name="dwYearTerm"><%=m_szTerm %></select></td>
                    <th>���ƣ�</th>
                    <td>
                        <input id="szTestPlanName" name="szTestPlanName" class="validate[required]" /></td>
                </tr>
                 <tr>
                    <th>�Ͽΰ༶��</th>
                    <td>
                        <input type="text" name="szGroupName" id="szGroupName" /></td>
                    <th>�γ̣�</th>
                    <td>
                        <input type="text" name="szCourseName" id="szCourseName" /></td>
                </tr>
                <tr>
                     <th>�Ͽν�ʦ��</th>
                    <td>
                        <input type="text" name="szTeacherName" id="szTeacherName" /></td>
                     <th>ʵ��ѧʱ����</th>
                    <td>
                       <div id="dwTestHour"></div></td>
                </tr>
               
                <tr>
                    <td colspan="4" style="text-align: center">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
      
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
        });
    </script>
</asp:Content>
