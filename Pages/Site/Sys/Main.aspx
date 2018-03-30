<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
              
            <li><a href="WDevice.aspx">资源场所管理</a></li>
             
            <%if (nIsAdminSup == 1)
              { %>
            
           <li><a href="Wbulid.aspx">楼宇管理</a></li>
            <li><a href="WLab.aspx">物管部门</a></li>
            <!--
            <li><a href="WInRoomDev.aspx">室内资源管理</a></li>
            <li><a href="WOutRoomDev.aspx">室外资源管理</a></li>
            -->
            <!-- <li><a href="LabAndDevClass.aspx">资源类型</a></li>    
          <li><a href="RoomAndDevKind.aspx">资源名称</a></li> 
            <li><a href="DevKindCG.aspx">场地类型</a></li>
            <li><a href="labCG.aspx">场地管理部门</a></li>-->
            <li><a href="Wbulid.aspx">楼宇</a></li>
            <li><a href="ServiceType.aspx">服务管理</a></li>
            <li><a href="CheckType.aspx">审核类型</a></li>
            <li><a href="YardActivity.aspx">场景管理</a></li>
            <li><a href="CodeTable.aspx?dwCodeType=6">字典表</a></li>
            <!--  <li><a href="SysFunRule.aspx">审核模块配置</a></li>  -->

            <!-- <li><a href="ResvRule.aspx">预约规则</a></li>-->
        
        <li><a href="creditkind.aspx">信用制度类别</a></li>
      
        <li><a href="CreditScroeRule.aspx">信用制度</a></li>
       
          <!--     <li><a href="fee.aspx">收费标准</a></li> -->
            <li><a href="admin.aspx">管理员及权限管理</a></li>
            <%if (ConfigConst.GCDebug == 1)
              {%>

            <%} %>
            <%} %>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript" src="../../../themes/js/MainJScript.js"></script>
</asp:Content>
