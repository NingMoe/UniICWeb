<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUseGroupSetAll.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>����</h2>
       
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setDev" href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="delDev" href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                    <a href="#" title="����Ϣ"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="����ʹ����Ա"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" title="����"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="����"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" title="Զ�̿���"><img src="../../../themes/icon_s/17.png"/></a>\
                    </div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#OPTDCTRL").html('<div class="OPTDBtnSet">\
                            <a href="javascript:fAllOp("open")" title="�����½�"><img src="../../../themes/icon_s/6.png"/></a>\
                            <a href="javascript:fAllOp("shutdown")" title="���½"><img src="../../../themes/icon_s/15.png"/></a>\
                            <a href="javascript:fAllOp("restart")" title="��Ҫ��½"><img src="../../../themes/icon_s/13.png"/></a>\
                            <a href="javascript:fAllOp("nologin")" title="����״̬"><img src="../../../themes/icon_s/21.png"/></a>\
                            <a href="javascript:fAllOp("neddlogin")" title="����ʹ����Ա"><img src="../../../themes/icon_s/3.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="�豸ж�ؿͻ���"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="������Ϣ"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="����"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="�ػ�"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="����"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="U������"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="U�̽���"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="��������"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="��������"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="ж�ؿͻ���"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="����"><img src="../../../themes/icon_s/5.png"/></a>\
                            <a href="javascript:fAllOp("unistall")" title="����"><img src="../../../themes/icon_s/5.png"/></a>\</div>');
                $(".OPTDBtnSet").UIAPanel({
                    theme: "none.png", borderWidth: "5", minWidth: "175", maxWidth: "175", minHeight: "25", maxHeight: "95", speed: 50
                });
                $(".setDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().children().first().val();
                  
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetDevice.aspx?op=set&id=' + dwDevID
                    });
                });
                $(".delDev").click(function () {
                    var dwDevID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwDevID);
                }, '��ʾ', 1, function () { });
            });
                $(".FixBtn").click(function () {
                    $.lhdialog({
                        title: '�½�',
                        width: '660px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevice.aspx?op=new'
                    });
                });
            });
            $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
        </script>
    </form>
</asp:Content>
