<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Sub_Plan" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="courseid" name="courseid" />
        <h2>课程实验计划</h2>
         <div class="toolbar">
            <div class="FixBtn">
                <a id="planImport">导入实验计划</a>
                <a id="newPlan">手动添加实验计划</a>

            </div>
          
        </div>
         <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                  
                   <th>学期:</th>


                   <td><select id="dwYearTerm" name="dwYearTerm">
                       <%=m_TermList %>
                       </select></td>
                   <th>教师工号</th>
                   <td><input type="text" id="pid" name="pid" /></td>
                   <th>课程名称</th>
                   <td><input type="text" id="courseName" name="courseName" /> </td>
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
        <div class="content">
          
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th >编号</th>
                        <th>计划名称</th>
                        <th>学期</th>
                        <th name="szTeacherName">教师</th>
                        <th>班级</th>
                        <th>课程</th>
                        <th>项目数</th>
                        <th name="dwTotalTestHour">学时</th>
                      <th>已排课学时</th>
                        <th>已完成学时</th>
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
                AutoCourseName($("#courseName"), 1, $("#courseid"), 1, false);

                $("#btn").button();
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
                
                $("#planImport").click(function () {
                    $.lhdialog({
                        title: '导入实验计划',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importtestplan.aspx?op=new'
                    });
                });
                $("#newPlan").click(function () {
                    $.lhdialog({
                        title: '手动添加实验计划',
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
