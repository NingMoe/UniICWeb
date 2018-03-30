<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="SysLog.aspx.cs" Inherits="_SysLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ϵͳ��־</title>

    <style type="text/css">
    .PageCtrl
    {
        margin:2px;
    }
    .canOrder
    {
        cursor:pointer;
    }
    </style>
    
    <style type="text/css">
    body{
    	font-family:  "Microsoft Yahei",sans-serif !important;
	font-family: "Microsoft Yahei","΢���ź�","����";
	font-family:Verdana, Arial, Helvetica, sans-serif;
	_font-family: Microsoft Yahei,΢���ź�,"����";/**ie6 ʶ��΢���źڲ�Ҫ����.ie6���ֻ���ʾƫ��λ�ã����ò��������� Tahoma,�����Ǿ�ֻ�ܺ����������**/
    font-size: 12px;
    background: #eeeeee;
    }
    table{
	    border-collapse:collapse;border-spacing:0;
	    width:100%;
	    background: white;
    }
    thead{
        background:#333333;
        color:#eeeeee;
        font-weight:bold;
    }
    td,th{
        border-left:1px solid #cccccc;
        border-right:1px solid #cccccc;
        padding:2px;
    }
    tr{
        border-bottom:1px solid #000000;
    }
    .error td
    {
        background: #ff9999;
    }
    .errcode
    {
        color:#ff0000;
    }
    
    .focus
    {
        border:1px solid #9999ff;
        color:blue;
        filter:alpha(opacity=80);
        -moz-opacity:0.8;
        opacity: 0.8;
        -khtml-opacity: 0.8;
    }    
    .select
    {
        background: #aaaaff;
        color:blue;
    }
    .select td
    {
        background:none;
    }

    /*================================*/
    .IconList
    {
        width:100%;
    }    
    .Icon
    {
        display:block;
        float:left;
        border:1px solid #aaaaaa;
        margin: 2px;
        padding: 2px;
        height:20px;
        vertical-align:middle;
    }
    .IconOK
    {
        background: url("UI_themes/images/bg_title.png");
    }
    .IconError
    {
        background: url("UI_themes/images/bg_header.png");
    }
    .Icon font
    {
        font-size:16px;
        font-family: "��������";
    }
    .IconOK font
    {
        color:green;
    }
    .IconError font
    {
        color:red;
    }

    .clear{
        clear:both;
        visibility:hidden;
    }
    </style>
    <script type="text/javascript" src="themes/jQuery/js/jquery-1.9.1.js"></script>
    <script type="text/javascript">
    $(function(){
        var trs = $("tbody tr");
        trs.dblclick(function(){
            $(".select").removeClass("select");

            var clsid = $(this).children(":last-child").text();
            if(clsid == "" || clsid == "0")return;
            $(".S_"+clsid).toggleClass("select");
        });
        trs.mouseover(function(){
            var clsid = $(this).children(":last-child").text();
            if(clsid == "" || clsid == "0")return;
            $(".S_"+clsid).addClass("focus");
        });
        trs.mouseout(function(){
            var clsid = $(this).children(":last-child").text();
            if(clsid == "" || clsid == "0")return;
            $(".S_"+clsid).removeClass("focus");
        });
        
          
        <%if(viewtype == 1){ %>
        $(".IconOK").html(function(pindex, phtml){
            return "<font>��</font>" + phtml;
        });
        $(".IconError").html(function(pindex, phtml){
            return "<font>��</font>" + phtml;
        });
        <%} %>
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:RadioButtonList ID="RBmenuList" runat="server" AutoPostBack="True" EnableTheming="True"
            RepeatDirection="Horizontal" Width="160px">
            <asp:ListItem Selected="True" Value="1">ͳ��</asp:ListItem>
            <asp:ListItem Value="2">��ϸ</asp:ListItem>
            <asp:ListItem Value="3">ҳ��</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="ButtonRefresh" runat="server" OnClick="ButtonRefresh_Click" Text="ˢ��" Width="53px"/>
        <asp:DropDownList ID="ListViewDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListViewDate_SelectedIndexChanged">
            <asp:ListItem Value="1">����</asp:ListItem>
            <asp:ListItem Value="2">����</asp:ListItem>
            <asp:ListItem Value="3">����</asp:ListItem>
            <asp:ListItem Value="4">����</asp:ListItem>
            <asp:ListItem Value="5">����</asp:ListItem>
        </asp:DropDownList>
        <%if (menuv == 1){ %>
            <div>
        վ��ͳ��:
        <table>
        <thead><th>վ��</th><th>�ɹ�����</th><th>ʧ������</th><th>��ʱ�ۼ�</th><th>�����ܼƴ�С</th><th>�����ܼƴ�С</th></thead>
        <tbody><%=szStationStatOut%></tbody>
        </table>
    </div>
    <div>
        ģ��ͳ��:
        <table>
        <thead><th>ģ��</th><th>�ɹ�����</th><th>ʧ������</th><th>��ʱ�ۼ�</th><th>�����ܼƴ�С</th><th>�����ܼƴ�С</th><th>վ������</th></thead>
        <tbody><%=szModuleStatOut%></tbody>
        </table>
    </div>
    <div>
        �ӿ�ͳ��:
        <table>
        <thead><th>�ӿ�</th><th>�ɹ�����</th><th>ʧ������</th><th>��ʱ�ۼ�</th><th>ƽ����ʱ</th><th>�����ܼƴ�С</th><th>�����ܼƴ�С</th><th>վ������</th></thead>
        <tbody><%=szCmdStatOut%></tbody>
        </table>
    </div>
    <div>
        �Ự�б�:
        <table>
        <thead><th>վ��</th><th>SessionID</th><th>������ʱ��</th><th>�û���</th><th>IP</th></thead>
        <tbody><%=szSDListOut%></tbody>
        </table>
    </div>
        <%}else if (menuv == 2){ %>
        <asp:Button ID="ButtonClear" runat="server" OnClientClick="return confirm('ȷ��Ҫ�����ɾ��������־�����Ҳ��ָܻ���');" OnClick="ButtonClear_Click" Text="���" Width="53px" Visible="False" />
        <asp:CheckBox ID="CheckBoxALL" runat="server" Text="ȫ��" AutoPostBack="True" Checked="True" OnCheckedChanged="CheckBoxALL_CheckedChanged" />
        <asp:DropDownList ID="ListViewType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListViewType_SelectedIndexChanged">
            <asp:ListItem Value="1">ͼ��</asp:ListItem>
            <asp:ListItem Value="2">��ϸ�б�</asp:ListItem>
        </asp:DropDownList>&nbsp; &nbsp;<asp:DropDownList ID="FILTER_MODULE" runat="server"
            AutoPostBack="True" OnSelectedIndexChanged="FILTER_MODULE_SelectedIndexChanged">
            <asp:ListItem Value="0">ȫ��</asp:ListItem>
            <asp:ListItem Value="1">PubERoom�ⲿ�ӿ�</asp:ListItem>
            <asp:ListItem Value="2">Station�ڵ����</asp:ListItem>
            <asp:ListItem Value="3">Device�豸����</asp:ListItem>
            <asp:ListItem Value="4">Admin����Ա����</asp:ListItem>
            <asp:ListItem Value="5">Rcnavi��Ϣ��Դ����</asp:ListItem>
            <asp:ListItem Value="6">Control������Ϸ����</asp:ListItem>
            <asp:ListItem Value="7">UseRuleʹ�ù������</asp:ListItem>
            <asp:ListItem Value="8">Account�ʻ�����</asp:ListItem>
            <asp:ListItem Value="9">Cardˢ���˹���</asp:ListItem>
        </asp:DropDownList><br />
        <%if (viewtype == 1){ %>
        <div class="IconList"><%=szOut %></div>
        <div class="clear"></div>
        <%}else if(viewtype == 2){ %>
    <table border="1">
    <thead><th>���</th><th id="dwDate">ʱ��</th><th>�ӿ�</th><th>����</th><th id="dwRetCode">����</th><th id="dwUseTime">��ʱ</th><th>��Ϣ</th><th>�����С</th><th>���ش�С</th><th id="szStationSN">վ����</th><th id="szSessionID">SessionID</th></thead>
    <tbody>
    <%=szOut %>
    </tbody>
    </table>
    <%} %>
    
    <div id="PageDiv" runat="server"></div>
    
    <%}else if(menuv == 3){ %>
    <asp:DropDownList ID="ListViewRS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListViewRS_SelectedIndexChanged">
            <asp:ListItem Value="1">��</asp:ListItem>
            <asp:ListItem Value="2">ϸ</asp:ListItem>
        </asp:DropDownList>
    <div>
        ҳ��ִ����ʱ:
        <table>
        <thead><th>URL</th><th>���ʴ���</th><th>��ʱ</th><th>����</th><th>��ע</th></thead>
        <tbody><%=szURLListOut%></tbody>
        </table>
    </div>
    <%} %>
    </form>
</body>
</html>
