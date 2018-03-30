<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="SupSys_DCS.aspx.cs" Inherits="SupSys_DCS"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2><%=szTitle %>管理</h2>
    <input type="hidden" name="dcsKind" id="dcsKind" />
    <div class="toolbar">
        <div class="tb_info">总数：5个，在线：5个</div>
        <div class="FixBtn"><a id="btnNewDCS">新建</a></div>
        <div class="tb_btn">
        <!--
            <div class="AdvOpts"><div class="AdvLab">高级选项</div>                          
                </fieldset>
               
            </div>
            -->
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="85px"><%=szTitle %>编号</th><th><%=szTitle %>名称</th><th>站点</th><th><%=szTitle %>状态</th><th>备注</th><th width="25px">操作</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
                 var kind="";
                if($('#dcsKind').val()=="1")                       
                {
                    kind="集控器";
                }
                else if($('#dcsKind').val()=="2")
                {
                    kind="摄像机";
                }
                
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setDCSBtn" title="修改"><img src="../themes/iconpage/edit.png"/></a>\
                         <a class="delDCSBtn"  href="#" title="删除"><img src="../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setDCSBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                var dwStaSN=$(this).parents("tr").children().first().attr("dwStaSN");                                  
                $.lhdialog({
                    title: '修改'+kind,
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_SetDCS.aspx?op=set&dwSN=' + dwSN+'&dwDCSKind='+$('#dcsKind').val()
                });
            });
              $(".delDCSBtn").click(function () {
                var dwSN = $(this).parents("tr").children().first().text();
                var dwStaSN=$(this).parents("tr").children().first().attr("dwStaSN");            
                ConfirmBox("确定删除?",function(){                
			        ShowWait();
			        TabReload($("#<%=formAdvOpts.ClientID%>").serialize()+"&delID="+dwSN+"&dwStaSN="+dwStaSN);
                },'提示',1,function(){});
            });
            $("#btnNewDCS").click(function () {
                $.lhdialog({
                    title: '新建'+kind,
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg_NewDCS.aspx?op=new'
                });
            });
        });
    </script>
</form>
</asp:Content>