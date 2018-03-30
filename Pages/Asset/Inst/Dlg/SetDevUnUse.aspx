<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetDevUnUse.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <input type="hidden" id="dwAttendantID" name="dwAttendantID" />
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <table>
                <tr>
                    <th>资产编号：</th>
                    <td>
                        <div id="szAssertSN"></div>
                    </td>
                    <th>资产名称：</th>
                    <td>
                        <div id="szDevName"></div>
                    </td>
                </tr>
                <tr>
                    <th>归属实验室：</th>
                    <td>
                        <div id="szRoomName"></div>
                    </td>
                    <th>归属部门：</th>
                    <td>
                        <div id="szDeptName"></div>
                    </td>
                </tr>
                <tr>
                    <th>附件说明</th>
                    <td>
                        <input type="file" name="fileurl" id="fileurl" size="45" class="validate[required]"/>
                      
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="4">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>

                        <button type="button" id="print">打印</button>
                    </td>

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
            AutoUserByName($("#szAttendantName"), 2, $("#dwAttendantID"), null, null, null);
            $("#OK,#print").button();
            $("#print").click(function () {
                window.print();
            });
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
