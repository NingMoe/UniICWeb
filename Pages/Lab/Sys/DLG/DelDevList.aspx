<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="DelDevList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle">批量删除</div>
        <div class="formtable">
              
            <table>
                       
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
            AutoRoom($("#devRoomName"), 2, $("#devRoomID"), null, null);
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
     

        setTimeout(function () {
            
        }, 1);
    });
    </script>
</asp:Content>
