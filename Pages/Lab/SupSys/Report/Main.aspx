<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
    <li><a href="Summary.aspx">主页</a></li>
    <li><a href="Course.aspx">当前课程</a></li>
    <li><a href="FreeUser.aspx">课外自由上机</a></li>
    <li><a href="CheckMng.aspx">审核管理</a></li>
    <li><a href="Device.aspx">设备管理</a></li>
    <li><a href="Room.aspx">房间管理</a></li>
    <li><a href="Lab.aspx">实验室管理</a></li>
    <li><a href="ResvTable.aspx">课程实验安排表</a></li>
    <li><a href="Plan.aspx">课程实验计划</a></li>
    </ul>
</div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
