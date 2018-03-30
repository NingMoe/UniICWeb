<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResvTable.ascx.cs" Inherits="UI.UserControl.ResvTable" %>
<link href="<%=MyVPath %>themes/css/ResvTable.css" rel="stylesheet" type="text/css" />

<%if(TextMode){ %>
<div class="ResvTblTextMode">
    <%=m_szDevTbl %>
</div>
<%}else{ %>
<style type="text/css">
#pdevtbl_<%=ClientID %>
{
	width: <%=Width%>px;
	height:<%=Height%>px;
	overflow:hidden;
    <%if(!AutoScroll){ %>
	overflow-y:auto;
    <%} %>
    position:relative;
}
#pdevtbl_<%=ClientID %> .devtbl td,th
{
	height:<%=RowHeight %>px;
}
#pdevtbl_<%=ClientID %> .DevName
{
	height:<%=RowHeight %>px;
    line-height:<%=RowHeight %>px;
}
#pdevtbl_<%=ClientID %> .bgWeek
{
	height:<%=RowHeight %>px;
}
#pdevtbl_<%=ClientID %> .bgWeek div
{
	height:<%=RowHeight %>px;
	line-height:<%=RowHeight+1 %>px;
}
#pdevtbl_<%=ClientID %> .AreaTip
{
    line-height:<%=RowHeight%>px;
}
</style>
<script src="<%=MyVPath %>themes/js/ResvTable.js" charset="utf-8" type="text/javascript" ></script>

<div class="<%=Theme %>" id="pdevtbl_<%=ClientID %>">
    <table id="devtbl_<%=ClientID %>" class="devtbl">
    <thead>
        <%if(ShowWeek){ %><tr class="week"><th><%
              if (HasDate)
              {%>
            <input id="m_dwStartDate" />
              <%}%></th><th><div class="bgWeek"><%=m_szWeek %></div></th></tr><%} %>
        <tr class="hour"><th></th><th><div class="bgWeek"><%=m_szSec %></div></th></tr>
    </thead>
    <tbody><%=m_szDevTbl %></tbody>                    
    </table>
    <asp:Button ID="Button_Week" runat="server" class="Stealth" OnClick="Button_Week_Click"/>
    <asp:HiddenField ID="HF_Week" runat="server" />
    <asp:Button ID="Button_Resv" runat="server" class="Stealth" OnClick="Button_Resv_Click"/>
    <asp:HiddenField ID="HF_Resv" runat="server" />
    <asp:HiddenField ID="HF_StartDate" runat="server" />
    <asp:HiddenField ID="HF_WinScroll" runat="server" />
</div>

<script type="text/javascript">
	function funcpdevtbl_<%=ClientID %>()
	{
		var resvMode = <%=ResvMode%>;
		var totalwidth = <%=Width%>;<%--页面宽度--%>
		var devcolwidth = <%=DevColWidth%>;<%--为机房列宽度--%>
		var rowheight = <%=RowHeight%>;
		var colcount = <%=m_colcount%>;
		<%if(m_nRoomCount > 1 && AutoScroll){%>
		var AutoScroll = true;
		<%}else{%>
		var AutoScroll = false;
		<%}%>
		var ScrollSpeed = <%=ScrollSpeed%>;
		<%if(CanResv){%>
		var CanResv = true;
		<%}else{%>
		var CanResv = false;
		<%}%>
		var MyVPath = "<%=MyVPath %>";
		var szSecAreaMap = "<%=m_szSecAreaMap %>";
		
		var ServerCtrls={
			HF_Resv : $("#<%=HF_Resv.ClientID%>"),
			Button_Resv: $("#<%=Button_Resv.ClientID%>"),
			Button_Week: $("#<%=Button_Week.ClientID%>"),
			HF_WinScroll: $("#<%=HF_WinScroll.ClientID%>"),
			HF_Week: $("#<%=HF_Week.ClientID%>"),
			HF_StartDate:$("#<%=HF_StartDate.ClientID%>")
		};
		var SetDate = '<%=SetDate%>';
		InitResvTable(resvMode,totalwidth,devcolwidth,rowheight,colcount,"<%=ClientID %>",AutoScroll,ScrollSpeed,CanResv,MyVPath,szSecAreaMap,ServerCtrls,SetDate);
	}
    $(function(){setTimeout(funcpdevtbl_<%=ClientID %>,100);});
</script>
<%} %>