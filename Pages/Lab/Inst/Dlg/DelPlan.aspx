<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="DelPlan.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_szTitle %></div>
        <input name="IsSubmit" value="true" type="hidden"/>
        <div class="formtable"><button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">

        .formtitle {
            padding: 6px;
            height: 30px;
            font-size: 20px;
            border-radius:10px;
            margin-top:-10px;
            margin-bottom:10px;
            text-align:center;
            color: #0088ff;
        }

        .formtable
        {
            height:350px;
            text-align: center;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
        });
    </script>
</asp:Content>
