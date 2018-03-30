<%@ Page Language="C#" AutoEventWireup="true" CodeFile="seatInfo.aspx.cs" Inherits="seatInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <script type="text/javascript">  
         setTimeout("document.getElementById('sub').click()", 3000);
    </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;font-size:12px;margin:10px auto">
    <div>
            欢迎使用座位管理系统
                   </div>
               <div>
                   全部座位数<%=szDevTotal %>，剩余座位数<%=szDevLev %>
               </div>
    </div>
        <div>
            <input type="submit" id="sub" style="display:none" />
        </div>
    </form>
</body>
</html>
