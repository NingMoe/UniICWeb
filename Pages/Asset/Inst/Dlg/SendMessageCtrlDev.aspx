<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SendMessageCtrlDev.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">       
        <div class="formtable">
            <div style="margin:50px 0px 0px 60px">消息内容：<input name="szParam" id="szParam" type="text" style="width:350px" class="validate[required]" /></div>
          <div style="margin:10px 0px 0px 120px"">
                        <button type="submit" id="OK">发送</button>
                        <button type="button" id="Cancel">取消</button>
             </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtitle {
            padding: 6px;
            background: #d0d0d0;
            height: 30px;
            color: #fff;
            font-size: 20px;
        }

        .formtable table {
            text-align: center;
            margin: auto;
        }

        td {
            padding: 6px;
        }

        input, select {
            width: 200px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {       
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        
        setTimeout(function () {
           
        }, 1);
    });
    </script>
</asp:Content>
