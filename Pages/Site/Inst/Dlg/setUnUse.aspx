<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="setUnUse.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <form id="Form2" runat="server">       
        <div class="formtable" style="margin-top:30px">
          <div style="float:left;margin-left:40px; margin-top:20px;font-weight:bold">
         禁用原因：</div>
            <div><textarea name="szMessageInfo" id="szMessageInfo" type="text" style="width:350px;height:60px" class="validate[required]" ></textarea></div>
          <div style="margin:10px 0px 0px 350px"">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
             </div>
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
