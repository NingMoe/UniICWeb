<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" name="changeInfo" id="changeInfo" />
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">教学科研仪器设备表(查看原始数据)</h2>        
        <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">
                <%if(!bLeader) {%>
                    <a href="RDevList.aspx" id="RDevList">查看原始数据</a>  
                    <a href="RDevList2.aspx" id="RDevList2">修改并保存数据</a>                
                     <%} %> 
                      <a href="RDevList3.aspx" id="RDevList3">发布数据</a>                    
                </div>
                <div style="margin:10px;">
                    <input type="submit" value="数据清空重新录入" id="subMit" title="已经修改的数据将会清空，请慎重" />
                </div>
                <!--
                <div style="margin-left: 30px; margin-bottom: 20px">
                    <table style="width:100%">
                        <tr>
                            <th class="thHead">实验方向:</th>
                            <td colspan="3" class="context"> <%=m_szLab%></td>                                                 
                        </tr>
                        <tr>
                            <td class="thHead">实验室:</td>
                            <td colspan="3" class="context"> <%=m_szRoom%></td>   
                        </tr>
                        <tr>                            
                            <td class="thHead">仪器类型:</td>
                            <td colspan="3" class="context"> <%=m_szKind%>
                                <input type="submit" value="保存" />
                            </td>        
                        </tr>
                       
                        <tr>
                             <th class="thHead"><%=ConfigConst.GCDevName %>:</th>
                            <td  colspan="3" class="context">
                              <select id="dwDevID" name="dwDevID" style="width:120px">
                            <%=m_szDev%>
                        </select></td>                         
                        
                        </tr>
                   
                    </table>
                </div>
                -->
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>学校代码</th>
                        <th>仪器编号</th>
                        <th>分类号</th>
                        <th>仪器名称</th>
                        <th>型号</th>                      
                        <th>规格</th>                  
                        <th>仪器来源</th>
                        <th>国别码</th>
                        <th>单价</th>                                               
                        <th>购置日期</th>  
                        <th>现状码</th>  
                        <th>使用方向</th>  
                        <th>单位编号</th>  
                        <th>单位名称</th>  
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
            $('.tdSet').click(function () {
                var vtd = $(this);
                var devid = vtd.parents("tr").children().first().data("id");
                var YearTerm = vtd.parents("tr").children().first().data("yearterm");
               
                var labid = vtd.parents("tr").children().first().data("labid");
             
                var vInputList = $(":input", vtd);
                if (vInputList.length > 0) {
                    return;
                }
                var html = vtd.html();
                var input = $("<input type='text' style='width:70px' />");
                input.val(html);
                vtd.empty();
                vtd.append(input);
                input.focus();
                var type = vtd.data("type");
                $(":input", vtd).on("blur", function (event) {
                    var value = $(this).val();
                    var szValue = "";
                    if (type = "dwStatCode") {
                        szValue = "{\"dwYearTerm\":" + YearTerm + ",\"dwDevID\":" + devid + ",\"dwStatCode\":" + value + ",\"szDeptSN\":\"" + "" + "\",\"dwLabID\":" + labid + "},";
                    }
                    else if (type = "szDeptSN") {
                        szValue = "{\"dwYearTerm\":" + YearTerm + ",\"dwDevID\":" + devid + ",\"dwStatCode\":" + "0" + ",\"szDeptSN\":\"" + value + "\",\"dwLabID\":" + labid + "},";
                    }
                    $("#changeInfo").val($("#changeInfo").val() + szValue);
                    vtd.empty();
                    vtd.html(value);
                });

            });
            $("input[name='szLab'],input[name='szRoom'],input[name='szDevKind']").click(function () {
                TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
             });
           
            $("#btnOK").button();           
        </script>
        <style>         
           
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

