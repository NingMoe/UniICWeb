<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetLab.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <input id="dwDeptID" name="dwDeptID" type="hidden" />
            <input id="dwCampusID" name="dwCampusID" type="hidden" />
            <input id="dwLabClass" name="dwLabClass" type="hidden" />
            <table>
                <tr>
                    <th>编号：</th>
                    <td>
                        <input id="szLabSN" name="szLabSN" class="validate[required]" /></td>
                    <th>名称：</th>
                    <td>
                        <input id="szLabName" name="szLabName" class="validate[required]" /></td>
                </tr>

                <tr>
                    <th>所属<%=ConfigConst.GCDeptName %>：</th>
                    <td>
                        <input type="text" id="szDeptName" name="szDeptName" /></td>
                    <th>备注：</th>
                    <td>
                        <input type="text" id="szMemo" name="szMemo" /></td>
                </tr>              
                <tr>
                    <td class="btnRow" colspan="4">
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
        <%if (bSet)
          {%>
           <%}%>
   
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
     

        setTimeout(function () {
            
        }, 1);
    });
    </script>
</asp:Content>
