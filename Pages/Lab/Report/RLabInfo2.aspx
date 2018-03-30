<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabInfo2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
        <input type="hidden" name="opSub" id="opSub" value="0" />

        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">实验室基本情况表(修改保存数据)</h2>
        <div class="toolbar">
           <div class="tb_info">
               <div class="UniTab" id="tabl">
               <%if(!bLeader) {%>
                <a href="RlabInfo.aspx" id="RlabInfo">查看原始数据</a>  
                    <a href="RlabInfo2.aspx" id="RlabInfo2">修改保存数据</a>       
                     <%} %> 
                      <a href="RlabInfo3.aspx" id="RDevList3">发布数据</a>                
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
                        <th rowspan="3">学校代码</th>
                        <th rowspan="3">实验室编号</th>
                        <th rowspan="3">实验室名称</th>
                        <th rowspan="3">实验室类别</th>
                        <th rowspan="3">建立年份</th>
                        <th rowspan="3">房屋使用面积</th>
                        <th rowspan="3">所属学科</th>
                        <th colspan="3">教师获奖与成果</th>
                        <th rowspan="3">学生获奖情况</th>
                        <th colspan="5">论文和教材情况</th>
                        <th colspan="5">科研及社会服务情况</th>
                        <th colspan="3">毕业设计和论文人数</th>
                        <th colspan="6">开放实验</th>
                        <th rowspan="3">兼任人员数</th>
                        <th colspan="2">实验教学运行经费</th>
                    </tr>
                    <tr>
                        <th rowspan="2">国家级</th>
                        <th rowspan="2">省部级</th>
                        <th rowspan="2">发明专利</th>
                        <th colspan="2">三大检索收录</th>
                        <th colspan="2">核心刊物</th>
                        <th rowspan="2">实验教材</th>
                        <th colspan="2">科研项目数</th>
                        <th rowspan="2">社会服务项目数</th>
                        <th colspan="2">教研项目数</th>
                        <th rowspan="2">专科生人数</th>
                        <th rowspan="2">本科生人数</th>
                        <th rowspan="2">研究生人数</th>
                        <th colspan="2">实验个数</th>
                        <th colspan="2">实验人数</th>
                        <th colspan="2">实验人时数</th>
                        <th rowspan="2">小计</th>
                        <th rowspan="2">其中教学实验年材料消耗费</th>
                    </tr>
                    <tr>
                        <th>教学</th>
                        <th>科研</th>
                        <th>教学</th>
                        <th>科研</th>
                        <th>省部级以上</th>
                        <th>其他</th>
                        <th>省部级以上</th>
                        <th>其他</th>
                        <th>校内</th>
                        <th>校外</th>
                        <th>校内</th>
                        <th>校外</th>
                        <th>校内</th>
                        <th>校外</th>
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
                    myObject.dwLabID = devid;
                    myObject.dwTNReward = 0;
                    myObject.dwTPReward = 0;
                    myObject.dwTPatent = 0;
                    myObject.dwTThreeIndex = 0;
                    myObject.dwRThreeIndex = 0;
                    myObject.dwTKernelJournal = 0;
                    myObject.dwRKernelJournal = 0;
                    myObject.dwTestBookNum = 0;
                    myObject.dwPRItemNum = 0;
                    myObject.dwRItemNum = 0;
                    myObject.dwSItemNum = 0;
                    myObject.dwPRItemNum = 0;
                    myObject.dwPTItemNum = 0;
                    myObject.dwBKThesisUsers = 0;
                    myObject.dwZKThesisUsers = 0;
                    myObject.dwSSThesisUsers = 0;
                    myObject.dwItemNum = 0;
                    myObject.dwOtherItemNum = 0;
                    myObject.dwUseUsers = 0;
                    myObject.dwOtherUsers = 0;
                    myObject.dwUseTime = 0;
                    myObject.dwOtherTime = 0;
                    myObject.dwPartTimeUsers = 0;
                    myObject.dwTotalCost = 0;
                    myObject.dwConsumeCost = 0;

                    var value = $(this).val();
                    var szValue = "";
                    if (type == "dwTNReward") {
                        myObject.dwTNReward = parseInt(value);
                    }
                    else if (type == "dwTPReward") {
                        myObject.dwTPReward = parseInt(value);
                    }
                    else if (type == "dwTPatent") {
                        myObject.dwTPatent = parseInt(value);
                    }
                    else if (type == "dwTThreeIndex") {
                        myObject.dwTThreeIndex = parseInt(value);
                    }
                    else if (type == "dwRThreeIndex") {
                        myObject.dwRThreeIndex = parseInt(value);
                    }
                    else if (type == "dwTThreeIndex") {
                        myObject.dwTThreeIndex = parseInt(value);
                    }
                    else if (type == "dwRKernelJournal") {
                        myObject.dwRKernelJournal = parseInt(value);
                    }
                    else if (type == "dwTestBookNum") {
                        myObject.dwTestBookNum = parseInt(value);
                    }
                    else if (type == "dwPRItemNum") {
                        myObject.dwPRItemNum = parseInt(value);
                    }
                    else if (type == "dwRItemNum") {
                        myObject.dwRItemNum = parseInt(value);
                    }
                    else if (type == "dwSItemNum") {
                        myObject.dwSItemNum = parseInt(value);
                    }
                    else if (type == "dwPRItemNum") {
                        myObject.dwPRItemNum = parseInt(value);
                    }
                    else if (type == "dwPTItemNum") {
                        myObject.dwPTItemNum = parseInt(value);
                    }
                    else if (type == "dwBKThesisUsers") {
                        myObject.dwBKThesisUsers = parseInt(value);
                    }
                    else if (type == "dwZKThesisUsers") {
                        myObject.dwZKThesisUsers = parseInt(value);
                    }
                    else if (type == "dwSSThesisUsers") {
                        myObject.dwSSThesisUsers = parseInt(value);
                    }
                    else if (type == "dwItemNum") {
                        myObject.dwItemNum = parseInt(value);
                    }
                    else if (type == "dwOtherItemNum") {
                        myObject.dwOtherItemNum = parseInt(value);
                    }
                    else if (type == "dwUseUsers") {
                        myObject.dwUseUsers = parseInt(value);
                    }
                    else if (type == "dwOtherUsers") {
                        myObject.dwOtherUsers = parseInt(value);
                    }
                    else if (type == "dwUseTime") {
                        myObject.dwUseTime = parseInt(value);
                    }
                    else if (type == "dwOtherTime") {
                        myObject.dwOtherTime = parseInt(value);
                    }
                    else if (type == "dwPartTimeUsers") {
                        myObject.dwPartTimeUsers = parseInt(value);
                    }
                    else if (type == "dwTotalCost") {
                        myObject.dwTotalCost = parseInt(value);
                    }
                    else if (type == "dwConsumeCost") {
                        myObject.dwConsumeCost = parseInt(value);
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
            .thHead
            {
                width: 80px;
                text-align: center;
            }

            .ListTbl th
            {
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

