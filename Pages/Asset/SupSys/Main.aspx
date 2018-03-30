<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div id="tabs">
    <ul>
    <li><a href="Summary.aspx">主页</a></li>
    <li><a href="DCS.aspx">门禁集控器管理</a></li>
    <li><a href="DoorCtrl.aspx">房间门禁管理</a></li>
    <li><a href="Camera.aspx">摄像机管理</a></li>
    <li><a href="DoorCamera.aspx">房间摄像头管理</a></li>
    <li><a href="Station.aspx">站点管理</a></li>
    </ul>
</div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
