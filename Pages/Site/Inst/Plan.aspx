<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Sub_Plan" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>课程实验计划</h2>
        <div class="toolbar">
            <div class="FixBtn"><a id="newPlan">手动添加实验计划</a></div>
            <div class="tb_btn">
                <!--
            <div class="AdvOpts"><div class="AdvLab">高级选项</div>
                <fieldset><legend>学期</legend>
                    <%if ((m_TermList & 1) != 0)
                      { %>
                    <label><input name="dwYearTerm" value="1" type="radio" />上学期</label>
                    <%}
                      if ((m_TermList & 2) != 0)
                      { %>
                    <label><input name="dwYearTerm" value="0" type="radio" />本学期</label>
                    <%}
                      if ((m_TermList & 4) != 0)
                      { %>
                    <label><input name="dwYearTerm" value="2" type="radio" />下学期</label>
                    <%} %>
                </fieldset>
                <fieldset><legend>课程</legend>
                    <label><input name="room" value="1" type="checkbox" />课程1</label>  <label><input name="room" value="2" type="checkbox" />课程2</label>
                </fieldset>
                <fieldset><legend>状态</legend>
                    <label><input name="room" value="1" type="checkbox" />已安排</label>  <label><input name="room" value="2" type="checkbox" />未安排</label>  <label><input name="room" value="2" type="checkbox" />部分安排</label>
                </fieldset>
            </div>
            -->
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">编号</th>
                        <th width="60px">计划名称</th>
                        <th width="60px">学期</th>
                        <th width="60px" name="szTeacherName">教师</th>
                        <th>班级</th>
                        <th>课程</th>
                        <th name="dwTotalTestHour">学时</th>
                        <th>状态</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="DoItemSet" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="DoItemDel" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });


                $(".DoItemDel").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: '删除实验计划',
                        width: '250px',
                        height: '100px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/DelPlan.aspx?op=del&id=' + dwID
                    });
                });


                $(".DoItemSet").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: '修改实验计划',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=set&id=' + dwID
                    });
                });

                $("#newPlan").click(function () {
                    $.lhdialog({
                        title: '手动添加实验计划',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
