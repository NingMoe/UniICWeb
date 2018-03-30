<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="TestCard.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>ʵ����Ŀ������</h2>
    <div class="toolbar">
        <div class="tb_info">             
        </div>
        <div class="FixBtn"><a id="btnImport">����ʵ����Ŀ��</a><a id="btnNew">�½�ʵ����Ŀ��</a></div>
        <div class="tb_btn">
          
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th name="szTestName">ʵ����Ŀ����</th><th name="szCategoryName">���</th><th name="dwGroupPeopleNum">ÿ������</th><th name="dwTestHour">ѧʱ</th><th>��ע</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>       
        <uc1:PageCtrl runat="server" ID="PageCtrl" />
    </div>
    <script type="text/javascript">
        $(function () {
            $(".ListTbl").UniTable();
            
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="btnSet" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="btnDel" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
            });
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '600px',
                    lock: true,
                    content: 'url:Dlg/SetTestCard.aspx?op=new'
                });
            });

            $("#btnImport").click(function () {
                $.lhdialog({
                    title: '����ʵ����Ŀ��',
                    width: '600px',
                    lock: true,
                    content: 'url:../Import.aspx?szDestName=TESTCARD&szTitle=' + escape("ʵ����Ŀ��") + '&szTemplateFile=<%=MyVPath%>Upload/TestCard_Template.csv'
                        + '&szDestFieldList=szTestName,szCategoryName,dwGroupPeopleNum,dwTestHour,dwTestClass,dwTestKind,dwRequirement,szConstraints,szMemo'
                });
            });

            $(".btnSet").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸�',
                    width: '600px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/SetTestCard.aspx?op=set&id=' + dwID
                });
            });
            $(".btnDel").click(function () {
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