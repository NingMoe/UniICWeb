<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ImportList.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>管理员</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn">
            <a id="importTutorStu">导入<%=ConfigConst.GCTutorName%>导师对应表</a>
            <a id="importResearch"> <%=ConfigConst.GCReachTestName%>导入</a>
        </div>
        <div class="tb_btn">
          
        </div>
    </div>

    <script type="text/javascript">
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#importTutorStu").click(function () {
                $.lhdialog({
                    title: '导入学生<%=ConfigConst.GCTutorName%>对应',
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
                    title: <%=ConfigConst.GCReachTestName%>+'导入',
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