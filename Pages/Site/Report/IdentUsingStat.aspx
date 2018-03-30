<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="IdentUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">身份统计</h2>        
        <input type="hidden" value="none" name="type" id="type" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                 <div class="tb_info">
              <div class="UniTab" id="tabl">
                   <a href="IdentUsingStat.aspx">身份统计</a>
              
                <a href="IdentUsingStatPie.aspx">身份统计饼图</a>
            </div>
        </div>
                <div style="margin-left: 100px; margin-bottom: 30px">
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
                       <!--
                       <tr>
                           <th class="thHead">
                           <%=ConfigConst.GCDeptName %>:
                               </th>
                           <td colspan="3">
                               <select id="dwDept" name="dwDept">
                                  <%=szDept %>
                                    </select>
                           </td>
                       </tr>
                       -->
                        <tr>
                            <th class="thHead">活动场景：</th>
                            <td colspan="3"><select name="dwActivitySN" id="dwActivitySN"><%=m_YardActivity %></select></td>
                        </tr>
                        <tr>
                             <td class="tdHead" colspan="4" style="text-align:center">
                                <input type="submit" id="btnOK" value="查询" />
                           <!--     <input type="button" id="btnExport" value="导出" />-->
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
        <div class="content">
            <div style="float:left">
                
                </div>
            <table class="ListTbl">
                <thead>
                    
                    <tr>
                        <th>名称</th>
                        <th>人数</th>
                         <th>使用人数</th>
                        <th>使用人次</th>
                        <th>使用总时间</th>
                       <th title="使用总时间除以人数">在馆平均时间</th>
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
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                $("#btnOK,#btnExport").button();
                $("#btnExport").click(function () {
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var vdept = $("#dwDept").val();
                    
                    $.lhdialog({
                        title: '导出',
                        width: '200px',
                        height: '50px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/IdentUsingStatExport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate + '&dwDept=' + vdept
                    });
                });
            });
        </script>
        <style>
            .thHead {
                background: #e5f1f4;
                text-align: right;
            }

            .tdHead {
                text-align: left;
            }

            td input {
                margin-left: 10px;
            }
        </style>
    </form>
</asp:Content>
