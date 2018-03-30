<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="Art.aspx.cs" Inherits="ClientWeb_xcus_jx_Art" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
    </style>
    <script>
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
                <div class="panel panel-default" style="min-height:420px;">             
                <div class="panel-heading">
                    <%=artTitle %>
                </div>
    <div runat="server" id="divContent" class="article_content" style="padding:10px;"></div>
                    </div>

</asp:Content>
