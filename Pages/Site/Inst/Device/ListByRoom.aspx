<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListByRoom.aspx.cs" Inherits="ListByKind"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>房间设备列表</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <button type="button" id="Back">返回</button>
        <div class="tb_btn">
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr>
                    <th width="60px">设备编号</th>
                    <th>名称</th>
                    <th>实验室</th>
                    <th>房间</th>
                    <th>型号</th>
                    <th>设备状态</th>
                    <th>使用状态</th>
                    <th>使用者</th>
                    <th width="25px">操作</th>
                </tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
        <uc1:PageCtrl runat="server" ID="PageCtrl" />
        <div class="BarStat tblBottomStat" data-color="2">
            <h1><span>--------</span><strong>台数</strong></h1>
            <p><span>Dell电脑</span><strong>30</strong></p>
            <p><span>apple电脑</span><strong>60</strong></p>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#Back").button().click(function(){
                TabJump("Device/Stat.aspx");
            });
            $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" title="开门"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="延长时间"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" title="查看视频"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="查看刷卡记录"><img src="../../../themes/icon_s/13.png"/></a>\
                    <a href="#" title="免登录"><img src="../../../themes/iconpage/del.png""/></a></div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
            });
        });
    </script>
</form>
</asp:Content>