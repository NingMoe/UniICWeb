<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RTestItemStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">教学实验项目表(查看原始数据)</h2>        
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
           
                     <%if(!bLeader) {%>
                <a href="RTestItemStat.aspx" id="RTestItemStat">查看原始数据</a>  
                    <a href="RTestItemStat2.aspx" id="RTestItemStat2">修改保存数据</a> 
                     <%} %>         
                    <a href="RTestItemStat3.aspx" id="RTestItemStat3">发布数据</a>             
                </div>
                <div style="margin:10px;">
                    <input type="submit" value="数据清空重新录入" id="subMit" title="已经修改的数据将会清空，请慎重" />
                </div>
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>学校代码</th>
                        <th>实验编号</th>
                        <th>实验名称</th>
                        <th>实验类别</th>
                        <th>实验类型</th>                      
                        <th>实验所属学科</th>                  
                        <th>实验要求</th>
                        <th>实验者类别</th>
                        <th>实验者人数</th>                                               
                        <th>每组人数</th>  
                        <th>实验学时数</th>  
                        <th>实验室编号</th>  
                        <th>实验室名称</th>                          
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
                $("#subMit").button();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });               
            });
          
           
            $("#btnok").button();

            $("#dwTeacher").autocomplete({
                source: "../data/searchAccount.aspx?type=logonname",
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {                           
                            $("#dwTeacher").val(ui.item.label);
                            $("#dwTeacherID").val(ui.item.id);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {
                     
                        ui.content.push({ label: " 未找到匹配项 " });
                    }
                }
            }).blur(function () {
                if ($(this).val() == "") {
                    $("#dwTeacherID").val("");
                }
            });;
            $("#dwCourse").autocomplete({
                source: "../data/searchCourse.aspx",
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#dwCourse").val(ui.item.label);
                            $("#dwCourseID").val(ui.item.id);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {                      
                        ui.content.push({ label: " 未找到匹配项 " });
                    }
                }
            }).blur(function () {
                if ($(this).val() == "")
                {
                    $("#dwCourseID").val("");
                }
            });

        </script>
        <style>         
            .tb_info table{border-bottom:1px solid #000}
            .tb_info table td{border-left:1px solid #000;border-right:1px solid #000;height:15px; border-top:1px solid #000}       
            .tb_info table th{border-left:1px solid #000;border-right:1px solid #000;height:15px; border-top:1px solid #000;}       
            .thHead
            {
                width:80px;
                text-align:center;
            }
            .context input
            {
                margin-left:15px;
            }
             .context select
            {
                margin-left:15px;

            }
        </style>
    </form>
</asp:Content>

