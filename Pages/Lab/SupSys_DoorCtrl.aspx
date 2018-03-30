<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SupSys_DoorCtrl.aspx.cs" Inherits="SupSys_DoorCtrl"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>门禁管理</h2>
    <div class="toolbar">
        <div class="tb_info">总数：5个，在线：5个</div>
        <div class="FixBtn"><a id="btnNewDoorCtrl">新建</a></div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">高级选项</div>
                <fieldset><legend>类别</legend>
                    <label><input name="kind" value="1" type="checkbox" />类别1</label>  <label><input name="kind" value="2" type="checkbox" />类别2</label>
                </fieldset>
                <fieldset><legend>状态</legend>
                    <label><input name="kind" value="1" type="checkbox" />开放中</label>  <label><input name="kind" value="2" type="checkbox" />未开放</label>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="65px">集控器编号</th><th>门禁编号</th><th>集控器名称</th><th>房间号</th><th>站点</th><th>备注</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setDoorCtrlBtn" title="修改"><img src="../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setDoorCtrlBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                $.lhdialog({
                    title: '修改集控器',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetDoorCtrl.aspx?op=set&dwSN=' + dwSN
                });
            });
            $("#btnNewDoorCtrl").click(function () {
                $.lhdialog({
                    title: '新建集控器',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetDoorCtrl.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>