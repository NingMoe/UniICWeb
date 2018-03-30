<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; margin-bottom: 20px; font-weight: bold"><%=ConfigConst.GCRoomName %>使用率排行榜</h2>
        <input type="hidden" value="11" name="dwPurpose" />
        <div class="toolbar" style="background: #e5f1f4">
            <div class="tb_info"  style="width: 99%">
                <div style="margin-bottom: 20px;width: 99%">
                    <table style="width: 99%">
                        <tr>
                            <th class="thHead">开始日期:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <td class="thHead">结束日期:</td>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                            </tr>
                        <tr>
                            <td class="thHead">实验室:</td>
                            <td style="text-align:left" colspan="3">
                                <select id="dwLabID" name="dwLabID" >
                                    <%=szLab %>
                                </select>
                            </td>
                            </tr>
                        <tr>
                             <td style="text-align:center" colspan="4">
                                <input type="submit" id="btnOK" value="查询" />
                                 <!--    <input type="button" id="btnExport" value="导出" />-->
                             </td>
                        </tr>
                    
                    </table>
                </div>
            </div>

        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th><%=ConfigConst.GCLabName %>编号</th>
                         <th><%=ConfigConst.GCLabName %>名称</th>
                        <th>房间编号</th>
                        <th>房间名称</th>
                         <th>使用人数</th>
                        <th><%=ConfigConst.GCDevName %>数</th>
                      <th>总学时数</th>
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
                 $(".ListTbl").UniTable();
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });              
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
                        content: 'url:Dlg/LabUsingStatExport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate
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
