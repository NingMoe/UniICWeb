<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>

             <li><a href="account.aspx">账户管理</a></li>
            
            <!--  <li><a href="channelgate.aspx">通道门管理</a></li>    -->
            <%if ((ConfigConst.GCSysKind & 1) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindRoom") > -1))))
              {%>
             
            <li><a href="SysKindRoom.aspx"><%=ConfigConst.GCSysKindRoom %>管理</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 2) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindPC") > -1))))
              {%>
            <li><a href="SysKindPC.aspx"><%=ConfigConst.GCSysKindPC %>管理</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 4) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindLend") > -1))))
              {%>
            <li><a href="SysKindLend.aspx"><%=ConfigConst.GCSysKindLend %>管理</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 8) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindSeat") > -1))))
              {%>
            <li><a href="SysKindSeat.aspx"><%=ConfigConst.GCSysKindSeat %>管理</a></li>
            <%} %>

            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DevKind.aspx") > -1 && ConfigConst.GCDevAndKind == 0)
              { %>
            <!--
            <li><a href="DevKind.aspx"><%=ConfigConst.GCKindName %>管理</a></li>
            -->
            <%} %>
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("Control") > -1 && ConfigConst.GCDevAndKind == 0)
              { %>
            <li><a href="Control.aspx">控制台管理</a></li>
             <li><a href="mondex.aspx">智能监控管理</a></li>            
            <%} %>
            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("creditkind") > -1 && ConfigConst.GCDebug == 1)
              {%>
         <li><a href="creditkind.aspx">信用制度类别</a></li>
             <%} %>
               <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("CreditScroeRule") > -1 && ConfigConst.GCDebug == 1)
              {%>
            <li><a href="CreditScroeRule.aspx">信用制度</a></li>
            <%} %>
           
            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("OpenRule") > -1 && nIsAdminSup == 1)
              { %>

      <li><a href="OpenRule.aspx">开放规则</a></li>
             <%} %>
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("ResvRule") > -1 && nIsAdminSup == 1)
              { %>
            <li><a href="ResvRule.aspx">预约规则</a></li>
             <%} %>
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("fee") > -1 && nIsAdminSup == 1)
              { %>
         <li><a href="fee.aspx">收费标准</a></li>
             <li><a href="CodeTable.aspx?dwCodeType=6">字典表</a></li>
              <%} %>
             <% if (szfunctionMode != null && (Parse(szfunctionMode) & 4) > 0)
             { %>   
         <li><a href="urlctrl.aspx">上网监控</a></li>
            <li><a href="swctrl.aspx">程序监控</a></li>
            <%} %>

         <!--   <li><a href="term.aspx">学期管理</a></li>-->
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("admin") > -1 && nIsAdminSup == 1)
              { %>
            <li><a href="admin.aspx">管理员及权限管理</a></li>
            <%} %>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript" src="../../../themes/icon_s/js/MainJScript.js"></script>
</asp:Content>
