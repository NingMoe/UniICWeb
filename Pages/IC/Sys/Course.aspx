<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>课程管理</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnResvRule">新建课程</a></div>
            <div class="tb_btn">
            </div>
        </div>
        
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                  
                   <th>课程名称:</th>
                   <td><input type="text" name="szCourseName" id="szCourseName" /></td>
                   
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szCourseCode">课程编码</th>
                        <th name="szCourseName">课程名称</th>
                        <th name="dwCourseProperty">课程性质</th>
                        <th name="dwCreditHour">学分</th>
                        <th name="dwTestNum">实验次数</th>
                        <th name="dwTestHour">实验学时数</th>
                        <th name="dwTheoryHour">理论学时数</th>
                        <th name="dwPracticeHour">实践学时数</th>
                        <th width="25px">操作</th>
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
                //$(".ListTbl").UniTable();

                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setResvRuleBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delResvRuleBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnResvRule").click(function () {
                    $.lhdialog({
                        title: '新建课程',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewCourse.aspx?op=new'
                    });
                });
                $(".setResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '修改课程',
                        width: '750px',
                        height: '380px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetCourse.aspx?op=set&dwID=' + dwID
                    });
                });
                $(".delResvRuleBtn").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
            });
        });
        </script>
    </form>
</asp:Content>
