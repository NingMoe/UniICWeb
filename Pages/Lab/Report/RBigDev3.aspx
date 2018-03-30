<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RBigDev3.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">贵重仪器设备表(查看原始数据)</h2>
        <div class="toolbar">
            <div class="tb_info">
                  
                <div class="UniTab" id="tabl">
                     <%if(!bLeader) {%>
                <a href="RBigDev.aspx" id="RBigDev">查看原始数据</a>  
                    <a href="RBigDev2.aspx" id="RBigDev2">修改保存数据</a> 
                     <%} %> 
                      <a href="RBigDev3.aspx" id="RDevList3">发布数据</a>                 
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

