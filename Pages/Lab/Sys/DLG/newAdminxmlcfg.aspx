<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="newAdminxmlcfg.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="oldValue" name="oldValue" />
        <div class="formtable">
            <table>
                <tr>
                    <td colspan="3" style="text-align:center">
                        <select id="selectValue" name="selectValue" style="width:auto">
                            <option value="ResvAbsTime">����ʱ��ԤԼ</option>
                            <option value="ResvTheme">ԤԼ����</option>
                        </select>
                    </td>
                </tr>
                   </table> 
                <div id="divresvAbsTime">
                       <table>
                <tr>
                    <th>ʱ���:</th>
                    <td>
                        ��ʼʱ�䣺<select id="startHour" name="startHour"><%=m_szHour %></select>:<select id="startMin" name="startMin"><%=m_szMin %></select>
                    </td>
                  <td>
                        ����ʱ�䣺<select id="EndHour" name="EndHour"><%=m_szHour %></select>:<select id="EndMin" name="EndMin"><%=m_szMin %></select>
                    </td>
                </tr>
                       </table> 
                    </div>
             
              <div id="divResvTheme" style="display:none">
                  <table>
                <tr>
                    <th>���⣺</th>
                    <td colspan="2">
                         
                        <input id="szThemeName" name="szThemeName" />
                            </td>                               
                </tr>  
              </table>   
                </div>
            <table>
                <tr>
                    <td colspan="4" class="tblBtn">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
     .ui-datepicker select.ui-datepicker-year { width: 43%;}
              .tb_infoInLine td input {
            width:120px;
            }
        select {
        width:80px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#selectValue").change(function () {
                var vVale = $(this).val();
                if (vVale == "ResvAbsTime") {
                    $("#divresvAbsTime").show();
                    $("#divResvTheme").hide();
                }
                else if (vVale == "ResvTheme") {

                    $("#divresvAbsTime").hide();
                    $("#divResvTheme").show();
                }
            });
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
      
    });
    </script>
</asp:Content>
