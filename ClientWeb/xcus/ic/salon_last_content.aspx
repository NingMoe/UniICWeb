<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salon_last_content.aspx.cs" Inherits="Page_" %>
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
		<Uni:nav runat="server" />
	
		
        <div class="salon_last">
            <h1>活动回顾</h1>
            	<table class="RowsTable" cellspacing="0">
				<thead>
				
				</thead>
				<tbody>
				<%=m_szInfo%>
				</tbody>
            </table>
           
        </div>
      <div id="space_tabs" class="space_tabs tabs2" runat="server" >
                    <ul>
                    
                    </ul>
                    <div id="space_tab_1">
                        <div class="img_large">
                        	<%=szImgBig%>
                        	<!--
                            <img src="images_upload/Activity/686-1_s.jpg" width="510" height="350">
                            -->
                        </div>
                        <div class="img_thumb">
                            <ul class="clear">
                            	<%=szImgSmall%>
                            	<!--
                              <li><a href="" class="cur">
                                    <img src="images_upload/Activity/686-1_s.jpg" width="84" height="55"></a></li>
                                <li><a href="" class="cur">
                                    <img src="images_upload/Activity/686-2_s.jpg" width="84" height="55"></a></li>                               
                          -->
                            </ul>
                        </div>
                    </div>                     
                    </div>
        <div class="copyright">版权说明</div>
    </div>
</div>
</body>
</html>

