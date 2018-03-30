<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
       
            <li><a href="DevUsingStat.aspx"><%=ConfigConst.GCDevName %>使用率统计</a></li>
       <!--
           <li><a href="DevKindUsingStat.aspx"><%=ConfigConst.GCKindName %>使用率统计</a></li>
           
            -->
        <!--    <li><a href="LabUsingStat.aspx"><%=ConfigConst.GCLabName %>使用率统计</a></li>
          <li><a href="RDevUsingTable.aspx">场所使用率统计图</a></li>-->


             <li><a href="DeptusingStat.aspx">场所<%=ConfigConst.GCDeptName %>统计</a></li>
          
            <li><a href="IdentUsingStat.aspx">身份统计</a></li>
            <li><a href="YardActivityState.aspx">场景统计</a></li>
            <li><a href="PersonUsingStat.aspx">申请人借用排行榜</a></li>
                <li><a href="RDevUsingTable.aspx">天使用率统计图</a></li>
            <li><a href="RDevUsingTableWeek.aspx">星期使用率统计图</a></li>


        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

