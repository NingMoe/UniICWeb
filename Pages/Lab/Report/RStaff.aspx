<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RStaff.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">专任实验室人员表(查看原始数据)</h2>        
        <div class="toolbar">
            <div class="tb_info">
               <div class="UniTab" id="tabl">
                 <%if(!bLeader) {%>
                    <a href="RStaff.aspx" id="RStaff">查看原始数据</a>  
                    <a href="RStaff2.aspx" id="RStaff2">修改保存数据</a>  
                       <%} %>         
                    <a href="RStaff3.aspx" id="RStaff3">发布数据</a>                
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

                $(".UniTab").UniTab();
                $("#subMit").button();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
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

