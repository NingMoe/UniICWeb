<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Camera.aspx.cs" Inherits="SupSys_DCS" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>���������</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn"><a id="btnNewDCS">�½�</a></div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">���</th>
                        <th>���������</th>
                        <th>IP��ַ</th>
                   
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
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setDCSBtn" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setDCSBtn").click(function () {
                    var dwSN = $(this).parents("tr").children().first().text();
                    $.lhdialog({
                        title: '�޸ļ�����',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetCam.aspx?op=set&dwSN=' + dwSN
                    });
                });
                $("#btnNewDCS").click(function () {
                    $.lhdialog({
                        title: '�½�������',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetCam.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
