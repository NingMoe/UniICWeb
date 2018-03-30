<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabCostInfo2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="changeInfo" name="changeInfo" />
          <input type="hidden" name="opSub" id="opSub" value="0" />
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">实验室经费情况表(修改保存数据)</h2>
        <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">
                    <%if(!bLeader) {%>
                    <a href="RLabCostInfo.aspx" id="RLabConstInfo">查看原始数据</a>  
                    <a href="RLabCostInfo2.aspx" id="RLabConstInfo2">修改保存数据</a>  
                       <%} %>                
                       <a href="RLabCostInfo3.aspx" id="RDevList3">发布数据</a>    
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
                        <th rowspan="3">实验室个数</th>
                        <th rowspan="3">实验室房屋面积</th>
                        <th colspan="10">经费投入（万元）</th>                       
                    </tr>
                    <tr>
                        <th rowspan="2">总计</th>
                        <th colspan="2">仪器设备购置经费</th>
                        <th colspan="2">仪器设备维护经费</th>
                        <th colspan="2">实验教学运行经费</th>
                        <th rowspan="2">实验室建设经费</th>
                        <th rowspan="2">实验教学研究与改革经费</th>
                        <th rowspan="2">其他</th>                         
                    </tr>
                    <tr>
                        <th>小计</th>
                        <th>其中教学仪器购置经费</th>
                        <th>小计</th>
                        <th>其中教学仪器维护经费</th>
                        <th>小计</th>
                        <th>其中年材料消耗经费</th>                       
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>         
        </div>
        <script type="text/javascript">
            $(function () {
                $("#btnOp,#btnSave").button();
                $("#btnOp").click(function () {

                    $("#opSub").val("1");
                    TabReload($(this).parents("form").serialize());
                });
                $(".UniTab").UniTab();
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
                    myObject.dwLabNum = 0;
                    myObject.dwLabArea = 0;
                    myObject.dwTotalCost = 0;
                    myObject.dwBuyCost = 0;
                    myObject.dwTBuyCost = 0;
                    myObject.dwKeepCost = 0;
                    myObject.dwTKeepCost = 0;
                    myObject.dwRunCost = 0;
                    myObject.dwCRunCost = 0;
                    myObject.dwBuildCost = 0;
                    myObject.dwRAndRCost = 0;
                    myObject.dwOtherCost = 0;

                    var value = $(this).val();
                    var szValue = "";
                    if (type == "dwLabNum") {
                        myObject.dwLabNum = parseInt(value);
                    }
                    else if (type == "dwLabArea") {
                        myObject.dwLabArea = parseInt(value);
                    }
                    else if (type == "dwTotalCost") {
                        myObject.dwTotalCost = parseInt(value);
                    }
                    else if (type == "dwBuyCost") {
                        myObject.dwBuyCost = parseInt(value);
                    }
                    else if (type == "dwTBuyCost") {
                        myObject.dwTBuyCost = parseInt(value);
                    }
                    else if (type == "dwKeepCost") {
                        myObject.dwKeepCost = parseInt(value);
                    }
                    else if (type == "dwTKeepCost") {
                        myObject.dwTKeepCost = parseInt(value);
                    }
                    else if (type == "dwRunCost") {
                        myObject.dwRunCost = parseInt(value);
                    }
                    else if (type == "dwCRunCost") {
                        myObject.dwCRunCost = parseInt(value);
                    }
                    else if (type == "dwBuildCost") {
                        myObject.dwBuildCost = parseInt(value);
                    }
                    else if (type == "dwRAndRCost") {
                        myObject.dwRAndRCost = parseInt(value);
                    }
                    else if (type == "dwOtherCost") {
                        myObject.dwOtherCost = parseInt(value);
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
            $("input[name='szLab'],input[name='szRoom']").click(function () {
                TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
             });
            $("#btnOK").button();
          
        </script>
        <style>          
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

