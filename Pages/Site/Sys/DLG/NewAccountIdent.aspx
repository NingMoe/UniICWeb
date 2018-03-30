<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewAccountIdent.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwAccNo" name="dwAccNo" type="hidden" />
            <input id="szTrueName" name="szTrueName" type="hidden" />
            <table>
                <tr>
                    <th><%=ConfigConst.GCTutorName %>姓名：</th>
                    <td>
                        <input id="szLognName" name="szLognName" class="validate[required]" /></td>
                    <th></th>
                    <td>
                       </td>
                </tr>

                <tr>
                    <th>电话：</th>
                    <td>
                      <input type="text" id="szTel" name="szTel" /></td>
                    <th>手机：</th>
                    <td>
                        <input type="text" id="szHandPhone" name="szHandPhone" /></td>
                </tr>
                <tr>
                    <th>邮箱：</th>
                    <td><input type="text" id="szEmail" name="szEmail" /></td>
                    <th>备注：</th>
                    <td>
                      <input type="text" name="szMemo" /></td>
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
        $("#Cancel").button().click(Dlg_Cancel);
        AutoUserByName($("#szLognName"), 2, $("#dwAccNo"), $("#szHandPhone"), $("#szTel"), $("#szEmail"));
        setTimeout(function () {
           
        }, 1);
    });
    </script>
</asp:Content>
