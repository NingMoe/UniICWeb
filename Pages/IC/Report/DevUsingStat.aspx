<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">使用率统计</h2>
       <!-- <input type="hidden" value="2" name="dwPurpose" />-->
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                <div>
                    <table style="width: 750px">
                        <%if (ConfigConst.GCICTypeMode == 1) { %>
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
                            <td class="thHead">结束日期:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                               <!--
                           <th>
                               区域
                           </th>
                               <td>
                                   <select id="szRoom" name="szRoom">
                                       <%=sz_Room %>
                                   </select>
                               </td>
                               -->
                        </tr>
                        <%} %>
                     
                         <tr>
                            <th class="thHead">类型:</th>
                            <td class="tdHead" colspan="3">
                            <select id="dwClassKind" name="dwClassKind">
                                <option value="0">全部</option>
                                <%if ((ConfigConst.GCSysKind & 1) > 0)
                                    { %>
                                <option value="1">研修间</option>
                                <%} if((ConfigConst.GCSysKind & 8) > 0) {%>
                                <option value="8">座位</option>
                                 <%} if((ConfigConst.GCSysKind & 2) > 0) {%>
                                <option value="2">电子阅览室</option>
                                <%} %>
                                </select> 
                            </td>
                             </tr>
                      
                        <tr>
                             <td class="tdHead" colspan="4" style="text-align:center">
                                <input type="submit" id="btnOK" value="查询" />
                                <input type="button" id="btnExport" value="导出" />
                                
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
                        <th>名称</th>    
                        <th><%=ConfigConst.GCClassName%></th>       
                        <th>所属<%=ConfigConst.GCLabName %></th>
                         <th name="dwTotalUseTime">使用总时间</th>
                         <th name="dwPIDNum">使用人数</th>
                        <th name="dwUseTimes">使用人次数</th>
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
                //$(".ListTbl").UniTable();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
            });
            });
            $(".ListTbl").UniTable();
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
