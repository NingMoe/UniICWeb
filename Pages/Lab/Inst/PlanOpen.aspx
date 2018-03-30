<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PlanOpen.aspx.cs" Inherits="Sub_Plan" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
       <input type="hidden" id="courseid" name="courseid" />
        <input type="hidden" id="pidHidden" name="pidHidden" />
        <h2>开放实验计划</h2>
        
         <div class="toolbar">
              <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="plan.aspx">教学实验计划</a>
                    <a href="planOpen.aspx">开放实验计划</a>
                </div>

            </div>
            <div class="FixBtn">
             <!--   <a id="planImport">导入开放实验计划</a>-->
                <a id="newPlan">添加开放实验计划</a>

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
                           <td>
                       <select id="pid" name="pid"></select></td>
                   <th>状态</th>
                   <td><select id="dwStatus" style="width:120px;" name="dwStatus"><%=szStatus %></select></td>
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
                      
                        <th>计划名称</th>
                        <th>学期</th>
                        <th name="szTeacherName">教师</th>
                        <th>班级</th>
                        <th>课程</th>
                        <th>额定人数</th>
                        <th>已申请人数</th>
                        <th>截止加入日期</th>
                            <th>状态</th>
                        <th name="dwTotalTestHour">总学时</th>
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
                setTimeout(function () { GetUniTeache(); }, 200);
                $(".ListTbl").UniTable();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="DoItemSet" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="GetGroupMember" title="查看上课名单"><img src="../../../themes/icon_s/17.png"/></a>\
                        <a href="#" class="DoItemDel" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                AutoCourseName($("#courseName"), 1, $("#courseid"), 1, false);
                $("#dwYearTerm").change(function () {
                    GetUniTeache();
                });
                function GetUniTeache()
                {
                    var vYearTerm = $("#dwYearTerm").val();
                    $.get(
            "../data/searchUniTeacher.aspx",
            { YearTerm: vYearTerm },
            function (data) {
                var vData = eval(data);
                $("#pid").empty();
                var vOption1 = $("<option value='0'>" + "全部" + "</option>");
                $("#pid").append(vOption1);
                for (var i = 0; i < vData.length; i++) {
                    var vOption = $("<option value='" + vData[i].id + "'>" + vData[i].label + "</option>");
                    $("#pid").append(vOption);
                }
                if ($("#pidHidden").val() != "")
                {
                    $("#pid").val($("#pidHidden").val());
                }
            });
                  
                }
             

                $("#btn").button();
                $(".DoItemDel").click(function () {
                    var dwID = $(this).parents("tr").data("id");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + dwID);
                     }, '提示', 1, function () { });
                });
                var tabl = $(".UniTab").UniTab();
                $(".GetGroupMember").click(function () {
                    var dwID = $(this).parents("tr").data("groupid");

                    $.lhdialog({
                        title: '查看上课名单',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?op=set&id=' + dwID
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
                        content: 'url:Dlg/SetPlan2Open.aspx?op=set&id=' + dwID
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
                        content: 'url:Dlg/SetPlanOpen.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
