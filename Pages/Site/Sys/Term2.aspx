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
                <div data-id="0" class="tab" title="ͼ��ݼ�������OPAC��Ŀ����ר�û�,����1...����8"><div class="text">����<br />ʵ����</div></div>
                <div data-id="10" class="tab" title="����9������10"><div class="text">ϵͳ<br />ʵ����</div></div>
            </div>
        </td><td class="ResvTable">asdf</td></tr></table>
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