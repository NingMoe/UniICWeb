<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="UrlInfo.aspx.cs" Inherits="_Default"%>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>������ϸ����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       <input type="hidden" id="delID" name="delID" />
        <div class="toolbar">
           
             
            <div id="newBtn" class="FixBtn"><a>�����ַ</a></div>
            
        </div>  
         
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">���</th>
                        <th>��ַ</th>
                        <th>��ע</th>
                        
                        <th width="25px">����</th>
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
                       \
                        <a href="#" class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#newBtn").click(function () {
                $.lhdialog({
                    title: '�½�������',
                    width: '600px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:SetUrl.aspx?op=new&id=<%=m_szClassID%>'
                });
            });
            $(".setBtn").click(function () {                
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸ĺ�����',
                    width: '600px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetUrlClassF.aspx?op=set&id=' + dwID
                });
            });
            $(".delBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwID);
                }, '��ʾ', 1, function () { });
              });
        });
        
            
        </script>
    </form>
</asp:Content>
