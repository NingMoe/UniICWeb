<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
            <!--<li><a href="Summary.aspx">��ҳ</a></li>-->
            
             <li><a href="DeviceList.aspx"><%=ConfigConst.GCDevName %>����</a></li>   
            
           <li><a href="Stockaking.aspx">�ʲ��̵�</a></li>   
     <!--  <li><a href="DevListUse.aspx">�ʲ�ʹ������</a></li>   -->
            
           <li><a href="ReservePersonRoomList.aspx">ԤԼ״��</a></li>
            
            
          <!--  <li><a href="Activityplan.aspx">�����</a></li>-->
            
            
            
            <li><a href="DisciList.aspx">ΥԼ�봦��</a></li>
           
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
