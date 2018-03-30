<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Report_Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
    <li><a href="Report_Summary.aspx">统计总览</a></li>
    <li><a href="Report_StatResv.aspx">教学实验统计</a></li>
    <li><a href="Report_StatResv.aspx">自由上机统计</a></li>
    <li><a href="Report_StatResv.aspx">设备维护统计</a></li>
    <li><a href="Report_StatResv.aspx">设备使用率统计</a></li>
    <li><a href="Report_StatResv.aspx">使用明细</a></li>
    <li><a href="Report_StatResv.aspx">刷卡明细</a></li>
    </ul>
</div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" src="../../../themes/icon_s/js/MainJScript.js"></script>
</asp:Content>
