<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Term2.aspx.cs" Inherits="Sub_Device"%>

<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formTabPanel" runat="server">  
    <div class="content">
        <table>
            <tr>
                <td class="ResvDevTab">
            <input name="DevStartLine" id="DevStartLine" value="0" type="hidden"/>
            <div class="ResvDevTabCont">
                <div data-id="0" class="tab" title="图书馆检索机，OPAC书目检索专用机,机房1...机房8"><div class="text">中心<br />实验室</div></div>
                <div data-id="10" class="tab" title="机房9，机房10"><div class="text">系统<br />实验室</div></div>
            </div>
        </td><td class="ResvTable">asdf</td></tr></table>
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
		$(".ResvDevTabCont .tab").bind('click', function () {
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