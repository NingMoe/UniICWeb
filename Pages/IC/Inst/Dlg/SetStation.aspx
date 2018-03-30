<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetStation.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <table>
            <tr><td>��������ţ�</td><td><div id="dwStaSN" name="dwStaSN"></div></td></tr>
            <tr><td>���������ƣ�</td><td><input id="szStaName" name="szStaName"/></td></tr>
            <tr><td>����ɱ�ţ�</td><td><input id="szLicSN" name="szLicSN"/></td></tr>
            <tr><td>��ϵͳ��ţ�</td><td><select id="dwSubSysSN" name="dwSubSysSN">
                <option value="256">ͨ��ʵ����</option>
                <option value="512">ICѧϰ�ռ�</option>
                <option value="257">����ʵ����</option>
                <option value="258">��ѧʵ����</option></select></td></tr>
            <tr><td>���������ţ�</td><td><input type="hidden" id="szDeptName" name="szDeptName"/><select id="dwOwnerDept" name="dwOwnerDept"><%=m_szDept %></select></td></tr>
            <tr><td>�������˺ţ�</td><td><input type="hidden" id="dwManagerID" name="dwManagerID"/><input type="hidden" id="szManName" name="szManName"/><input id="szManName2"/></td></tr>
            <tr><td>ֵ��Ա�˺ţ�</td><td><input type="hidden" id="dwAttendantID" name="dwAttendantID"/><input type="hidden" id="szAttendantName" name="szAttendantName"/><input id="szAttendantName2"/></td></tr>
            <tr><td>��������ע��</td><td><input id="szMemo" name="szMemo"/></td></tr>
            <tr><td></td><td><button type="submit" id="OK">ȷ��</button><button type="button" id="Cancel">ȡ��</button></td></tr>
        </table>
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtitle {
            padding:6px;
            background: #d0d0d0;
            height:30px;
            color: #fff;
            font-size: 20px;
        }
        .formtable table{
            text-align:center;
            margin:auto;
        }
        td {
         padding:6px;
        }
        input, select {
            width: 200px;
        }
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        <%if(bSet){%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        $("#szManName2").autocomplete({
            source: "searchAccount.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szManName").val(ui.item.label);
                        $("#szManName2").val(ui.item.label);
                        $("#dwManagerID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwManagerID").val("");
                    $("#szManName").val("");
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($("#dwManagerID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szManName").val());
            }
        });

        $("#szAttendantName2").autocomplete({
            source: "searchAccount.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szAttendantName").val(ui.item.label);
                        $("#szAttendantName2").val(ui.item.label);
                        $("#dwAttendantID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwAttendantID").val("");
                    $("#szAttendantName").val("");
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($("#dwAttendantID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szAttendantName").val());
            }
        });

        setTimeout(function () {
            if ($("#dwManagerID").val() == "") {
                $("#szManName2").val("");
            } else {
                $("#szManName2").val($("#szManName").val());
            }

            if ($("#dwAttendantID").val() == "") {
                $("#szAttendantName2").val("");
            } else {
                $("#szAttendantName2").val($("#szAttendantName").val());
            }
            
             if ( $("#dwOwnerDept").val() == "") {
                $("#szDeptName").val("");
            } else {
                $("#szDeptName").val( $("#dwOwnerDept").find("option:selected").text());
            }
        }, 1);
    });
</script>
</asp:Content>
