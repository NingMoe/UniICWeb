<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
        <li><a href="Summary.aspx">��ҳ</a></li>   
             <!--
          <li><a href="DevRoomList.aspx?dwClassKind=4097"><%=ConfigConst.GCRoomName %>����</a></li>
          <li><a href="DevRoomListMeeting.aspx?dwClassKind=2049">�����ҹ���</a></li>       
        <li><a href="DevRoomListGradute.aspx?dwClassKind=257">�о����ҹ���</a></li>  
       --> 
        
        <%if (nJLRoomManager!=1) {%>
        <li><a href="DeviceList.aspx"><%=ConfigConst.GCDevName %>����</a></li>   
        <%} %>
        <li><a href="RoomList.aspx"><%=ConfigConst.GCRoomName %>����</a></li>  
        <%if (nJLRoomManager!=1) {%>
         <li><a href="Plan.aspx">ʵ��ƻ�</a></li>   
        <li><a href="classgroup.aspx">�γ̰�</a></li>   
        
        <!--<li><a href="AttendRule.aspx">����</a></li>   
        <li><a href="TestPlan.aspx">ʵ��ƻ�</a></li>   -->
        <li><a href="RoomResvstate.aspx">�ſ����</a></li>   
        <li><a href="PlaticTeachResv.aspx">����״��</a></li> 
        <!--<li><a href="ResvTable.aspx">�ſ����1</a></li>    
        <li><a href="ResearchCheckList.aspx">��˹���</a></li>   -->
      <!--    <li><a href="ReserveRoomList.aspx">ԤԼ״��</a></li>-->
          <li><a href="VideoList.aspx"><%=ConfigConst.GCRoomName %>��Ƶ��ز�ѯ</a></li>
      <li><a href="ReserveTeachRoomList.aspx">ԤԼ״��</a></li>
         <li><a href="DisciList.aspx">ΥԼ�봦��</a></li>
         <%if (ConfigConst.GCDebug == 1){%>
          <li><a href="MemberInGroup.aspx">��ѯ���������б�</a></li>
            <%} %>
       <li><a href="bill.aspx">�˵�</a></li>
    <!--  <li><a href="ICINTRONotice.aspx">֪ͨ��ռ����</a></li>-->
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
