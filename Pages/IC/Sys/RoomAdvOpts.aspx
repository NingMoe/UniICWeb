<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomAdvOpts.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <fieldset><legend>ʵ����</legend>
        <%=m_szLab %>
    </fieldset>
    <!--<fieldset><legend>״̬</legend>
        <label><input name="stat" value="1" type="checkbox" />����</label>  <label><input name="stat" value="2" type="checkbox" />�쳣</label>  <label><input name="stat" value="3" type="checkbox" />�ſ�</label>  <label><input name="stat" value="4" type="checkbox" />ʹ����</label>
    </fieldset>-->
</asp:Content>