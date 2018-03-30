<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PersonUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">个人使用率排行榜</h2>        
        <input type="hidden" value="none" name="type" id="type" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
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
                            <th class="thHead">结束日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                            
                        </tr>
                        <%} %>
                       <tr>
                        
                       </tr>
                        <tr>
                             <td class="tdHead" colspan="6" style="text-align:center">
                                <input type="submit" id="btnOK" value="查询" />
                          <!--      <input type="button" id="btnExport" value="导出" />-->
                                
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
                        <th width="100px">学工号</th>
                        <th>姓名</th>
                        <th>班级</th>
                         <th>学院</th>
                        <th name="dwUseTimes">使用次数</th>
                        <th name="dwUseTime">使用总时间</th>
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
                $("#btnOK,#btnExport").button();
                $("#btnExport").click(function () {
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var orderkey = $("#orderkey").val();
                    $.lhdialog({
                        title: '导出',
                        width: '200px',
                        height: '50px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/personusingstatexport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate + '&orderkey=' + orderkey
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
