<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceListRunState.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=Title %></h2>
        <div class="toolbar">
        <button type="button" id="Back">返回</button>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>编号</th>
                        <th>机器名</th>
                        <th>IP地址</th>
                        <th>所属设备类型</th>
                        <th>型号</th>
                        <th>规格</th>
                        <th>所属房间</th>
                        <th>所属实验室</th>
                        <th>使用者</th>
                         <th>状态</th>
                        <th width="25px">操作</th>
                    </tr>
                  
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
            <div class="BarStat tblBottomStat" data-color="2">
                <h1><span>--------</span><strong>设备数</strong></h1>
                <p><span>Dell电脑</span><strong>12</strong></p>
                <p><span>Apple电脑</span><strong>21</strong></p>
                <p><span>Lenovo电脑</span><strong>17</strong></p>
            </div>
            <div class="BarStat tblBottomStat" data-color="2">
                <h1><span>--------</span><strong>设备数</strong></h1>
                <p><span>房间1</span><strong>12</strong><strong>17</strong></p>
                <p><span>房间2</span><strong>17</strong><strong>5</strong></p>
                <p><span>房间3</span><strong>17</strong><strong>25</strong></p>
            </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTD1").html('<div class="OPTDBtn">\
                    <a href="#" title="远程开机"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTD2").html('<div class="OPTDBtn">\
                    <a href="#" title="远程关机"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTD3").html('<div class="OPTDBtn">\
                    <a href="#" title="需要登录"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTD4").html('<div class="OPTDBtn">\
                    <a href="#" title="恢复正常"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
        </script>
    </form>
</asp:Content>
