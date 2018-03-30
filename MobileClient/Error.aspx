<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="_Default" %>
<html>
<head>
<meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<link rel="apple-touch-icon" href="icon.png"/>
	<link rel="apple-touch-startup-image" href="icon.png"/>
	<link rel="stylesheet" type="text/css" media="screen" href="style.css" />
    <script type="text/javascript" src="res/jqTools/jquery.tools.min.js"></script>
    <script type="text/javascript" src="js.aspx"></script>
</head>
<body>
<form runat="server">

<div id="regDiv">
    <div class="Head">
    <br />
        <p style="text-align:center;"><%=szErrMsg%></p>
    </div>
</div>

</form>
</body>
</html>