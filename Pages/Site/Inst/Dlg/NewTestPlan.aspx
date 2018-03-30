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
                    <th>学期：</th>
                    <td>
                        <select id="dwYearTerm" name="dwYearTerm"><%=m_szTerm %></select></td>
                    <th>名称：</th>
                    <td>
                        <input id="szTestPlanName" name="szTestPlanName" class="validate[required]" /></td>
                </tr>
                 <tr>
                    <th>上课班级：</th>
                    <td>
                        <input type="text" name="szGroupName" id="szGroupName" /></td>
                    <th>课程：</th>
                    <td>
                        <input type="text" name="szCourseName" id="szCourseName" /></td>
                </tr>
                <tr>
                     <th>上课教师：</th>
                    <td>
                        <input type="text" name="szTeacherName" id="szTeacherName" /></td>
                     <th>实验学时数：</th>
                    <td>
                       <div id="dwTestHour"></div></td>
                </tr>
               
                <tr>
                    <td colspan="4" style="text-align: center">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
