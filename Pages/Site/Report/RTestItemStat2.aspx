<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RTestItemStat2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
         <input type="hidden" name="opSub" id="opSub" value="0" />

        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">教学实验项目表(修改保存数据)</h2>        
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                     <%if(!bLeader) {%>
                <a href="RTestItemStat.aspx" id="RTestItemStat">查看原始数据</a>  
                    <a href="RTestItemStat2.aspx" id="RTestItemStat2">修改保存数据</a> 
                     <%} %>         
                    <a href="RTestItemStat3.aspx" id="RTestItemStat3">发布数据</a> 
                </div>
                 <div style="margin:10px">
                    <input type="submit" value="保存已修改的数据" id="btnSave" />
                    <input type="button" value="发布修改好的数据" id="btnOp" style="margin-left:10px" />

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
                $("#btnOp").click(function () {

                    $("#opSub").val("1");
                    TabReload($(this).parents("form").serialize());
                });
                $("#btnOp,#btnSave").button();
                $(".UniTab").UniTab();
                $("#subMit").button();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });               
            });
            $('.tdSet').click(function () {
                var vtd = $(this);
                var vdwTesteeKind = '<%=dwTesteeKind%>';
                var vdwAcademicSubject = '<%=dwAcademicSubject%>';
                var vszLab = '<%=szLab%>';

                var devid = vtd.parents("tr").children().first().data("id");
                var dwTesteeKind = vtd.parents("tr").children().first().data("dwtesteekind");
                var dwLabID = vtd.parents("tr").children().first().data("dwlabid");
                var dwAcademicSubject = vtd.parents("tr").children().first().data("dwacademicsubject");

                var type = vtd.data("type");
                var kind = vtd.data("kind");
                var valueTd = vtd.data("value");
               
                var vInputList = $("input", vtd);
                if (vInputList.length > 0) {
                    return;
                }
                var vSelectList = $("select", vtd);
                if (vSelectList.length > 0) {
                    return;
                }
                var html = vtd.html();
                var input;
                //debugger;
                if (kind == "select") {
                    if (type == "dwTesteeKind") {
                        input = $("<select style='width:70px' >" + vdwTesteeKind + "</select>");
                    }
                    else if (type == "dwAcademicSubject") {
                        input = $("<select style='width:90px' >" + vdwAcademicSubject + "</select>");
                    }
                    else if (type == "dwLabID") {
                        input = $("<select style='width:70px' >" + vszLab + "</select>");
                    }
                    input.val(valueTd);
                    vtd.empty();
                    vtd.append(input);
                    input.focus();
                }
                else if (kind == "text") {
                    input = $("<input type='text' style='width:40px' />");
                    input.val(html);
                    vtd.empty();
                    vtd.append(input);
                    input.focus();
                }
              
                $("select,input", vtd).on("blur", function (event) {
                    var myObject = new Object();
                    var data;
                    myObject.dwTestCardID = devid;
                    myObject.dwTesteeKind = dwTesteeKind;
                    myObject.dwAcademicSubject = dwAcademicSubject;
                    myObject.dwTesteeNum = 0;
                    myObject.dwLabID = dwLabID;
                    var value = $(this).val();
                    var szValue = "";
                   
                    if (type == "dwTesteeKind") {
                        myObject.dwTesteeKind = parseInt(value);
                        vtd.data("dwtesteekind", value);
                    }
                    else if (type == "dwAcademicSubject") {
                        myObject.dwAcademicSubject = parseInt(value);
                        vtd.attr("data-dwacademicsubject", value);
                    }
                    else if (type == "dwTesteeNum") {
                        myObject.dwTesteeNum = parseInt(value);
                    }
                    else if (type == "dwLabID") {
                        myObject.dwLabID = parseInt(value);
                        vtd.data("dwLabID", value);
                    }
                    szValue = $.toJSON(myObject);
                    if ($("#changeInfo").val() == "") {
                        $("#changeInfo").val(szValue);
                    } else {
                        $("#changeInfo").val($("#changeInfo").val() + "," + szValue);
                    }
                    vtd.empty();
                    if (this.tagName.toUpperCase() == "SELECT") {
                        //debugger;
                        vtd.html($(this).find("option:selected").text());
                    }
                    else {
                        vtd.html(value);
                    }

                });
            });
            $("input[name='szLab'],input[name='szRoom'],input[name='szDevKind']").click(function () {
                TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
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

