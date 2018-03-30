<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Bill.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>费用状况</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
        <div class="toolbar">
            <div class="tb_btn">
            </div>
        </div>
        <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
      
                <table style="margin: 15px; width: 99%">      
                 <!-- <tr>
                    
                    <td  style="width:20%; text-align:center">预约状态</td>
                    <td style="width:75%;text-align:left">
                        <LABEL><INPUT class="enum" value="512" type="radio" name="dwCheckStat" > 调整生效后结束时间</LABEL>
                        <LABEL><INPUT class="enum" value="524288" type="radio" name="dwCheckStat" > 待结算</LABEL>    
                        <LABEL><INPUT class="enum" value="4194304" type="radio" name="dwCheckStat" > 打印校内转账凭证</LABEL>                      
                    </td>
                   
                    </tr>
                 -->
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
                        <td colspan="3">
                            <input type="text" name="dwPID" id="dwPID" /></td>

                    </tr>
                <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="查询" style="height: 25px" /></th>
                    </tr>
            </table>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>学工号</th>
                        <th>姓名</th>
                        <th>仪器</th>
                        <th>使用时间</th>
                        <th>使用时长</th>
                        <th>费率</th>
                        <th>收费金额</th>
                      
                        <th>账单时间</th>
                        <th>状态</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                $("#btnDevKind").click(function () {
                    $.lhdialog({
                        title: '新建',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevKind.aspx?op=new'
                    });
                });
                $(".InfoDevKindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '介绍',
                        width: '760px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwDevKind + "&type=DevKindInfo"
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevKind.aspx?op=set&id=' + dwDevKind
                    });
                });
                $(".DelbtnDevKind").click(function () {
                    var devKindID = $(this).parents("tr").children().first().children().first().val();
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + devKindID);
                    }, '提示', 1, function () { });
                });
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
