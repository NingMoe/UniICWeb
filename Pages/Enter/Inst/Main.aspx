<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
            <li><a href="Summary.aspx">��ҳ</a></li>
            <%if ((ConfigConst.GCSysKind & 1) > 0&&(nIsAdminSup == 1 || (szAdminPar.IndexOf("DevRoomResvState") > -1)))
              {%>
            <li><a href="DevRoomResvState.aspx"><%=ConfigConst.GCSysKindRoom %>����</a></li>
            <%} %>
            <%if ( (ConfigConst.GCSysKind & 2) > 0&&(nIsAdminSup == 1 || (szAdminPar.IndexOf("DevPCList") > -1)))
              {%>
            <li><a href="DevPCList.aspx"><%=ConfigConst.GCSysKindPC %>����</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 4) > 0&&(nIsAdminSup == 1 || (szAdminPar.IndexOf("DevLendList") > -1)))
              {%>
            <li><a href="DevLendList.aspx"><%=ConfigConst.GCSysKindLend %>����</a></li>
            <%} %>
            <%if ((ConfigConst.GCSysKind & 8) > 0&&(nIsAdminSup == 1 || (szAdminPar.IndexOf("DevSeatList") > -1)))
              {%>
            <li><a href="DevSeatList.aspx"><%=ConfigConst.GCSysKindSeat %>����</a></li>
            <%} %>
            <%if((ConfigConst.GCSysKind & 16) > 0&&(nIsAdminSup == 1 || szAdminPar.IndexOf("Activityplan") > -1))
              {%>
            <li><a href="Activityplan.aspx">�����</a></li>
              <%} %>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("ReserveRoomList") > -1) {%>
             
            <li><a href="<%=szResvList %>">ԤԼ״��</a></li>
          <li><a href="bill.aspx">����״��</a></li>
             <%} %>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DisciList") > -1)
              {%>
            <li><a href="DisciList.aspx">ΥԼ�봦��</a></li>
             <%} %>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("ICINTROClass") > -1)
              {%>
         <!--   <li><a href="ICINTRONotice.aspx">֪ͨ��ռ�չʾ</a></li>-->
             <%} %>
        </ul>
    </div>
    <script type="text/javascript">
        $(function () {

        });
    </script>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
