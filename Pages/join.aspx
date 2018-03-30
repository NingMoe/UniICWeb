<%@ Page Language="C#" AutoEventWireup="true" CodeFile="join.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/HeadInclude.ascx" TagPrefix="unifound" TagName="HeadInclude" %>

<html>
<head>
 <meta charset="UTF-8">
  <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="viewport" content="height=device-height, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>活动</title>
    <link rel="stylesheet" href="appui/seatStyle.css">
    <script src="appui/Adaptive.js"></script>
      <unifound:HeadInclude runat="server" ID="HeadInclude" />
    <script>
        $(function () {
            $("#join").click(function () {
                $("#op").val("join");
            });
            $("#out").click(function () {
                debugger;
                $("#op").val("out");
            });
        });
    </script>
</head>
<body>
    <div class="wrap">
         <form id="form" runat="server">
             <%if(szRes=="") {%>
    <div class="header">
        <input type="hidden" id="op" name="op" value="" />
              <p style="margin-top: 2rem;">活动</p>
            <div class="activity-title"><%=activityName %></div>
        <img src="appui/images/choose-time.png">
             </div>
    <div class="btn-group">
          <%if (!bIsIN)
              {%>
            
    <button type="submit"  id="join" >加入</button>
          <%}
    else
    { %>
           <button type="submit"  id="out" >退出</button>
             
       
          <%} %> </div>

               <%} %>   <%else
    { %>
              <div class="header" style="margin-top: 2rem;">
        <img src="appui/images/operate.png">
        <p style="width: 80%;margin: 0 auto;"><%=szRes %></p>
    </div>
                <%} %>  
    </form>
            <div class="background back-result"></div>
   
        </div>
</body>
</html>