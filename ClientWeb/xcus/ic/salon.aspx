<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salon.aspx.cs" Inherits="Page_" %>
<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx"%>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx"%>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="UTF-8" />
<title>我的空间</title>
<meta content="" name="keywords"/>
<meta content=""  name="description" />
<link rel="stylesheet" href="style/css/main.css">
<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
<script type="text/javascript" src="js/site.js"></script>
<style>
.seat{display:none;}
 .hidden
   { display:none;} 
   .HeaderStyle
{
  text-align:center;
}
</style>
</head>
<body>
<div class="body">
<Uni:sidebar ID="Sidebar1" runat="server" />
    <div class="content clear">
		<Uni:nav ID="Nav1" runat="server" />
		        <form id="Form1" runat="server">
        <div class="salon">
            <h1>我的空间</h1>
            <div id="space_tabs" class="space_tabs tabs2">
                <ul>                   
                    <li id="tab_1"><a href="#space_tab_1">修改资料</a></li>
                     <li id="tab_2"><a href="#space_tab_2">预约记录</a></li>
                </ul>               
        
                
                <div id="space_tab_1">                  
                   <table class="RowsTable" cellspacing="0">
				<thead>
					
				</thead>
				<tbody>
			   <%=szActivity %> 
				</tbody>
            </table>
			
                </div>
         
            <div id="space_tab_2">  
             <table class="RowsTable" cellspacing="0">
				<thead>
				
				</thead>
				<tbody>
			   <%=szActivityHistory %> 
				</tbody>
            </table>                                       
            </div>
        </div>
        </div>
        </form>
        <div class="copyright">版权说明</div>
    </div>
</div>
<Uni:dialog ID="Dialog1" runat="server" />
<script>
$("form").submit(function(){
	var data = $(this).serialize();
$.ajax({
	type:"GET",
		url:"Ajax_Code/account.aspx?act=update&"+data,
		dataType:"json",
		success:function(object){
			if(object.MsgId>0)
				alert(object.Message);
			else{
				alert("更新成功");
				location.reload();
			}
		}
	});
});

$.ajax({
	type:"GET",
		url:"/app/action.reserve.php?act=acc",
		dataType:"json",
		success:function(object){
		$('table.room tbody').empty();
		for(var key in object){
			$('table.room tbody').append(object[key].html);
		};
		$('a.Cancel').click(function(){
			$.ajax({
				type:"GET",
					url:"/app/action.reserve.php?act=cancel&id="+$(this).attr('href'),
					dataType:"json",
					success:function(object){
						if(object.MsgId>0){
						}else{
							$('table.room tbody tr[row='+object.RowId+']').remove();
						}
					}
			});
			return false;
		});
	}
});
/*
$.ajax({
	type:"GET",
		url:"/app/action.device.php?act=acc",
		dataType:"json",
		success:function(object){
		$('table.device tbody').empty();
		for(var key in object){
			$('table.device tbody').append(object[key].html);
		};
		$('a.DCancel').click(function(){
			$.ajax({
				type:"GET",
					url:"/app/action.device.php?act=cancel&id="+$(this).attr('href'),
					dataType:"json",
					success:function(object){
						if(object.MsgId>0){
						}else{
							$('table.device tbody tr[row='+object.RowId+']').remove();
						}
					}
			});
			return false;
		});
	}
});

$.ajax({
	type:"GET",
		url:"/app/action.salon.php?act=acc",
		dataType:"json",
		success:function(object){
		$('table.salon tbody').empty();
		for(var key in object){
			$('table.salon tbody').append(object[key].html);
		};
		$('a.SCancel').click(function(){
			$.ajax({
				type:"GET",
					url:"/app/action.salon.php?act=cancel&id="+$(this).attr('href'),
					dataType:"json",
					success:function(object){
						if(object.MsgId>0){
						}else{
							$('table.salon tbody tr[row='+object.RowId+']').remove();
						}
					}
			});
		});
		}
		});
*/
</script>
</body>
</html>

