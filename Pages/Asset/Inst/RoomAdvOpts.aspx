<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomAdvOpts.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <fieldset><legend>实验室</legend>
        <%=m_szLab %>
    </fieldset>
    <fieldset><legend>状态</legend>
        <label><input name="stat" value="1" type="checkbox" />正常</label>  <label><input name="stat" value="2" type="checkbox" />异常</label>  <label><input name="stat" value="3" type="checkbox" />门开</label>  <label><input name="stat" value="4" type="checkbox" />使用中</label>
    </fieldset>
</asp:Content>