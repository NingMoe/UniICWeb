<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
            <!--<li><a href="Summary.aspx">主页</a></li>-->
            
             <li><a href="DeviceList.aspx"><%=ConfigConst.GCDevName %>管理</a></li>   
            
           <li><a href="Stockaking.aspx">资产盘点</a></li>   
     <!--  <li><a href="DevListUse.aspx">资产使用周期</a></li>   -->
            
           <li><a href="ReservePersonRoomList.aspx">预约状况</a></li>
            
            
          <!--  <li><a href="Activityplan.aspx">活动安排</a></li>-->
            
            
            
            <li><a href="DisciList.aspx">违约与处罚</a></li>
           
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("ICINTROClass") > -1)
              {%>
         <!--   <li><a href="ICINTRONotice.aspx">通知与空间展示</a></li>-->
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
