<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDiscore.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">使用率统计</h2>
        <input type="hidden" value="2" name="dwPurpose" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                <div>
                   <table style="width: 750px">
                         <%if(ConfigConst.GCICTypeMode==1) { %>
                         <tr>
                            <th class="thHead"></th>
                           <td class="tdHead" colspan="3" style="text-align:center;height:35px">
                            <select id="dwYearTerm" name="dwYearTerm">
                                <%=m_TermList %>
                            </select>    
                            </td>
                            
                           
                        </tr>

                     
                        <%} else {%>
                           <tr>
                            <th class="thHead">开始日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th class="thHead">结束日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                          
                        </tr>
                        <%} %>
                        <tr>
                             <td class="tdHead" colspan="4" style="text-align:center">
                                <input type="submit" id="btnOK" value="查询" />
                              <!--  <input type="button" id="btnExport" value="导出" />-->
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div style="margin-top: 10px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                         <th>违约类型</th>    
                        <th>预约条数</th>    
                        <th>违约条数</th>    
                        <th title="违约条数除以预约条数">违约率</th>    
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>

        </div>
        <script type="text/javascript">
            $(function () {
                //$(".ListTbl").UniTable();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
            });
        });
            $("#btnOK,#btnExport").button();
            $("#btnExport").click(function () {
                var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                $.lhdialog({
                    title: '导出',
                    width: '200px',
                    height: '50px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/DevUsingStatExport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate
                });
            });
        </script>
        <style>
            .thHead
            {
                background: #e5f1f4;
                text-align: right;
            }

            .tdHead
            {
                text-align: left;
            }

            td input
            {
                margin-left: 10px;
            }
        </style>
    </form>
</asp:Content>
