<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PersonUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold">个人使用率排行榜</h2>        
        <input type="hidden" value="none" name="type" id="type" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                <div style="margin-left: 100px; margin-bottom: 30px">
                    <table style="width: 650px">
                        <tr>
                            <th class="thHead">开始日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <td class="thHead">结束日期:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                             <td class="tdHead">
                                <input type="submit" id="btnOK" value="查询" />
                                 <asp:button runat="server" text="导出" id="btnExport" />
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
                        <th>使用次数</th>
                        <th>使用总时间</th>
                      
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
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
            });
            $("#<%=btnExport.ClientID%>").click(function () {
                $("#type").val("export");
                var dwStartDate= $("#<%=dwStartDate.ClientID%>").val();
                var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                $.lhdialog({
                    title: '导出',
                    width: '300px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:DLG/PersonUsingStatExport.aspx?dwStartDate=' + dwStartDate + '&dwEndDate=' + dwEndDate
                });
            });
           
            $("#btnOK").button();
            $("#<%=btnExport.ClientID%>").button();        
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
