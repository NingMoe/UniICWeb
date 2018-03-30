<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Rule.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>规则管理</h2>
    <div class="toolbar">
        <div class="tb_info">
            <div id="tab1" class="UniTab">
                <a href="Rule.aspx">人员管理</a><a href="OpenRule.aspx">开放规则</a>
            </div></div>
        <div class="FixBtn"><a>新建设备</a></div>
        <div class="tb_btn">
            <div class="AdvOpts" width="350" height="250"><div class="AdvLab">高级选项</div>
                <fieldset><legend>实验室</legend>
                    <label><input name="lab" value="1" type="checkbox" />实验室1</label>  <label><input name="lab" value="2" type="checkbox" />实验室2</label>
                </fieldset>
                <fieldset><legend>机房</legend>
                    <label><input name="room" value="1" type="checkbox" />机房1</label>  <label><input name="room" value="2" type="checkbox" />机房2</label>
                </fieldset>
                <fieldset><legend>状态</legend>
                    <label><input name="stat" value="1" type="checkbox" />空闲</label>  <label><input name="room" value="2" type="checkbox" />教学使用中</label>    <label><input name="room" value="2" type="checkbox" />自由上机使用中</label>  <label><input name="room" value="2" type="checkbox" />故障</label>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">编号</th><th>实验室</th><th>房间</th><th>设备名称</th><th>使用信息</th><th>状态</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <tr><td>1</td><td>实验室1</td><td>房间1</td><td>设备1</td><td><a title="教师：李四, 班级：班级1, 课程:课程1, 更多。。。">班级1教学使用中</a></td><td>正常</td><td><div class="OPTD"></div></td></tr>
                <tr><td>1</td><td>实验室2</td><td>房间2</td><td>设备2</td><td><a>张三自由上机中</a></td><td>正常</td><td><div class="OPTD"></div></td></tr>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".UniTab").UniTab();

            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" title="截屏"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" title="查看进程"><img src="../../../themes/icon_s/13.png"/></a>\
                        <a href="#" title="发消息"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a href="#" title="远程开机"><img src="../../../themes/icon_s/15.png"/></a>\
                        <a href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
        });
    </script>
</form>
</asp:Content>