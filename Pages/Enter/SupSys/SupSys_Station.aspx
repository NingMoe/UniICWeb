<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SupSys_Station.aspx.cs" Inherits="Sub_Station"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>站点管理</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <div class="FixBtn"><a id="btnNewStation">新建站点</a></div>
      
        <div class="tb_btn">
        <!--
            <div class="AdvOpts"><div class="AdvLab">高级选项</div>
                <fieldset><legend>类别</legend>
                    <label><input name="kind" value="1" type="checkbox" />类别1</label>  <label><input name="kind2" value="2" type="checkbox" />类别2</label>
                </fieldset>
                <fieldset><legend>状态</legend>
                    <label><input name="kind1" value="1" type="checkbox" />开放中</label>  <label><input name="kind4" value="2" type="checkbox" />未开放</label>
                </fieldset>
            </div>
            -->
        </div>
       
    </div>
   
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">编号</th><th>名称</th><th>系统</th><th><%=ConfigConst.GCDeptName %></th><th>管理员</th><th>值班员</th><th>状态</th><th>备注</th><th width="25px">操作</th></tr>
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
                        <a class="setStationBtn" title="修改"><img src="../themes/iconpage/edit.png"/></a>\
                         <a class="delStationBtn"  href="#" title="删除"><img src="../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setStationBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                $.lhdialog({
                    title: '修改站点',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetStation.aspx?op=set&dwStaSN=' + dwSN
                });
            });
             $(".delStationBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                ConfirmBox("确定删除站点?",function(){                
			        ShowWait();
			        TabReload($("#<%=formAdvOpts.ClientID%>").serialize()+"&delID="+dwSN);
                },'提示',1,function(){});
            });
            $("#btnNewStation").click(function () {
                $.lhdialog({
                    title: '新建站点',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_NewStation.aspx?op=new'
                });
            });                                     
        });
    </script>
</form>
</asp:Content>