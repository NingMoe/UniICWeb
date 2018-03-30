<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="CheckDevUnuse.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server" enctype="multipart/form-data">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="dwDevID" name="dwDevID" />
        <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwApproveID" name="dwApproveID" />
        <div class="formtable">
            <table>
                <tr>
                    <th>描述：</th>
                    <td>
                        <div id="szOOSInfo" name="szOOSInfo"></div>
                    </td>
                </tr>
                  <tr>
                    <th>申请日期：</th>
                    <td>
                        <div id="dwApplyDate2" name="dwApplyDate2"></div>
                    </td>
                </tr>
                  <tr>
                    <th>申请人：</th>
                    <td>
                        <div id="szApplyName" name="szApplyName"></div>
                    </td>
                </tr>
                <tr>
                    <th>审批结果</th>
                    <td>
                 <select name="dwstae" id="dwstae">
                     <option value="2">审批通过</option>
                     <option value="4">审批不通过</option>
                 </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                    <a target="_blank" href="<%=szHref %>">查看附件</a>
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
            $("#Cancel").button().click(Dlg_Cancel);
            AutoUserByName($("#szApproveName"), 2, $("#dwApproveID"), null, null, null);
            AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), null, null);
            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
