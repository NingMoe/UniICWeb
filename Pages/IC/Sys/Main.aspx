<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>

             <li><a href="account.aspx">�˻�����</a></li>
            
            <!--  <li><a href="channelgate.aspx">ͨ���Ź���</a></li>    -->
            <%if ((ConfigConst.GCSysKind & 1) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindRoom") > -1))))
              {%>
             
            <li><a href="SysKindRoom.aspx"><%=ConfigConst.GCSysKindRoom %>����</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 2) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindPC") > -1))))
              {%>
            <li><a href="SysKindPC.aspx"><%=ConfigConst.GCSysKindPC %>����</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 4) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindLend") > -1))))
              {%>
            <li><a href="SysKindLend.aspx"><%=ConfigConst.GCSysKindLend %>����</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 8) > 0&&((nIsAdminSup == 1 || (szAdminPar.IndexOf("SysKindSeat") > -1))))
              {%>
            <li><a href="SysKindSeat.aspx"><%=ConfigConst.GCSysKindSeat %>����</a></li>
            <%} %>

            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DevKind.aspx") > -1 && ConfigConst.GCDevAndKind == 0)
              { %>
            <!--
            <li><a href="DevKind.aspx"><%=ConfigConst.GCKindName %>����</a></li>
            -->
            <%} %>
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("Control") > -1 && ConfigConst.GCDevAndKind == 0)
              { %>
            <li><a href="Control.aspx">����̨����</a></li>
             <li><a href="mondex.aspx">���ܼ�ع���</a></li>            
            <%} %>
            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("creditkind") > -1 && ConfigConst.GCDebug == 1)
              {%>
         <li><a href="creditkind.aspx">�����ƶ����</a></li>
             <%} %>
               <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("CreditScroeRule") > -1 && ConfigConst.GCDebug == 1)
              {%>
            <li><a href="CreditScroeRule.aspx">�����ƶ�</a></li>
            <%} %>
           
            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("OpenRule") > -1 && nIsAdminSup == 1)
              { %>

      <li><a href="OpenRule.aspx">���Ź���</a></li>
             <%} %>
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("ResvRule") > -1 && nIsAdminSup == 1)
              { %>
            <li><a href="ResvRule.aspx">ԤԼ����</a></li>
             <%} %>
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("fee") > -1 && nIsAdminSup == 1)
              { %>
         <li><a href="fee.aspx">�շѱ�׼</a></li>
             <li><a href="CodeTable.aspx?dwCodeType=6">�ֵ��</a></li>
              <%} %>
             <% if (szfunctionMode != null && (Parse(szfunctionMode) & 4) > 0)
             { %>   
         <li><a href="urlctrl.aspx">�������</a></li>
            <li><a href="swctrl.aspx">������</a></li>
            <%} %>

         <!--   <li><a href="term.aspx">ѧ�ڹ���</a></li>-->
              <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("admin") > -1 && nIsAdminSup == 1)
              { %>
            <li><a href="admin.aspx">����Ա��Ȩ�޹���</a></li>
            <%} %>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript" src="../../../themes/icon_s/js/MainJScript.js"></script>
</asp:Content>
