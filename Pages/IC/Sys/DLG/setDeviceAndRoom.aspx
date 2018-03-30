<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="setDeviceAndRoom.aspx.cs" Inherits="_Default" %>

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
                    <td> <input id="szRoomNo" name="szRoomNo" class="validate[required]" title="����Ա�벻Ҫ�޸�" /></td>
               
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
                    <td colspan="3"><label><input class="enum" type="checkbox" id="dwProperty2" name="dwProperty2" value="65536">��</label></td>
                </tr>
                <%} %>

                  
                  <tr>
                     <th>����Ա��</th>
                    <td><select id="dwAttendantID" name="dwAttendantID"><%=m_szManager %></select></td>
                <th>��ϵ��ʽ:</th>
                    <td>
                       <input type="text" id="szAttendantTel" name="szAttendantTel" /></td>     
                </tr>
                 <tr>
                     <th>
                         Ӳ������
                     </th>
                        <td>
                       <input type="text" id="szExtInfo" name="szExtInfo" /></td>   
                     <th></th> 
                    <td colspan="" style="text-align:left"><label><input class="enum" type="checkbox" id="chkopen" name="chkopen" value="1" >�����⿪��</label></td>
                </tr>
                <tr>
                    <th>������Ϣ:</th>
                    <td colspan="1">
                       <input type="text" id="szDevURL" name="szDevURL" /></td>     
               
                    <th>ԤԼ����ʾ˳��:</th>
                    <td colspan="1">
                       <select id="dwUnitPrice" name="dwUnitPrice"><%=szOption %></select></td>     
                </tr>
                  <%string openroomgroup = "0"; try { openroomgroup = System.Web.Configuration.WebConfigurationManager.AppSettings["openroomgroup"]; }
                    catch { } if (openroomgroup == "1")
                    { %>
                 <tr>
                   <th>��Ϸ��䣺</th>
                   <td colspan="3" style="text-align:left">
                       <div style="width:99%;height:65px;overflow:scroll">
                       <%=szSubRoom %>
                           </div>

                   </td>
                   </tr>
                <%} %>
                 <tr>
                    <th>У����</th>
                    <td colspan="3"><select id="dwCampusID" name="dwCampusID"><%=szCamp %></select></td>
                </tr>
                <tr>
                    <th>�����Ž�:</th>
                    <td>
                        <select id="dwRoomID" name="dwRoomID">
                        <%=szRoomList %>
                        </select>
                    </td>
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
            $(document).tooltip();
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDevKind($("#szKindName"),2,$("#dwKindID"),1,false);
        setTimeout(function () {           
        }, 1);
    });
    </script>
</asp:Content>
