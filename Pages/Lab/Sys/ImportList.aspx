<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ImportList.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>����Ա</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn">
            <a id="importTutorStu">����<%=ConfigConst.GCTutorName%>��ʦ��Ӧ��</a>
            <a id="importResearch"> <%=ConfigConst.GCReachTestName%>����</a>
        </div>
        <div class="tb_btn">
          
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#importTutorStu").click(function () {
                $.lhdialog({
                    title: '����ѧ��<%=ConfigConst.GCTutorName%>��Ӧ',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/importTutorStu.aspx?op=new'
                });
            });
            $(".importResearch").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: <%=ConfigConst.GCReachTestName%>+'����',
                    width: '750px',
                    height: '420px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/importResearchTest.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>