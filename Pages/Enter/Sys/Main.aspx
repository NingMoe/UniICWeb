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
