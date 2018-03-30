<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResvTable.aspx.cs" Inherits="Sub_ResvTable"%>
<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formTabPanel" runat="server">
    <h2>课程实验安排表</h2>
    <div class="toolbar">
        <div class="tb_info"><div>　　　　　<label>已安排数量：</label><a>16条</a>， <label>已安排学时：</label><a>30个</a></div></div>
        <div class="FixBtn"><select><option>本日安排表</option><option selected="selected">本周安排表</option><option>本学期安排表</option></select></div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">高级选项</div>
                <fieldset><legend>学期</legend>
                    <%if ((m_TermList & 1) != 0)
                      { %>
                    <label><input name="reserved.dwYearTerm" value="1" type="radio" />上学期</label>
                    <%}
                      if ((m_TermList & 2) != 0)
                      { %>
                    <label><input name="reserved.dwYearTerm" value="0" type="radio" />本学期</label>
                    <%}
                      if ((m_TermList & 4) != 0)
                      { %>
                    <label><input name="reserved.dwYearTerm" value="2" type="radio" />下学期</label>
                    <%} %>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table><tr><td class="ResvDevTab">
            <input name="DevStartLine" id="DevStartLine" value="0" type="hidden"/>
            <div class="ResvDevTabCont">
                <div data-id="0" class="tab" title="图书馆检索机，OPAC书目检索专用机,机房1...机房8"><div class="text">科研测试中心<br />实验室</div></div>
                <div data-id="10" class="tab" title="机房9，机房10"><div class="text">系统<br />实验室</div></div>
            </div>
        </td><td class="ResvTable">
            <uc1:ResvTable runat="server" ID="ResvTable" HasDate="true" Theme="dream" ResvMode="1" DevColWidth="100" ShowWeek="true" AutoScroll="false" CanResv="true" Width="730" Height="460"/>
        </td></tr></table>
    </div>
</form>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="ScriptContent" Runat="Server">
<script type="text/javascript">
	var ResvRule = [
		 //secLink,时间节次关联必须放最前面。
		{ dev: "1", secLink: [[2], [3], [4, 5, 6]] },
		{ dev: "*", secLink: [[2, 3], [4, 5, 6]] },

		// onlySecCount:最多可选时间点个数，continuous：多个时间点时是否必须连续，onlySingle：是否一次只能选一台设备。
		{ dev: "*", onlySecCount: 4, continuous: true, onlySingle: true },

		  //RetOK:true表示符合预约条件检测，必须放最后面。
		{ dev: "*", RetOK: true, needSecCount: 1 }
	];

	//确认预约函数
	function ResvHandle(val, dwDate) {
	    if(val.length == 0)return;
	    var RoomID = val[0].dev;
	    var dwBeginSec = 9999;
	    var dwEndSec = -1;
	    for (var i = 0; i < val.length; i++) {
	        if (val[i].sec < dwBeginSec) {
	            dwBeginSec = val[i].sec;
	        }
	        if (val[i].sec > dwEndSec) {
	            dwEndSec = val[i].sec;
	        }
		}

		$.lhdialog({
			title: '新建实验安排',
			width: '800px',
			height: '400px',
			lock: true,
			data: Dlg_Callback,
			content: 'url:Dlg/SetResv.aspx?dwDate=' + dwDate + '&RoomID=' + RoomID + '&dwBeginSec=' + dwBeginSec + '&dwEndSec=' + dwEndSec
		});
	}

	//选择星期
	function ChangeWeek(d) {
		if (TabReload) {
			ShowWait();
			TabReload($("#<%=formTabPanel.ClientID%>").serialize());
		}
	}

	//选择开始日期
	function ChangeStartDate(d) {
		if (TabReload) {
			ShowWait();
			TabReload($("#<%=formTabPanel.ClientID%>").serialize());
		}
	}
	
    $(document).ready(function () {
	    HideWait();
	    $(".DevName").parent().css("border-left", "0px");
		$(".ResvDevTabCont .tab");
		var DevStartLine = $("#DevStartLine");
		DevStartLine.val('<%=Request["DevStartLine"]%>');
	    if (DevStartLine.val() == '') DevStartLine.val("0");

	    var dis = false;
	    $(".ResvDevTabCont .tab").bind('click',function () {
	        if (dis) return;
	        dis = true;
	        ShowWait();
	        DevStartLine.val($(this).data("id"));
	        TabReload($("#<%=formTabPanel.ClientID%>").serialize());
	    });

	    $(".ResvDevTabCont .tab[data-id='" + DevStartLine.val() + "']").addClass("actTab").unbind('click');

	});
</script>
</asp:Content>
