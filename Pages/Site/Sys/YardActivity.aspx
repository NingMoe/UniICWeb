<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="YardActivity.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��������</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a id="YardActivity" href="YardActivity.aspx">��������</a><a id="YardResvRule" href="YardResvRule.aspx">��������</a>
                </div>

            </div>
              <div class="FixBtn"><a id="btnNewLab">�½�����</a></div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>��������</th>
                        <th>�����볡������</th>
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">

            $(function () {
                $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="setRuleBtn" title="���ù�������"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a class="setRuleInfo" title="��������"><img src="../../../themes/icon_s/11.png"/></a>\
                        <a class="setRuleNotice" title="������֪"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a class="admin"  href="#" title="���ó�������Ա"><img src="../../../themes/icon_s/13.png"/></a>\
                        <a class="checkinfo"  href="#" title="�������˵��"><img src="../../../themes/icon_s/16.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setRuleBtn").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    fdata = "ExtValue=" + dwLabID;
                    TabInJumpReload("YardResvRule", fdata);
                });
                $(".setRuleInfo").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/pro/page/editContent.aspx?name=��������&type=aty_intro&id=" + dwLabID);
                });
                $(".setRuleNotice").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/pro/page/editContent.aspx?name=������֪&type=aty_rule&id=" + dwLabID);
                });
                $(".checkinfo").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    window.open("../../../ClientWeb/pro/page/editContent.aspx?name=�������˵��&type=ck_intro&id=" + dwLabID);
                });
                $(".setLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '820px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewYardActivityp.aspx?op=set&dwLabID=' + dwLabID
                    });
                });
                $(".admin").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���ó�������Ա',
                        width: '650px',
                        height: '350px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newYardAdmin.aspx?op=set&dwLabID=' + dwLabID
                    });
                });
                
                $(".setLabManager").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwMangGroupID = $(this).parents("tr").children().first().siblings().attr("data-manGroupID");
                    $.lhdialog({
                    title: '����<%=ConfigConst.GCLabName %>����Ա',
                    width: '660px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/ManGroupList.aspx?op=set&dwID=' + dwMangGroupID
                });
            });

                $(".InfoLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '����',
                        width: '760px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwLabID + "&type=LabInfo"
                    });
                });
                $(".delLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
            });
                $("#btnNewLab").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        content: 'url:Dlg/NewYardActivityp.aspx?op=new'
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
        <style>
           
        </style>
    </form>
</asp:Content>
