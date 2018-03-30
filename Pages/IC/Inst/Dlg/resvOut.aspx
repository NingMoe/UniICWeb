<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="resvOut.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <form id="Form2" runat="server">       


      <div class="formtable">
        <table>
            <tr><td style="text-align:right">扣除分数(总共300分)：</td><td style="text-align:left"> <input type="text" id="score" name="score" title="扣完三百分及黑名单" /></td></tr>
            <tr><td style="text-align:right">取消原因：</td><td style="text-align:left"><textarea name="szMessageInfo" id="szMessageInfo" type="text" style="width:350px;height:60px" class="validate[required]" ></textarea></td></tr>

            <tr><td></td><td>   <button type="submit" id="OK">确定</button>  <button type="button" id="Cancel">取消</button></td></tr>
        </table>
    </div>


    </form>
    </asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
    });
</script>
    <style>
        .th
        {
            font-size:14px;
            font-weight:bold;
            text-align:right;
            height:30px;
        }
        td
        {
            text-align:left;
            width:200px
        }
            td div
            {
                margin-left:15px;
            }
    </style>
</asp:Content>
