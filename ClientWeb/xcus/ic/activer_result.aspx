<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activer_result.aspx.cs" Inherits="Page_" %>
<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx"%>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx"%>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="UTF-8" />
<title>预约空间</title>
<meta content="" name="keywords"/>
<meta content=""  name="description" />
<link rel="stylesheet" href="style/css/main.css">
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
<script type="text/javascript" src="js/site.js"></script>
</head>
<body>
<div class="body">
<Uni:sidebar runat="server" />
    <div class="content clear">
		<Uni:nav runat="server" />
        <div class="reservation">
            <h1>预约空间</h1>
            <div class="result"><%=szResult %></div>
        </div>
        <div class="copyright">版权说明</div>
    </div>
</div>
<Uni:dialog runat="server" />
</body>
</html>

