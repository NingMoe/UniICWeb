<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Rule.aspx.cs" Inherits="Sub_Device"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>�������</h2>
    <div class="toolbar">
        <div class="tb_info">
            <div id="tab1" class="UniTab">
                <a href="Rule.aspx">��Ա����</a><a href="OpenRule.aspx">���Ź���</a>
            </div></div>
        <div class="FixBtn"><a>�½��豸</a></div>
        <div class="tb_btn">
            <div class="AdvOpts" width="350" height="250"><div class="AdvLab">�߼�ѡ��</div>
                <fieldset><legend>ʵ����</legend>
                    <label><input name="lab" value="1" type="checkbox" />ʵ����1</label>  <label><input name="lab" value="2" type="checkbox" />ʵ����2</label>
                </fieldset>
                <fieldset><legend>����</legend>
                    <label><input name="room" value="1" type="checkbox" />����1</label>  <label><input name="room" value="2" type="checkbox" />����2</label>
                </fieldset>
                <fieldset><legend>״̬</legend>
                    <label><input name="stat" value="1" type="checkbox" />����</label>  <label><input name="room" value="2" type="checkbox" />��ѧʹ����</label>    <label><input name="room" value="2" type="checkbox" />�����ϻ�ʹ����</label>  <label><input name="room" value="2" type="checkbox" />����</label>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">���</th><th>ʵ����</th><th>����</th><th>�豸����</th><th>ʹ����Ϣ</th><th>״̬</th><th width="25px">����</th></tr>
            </thead>
            <tbody id="ListTbl">
                <tr><td>1</td><td>ʵ����1</td><td>����1</td><td>�豸1</td><td><a title="��ʦ������, �༶���༶1, �γ�:�γ�1, ���ࡣ����">�༶1��ѧʹ����</a></td><td>����</td><td><div class="OPTD"></div></td></tr>
                <tr><td>1</td><td>ʵ����2</td><td>����2</td><td>�豸2</td><td><a>���������ϻ���</a></td><td>����</td><td><div class="OPTD"></div></td></tr>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".UniTab").UniTab();

            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" title="����"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" title="�鿴����"><img src="../../../themes/icon_s/13.png"/></a>\
                        <a href="#" title="����Ϣ"><img src="../../../themes/iconpage/del.png""/></a>\
                        <a href="#" title="Զ�̿���"><img src="../../../themes/icon_s/15.png"/></a>\
                        <a href="#" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
            });
        });
    </script>
</form>
</asp:Content>