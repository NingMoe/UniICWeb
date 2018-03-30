<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceListRunState.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=Title %></h2>
        <div class="toolbar">
        <button type="button" id="Back">����</button>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>���</th>
                        <th>������</th>
                        <th>IP��ַ</th>
                        <th>�����豸����</th>
                        <th>�ͺ�</th>
                        <th>���</th>
                        <th>��������</th>
                        <th>����ʵ����</th>
                        <th>ʹ����</th>
                         <th>״̬</th>
                        <th width="25px">����</th>
                    </tr>
                  
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
            <div class="BarStat tblBottomStat" data-color="2">
                <h1><span>--------</span><strong>�豸��</strong></h1>
                <p><span>Dell����</span><strong>12</strong></p>
                <p><span>Apple����</span><strong>21</strong></p>
                <p><span>Lenovo����</span><strong>17</strong></p>
            </div>
            <div class="BarStat tblBottomStat" data-color="2">
                <h1><span>--------</span><strong>�豸��</strong></h1>
                <p><span>����1</span><strong>12</strong><strong>17</strong></p>
                <p><span>����2</span><strong>17</strong><strong>5</strong></p>
                <p><span>����3</span><strong>17</strong><strong>25</strong></p>
            </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTD1").html('<div class="OPTDBtn">\
                    <a href="#" title="Զ�̿���"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTD2").html('<div class="OPTDBtn">\
                    <a href="#" title="Զ�̹ػ�"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTD3").html('<div class="OPTDBtn">\
                    <a href="#" title="��Ҫ��¼"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTD4").html('<div class="OPTDBtn">\
                    <a href="#" title="�ָ�����"><img src="../../../themes/icon_s/10.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
        </script>
    </form>
</asp:Content>
