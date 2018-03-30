<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Sub_Plan" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="courseid" name="courseid" />
        <input type="hidden" id="pidHidden" name="pidHidden" />
        <h2>��ѧʵ��ƻ�</h2>

        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="plan.aspx">��ѧʵ��ƻ�</a>
                    <a href="planOpen.aspx">����ʵ��ƻ�</a>
                </div>

            </div>
            <div class="FixBtn">
                <!--   <a id="planImport">�����ѧʵ��ƻ�</a>-->
                <a id="newPlan">��ӽ�ѧʵ��ƻ�</a>
                
            </div>

        </div>
            <div style="margin-top:8px;width:99%;">  
             <a class="button" id="outplan">����ʵ��ƻ�</a>
                 <a class="button" id="outplanResv">�����ſ�״��</a> 
        </div>
        <div class="tb_infoInLine" style="margin: 5px 0px">
            <table style="width: 99%">
                <tr>

                    <th>ѧ��:</th>
                    <td>
                        <select id="dwYearTerm" name="dwYearTerm">
                            <%=m_TermList %>
                        </select></td>
                    <th>��ʦ����</th>
                    <td>
                        <select id="pid" name="pid"></select></td>
                    <th>�γ�����</th>
                    <td>
                        <input type="text" id="courseName" name="courseName" />
                    </td>
                    <th>
                        <input type="submit" id="btn" value="��ѯ" /></th>
                </tr>
            </table>
        </div>
        <div class="content">

            <table class="ListTbl">
                <thead>
                    <tr>

                        <th>�ƻ�����</th>
                        <th>ѧ��</th>
                        <th name="szTeacherName">��ʦ</th>
                        <th>�༶</th>
                        <th>�γ�</th>
                        <th name="dwTotalTestHour">��ѧʱ</th>
                        <th>���ſ�ѧʱ</th>
                        <th>�����ѧʱ</th>
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
                setTimeout(function () { GetUniTeache(); }, 200);
                $(".ListTbl").UniTable();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="DoItemSet" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="GetGroupMember" title="�鿴�Ͽ�����"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a href="#" class="setResv" title="�ſ�"><img src="../../../themes/icon_s/18.png"/></a>\
                        <a href="#" class="getResv" title="�鿴�ſ����"><img src="../../../themes/icon_s/19.png"/></a>\
                        <a href="#" class="DoItemDel" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                AutoCourseName($("#courseName"), 1, $("#courseid"), 1, false);
                $("#dwYearTerm").change(function () {
                    GetUniTeache();
                });

                $(".setResv").click(function () {
                    var testplanid = $(this).parents("tr").data("id");
                    var fdata = "testplanid=" + testplanid;
                    TabJumpReload(fdata, 5);
                });

                function GetUniTeache() {
                    var vYearTerm = $("#dwYearTerm").val();
                    $.get(
            "../data/searchUniTeacher.aspx",
            { YearTerm: vYearTerm },
            function (data) {
                var vData = eval(data);
                $("#pid").empty();
                var vOption1 = $("<option value='0'>" + "ȫ��" + "</option>");
                $("#pid").append(vOption1);
                for (var i = 0; i < vData.length; i++) {
                    var vOption = $("<option value='" + vData[i].id + "'>" + vData[i].label + "</option>");
                    $("#pid").append(vOption);
                }
                if ($("#pidHidden").val() != "") {
                    $("#pid").val($("#pidHidden").val());
                }
            });

                }
                $(".button").button();
                
                $("#btn").button();
                $(".DoItemDel").click(function () {
                    var dwID = $(this).parents("tr").data("id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + dwID);
                    }, '��ʾ', 1, function () { });
                });

                $("#outplan").click(function () {
                    var dwYearTerm = $("#dwYearTerm").val();
                    var pid = $("#pid").val();
                    var courseid = $("#courseid").val();
                    var pidHidden = $("#pidHidden").val();
                    $.lhdialog({
                        title: '����ʵ��ƻ�',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/planOutExport.aspx?op=set&dwYearTerm=' + dwYearTerm + "&pid=" + pid + "&courseid=" + courseid + "&pidHidden=" + pidHidden
                    });

                });
                $("#outplanResv").click(function () {
                    var dwYearTerm = $("#dwYearTerm").val();
                    var pid = $("#pid").val();
                    var courseid = $("#courseid").val();
                    var pidHidden = $("#pidHidden").val();
                    $.lhdialog({
                        title: '�����ſ�״��',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/planresvOutExport.aspx?op=set&dwYearTerm=' + dwYearTerm + "&pid=" + pid + "&courseid=" + courseid + "&pidHidden=" + pidHidden
                    });

                });
                var tabl = $(".UniTab").UniTab();
                $(".GetGroupMember").click(function () {
                    var dwID = $(this).parents("tr").data("groupid");
                    $.lhdialog({
                        title: '�鿴�Ͽ�����',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?op=set&id=' + dwID
                    });
                });
                
                $(".getResv").click(function () {
                    var dwID = $(this).parents("tr").data("id");
                    $.lhdialog({
                        title: '�鿴�ſ����',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/getTestPlanResv.aspx?op=set&id=' + dwID
                    });
                });

                $(".DoItemSet").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: '�޸�ʵ��ƻ�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan2.aspx?op=set&id=' + dwID
                    });
                });

                $("#planImport").click(function () {
                    $.lhdialog({
                        title: '����ʵ��ƻ�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importtestplan.aspx?op=new'
                    });
                });
                $("#newPlan").click(function () {
                    $.lhdialog({
                        title: '�ֶ����ʵ��ƻ�',
                        width: '1000px',
                        height: '550px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
