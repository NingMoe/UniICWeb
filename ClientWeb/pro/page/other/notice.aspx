<%@ Page Language="C#" AutoEventWireup="true" CodeFile="notice.aspx.cs" MasterPageFile="../pageMaster.master" Inherits="ClientWeb_pro_page_other_notice" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="../../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <script>
        $(function () {
            var req = uni.getReq();
            var key = req["dlg_key"];
            $(".agree").click(function () {
                parent.uni.dlgInst[key].agree = true;
                parent.uni.dlgInst[key].dialog("close");
            });
            });
    </script>
    <style>
        table { border-collapse: collapse; }
        th, td { border-width: 1px; border-style: solid; }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
    <div runat="server" id="noticeCon" class="panel-body" style="min-height:460px;">
    </div></div>
    <div class="text-center">
        <div class="btn-group">
            <button type="button" class="btn btn-info agree"><%=Translate("同意") %></button>
            <button type="button" class="btn btn-warning  dlg_page_close"><%=Translate("拒绝") %></button>
        </div></div>
</asp:Content>


