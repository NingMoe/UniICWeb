<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="outPortContrl.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                
                <tr>
                    <td colspan="4" class="tblBtn">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
