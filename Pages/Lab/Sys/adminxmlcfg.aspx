<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="adminxmlcfg.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>本地参数配置</h2>
            <div class="toolbar">
        <div class="tb_info">
        </div>
        <div class="FixBtn">
               <a id="btnNew" class="btnClss">新建</a>
        </div>
    </div>
        <div style="margin-top:8px;">
        </div>
        <div>
             <div  class="tb_infoInLine"  style="margin:5px 0px">
            <table style="width:99%">
               <tr>
                   <td>
                       选择类型：
                       <select id="kind" name="kind">
                       <option value="ResvAbsTime">绝对时间预约</option>
                       <option value="ResvTheme">预约主题</option>
                       </select></td>
                  <th><input type="submit" id="btn" value="查询" /></th>
               </tr>
           </table>
               </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>选项</th>                
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
              <style>
            .tb_infoInLine table tr th{
            text-align:center;
            }
            .tb_infoInLine table tr td input{
            margin-left:5px;
            }
            .tb_infoInLine table tr td select{
            margin-left:5px;
            }
        </style>
        <script type="text/javascript">
            $(function () {
                $("#btn,#newList,#setDevKind,#setDevRoom").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\ </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var name = $(this).parents("tr").children().first().data("fieldname");
                    $.lhdialog({
                        title: '修改',
                        width: '660px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setAdminxmlcfg.aspx?op=set&id=' + dwDevID + '&fieldName=' + name
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().data("id");
                    var name = $(this).parents("tr").children().first().data("fieldname");
                    ConfirmBox("确定删除?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=del&delID=" + dwDevID + '&fieldName=' + name);
                    }, '提示', 1, function () { });
                  });
                $("#btnNew").button()
                    .click(function () {
                        $.lhdialog({
                            title: '新建',
                            width: '660px',
                            height: '520px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/NewAdminxmlcfg.aspx?op=new'
                        });
                    });

            });
            $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: false,});

        </script>
    </form>
</asp:Content>