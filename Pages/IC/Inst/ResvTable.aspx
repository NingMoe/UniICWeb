<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResvTable.aspx.cs" Inherits="Sub_ResvTable"%>
<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formTabPanel" runat="server">
    <h2>�γ�ʵ�鰲�ű�</h2>
    <div class="toolbar">
        <div class="tb_info"><div>����������<label>�Ѱ���������</label><a>16��</a>�� <label>�Ѱ���ѧʱ��</label><a>30��</a></div></div>
        <div class="FixBtn"><select><option>���հ��ű�</option><option selected="selected">���ܰ��ű�</option><option>��ѧ�ڰ��ű�</option></select></div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">�߼�ѡ��</div>
                <fieldset><legend>ѧ��</legend>
                    <%if ((m_TermList & 1) != 0)
                      { %>
                    <label><input name="reserved.dwYearTerm" value="1" type="radio" />��ѧ��</label>
                    <%}
                      if ((m_TermList & 2) != 0)
                      { %>
                    <label><input name="reserved.dwYearTerm" value="0" type="radio" />��ѧ��</label>
                    <%}
                      if ((m_TermList & 4) != 0)
                      { %>
                    <label><input name="reserved.dwYearTerm" value="2" type="radio" />��ѧ��</label>
                    <%} %>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table><tr><td class="ResvDevTab">
            <input name="DevStartLine" id="DevStartLine" value="0" type="hidden"/>
            <div class="ResvDevTabCont">
                <div data-id="0" class="tab" title="ͼ��ݼ�������OPAC��Ŀ����ר�û�,����1...����8"><div class="text">���в�������<br />ʵ����</div></div>
                <div data-id="10" class="tab" title="����9������10"><div class="text">ϵͳ<br />ʵ����</div></div>
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
		 //secLink,ʱ��ڴι����������ǰ�档
		{ dev: "1", secLink: [[2], [3], [4, 5, 6]] },
		{ dev: "*", secLink: [[2, 3], [4, 5, 6]] },

		// onlySecCount:����ѡʱ��������continuous�����ʱ���ʱ�Ƿ����������onlySingle���Ƿ�һ��ֻ��ѡһ̨�豸��
		{ dev: "*", onlySecCount: 4, continuous: true, onlySingle: true },

		  //RetOK:true��ʾ����ԤԼ������⣬���������档
		{ dev: "*", RetOK: true, needSecCount: 1 }
	];

	//ȷ��ԤԼ����
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
			title: '�½�ʵ�鰲��',
			width: '800px',
			height: '400px',
			lock: true,
			data: Dlg_Callback,
			content: 'url:Dlg/SetResv.aspx?dwDate=' + dwDate + '&RoomID=' + RoomID + '&dwBeginSec=' + dwBeginSec + '&dwEndSec=' + dwEndSec
		});
	}

	//ѡ������
	function ChangeWeek(d) {
		if (TabReload) {
			ShowWait();
			TabReload($("#<%=formTabPanel.ClientID%>").serialize());
		}
	}

	//ѡ��ʼ����
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
