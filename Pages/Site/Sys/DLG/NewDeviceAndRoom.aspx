<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDeviceAndRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwDevID" />        
            <input type="hidden" name="dwCtrlMode" id="dwCtrlMode" value="1" />        
        <input type="hidden" name="dwKindID" id="dwKindID" />
        <div class="formtable">
            <table>
                <tr>
                    <th><%=ConfigConst.GCSysKindRoom %>���(*)��</th>
                    <td> <input id="szRoomNo" name="szRoomNo" class="validate[required]" /></td>
               
                    <th><%=ConfigConst.GCSysKindRoom %>����(*)��</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>               
                <tr>
                    <th>����<%=ConfigConst.GCLabName %>��</th>
                    <td>
                        <select id="dwLabID" name="dwLabID">
                            <%=m_szLab %>
                        </select></td>
                           <th>�������ͣ�</th>
                    <td>
                       <input type="text" id="szKindName" name="szKindName" /></td>       
                </tr>              
                <tr>
                  <th>���Ź���</th>
                    <td>
                        <select id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=m_szOpenRule %>
                        </select></td>
              
                     <th>��ط�ʽ��</th> 
                   <td> <%=m_szRoomMode %></td>
                </tr>
                <%if((ConfigConst.GCSysKind & 16) > 0) {%>
             
                <tr>
                  <th>�Ƿ�֧�ֻ���ţ�</th>
                    <td><label><input class="enum" type="checkbox" id="dwProperty2" name="dwProperty2" value="65536">��</label></td>
                </tr>
                <%} %>
                <tr>
                     <th>����Ա��</th>
                    <td><select id="dwAttendantID" name="dwAttendantID"><%=m_szManager %></select></td>
            
                </tr>
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
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {                
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDevKind($("#szKindName"),2,$("#dwKindID"),1,false);
        setTimeout(function () {           
        }, 1);
    });
    </script>
</asp:Content>
