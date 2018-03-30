<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLABSUMMARY.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">高等学校实验室综合信息表一(查看原始数据)</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                      <%if(!bLeader) {%>
            <a href="RLABSUMMARY.aspx" id="RLABSUMMARY">查看原始数据</a>  
                    <a href="RLABSUMMARY2.aspx" id="RLABSUMMARY2">修改保存数据</a>    
                    
 <%} %>                 
                     <a href="RLABSUMMARY3.aspx" id="RDevList3">发布数据</a> 

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
                        <th rowspan="4">学校代码</th>
                        <th rowspan="4">单位名称</th>
                        <th rowspan="4">实验室个数</th>
                        <th rowspan="4">实验室房屋面积</th>
                        <th colspan="4">仪器设备</th>
                        <th colspan="7">教学任务</th>
                        <th rowspan="4">科研任务承担<%=ConfigConst.GCReachTestName%>及服务项目数</th>
                        <th colspan="7">工作人员数</th>
                        <th colspan="3">成果</th>
                    </tr>                    
                    <tr>
                        <th rowspan="3">台件</th>
                        <th rowspan="3">金额(万)</th>
                        <th rowspan="2" colspan="2">其中贵重仪器设备台件</th>
                        <th rowspan="2" colspan="2">教学实验</th>
                        <th rowspan="2" colspan="5">人时数</th>
                        <th rowspan="3">合计</th>
                        <th colspan="5">专任</th>
                        <th rowspan="3">兼任人员数</th>               
                        <th rowspan="3">论文数</th>
                        <th rowspan="3">教师获奖与成果数</th>
                        <th rowspan="3">学生获奖数</th>

                    </tr>
                    <tr>
                   <th colspan="2">教师</th>
                        <th colspan="2">实验技术人员</th>
                        <th rowspan="2">其他人员</th>
                        </tr>
                    <tr>
                        <th>台件</th>
                        <th>金额(万)</th>
                        <th>项目数</th>
                        <th>时数</th>
                        <th>合计</th>
                        <th>博士研究生</th>                       
                        <th>硕士研究生</th>                       
                        <th>本科生</th>                       
                        <th>专科生</th>                       
                        <th>高级职称</th>    
                        <th>中级职称</th>                                             
                        <th>高级职称</th>    
                        <th>中级职称</th>    
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
          
        </div>
        <script type="text/javascript">
            $(function () {
                $(".UniTab").UniTab();
                $("#subMit").button();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
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

