<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="stockakingDetail.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>盘点明细</h2>
         <div class="tb_info" style="margin-bottom:15px">
            <div class="UniTab" id="tabl">
                <a href="stockaking.aspx" id="stockaking">盘点计划</a>
                <a href="stockakingDetail.aspx" id="stockakingDetail">盘点明细</a>
            </div>
              </div>
       
         <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <th>盘点计划:</th>
                   <td><select name="dwSTID" id="dwSTID"><%=sz_Stocking %></select></td>
                   
                    <td><input type="submit" id="btn" value="查询" /></td>
                </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>资产编号</th>
                        <th>资产名称</th>
                        <th>单价(元)</th>
                        <th>所属类型</th>
                         <th>所在实验室</th>
                        <th>所属部门</th>
                        <th>盘点员</th>
                        <th>盘点状态</th>
                        <th>盘点描述</th>
                        <th>操作</th>
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
                var tabl = $(".UniTab").UniTab();
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="stnormal" href="#" title="盘点正常"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="stfail" title="盘点异常"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btn").button();
         
            $(".setLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定盘点已经结束?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=over&delID=" + dwID);
                }, '提示', 1, function () { });
            });
            
            $(".stnormal").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    var dwDevID = $(this).parents("tr").children().first().attr("data-devid");
                   
                ConfirmBox("确定资产正常?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=setNormail&delID=" + dwID + '&devid=' + dwDevID);
                }, '提示', 1, function () { });
            });
                $(".stfail").click(function () {
                    var dwID = $(this).parents("tr").children().first().attr("data-id");
                    var dwDevID = $(this).parents("tr").children().first().attr("data-devid");
                    $.lhdialog({
                        title: '设置盘点异常',
                        width: '600px',
                        height: '350px',
                        lock: true,
                        content: 'url:Dlg/SetSTDetailStaus.aspx?op=set&delID=' + dwID + '&devid=' + dwDevID
                    });
                });

            $(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
