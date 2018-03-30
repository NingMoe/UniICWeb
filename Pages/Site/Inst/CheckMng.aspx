<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="CheckMng.aspx.cs" Inherits="Sub_CheckMng"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>审核管理</h2>
    <div class="toolbar">
    <div class="tb_info">总数：5条，已审核：0条，未审核：2条</div>
    <div class="tb_btn">
        <div class="AdvOpts"><div class="AdvLab">高级选项</div>
            <fieldset><legend>类别</legend>
                <label><input name="room" value="1" type="checkbox" />教学预约</label>  <label><input name="room" value="2" type="checkbox" />个人预约</label>
            </fieldset>
            <fieldset><legend>状态</legend>
                <label><input name="stat" value="1" type="checkbox" />未审核</label>  <label><input name="stat" value="2" type="checkbox" />已审核</label>
            </fieldset>
        </div>
    </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>编号</th><th>类别</th><th>名称</th><th>状态</th><th width="80px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <tr><td>1</td><td>教学预约</td><td>张三预约机房1</td><td>未审核</td><td><div class="OPTD"></div></td></tr>
                <tr><td>2</td><td>个人预约</td><td>张三预约设备1</td><td>未审核</td><td><div class="OPTD"></div></td></tr>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
    $(function () {
        $(".OPTD").html('<div class="OPTDBtn">\
        <a href="#" title="审核"><img src="../../../themes/icon_s/20.png"/></a></div>');
        $(".OPTDBtn").UIAPanel({
            theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
        });
    });
    </script>
</form>
</asp:Content>

