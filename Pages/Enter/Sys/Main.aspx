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
