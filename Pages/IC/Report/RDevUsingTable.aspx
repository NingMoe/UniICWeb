<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevUsingTable.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">使用率统计图</h2>
        <div class="toolbar">
            <div class="tb_info">
                <div style="margin-left: 30px; margin-bottom: 20px">
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
                            <td class="thHead">结束日期:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                           
                        </tr>
                        <%} %>
                       <tr>
                             <th class="thHead">子系统:</th>
                          <td  class="tdHead" colspan="3">
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
                             
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
     <div id="szResv" class="LineStat" data-color="1" style="min-width:0px" data-unit="次数" data-name="使用次数">
                   <h1><span></span><strong>使用设备</strong><strong>所需设备</strong></h1>
         <%=szResvRate %>
                </div>
        <script type="text/javascript">
            $(function () {
                $('#btnOK').button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
           });
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

