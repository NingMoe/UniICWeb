<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RBigDev2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">贵重仪器设备表(修改保存数据)</h2>
        <input type="hidden" id="changeInfo" name="changeInfo" />
         <input type="hidden" name="opSub" id="opSub" value="0" />

        <div class="toolbar">
            <div class="tb_info">
                  
                <div class="UniTab" id="tabl">
                    <%if(!bLeader) {%>
                <a href="RBigDev.aspx" id="RBigDev">查看原始数据</a>  
                    <a href="RBigDev2.aspx" id="RBigDev2">修改保存数据</a> 
                     <%} %> 
                      <a href="RBigDev3.aspx" id="RDevList3">发布数据</a>              
                </div>
                  <div style="margin:10px;">
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
                        <th rowspan="2">仪器编号</th>
                        <th rowspan="2">分类号</th>
                        <th rowspan="2">仪器名称</th>
                        <th rowspan="2">单价</th>
                        <th rowspan="2">型号</th>
                        <th rowspan="2">规格</th>
                        <th colspan="4">使用机时</th>
                        <th rowspan="2">测样数</th>
                        <th colspan="3">培训人员数</th>
                        <th rowspan="2">教学实验项目数</th>
                        <th rowspan="2">科研项目数</th>
                        <th rowspan="2">社会服务项目数</th>
                        <th colspan="2">获奖情况</th>
                        <th colspan="2">发明专利</th>
                        <th colspan="2">论文情况</th>
                        <th rowspan="2">负责人姓名</th>
                    </tr>
                    <tr>
                        <th>教学</th>
                        <th>科研</th>
                        <th>社会服务</th>
                        <th>其中开放使用机时</th>
                        <th>学生</th>
                        <th>教师</th>
                        <th>其他</th>
                        <th>国家级</th>
                        <th>省部级</th>
                        <th>教师</th>
                        <th>学生</th>
                        <th>三大检索</th>
                        <th>核心期刊</th>
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
            });
          
           
            $('.tdSet').click(function () {
                var vtd = $(this);
                var devid = vtd.parents("tr").children().first().data("id");
                var labid = vtd.parents("tr").children().first().data("labid");
                var vInputList = $(":input", vtd);
                if (vInputList.length > 0) {
                    return;
                }
                var html = vtd.html();
                var input = $("<input type='text' style='width:40px' />");
                input.val(html);
                vtd.empty();
                vtd.append(input);
                input.focus();
                var type = vtd.data("type");

                $(":input", vtd).on("blur", function (event) {
                    var myObject = new Object();
                    myObject.dwDevID = devid;
                    myObject.dwTUseTime = 0;
                    myObject.dwRUseTime = 0;
                    myObject.dwSUseTime = 0;
                    myObject.dwOUseTime = 0;
                    myObject.dwSampleNum = 0;
                    myObject.dwUseStudents = 0;
                    myObject.dwUseTeachers = 0;
                    myObject.dwUseOthers = 0;
                    myObject.dwRItemNum = 0;
                    myObject.dwTItemNum = 0;
                    myObject.dwSItemNum = 0;
                    myObject.dwNReward = 0;
                    myObject.dwPReward = 0;
                    myObject.dwTPatent = 0;
                    myObject.dwSPatent = 0;
                    myObject.dwThreeIndex = 0;
                    myObject.dwKernelJournal = 0;
                    myObject.szAttendantName = "";
                    var value = $(this).val();
                    var szValue = "";
                    if (type == "dwTUseTime") {
                        myObject.dwTUseTime = parseInt(value);
                    }
                    else if (type == "dwRUseTime") {
                        myObject.dwRUseTime = parseInt(value);
                    }
                    else if (type == "dwSUseTime") {
                        myObject.dwSUseTime = parseInt(value);
                    }
                    else if (type == "dwOUseTime") {

                        myObject.dwOUseTime = parseInt(value);
                    }
                    else if (type == "dwSampleNum") {

                        myObject.dwSampleNum = parseInt(value);
                    }
                    else if (type == "dwUseStudents") {

                        myObject.dwUseStudents = parseInt(value);
                    }
                    else if (type == "dwUseTeachers") {

                        myObject.dwUseTeachers = parseInt(value);
                    }
                    else if (type == "dwUseOthers") {

                        myObject.dwUseOthers = parseInt(value);
                    }
                    else if (type == "dwRItemNum") {

                        myObject.dwRItemNum = parseInt(value);
                    }
                    else if (type == "dwTItemNum") {

                        myObject.dwTItemNum = parseInt(value);
                    }
                    else if (type == "dwSItemNum") {

                        myObject.dwSItemNum = parseInt(value);
                    }
                    else if (type == "dwNReward") {

                        myObject.dwNReward = parseInt(value);
                    }
                    else if (type == "dwPReward") {

                        myObject.dwPReward = parseInt(value);
                    }
                    else if (type == "dwTPatent") {

                        myObject.dwTPatent = parseInt(value);
                    }
                    else if (type == "dwSPatent") {

                        myObject.dwSPatent = parseInt(value);
                    }
                    else if (type == "dwThreeIndex") {

                        myObject.dwThreeIndex = parseInt(value);
                    }
                    else if (type == "dwKernelJournal") {

                        myObject.dwKernelJournal = parseInt(value);
                    }
                    else if (type == "szAttendantName") {
                        myObject.szAttendantName = (value);
                    }
                    szValue = $.toJSON(myObject);
                    if ($("#changeInfo").val() == "") {
                        $("#changeInfo").val(szValue);
                    } else {
                        $("#changeInfo").val($("#changeInfo").val() + "," + szValue);
                    }

                    vtd.empty();
                    vtd.html(value);
                });

            });
            $("#btnOK").button();
         
        </script>
        <style>
            .tb_info table           
            .thHead
            {
                width: 80px;
                text-align: center;
            }
            .context2 input
            {
                margin-right: 20px;
            }

            .context input
            {
                margin-left: 15px;
            }

            .context select
            {
                margin-left: 15px;
            }
        </style>
    </form>
</asp:Content>

