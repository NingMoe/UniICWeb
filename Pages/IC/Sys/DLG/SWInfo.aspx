<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SWInfo.aspx.cs" Inherits="_Default"%>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>名单详细设置</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
        <div class="toolbar">
           
             
            <div id="newBtn" class="FixBtn"><a>添加软件</a></div>
            
        </div>  
         
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">图标</th>
                        <th>软件名</th>
                        <th>类型</th>
                        <th>备注</th>
                        
                        <th width="25px">操作</th>
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
            $(".UniTab").UniTab();
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="setBtn" title="修改"><img src="../../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="delBtn" title="删除"><img src="../../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#newBtn").button().click(function () {
                $.lhdialog({
                    title: '添加软件',
                    width: '600px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:SetSW.aspx?op=new&id=<%=m_szClassID%>'
                });
            });
          
            $(".delBtn").click(function () {
             
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定删除?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '提示', 1, function () { });
              });
        });
        
            
        </script>
    </form>
</asp:Content>
