<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDevUnuse.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <div class="formtable">
            <table>
                <tr>
                    <th>资产名称：</th>
                    <td>
                        <input id="szDevName" name="szDevName" /></td>
                </tr>
                <tr>
                    <th><%=ConfigConst.GCDevName %>所在实验室：</th>
                    <td>
                        <input id="szRoomName" name="szRoomName" /></td>
                </tr>
                <tr>
                    <th>价格区间：</th>
                    <td>
                        <input type="text" style="width: 80px" id="dwPricStart" name="dwPricStart" />到<input type="text" style="width: 80px" id="dwPriceEnd" name="dwPriceEnd" /></td>
                </tr>
                <tr>
                    <th>购置日期：</th>
                    <td>
                        <input type="text" style="width: 80px" id="dwDateStart" name="dwDateStart" />到<input type="text" style="width: 80px" id="dwDateEnd" name="dwDateEnd" /></td>
                </tr>

                
                <tr>
                    <th>附件说明</th>
                    <td>
                        <input type="file" name="fileurl" id="fileurl" size="45" />

                    </td>
                </tr>

                <tr>
                    <th></th>
                    <td>
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">关闭</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
           </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            setInterval(function () {
                if (!($("#szDevName").val() == "")) {
                    $("#szRoomName").attr("disabled", "disabled");
                    $("#dwPriceEnd").attr("disabled", "disabled");
                    $("#dwPricStart").attr("disabled", "disabled");
                    $("#dwDateStart").attr("disabled", "disabled");
                    $("#dwDateEnd").attr("disabled", "disabled");

                    
                } else {
                    $("#szRoomName").removeAttr("disabled");
                    $("#dwPriceEnd").removeAttr("disabled");
                    $("#dwPricStart").removeAttr("disabled");
                    $("#dwDateStart").removeAttr("disabled");
                    $("#dwDateEnd").removeAttr("disabled");
                }
                }, 100)
            $("#Cancel").button().click(Dlg_Cancel);
            $("#dwDateStart,#dwDateEnd").datepicker({
                changeMonth: true,
                changeYear: true
            });
            AutoUserByName($("#szApproveName"), 2, $("#dwApproveID"), null, null, null);
            AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), null, null);
            AutoDevice($("#szDevName"), 2, $("#dwDevID"), null, null, null, null);
            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
