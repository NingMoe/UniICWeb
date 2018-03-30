<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetFeeAll.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <input name="feeSN" id="feeSN" type="hidden" />
        <input name="ident" id="ident" type="hidden" />
        <div class="formtitle">
            <h2>收费标准</h2>
        </div>
        <div id="szFeeList" style="margin: 15px 20px">
            <%=m_szFee %>
        </div>
        <div id="feeDiv" class="formtable">

            <table style="width: 600px; margin: 0 auto; border-left: 1px solid #000; border-right: 1px solid #000; border-top: 1px solid #000" cellspacing="1">
                <tr>
                    <td>费用类型</td>
                    <td>单价(0.01元)</td>
                    <td>计时单位(分钟)</td>
                </tr>
                <tr>
                    <td>使用费</td>
                    <td>
                        <input type="text" name="useFeeUint" id="useFeeUint" /></td>
                    <td>
                        <input type="text" name="useTimeUint" id="useTimeUint" /></td>
                </tr>
                <tr>
                    <td>耗材费</td>
                    <td>
                        <input type="text" name="conFeeUint" id="conFeeUint" /></td>
                    <td>
                        <input type="text" name="conTimeUint" id="conTimeUint" /></td>
                </tr>
                <tr>
                    <td>代检费</td>
                    <td>
                        <input type="text" name="entFeeUint" id="entFeeUint" /></td>
                    <td>
                        <input type="text" name="entTimeUint" id="entTimeUint" /></td>
                </tr>
                <tr>
                    <td>样本费</td>
                    <td>
                        <input type="text" name="sampleFeeUint" id="sampleFeeUint" /></td>
                    <td>
                        <input type="text" name="sampleTimeUint" id="sampleTimeUint" /></td>

                </tr>

            </table>

        </div>
        <div style="margin-top: 20px;" class="formtable">
            <table>
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="button" id="OK">确定</button>
                        <button type="button" id="Cancel">关闭</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        #feeDiv table {
            width: 650px;
            border-bottom: 1px solid #000;
        }

            #feeDiv table td {
                border-left: 1px solid #000;
                height: 30px;
                border-top: 1px solid #000;
            }

                #feeDiv table td input {
                    width: 80px;
                }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            <%if(nIsAdminSup!=1){ %>
            $("input[type='text']").attr("readonly", "readonly")//将input元素设置为readonly
            $("input[type='text']").attr("disabled", "disabled")//将input元素设置为disabled
            <%}%>

            $("#szFeeList").button();
            $("#OK").click(function () {
                var feeSN = $("#feeSN").val();

                SaveFee(feeSN, 2);
                $("#feeSN").val($(this).attr("id"))
                feeSN = ($(this).attr("id"));
                GetFee(feeSN);
                MessageBox("设置成功", "提示", 3, function () { Dlg_OK() });
            });

            $("input[name='feeSN']").click(function () {
                var feeSN = $("#feeSN").val();

                SaveFee(feeSN, 2);
                $("#feeSN").val($(this).attr("id"))
                feeSN = ($(this).attr("id"));
                GetFee(feeSN);
            });
            function GetFee(feeSN) {
                $.get(
                     "../../data/GetUniFee.aspx",
                     { feeSN: feeSN },
                     function (data) {
                         var obj = jQuery.parseJSON(data);
                         $("#useFeeUint").val(obj.useFeeUint);
                         $("#useTimeUint").val(obj.useTimeUint);

                         $("#conFeeUint").val(obj.conFeeUint);
                         $("#conTimeUint").val(obj.conTimeUint);

                         $("#entFeeUint").val(obj.entFeeUint);
                         $("#entTimeUint").val(obj.entTimeUint);

                         $("#sampleFeeUint").val(obj.sampleFeeUint);
                         $("#sampleTimeUint").val(obj.sampleTimeUint);
                     }
                   );

            }
            function SaveFee(feeSN, ident) {
                var useFeeUint = $("#useFeeUint").val();
                var useTimeUint = $("#useTimeUint").val();

                var conFeeUint = $("#conFeeUint").val();
                var conTimeUint = $("#conTimeUint").val();

                var entFeeUint = $("#entFeeUint").val();
                var entTimeUint = $("#entTimeUint").val();

                var sampleFeeUint = $("#sampleFeeUint").val();
                var sampleTimeUint = $("#sampleTimeUint").val();

                $.get(
                     "../../data/saveUniFee.aspx",
                     { feeSN: feeSN, ident: ident, useFeeUint: useFeeUint, useTimeUint: useTimeUint, conFeeUint: conFeeUint, conTimeUint: conTimeUint, entFeeUint: entFeeUint, entTimeUint: entTimeUint, sampleFeeUint: sampleFeeUint, sampleTimeUint: sampleTimeUint },
                     function (data) {
                         if (data == "success") {

                         }
                         else {

                         }

                     }
                   );

            }
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
        });
    </script>
</asp:Content>
