<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewICResv.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input name="szDevID" id="szDevID" type="hidden" />
        <input name="szowner" id="szowner" type="hidden" />
        <div class="formtable">
            <table>
                <tr>
                    <th><%=ConfigConst.GCRoomName %>��</th>
                    <td colspan="3">
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>��ʼ���ڣ�</th>
                    <td>
                        <input id="szStartDate" name="szStartDate" class="validate[required]" /></td>
                    <th>�������ڣ�</th>
                    <td>
                        <input id="szEndDate" name="szEndDate" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>���ڣ�</th>
                    <td colspan="3">
                       <%=szWeek %>
                    </td>
                </tr>
                <tr>
                    <th>��ʼʱ�䣺</th>
                    <td>
                        <select name="startTimeHour" id="startTimeHour" style="width:40px">
                            <%=TimeHour %>
                        </select>ʱ
                        <select  name="startTimeMin" id="startTimeMin" style="width:40px">
                            <%=TimeMin %>
                        </select>��
                     </td>
                    <th>����ʱ�䣺</th>
                    <td>
                         <select name="endTimeHour" id="endTimeHour" style="width:40px">
                            <%=TimeHour %>
                        </select>ʱ
                        <select  name="endTimeMin" id="endTimeMin" style="width:40px">
                            <%=TimeMin %>
                        </select>��
                        </td>
                </tr>
                <!--
                <tr>
                    <th>������Ա���ţ�</th>
                    <td>
                        <input id="szLogonName" name="szLogonName" title="������������ģ�����" class="validate[required]" /></td>
                    <th>����</th>
                    <td>
                        <label id="szTrueName"></label>
                    </td>
                </tr>
                -->
                <tr>
                    <th>��ע��</th>
                    <td colspan="3">
                        <input id="szMemo" name="szMemo" /></td>
                </tr>

                <tr>
                    <td colspan="4" style="text-align: center">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .enum {
            margin-left:18px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            AutoRoom($("#szDevName"), 2, $("#szDevID"), null, null, null, null);
            //AutoRoom($("#szDevName"), 2, $("#szDevID"), null, null, null, null);
            $("#szStartDate,#szEndDate").datepicker({
            });
            $('#szStaName').val($("#dwStaSN").find("option:selected").text());
            $("#dwStaSN").change(function () {
                $("#szStaName").val($(this).find("option:selected").text());
            });
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);
            $("#szLogonName").keyup(function () {
                var vlist = $("#szLogonName").val().split('��');
                var vlistE = $("#szLogonName").val().split(',');
                if (vlist.length < vlistE.length) {
                    vlist = vlistE;
                }
                var vRes = "";
                var vOwner = "";
                var i = 0;
                for (i = 0; i < vlist.length; i++) {
                    var vTemp = vlist[i];
                    $.get(
                         "../../data/searchaccount.aspx",
                         { Type: "logonname", term: vTemp },
                         function (data) {
                             var vJson = eval(data);
                             if (vJson[0] != null && vJson[0].szTrueName != null) {
                                 vRes = vRes + vJson[0].szTrueName + ";";
                                 vOwner = vOwner + vJson[0].id + ";";
                                 $("#szTrueName").html("");
                                 $("#szTrueName").html(vRes);
                                 $("#szowner").val();
                                 $("#szowner").val(vOwner);
                             }

                         }
                       );

                }

            });
        });
    </script>
</asp:Content>
