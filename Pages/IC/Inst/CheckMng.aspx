<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="CheckMng.aspx.cs" Inherits="Sub_CheckMng"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>��˹���</h2>
    <div class="toolbar">
    <div class="tb_info">������5��������ˣ�0����δ��ˣ�2��</div>
    <div class="tb_btn">
        <div class="AdvOpts"><div class="AdvLab">�߼�ѡ��</div>
            <fieldset><legend>���</legend>
                <label><input name="room" value="1" type="checkbox" />��ѧԤԼ</label>  <label><input name="room" value="2" type="checkbox" />����ԤԼ</label>
            </fieldset>
            <fieldset><legend>״̬</legend>
                <label><input name="stat" value="1" type="checkbox" />δ���</label>  <label><input name="stat" value="2" type="checkbox" />�����</label>
            </fieldset>
        </div>
    </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th>���</th><th>���</th><th>����</th><th>״̬</th><th width="80px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <tr><td>1</td><td>��ѧԤԼ</td><td>����ԤԼ����1</td><td>δ���</td><td><div class="OPTD"></div></td></tr>
                <tr><td>2</td><td>����ԤԼ</td><td>����ԤԼ�豸1</td><td>δ���</td><td><div class="OPTD"></div></td></tr>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
    $(function () {
        $(".OPTD").html('<div class="OPTDBtn">\
        <a href="#" title="���"><img src="../../../themes/icon_s/20.png"/></a></div>');
        $(".OPTDBtn").UIAPanel({
            theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
        });
    });
    </script>
</form>
</asp:Content>

