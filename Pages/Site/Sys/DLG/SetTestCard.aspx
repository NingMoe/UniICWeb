<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetTestCard.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input name="dwTestCardID" type="hidden" />
        <table>
            <tr><th>实验项目名称：</th><td><input id="szTestName" name="szTestName" class="validate[required]" /></td><th>类别名称：</th><td><input id="szCategoryName" name="szCategoryName"  class="validate[required]" /></td></tr>
            <tr><th>每组人数：</th><td><input id="dwGroupPeopleNum" name="dwGroupPeopleNum"/></td><th>学时数：</th><td><input id="dwTestHour" name="dwTestHour"/></td></tr>
            <tr><th>实验类别：</th><td><select id="dwTestClass" name="dwTestClass">
                <option value="1">基础</option>
                <option value="2">专业基础</option>
                <option value="3">专业</option>
                <option value="4">其它</option></select></td><th>实验类型：</th><td>
                    <select id="dwTestKind" name="dwTestKind">
                <option value="1">演示性</option>
                <option value="2">验证性</option>
                <option value="3">综合性</option>
                <option value="4">研究设计</option>
                <option value="5">其它</option></select></td>
            </tr>
            <tr><th>实验要求：</th><td><select id="dwRequirement" name="dwRequirement">
                <option value="1">必修</option>
                <option value="2">选修</option>
                <option value="3">其它</option></select></td><th>备注：</th><td><input id="szMemo" name="szMemo"/></td></tr>

            <tr><td class="btnRow" colspan="4"><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
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
