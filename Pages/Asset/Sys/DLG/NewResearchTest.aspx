<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewResearchTest.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
         <input type="hidden" id="dwTutorID" name="dwTutorID" />
         <input type="hidden" id="dwLeaderID" name="dwLeaderID" />
            <input type="hidden" id="dwDeptID" name="dwDeptID" />
            <table>
                <tr>
                    <th><%=ConfigConst.GCReachTestName %>编号：</th>
                    <td>
                        <input id="szRTSN" name="szRTSN" class="validate[required]" /></td>
                    <th><%=ConfigConst.GCReachTestName %>名称：</th>
                    <td>
                        <input id="szRTName" name="szRTName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th><%=ConfigConst.GCTutorName %>姓名：</th>
                    <td>
                        <input id="szTutorName" name="szTutorName" class="validate[required]" /></td>
                    <th><%=ConfigConst.GCLeadName %>姓名：</th>
                    <td>
                        <input id="szLeaderName" name="szLeaderName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>承担部门：</th>
                    <td>
                       <input type="text" id="szDeptName" name="szDeptName" /> 
                      </td>
                    <th>下达日期：</th>
                    <td>
                        <input type="text" id="dwBeginDate" name="dwBeginDate" /></td>
                </tr>
                <tr>
                    <th>下达部门：</th>
                    <td>
                     <input id="szFromUnit" name="szFromUnit" /></td>
                    <th>级别</th>
                    <td>
                          <select id="dwRTLevel" name="dwRTLevel"><%=m_szLevel %></select>
                    </td>
                </tr>
                 <tr>
                    <th>经费卡号一：</th>
                    <td>
                     <input type="text" id="szFoundNo1" name="szFoundNo1" /></td>
                    <th>经费卡号二</th>
                    <td><input type="text" id="szFoundNo2" name="szFoundNo2" />
                    </td>
                </tr>
                 <tr>
                    <th>经费卡号三：</th>
                    <td>
                     <input type="text" id="szFoundNo3" name="szFoundNo3" /></td>
                    <th></th>
                    <td>
                    </td>
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
            $("#OK").button();
            $("#dwBeginDate").datepicker();
            $("#Cancel").button().click(Dlg_Cancel);
            AutoUserByIdent($("#szTutorName"), 2, $("#dwTutorID"));
            $("#szTutorName").on("autocompleteselect", function (event, ui) {
                $("#szLeaderName").val(ui.item.szTrueName);
                $("#dwLeaderID").val(ui.item.id);
            });
            AutoUserByIdent($("#szLeaderName"), 2, $("#dwLeaderID"));
            setTimeout(function () {

            }, 1);
            AutoDept($("#szDeptName"),2,$("#dwDeptID"),false);
        });
    </script>
</asp:Content>
