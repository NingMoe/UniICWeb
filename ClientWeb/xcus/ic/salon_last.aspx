<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salon_last.aspx.cs" Inherits="Page_" %>
<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx"%>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx"%>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="UTF-8" />
<title>活动回顾</title>
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
		<Uni:nav ID="Nav1" runat="server" />
		<form id="form" runat="server">
       <div class="salon_list">
				<h1>活动预告</h1>
				活动月份:<asp:DropDownList ID="ActivityDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ActivityDate_SelectedIndexChanged">				
				</asp:DropDownList>
				<table class="RowsTable" cellspacing="0">
				<thead>
					<tr>
						<th class="title">活动主题</th>
						<th class="date">活动日期</th>
					</tr>
				</thead>
				<tbody>
				<%=m_szInfo%>
				</tbody>
            </table>
			</div>
		</form>
			<div class="copyright">版权说明</div>
    </div>
</body>
</html>

