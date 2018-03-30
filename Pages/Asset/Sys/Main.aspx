<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
        
          <li><a href="Dept.aspx">基本条件设置</a></li>
         
           <li><a href="Room.aspx"><%=ConfigConst.GCLabName%>管理</a></li>
          
          <li><a href="AssertList.aspx">设备资产设置</a></li>
          
       <!--  <li><a href="devKind.aspx"><%=ConfigConst.GCKindName %>管理</a></li>
      
        <li><a href="ResearchTest.aspx"><%=ConfigConst.GCReachTestName%>管理</a></li>    
        
        <li><a href="AccountIdent.aspx"><%=ConfigConst.GCTutorName%>管理</a></li> 
       -->
      <!-- 
      <li><a href="Lab.aspx"><%=ConfigConst.GCLabName%>管理</a></li>
        <li><a href="Room.aspx"><%=ConfigConst.GCRoomName%>管理</a></li>
      <li><a href="RoomGroup.aspx">房间组合管理</a></li>-->
        
   <!--
          <li><a href="device.aspx"><%=ConfigConst.GCDevName %>管理</a></li>
       
          <li><a href="SysKindRoom.aspx"><%=ConfigConst.GCRoomName %>管理</a></li>    
         <li><a href="DevAndRoomList.aspx?classkind=2049">会议室管理</a></li>     
        <li><a href="DevAndRoomList.aspx?classkind=257">研究生室管理</a></li>     
        -->
        <!--
        <li><a href="channelgate.aspx">通道门管理</a></li>    
        -->
        <!--   
         <%if(ConfigConst.GCDevAndKind==0) { %>
     <li><a href="DevKind.aspx"><%=ConfigConst.GCKindName %>管理</a></li> <%} %> 
         -->

         <%if(ConfigConst.GCDebug==1) {%>
        <li><a href="DevKindAndClass.aspx">类型管理</a></li>
         <%} %>
        <!--
   <li><a href="Control.aspx">控制台管理</a></li>
         <li><a href="course.aspx">课程管理</a></li>
          <li><a href="Term.aspx">学期管理</a></li>
        -->
         <!-- <li><a href="SysFunRule.aspx">审核模块配置</a></li>  -->
    
          <%if(nIsAdminSup==1){ %>

  <!--<li><a href="OpenRule.aspx">开放规则</a></li>-->

            <%if (((nIsAdminSup == 1 || (szAdminPar.IndexOf("ResvRule") > -1))))
              {%>
          <li><a href="ResvRule.aspx">预约规则</a></li>
            <%} %>
  
       <!-- <%if(ConfigConst.GCDebug==1) {%>
        <li><a href="creditkind.aspx">信用制度类别</a></li>
         <%} %>
        <li><a href="CreditScroeRule.aspx">信用制度</a></li>-->
    <!--<li><a href="fee.aspx">收费标准</a></li>        -->

         <%if (((nIsAdminSup == 1)))
              {%>
    <li><a href="admin.aspx">管理员及权限管理</a></li>
         <%} %>
            <%if(ConfigConst.GCDebug==1) {%>
       
        <%} %>
         <%} %>
    </ul>
</div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" src="../../../themes/js/MainJScript.js"></script>
</asp:Content>