<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLABSUMMARYII2.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        
        <input type="hidden" id="changeInfo" name="changeInfo" />
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">高等学校实验室综合信息表二(修改保存数据)</h2>
        <div class="toolbar">
             <div class="tb_info">
                <div class="UniTab" id="tabl">
               <%if(!bLeader) {%>
                <a href="RLABSUMMARYII.aspx" id="RLABSUMMARYII">查看原始数据</a>  
                    <a href="RLABSUMMARYII2.aspx" id="RLABSUMMARYII2">修改保存数据</a>    
                     <%} %>       
                     <a href="RLABSUMMARYII23.aspx" id="RLABSUMMARYII23">发布数据</a> 
          
                </div>
               <div style="margin:10px">
                    <input type="submit" value="保存已修改的数据" id="btnSave" />
                    <input type="submit" value="发布修改好的数据" id="btnOp" style="margin-left:10px" />
                </div>
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="3">学校代码</th>
                        <th rowspan="3">单位名称</th>
                        <th rowspan="3">实验室个数</th>
                        <th rowspan="3">实验室面积</th>
                        <th colspan="4">仪器设备</th>                        
                    </tr>                    
                    <tr>
                        <th rowspan="2">台件</th>
                        <th rowspan="2">金额(万)</th>
                        <th colspan="2">其中贵重仪器设备台件</th>                      
                    </tr>                                   
                    <tr>
                        <th>台件</th>
                        <th>金额(万)</th>                   
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
                    myObject.dwDevNum = 0;
                    myObject.dwDevMoney = 0;
                    myObject.dwBigDevNum = 0;
                    myObject.dwBigMoney = 0;
                    myObject.dwBigDevNum = 0;
                  
                    var value = $(this).val();
                    var szValue = "";
                    debugger;
                    for (var p in myObject) {
                        if (p.toString() == type) {
                            myObject[p] = parseInt(value);
                        }
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

