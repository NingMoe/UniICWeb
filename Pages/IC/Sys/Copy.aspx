<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Copy.aspx.cs" Inherits="Sub_Lab"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2></h2>
    <div class="toolbar">
        <input type="button" id="btnCopyFee" value="�����շѱ�׼" /> 
        <input type="button" id="btnCopyResvRule" value="����ԤԼ����" /> 
    </div>
    <div class="content">
    </div>
    <script type="text/javascript">
      
        $(function () {
            $("#btnCopyFee").button();
            $("#btnCopyResvRule").button();
            $(".btnCopyFee").click(function () {
                $.lhdialog({
                    title: '�����շѱ�׼',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/CopyFee.aspx?op=new'
                });
            });
            $(".btnCopyResvRule").click(function () {
                $.lhdialog({
                    title: '����ԤԼ����',
                    width: '760px',
                    height: '550px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../../ueditor/default.aspx?id=' + dwLabID + "&type=LabInfo"
                });
            });
        });
    </script>
</form>
</asp:Content>