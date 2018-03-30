<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabInfo.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">实验室基本情况表(查看原始数据)</h2>
        <div class="toolbar">
           <div class="tb_info">
               <div class="UniTab" id="tabl">
                     <%if(!bLeader) {%>
                <a href="RlabInfo.aspx" id="RlabInfo">查看原始数据</a>  
                    <a href="RlabInfo2.aspx" id="RlabInfo2">修改保存数据</a>       
                     <%} %> 
                      <a href="RlabInfo3.aspx" id="RDevList3">发布数据</a>            
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

