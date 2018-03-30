<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="SysLog.aspx.cs" Inherits="_SysLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>系统日志</title>

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
	font-family: "Microsoft Yahei","微软雅黑","宋体";
	font-family:Verdana, Arial, Helvetica, sans-serif;
	_font-family: Microsoft Yahei,微软雅黑,"宋体";/**ie6 识别微软雅黑不要引号.ie6汉字会显示偏上位置，先用不存在字体 Tahoma,试下那就只能忽略这个问题**/
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
        font-family: "华文琥珀";
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
            return "<font>√</font>" + phtml;
        });
        $(".IconError").html(function(pindex, phtml){
            return "<font>×</font>" + phtml;
        });
        <%} %>
    });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:RadioButtonList ID="RBmenuList" runat="server" AutoPostBack="True" EnableTheming="True"
            RepeatDirection="Horizontal" Width="160px">
            <asp:ListItem Selected="True" Value="1">统计</asp:ListItem>
            <asp:ListItem Value="2">明细</asp:ListItem>
            <asp:ListItem Value="3">页面</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="ButtonRefresh" runat="server" OnClick="ButtonRefresh_Click" Text="刷新" Width="53px"/>
        <asp:DropDownList ID="ListViewDate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListViewDate_SelectedIndexChanged">
            <asp:ListItem Value="1">今天</asp:ListItem>
            <asp:ListItem Value="2">本周</asp:ListItem>
            <asp:ListItem Value="3">本月</asp:ListItem>
            <asp:ListItem Value="4">本年</asp:ListItem>
            <asp:ListItem Value="5">所有</asp:ListItem>
        </asp:DropDownList>
        <%if (menuv == 1){ %>
            <div>
        站点统计:
        <table>
        <thead><th>站点</th><th>成功数量</th><th>失败数量</th><th>用时累计</th><th>发送总计大小</th><th>接收总计大小</th></thead>
        <tbody><%=szStationStatOut%></tbody>
        </table>
    </div>
    <div>
        模块统计:
        <table>
        <thead><th>模块</th><th>成功数量</th><th>失败数量</th><th>用时累计</th><th>发送总计大小</th><th>接收总计大小</th><th>站点数量</th></thead>
        <tbody><%=szModuleStatOut%></tbody>
        </table>
    </div>
    <div>
        接口统计:
        <table>
        <thead><th>接口</th><th>成功数量</th><th>失败数量</th><th>用时累计</th><th>平均用时</th><th>发送总计大小</th><th>接收总计大小</th><th>站点数量</th></thead>
        <tbody><%=szCmdStatOut%></tbody>
        </table>
    </div>
    <div>
        会话列表:
        <table>
        <thead><th>站点</th><th>SessionID</th><th>最后操作时间</th><th>用户名</th><th>IP</th></thead>
        <tbody><%=szSDListOut%></tbody>
        </table>
    </div>
        <%}else if (menuv == 2){ %>
        <asp:Button ID="ButtonClear" runat="server" OnClientClick="return confirm('确定要清除吗？删除所有日志，并且不能恢复！');" OnClick="ButtonClear_Click" Text="清除" Width="53px" Visible="False" />
        <asp:CheckBox ID="CheckBoxALL" runat="server" Text="全部" AutoPostBack="True" Checked="True" OnCheckedChanged="CheckBoxALL_CheckedChanged" />
        <asp:DropDownList ID="ListViewType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListViewType_SelectedIndexChanged">
            <asp:ListItem Value="1">图标</asp:ListItem>
            <asp:ListItem Value="2">详细列表</asp:ListItem>
        </asp:DropDownList>&nbsp; &nbsp;<asp:DropDownList ID="FILTER_MODULE" runat="server"
            AutoPostBack="True" OnSelectedIndexChanged="FILTER_MODULE_SelectedIndexChanged">
            <asp:ListItem Value="0">全部</asp:ListItem>
            <asp:ListItem Value="1">PubERoom外部接口</asp:ListItem>
            <asp:ListItem Value="2">Station节点管理</asp:ListItem>
            <asp:ListItem Value="3">Device设备管理</asp:ListItem>
            <asp:ListItem Value="4">Admin管理员管理</asp:ListItem>
            <asp:ListItem Value="5">Rcnavi信息资源导航</asp:ListItem>
            <asp:ListItem Value="6">Control上网游戏控制</asp:ListItem>
            <asp:ListItem Value="7">UseRule使用规则管理</asp:ListItem>
            <asp:ListItem Value="8">Account帐户管理</asp:ListItem>
            <asp:ListItem Value="9">Card刷卡端管理</asp:ListItem>
        </asp:DropDownList><br />
        <%if (viewtype == 1){ %>
        <div class="IconList"><%=szOut %></div>
        <div class="clear"></div>
        <%}else if(viewtype == 2){ %>
    <table border="1">
    <thead><th>序号</th><th id="dwDate">时间</th><th>接口</th><th>命令</th><th id="dwRetCode">返回</th><th id="dwUseTime">用时</th><th>信息</th><th>请求大小</th><th>返回大小</th><th id="szStationSN">站点编号</th><th id="szSessionID">SessionID</th></thead>
    <tbody>
    <%=szOut %>
    </tbody>
    </table>
    <%} %>
    
    <div id="PageDiv" runat="server"></div>
    
    <%}else if(menuv == 3){ %>
    <asp:DropDownList ID="ListViewRS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListViewRS_SelectedIndexChanged">
            <asp:ListItem Value="1">粗</asp:ListItem>
            <asp:ListItem Value="2">细</asp:ListItem>
        </asp:DropDownList>
    <div>
        页面执行用时:
        <table>
        <thead><th>URL</th><th>访问次数</th><th>用时</th><th>步骤</th><th>备注</th></thead>
        <tbody><%=szURLListOut%></tbody>
        </table>
    </div>
    <%} %>
    </form>
</body>
</html>
