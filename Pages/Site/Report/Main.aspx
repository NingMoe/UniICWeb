<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
       
            <li><a href="DevUsingStat.aspx"><%=ConfigConst.GCDevName %>ʹ����ͳ��</a></li>
       <!--
           <li><a href="DevKindUsingStat.aspx"><%=ConfigConst.GCKindName %>ʹ����ͳ��</a></li>
           
            -->
        <!--    <li><a href="LabUsingStat.aspx"><%=ConfigConst.GCLabName %>ʹ����ͳ��</a></li>
          <li><a href="RDevUsingTable.aspx">����ʹ����ͳ��ͼ</a></li>-->


             <li><a href="DeptusingStat.aspx">����<%=ConfigConst.GCDeptName %>ͳ��</a></li>
          
            <li><a href="IdentUsingStat.aspx">���ͳ��</a></li>
            <li><a href="YardActivityState.aspx">����ͳ��</a></li>
            <li><a href="PersonUsingStat.aspx">�����˽������а�</a></li>
                <li><a href="RDevUsingTable.aspx">��ʹ����ͳ��ͼ</a></li>
            <li><a href="RDevUsingTableWeek.aspx">����ʹ����ͳ��ͼ</a></li>


        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

