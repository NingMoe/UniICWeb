<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetControl.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>

        <div class="formtable">
            <input type="hidden" id="dwConsoleSN" name="dwConsoleSN" />
            <table>
                <tr>
                    <th>����̨���ƣ�</th>
                    <td>
                        <input type="text" class="validate[required]" id="szConsoleName" name="szConsoleName" /></td>
                    <th>IP��ַ��</th>
                    <td>
                        <input id="szIP" name="szIP" class="validate[required,validate[custom[ipv4]]" /></td>
                </tr>
                <tr>
                    <th>��ʼʱ�䣺</th>
                    <td>
                        <div class="TimePicker TimePicker-time">
                            <input name="dwOpenTime" id="dwOpenTime" class="TimePicker-time-input" /></div>
                    </td>
                    <th>����ʱ�䣺</th>
                    <td>
                        <div class="TimePicker TimePicker-time">
                            <input name="dwCloseTime" id="dwCloseTime" class="TimePicker-time-input" /></div>
                    </td>
                </tr>
                <tr>
                    <th>���ͣ�</th>
                    <td>
                        <select id="dwKind" name="dwKind"><%=dwKindList %>></select></td>
                    <th>����</th>
                    <td><%=szKindObject %></td>
                </tr>
                <tr>                    
                    <td colspan="4">����<%=ConfigConst.GCRoomName %>��<%=m_szRoom %></td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtable label {
        margin-right:10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);

            var opt = {
                'time': {
                    preset: 'time'
                }
            }

            $('#dwOpenTime').scroller('destroy').scroller($.extend(opt["time"], {
                theme: "ios",
                mode: "scroller",
                lang: "zh",
                display: "bubble",
                animate: "flip"
            }));

            $('#dwCloseTime').scroller('destroy').scroller($.extend(opt["time"], {
                theme: "ios",
                mode: "scroller",
                lang: "zh",
                display: "bubble",
                animate: "flip"
            }));
            $('.TimePicker').hide();
            $('.TimePicker-time').show();

        });
    </script>
</asp:Content>
