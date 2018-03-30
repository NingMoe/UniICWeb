<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListByKind.aspx.cs" Inherits="ListByKind"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>PC���͵��豸�б�</h2>
    <div class="toolbar">
        <div class="tb_info"></div>
        <button type="button" id="Back">����</button>
        <div class="tb_btn">
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr>
                    <th width="60px">�豸���</th>
                    <th>����</th>
                    <th>ʵ����</th>
                    <th>����</th>
                    <th>�ͺ�</th>
                    <th>�豸״̬</th>
                    <th>ʹ��״̬</th>
                    <th>ʹ����</th>
                    <th width="25px">����</th>
                </tr>
            </thead>
            <tbody id="ListTbl">
                <%=m_szOut %>
            </tbody>
        </table>
        <uc1:PageCtrl runat="server" ID="PageCtrl" />
        <div class="BarStat tblBottomStat" data-color="2">
            <h1><span>--------</span><strong>̨��</strong></h1>
            <p><span>����1</span><strong>30</strong></p>
            <p><span>����2</span><strong>60</strong></p>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#Back").button().click(function(){
                TabJump("Device/Stat.aspx");
            });
            $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" title="����"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="�ӳ�ʱ��"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" title="�鿴��Ƶ"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="�鿴ˢ����¼"><img src="../../../themes/icon_s/13.png"/></a>\
                    <a href="#" title="���¼"><img src="../../../themes/iconpage/del.png""/></a></div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
            });
        });
    </script>
</form>
</asp:Content>