<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
          
        <li><a href="UniTeacher.aspx">��ʦ/��Ա����</a></li>
       <!-- 
        <li><a href="ResearchTest.aspx"><%=ConfigConst.GCReachTestName%>����</a></li>    
       
        <li><a href="AccountIdent.aspx"><%=ConfigConst.GCTutorName%>����</a></li> 
      -->
     <!--
     <li><a href="Lab.aspx"><%=ConfigConst.GCLabName%>����</a></li>
        <li><a href="Room.aspx"><%=ConfigConst.GCRoomName%>����</a></li>
        -->
      <!-- 
        <li><a href="devKind.aspx"><%=ConfigConst.GCKindName %>����</a></li>-->
  <!--  -->
        <!-- 
          <li><a href="SysKindRoom.aspx"><%=ConfigConst.GCRoomName %>����</a></li>    
         <li><a href="DevAndRoomList.aspx?classkind=2049">�����ҹ���</a></li>     
        <li><a href="DevAndRoomList.aspx?classkind=257">�о����ҹ���</a></li>     
       -->
        <!-- 
              
         <%if(ConfigConst.GCDevAndKind==0) { %>
     <li><a href="DevKind.aspx"><%=ConfigConst.GCKindName %>����</a></li> <%} %> 
        
         <%if(ConfigConst.GCDebug==1) {%>
        <li><a href="DevKindAndClass.aspx">���͹���</a></li>
         <%} %>
            -->
       <!-- <li><a href="channelgate.aspx">ͨ���Ź���</a></li>    -->
       
   
        <li><a href="device.aspx"><%=ConfigConst.GCDevName %>����</a></li>
         <li><a href="RoomGroup.aspx">������Ϲ���</a></li>
   <li><a href="Control.aspx">����̨����</a></li>
         <li><a href="course.aspx">�γ̹���</a></li>
          <li><a href="Term.aspx">ѧ�ڹ���</a></li>
         <!-- <li><a href="SysFunRule.aspx">���ģ������</a></li>  -->
    
          <%if(nIsAdminSup==1){ %>

  <li><a href="OpenRule.aspx">���Ź���</a></li>
    <li><a href="ResvRule.aspx">ԤԼ����</a></li>
        <%if(ConfigConst.GCDebug==1) {%>
        <li><a href="creditkind.aspx">�����ƶ����</a></li>
         
        <li><a href="CreditScroeRule.aspx">�����ƶ�</a></li><%} %>
   <li><a href="fee.aspx">�շѱ�׼</a></li>
        <!-- 
         <li><a href="UrlCtrl.aspx">�������</a></li>
    <li><a href="SWCtrl.aspx">������</a></li>
        -->
         <li><a href="adminxmlcfg.aspx">���ز�������</a></li>
    <li><a href="admin.aspx">����Ա��Ȩ�޹���</a></li>
            <%if(ConfigConst.GCDebug==1) {%>
       
        <%} %>
         <%} %>
    </ul>
</div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" src="../../../themes/js/MainJScript.js"></script>
</asp:Content>