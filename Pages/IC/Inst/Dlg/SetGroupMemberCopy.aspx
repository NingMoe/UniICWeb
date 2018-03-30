<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetGroupMemberCopy.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">

            <table>
                 <tr>
                  <th   style="height:60px;width:60px;">
                       人员名单:
                   </th>
                    <td style="height:60px">
                       <%=m_szGroupMemberList %> 
                    </td>
                </tr>
                <tr>                    
                   <th  style="height:60px;width:60px;">
                       选择房间：
                   </th>
                    <td  style="height:60px">
                       <%=m_szDeviceList %> 
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="tblBtn">
                         <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button>
                    </td>
                </tr>
            </table>
           
        </div>

    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        label {
        margin-left:10px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);;
        });
    </script>
</asp:Content>
