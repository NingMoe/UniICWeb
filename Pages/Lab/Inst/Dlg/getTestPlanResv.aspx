<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="getTestPlanResv.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
          <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>上课教师</th>
                        <th>班级/课程</th>
                       <th>项目名称</th>
                        <th>学时数</th>
                       
                        <th>上课时间</th>
                        <th>上课房间</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
         
        </div>
        <div class="formtable">
            <table>
                <tr>
                    <td style="text-align:center">
                      
                        <button type="button" id="Cancel">关闭</button></td>
                </tr>
            </table>
        </div>
        <div>

        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
      
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {       
        $("#Cancel").button().click(Dlg_Cancel);      
        setTimeout(function () { }, 1);
    });
    </script>
</asp:Content>
