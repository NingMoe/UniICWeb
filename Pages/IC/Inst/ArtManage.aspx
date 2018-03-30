<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ArtManage.aspx.cs" Inherits="Pages_Inst_ArtManage" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold">信息维护</h2>
        <input type="hidden" id="cur_cls" name="cur_cls" />
        <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <%=clsList %>
                </div>
            </div>
        </div>
                    <div style="border-top:1px solid #ccc;height:30px;text-align:right;padding:2px 5px;">
                <a id="btnNew">添加文章</a>
            </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th style="text-align: center; min-width: 80px">标题
                        </th>
                        <th style="text-align: center; min-width: 80px;">摘要备注
                        </th>
                        <th style="text-align: center;">来源
                        </th>
                        <th style="text-align: center; width: 120px">创建日期
                        </th>
                        <th style="text-align: center; width: 60px">状态
                        </th>
                        <th style="text-align: center; width: 120px">操作
                        </th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=artList %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />

        </div>
        <script type="text/javascript">
            $(function () {
                $(".UniTab").UniTab();
                $(".ListTbl").UniTable();
                $("#btnNew").button().click(function () {
                    var tt = $("#tabl a.ui-tabs-active").html();
                    $.lhdialog({
                        title: tt,
                        width: '780px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ArtDlg.aspx?op=new&cl=<%=clsSn%>'
                    });
                });
                $(".edit").click(function () {
                    var id = $(this).attr("artId");
                    $.lhdialog({
                        title: "编辑文章",
                        width: '780px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/ArtDlg.aspx?op=edit&id='+id+'&cl=<%=clsSn%>'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
