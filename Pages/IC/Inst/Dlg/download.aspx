<%@ Page Language="C#" AutoEventWireup="true" CodeFile="download.aspx.cs" Inherits="Page_download" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title> 下载实验结果 </title>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	
	<link rel="icon" href="favicon.ico" mce_href="favicon.ico" type="image/x-icon"/>
	<link rel="icon" href="favicon.gif" type="image/gif"/>

	<link rel="stylesheet" href="res/general.css"/>
</head>
<body>

<p class="toolbar">
<a href="upload.aspx">上传实验结果</a> | <a href="List.aspx">实验结果列表</a> | 
</p>
<div style="text-align:center;">
<%
	Response.Write(m_szErrMsg);
%>
<button onclick="history.go(-1);">返回</button>
</div>
</body>
</html>
