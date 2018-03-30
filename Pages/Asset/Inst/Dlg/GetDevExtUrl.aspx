<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetDevExtUrl.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server" enctype="multipart/form-data">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <div>
            <img alt="" src="<%=szImgUrl %>" />
        </div>
        <table>
            <tr>
                <td style="text-align:center" colspan="4"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button>
                  
                </td>

            </tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    </style>
<script language="javascript" type="text/javascript" >
   $(function () {  
       AutoUserByName($("#szAttendantName"),2, $("#dwAttendantID"), null, null, null);
       $("#OK,#print").button();
       $("#print").click(function () {
           window.print();
       });
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(function () {
           
        }, 1);
    });
</script>
</asp:Content>
