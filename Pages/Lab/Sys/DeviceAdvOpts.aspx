<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceAdvOpts.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <fieldset><legend>ʵ����</legend>
        <%=m_szLab %>
    </fieldset>
    <fieldset><legend>����</legend>
        <%=m_szRoom %>
    </fieldset>
    <!--<fieldset><legend>״̬</legend>
        <label><input name="stat" value="1" type="checkbox" />����</label>  <label><input name="room" value="2" type="checkbox" />��ѧʹ����</label>    <label><input name="room" value="3" type="checkbox" />�����ϻ�ʹ����</label>  <label><input name="room" value="4" type="checkbox" />����</label>
    </fieldset>-->
</asp:Content>