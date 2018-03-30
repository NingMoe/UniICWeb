<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salon_pre.aspx.cs" Inherits="Page_" %>
<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx"%>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx"%>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="UTF-8" />
<title>活动预告</title>
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
       <div class="salon_pre">
				<h1>活动预告</h1>
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
		
			<div class="copyright">版权说明</div>
    </div>
</div>
<Uni:dialog runat="server" />
<script type="text/javascript">
$(function(){
	// news
    $(".RowsTable th span").hover(function(){
        $(this).toggleClass("hover");
    });
    $(".RowsTable th span").click(function(){
        $(this).parent().parent().next().toggle();
    })

    $("a.join").click(function () {
        if (!IsLogin()) {
            alert("报名请先登录");
            return;
        }
		var value = $(this).attr("gid");
		var purpose=$(this).attr("purpose");
		$.ajax({
			type:"GET",
				url:"Ajax_Code/salon.aspx?act=res&gid="+value+"&purpose="+purpose,
				dataType:"json",
				success:function(object)	{
					if(object.MsgId>0)
						alert(object.Message);
					else
						alert("预约成功");
						location.reload();
					}
		});
		return false;
	});
})
</script>
</body>
</html>

