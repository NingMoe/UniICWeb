<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RStaff2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
        
         <input type="hidden" name="opSub" id="opSub" value="0" />
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">专任实验室人员表(修改保存数据)</h2>        
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                      <%if(!bLeader) {%>
                    <a href="RStaff.aspx" id="RStaff">查看原始数据</a>  
                    <a href="RStaff2.aspx" id="RStaff2">修改保存数据</a>  
                       <%} %>         
                    <a href="RStaff3.aspx" id="RStaff3">发布数据</a>                
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
                        <th rowspan="2">学校代码</th>
                        <th rowspan="2">人员编码</th>
                        <th rowspan="2">实验室编号</th>
                        <th rowspan="2">实验室名称</th>
                        <th rowspan="2">姓名</th>                      
                        <th rowspan="2">性别</th>                  
                        <th rowspan="2">出生年月</th>
                        <th rowspan="2">所属学科</th>
                        <th rowspan="2">专业技能职务</th>                                               
                        <th rowspan="2">文化程度</th>  
                        <th rowspan="2">专家类别</th>  
                        <th colspan="2">国内培训</th>  
                        <th colspan="2">国外培训</th>                         
                    </tr>    
                    <tr>                  
                        <th>学历教育时间</th>  
                        <th>非学历教育实践</th>                         
                    
                        <th>学历教育时间</th>  
                        <th>非学历教育实践</th>                         
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
                $("#btnOp,#btnSave").button();
                $("#btnOp").click(function () {

                    $("#opSub").val("1");
                    TabReload($(this).parents("form").serialize());
                });
                $(".UniTab").UniTab();
                $("#subMit").button();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $('.tdSet').click(function () {
                    var vtd = $(this);
                    var vdwProfessionalTitle = '<%=dwProfessionalTitle%>';
                    var vdwAcademicSubject = '<%=dwAcademicSubject%>';
                    var vdwEducation = '<%=dwEducation%>';
                    var vdwExpertType = '<%=dwExpertType%>';
                     var vszLab = '<%=szLab%>';
                     //debugger;

                     var devid = vtd.parents("tr").children().first().data("id");
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
                     debugger;
                    if (kind == "select") {
                         if (type == "dwProfessionalTitle") {
                             input = $("<select style='width:70px' >" + vdwProfessionalTitle + "</select>");
                         }
                         else if (type == "dwAcademicSubject") {
                             input = $("<select style='width:90px' >" + vdwAcademicSubject + "</select>");
                         }
                         else if (type == "dwEducation") {
                             input = $("<select style='width:70px' >" + vdwEducation + "</select>");
                         }
                         else if (type == "dwExpertType") {
                             input = $("<select style='width:70px' >" + vdwExpertType + "</select>");
                         }
                         else if (type == "dwlabid") {
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
                         myObject.dwAccNo = devid;
                         myObject.dwProfessionalTitle = 0;
                         myObject.dwAcademicSubject = 0;
                         myObject.dwEducation = 0;
                         myObject.dwExpertType = 0;
                         myObject.dwLabID = 0;
                         myObject.dwInlandUduTime = 0;
                         myObject.dwInlandOtherTime = 0;
                         myObject.dwAbroadUduTime = 0;
                         myObject.dwAbroadOtherTime = 0;
                         var value = $(this).val();
                         var szValue = "";
                        
                         if (type == "dwProfessionalTitle") {
                             myObject.dwProfessionalTitle = parseInt(value);
                         }
                         else if (type == "dwAcademicSubject") {
                             myObject.dwAcademicSubject = parseInt(value);
                         }
                         else if (type == "dwEducation") {
                             myObject.dwEducation = parseInt(value);
                         }
                         else if (type == "dwExpertType") {
                             myObject.dwExpertType = parseInt(value);
                         }
                         else if (type == "dwlabid") {
                             myObject.dwLabID = parseInt(value);
                         }
                         else if (type == "dwInlandUduTime") {
                             myObject.dwInlandUduTime = parseInt(value);
                         }
                         else if (type == "dwInlandOtherTime") {
                             myObject.dwInlandOtherTime = parseInt(value);
                         }
                         else if (type == "dwAbroadUduTime") {
                             myObject.dwAbroadUduTime = parseInt(value);
                         }
                         else if (type == "dwAbroadOtherTime") {
                             myObject.dwAbroadOtherTime = parseInt(value);
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
            });                     
          
            $("#btnOK").button();           
        </script>
        <style>
            .ListTbl th
            {
                text-align:center;
            }
             .ListTbl td
            {
                text-align:center;
            }
            .tb_info table{border-bottom:1px solid #000}
            .tb_info table td{text-align:center; border-left:1px solid #000;border-right:1px solid #000;height:15px; border-top:1px solid #000}       
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

