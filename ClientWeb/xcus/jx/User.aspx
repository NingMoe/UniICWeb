<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="net/Master.master" CodeFile="User.aspx.cs" Inherits="ClientWeb_xcus_jx_User" %>
<%@ Register TagPrefix="Uni" TagName="info" Src="~/ClientWeb/pro/net/userinfo.ascx" %>
<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <link rel="stylesheet" type="text/css" href="../../fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <style>
        #updateacc .tr_credit {display:none;}
    </style>
    <script>
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <Uni:info runat="server" />
</asp:Content>
