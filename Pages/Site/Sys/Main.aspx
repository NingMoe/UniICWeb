<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
              
            <li><a href="WDevice.aspx">��Դ��������</a></li>
             
            <%if (nIsAdminSup == 1)
              { %>
            
           <li><a href="Wbulid.aspx">¥�����</a></li>
            <li><a href="WLab.aspx">��ܲ���</a></li>
            <!--
            <li><a href="WInRoomDev.aspx">������Դ����</a></li>
            <li><a href="WOutRoomDev.aspx">������Դ����</a></li>
            -->
            <!-- <li><a href="LabAndDevClass.aspx">��Դ����</a></li>    
          <li><a href="RoomAndDevKind.aspx">��Դ����</a></li> 
            <li><a href="DevKindCG.aspx">��������</a></li>
            <li><a href="labCG.aspx">���ع�����</a></li>-->
            <li><a href="Wbulid.aspx">¥��</a></li>
            <li><a href="ServiceType.aspx">�������</a></li>
            <li><a href="CheckType.aspx">�������</a></li>
            <li><a href="YardActivity.aspx">��������</a></li>
            <li><a href="CodeTable.aspx?dwCodeType=6">�ֵ��</a></li>
            <!--  <li><a href="SysFunRule.aspx">���ģ������</a></li>  -->

            <!-- <li><a href="ResvRule.aspx">ԤԼ����</a></li>-->
        
        <li><a href="creditkind.aspx">�����ƶ����</a></li>
      
        <li><a href="CreditScroeRule.aspx">�����ƶ�</a></li>
       
          <!--     <li><a href="fee.aspx">�շѱ�׼</a></li> -->
            <li><a href="admin.aspx">����Ա��Ȩ�޹���</a></li>
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
