<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetTestCard.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input name="dwTestCardID" type="hidden" />
        <table>
            <tr><th>ʵ����Ŀ���ƣ�</th><td><input id="szTestName" name="szTestName" class="validate[required]" /></td><th>������ƣ�</th><td><input id="szCategoryName" name="szCategoryName"  class="validate[required]" /></td></tr>
            <tr><th>ÿ��������</th><td><input id="dwGroupPeopleNum" name="dwGroupPeopleNum"/></td><th>ѧʱ����</th><td><input id="dwTestHour" name="dwTestHour"/></td></tr>
            <tr><th>ʵ�����</th><td><select id="dwTestClass" name="dwTestClass">
                <option value="1">����</option>
                <option value="2">רҵ����</option>
                <option value="3">רҵ</option>
                <option value="4">����</option></select></td><th>ʵ�����ͣ�</th><td>
                    <select id="dwTestKind" name="dwTestKind">
                <option value="1">��ʾ��</option>
                <option value="2">��֤��</option>
                <option value="3">�ۺ���</option>
                <option value="4">�о����</option>
                <option value="5">����</option></select></td>
            </tr>
            <tr><th>ʵ��Ҫ��</th><td><select id="dwRequirement" name="dwRequirement">
                <option value="1">����</option>
                <option value="2">ѡ��</option>
                <option value="3">����</option></select></td><th>��ע��</th><td><input id="szMemo" name="szMemo"/></td></tr>

            <tr><td class="btnRow" colspan="4"><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
    });
</script>
</asp:Content>
