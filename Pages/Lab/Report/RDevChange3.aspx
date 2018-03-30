<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevChange3.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">教学科研仪器设备增减变动情况表(查看原始数据)</h2>        
        <div class="toolbar">
               
             
            <div class="tb_info">

                 <div class="UniTab" id="tabl">
                       <%if(!bLeader) {%>

                <a href="RDevChange.aspx" id="RDevChange">查看原始数据</a>  
                    <a href="RDevChange2.aspx" id="RDevChange2">修改并保存数据</a>  

                      <%} %>               
                     <a href="RDevChange3.aspx" id="RDevList3">发布数据</a> 
   
                </div>
             
            </div>

        </div>
        <div class="content">
           <table class="ListTbl">
                <thead>
                    <tr>
                        <th rowspan="3">学校代码</th>
                        <th colspan="4">上学年末实有数</th>
                        <th colspan="2">本学年增加数</th>
                        <th colspan="2">本学年减少数</th>
                        <th colspan="4">本学年末实有数</th>                       
                    </tr>
                     <tr>
                        <th rowspan="2">台件</th>
                        <th rowspan="2">金额(元)</th>
                        <th colspan="2">其中10万元(含)以上</th>
                      
                        <th rowspan="2">台件</th>
                        <th rowspan="2">金额(元)</th>                          
                        <th rowspan="2">台件</th>
                        <th rowspan="2">金额(元)</th>                         
                        <th rowspan="2">台件</th>
                        <th rowspan="2">金额(元)</th>   
                        <th colspan="2">其中10万元(含)以上</th>                 
                    </tr>
                     <tr>
                         <th>台件</th>
                        <th>金额(元)</th> 
                           <th>台件</th>
                        <th>金额(元)</th>                                       
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

