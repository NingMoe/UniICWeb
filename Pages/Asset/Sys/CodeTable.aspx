<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="CodeTable.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwCodeType" name="dwCodeType" />
        <h2><%=szTitleName%>����</h2>
        <div class="toolbar">
               <div class="tb_info">
                <div class="UniTab" id="tabl">
                    
                    <a href="Dept.aspx?" id="dwCodeType5">��λ����</a>
                    <a href="CodeTable.aspx?dwCodeType=4" id="dwCodeType4">ѧ�ƹ���</a>
                </div>
            </div>

            <div class="FixBtn"><a id="btnNew">�½�<%=szTitleName%></a></div>
           
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szCodeSN">����</th>
                        <th name="szCodeName">����</th>
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
            var tabl = $(".UniTab").UniTab();

            $(function () {
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delLabBtn"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setLabBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                var dwCodeType = $(this).parents("tr").children().first().attr("data-type");
                $.lhdialog({
                    title: '�޸�',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewCode.aspx?op=set&szCodeSN=' + dwLabID + '&dwCodeType=' + dwCodeType
                });
            });
           
            $(".delLabBtn").click(function () {
                var szLabSN = $(this).parents("tr").children().first().text();
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                var dwCodeType = $(this).parents("tr").children().first().attr("data-type");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID + '&dwCodeType=' + dwCodeType);
                }, '��ʾ', 1, function () { });
            });
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/NewCode.aspx?op=new&dwCodeType=' + $("#dwCodeType").val()
                });
            });
            $(".ListTbl").UniTable({HeaderIndex:false});
            var tabl = $(".UniTab").UniTab();

        });
        </script>
    </form>
</asp:Content>