<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
        <li><a href="Summary.aspx">主页</a></li>   
             <!--
          <li><a href="DevRoomList.aspx?dwClassKind=4097"><%=ConfigConst.GCRoomName %>管理</a></li>
          <li><a href="DevRoomListMeeting.aspx?dwClassKind=2049">研讨室管理</a></li>       
        <li><a href="DevRoomListGradute.aspx?dwClassKind=257">研究生室管理</a></li>  
       --> 
        
        <%if (nJLRoomManager!=1) {%>
        <li><a href="DeviceList.aspx"><%=ConfigConst.GCDevName %>管理</a></li>   
        <%} %>
        <li><a href="RoomList.aspx"><%=ConfigConst.GCRoomName %>管理</a></li>  
        <%if (nJLRoomManager!=1) {%>
         <li><a href="Plan.aspx">实验计划</a></li>   
        <li><a href="classgroup.aspx">课程班</a></li>   
        
        <!--<li><a href="AttendRule.aspx">考勤</a></li>   
        <li><a href="TestPlan.aspx">实验计划</a></li>   -->
        <li><a href="RoomResvstate.aspx">排课情况</a></li>   
        <li><a href="PlaticTeachResv.aspx">考勤状况</a></li> 
        <!--<li><a href="ResvTable.aspx">排课情况1</a></li>    
        <li><a href="ResearchCheckList.aspx">审核管理</a></li>   -->
      <!--    <li><a href="ReserveRoomList.aspx">预约状况</a></li>-->
          <li><a href="VideoList.aspx"><%=ConfigConst.GCRoomName %>视频监控查询</a></li>
      <li><a href="ReserveTeachRoomList.aspx">预约状况</a></li>
         <li><a href="DisciList.aspx">违约与处罚</a></li>
         <%if (ConfigConst.GCDebug == 1){%>
          <li><a href="MemberInGroup.aspx">查询人所在组列表</a></li>
            <%} %>
       <li><a href="bill.aspx">账单</a></li>
    <!--  <li><a href="ICINTRONotice.aspx">通知与空间介绍</a></li>-->
        <%} %>
    </ul>
</div>
    <script type="text/javascript">
        $(function () {
        });
    </script>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
