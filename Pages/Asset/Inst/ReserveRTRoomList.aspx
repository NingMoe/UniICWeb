<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveRTRoomList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>实验室预约状况</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                     <a href="ReserveRoomList.aspx">会议室预约状况</a>
                    <a href="ReserveRTRoomList.aspx">科研预约状况</a>
                </div>

            </div>

        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                    <tr>
                        <th>开始日期:</th>
                        <td>
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                        <th>结束日期:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th><%=ConfigConst.GCSysKindRoom %>名称:</th>
                        <td>
                            <input type="text" name="devName" id="devName" /></td>

                    </tr>
                    <tr>
                        <th>状态</th>
                        <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">全部</label>
                            <label><input class="enum" value="512" type="radio" name="dwCheckStat">生效中</label>
                            <label><input class="enum" value="262144" type="radio" name="dwCheckStat">违约</label>
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">已结束</label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="查询" style="height: 25px" /></th>
                    </tr>
                </table>

            </div>
        </div>
        <div class="content" style="margin-top: 10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>预约号</th>
                        <th>申请人姓名</th>
                        <th><%=ConfigConst.GCSysKindRoom %>名称</th>
                        <th>项目名称</th>
                        <th>导师</th>
                        <th>状态</th>
                        <th name="dwOccurTime">提交时间</th>
                        <th name="dwBeginTime">申请时间</th>
                        <th width="25px">操作</th>
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
                $("#btnOK").button();
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $(".OPTD1").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="审核"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTD1").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '审核',
                        width: '700px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/CheckResv.aspx?op=set&id=' + dwResvID
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
        <style>
            #tbSearch {
                border-width: 1px;
                border-color: #d1c1c1;
                cursor: hand;
            }

                #tbSearch td {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;
                }

            td input {
                margin-left: 20px;
            }
        </style>
    </form>
</asp:Content>
