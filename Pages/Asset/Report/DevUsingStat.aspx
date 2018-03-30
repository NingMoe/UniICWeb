<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold"><%=ConfigConst.GCDevName %>使用率统计</h2>
        <input type="hidden" value="2" name="dwPurpose" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info">
                <div>
                    <table style="width: 650px">
                        <tr>
                            <th class="thHead">开始日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <td class="thHead">结束日期:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                            <td class="tdHead">
                                <input type="submit" id="btnOK" value="查询" /></td>
                        </tr>

                    </table>
                </div>
            </div>

        </div>
        <div style="margin-top: 10px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th><%=ConfigConst.GCDevName%>名称</th>    
                        <th><%=ConfigConst.GCClassName%></th>       
                        <th>管理员</th>               
                        <th>所属<%=ConfigConst.GCLabName %></th>
                        <th>型号/规格</th>
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
        $("#btnOK").button();

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
