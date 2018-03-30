<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceAdvOpts.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <fieldset><legend>实验室</legend>
        <%=m_szLab %>
    </fieldset>
    <fieldset><legend>机房</legend>
        <%=m_szRoom %>
    </fieldset>
    <fieldset><legend>状态</legend>
        <label><input name="stat" value="1" type="checkbox" />空闲</label>  <label><input name="room" value="2" type="checkbox" />教学使用中</label>    <label><input name="room" value="3" type="checkbox" />自由上机使用中</label>  <label><input name="room" value="4" type="checkbox" />故障</label>
    </fieldset>
</asp:Content>