<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RLabCostInfo.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">实验室经费情况表(查看原始数据)</h2>
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
                   <input type="submit" value="数据清空重新录入" id="subMit" title="已经修改的数据将会清空，请慎重" />
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
                $("#btnOp,#btnSave,#subMit").button();
                $(".UniTab").UniTab();
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

