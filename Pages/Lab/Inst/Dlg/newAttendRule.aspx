<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="newAttendRule.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwAttendID" name="dwAttendID" type="hidden" />
            <table>

                <tr>
                    <th>���ƣ�</th>
                    <td colspan="3">
                        <input id="szAttendName" name="szAttendName" class="validate[required]" /></td>
                </tr>   
                     <tr>
                    <th>��ʼ���ڣ�</th>
                    <td>
                        <input id="dwStartDate" name="dwStartDate" /></td>
                           <th>�������ڣ�</th>
                    <td>
                        <input id="dwEndDate" name="dwEndDate" /></td>
                </tr>  
                <tr>
                    <th>���Ź���</th>
                    <td colspan="3">
                        <select class="opentime" id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=szOpenRule %>
                        </select>
                    </td>
                
                </tr>
                <!--   
                <tr>
                    <th>���ڷ�ʽ��</th>
                    <td colspan="3">
                            <label><input class="enum" value="1" type="radio" name="AttendKind" checked="checked" />ʱ��㿼��</label>
                            <label><input class="enum" value="2" type="radio" name="AttendKind" />ʱ������</label>
                    </td>
                </tr>
             -->
              <tr>
                  <td colspan="4">
                      <div class="divTimeIn" style="text-align:left">
                      <table>
                             <tr>
                    <th>�������ʱ�䣺</th>
                    <td>
                        <select id="dwEarlyInTimeH" name="dwEarlyInTimeH"><%=szHour %></select>ʱ
                        <select id="dwEarlyInTimeM" name="dwEarlyInTimeM"><%=szMin %></select>��
                    </td>
                    <th>�������ʱ�䣺</th>
                    <td>
                         <select id="dwLateInTimeH" name="dwLateInTimeH"><%=szHour %></select>ʱ
                        <select id="dwLateInTimeM" name="dwLateInTimeM"><%=szMin %></select>��
                    </td>
                </tr> 
                           <tr>
                    <th>�����뿪ʱ�䣺</th>
                    <td>
                         <select id="dwEarlyOutTimeH" name="dwEarlyOutTimeH"><%=szHour %></select>ʱ
                        <select id="dwEarlyOutTimeM" name="dwEarlyOutTimeM"><%=szMin %></select>��
                    </td>
                    <th>�����뿪ʱ�䣺</th>
                    <td>
                        <select id="dwLateOutTimeH" name="dwLateOutTimeH"><%=szHour %></select>ʱ
                        <select id="dwLateOutTimeM" name="dwLateOutTimeM"><%=szMin %></select>��
                    </td>
                </tr> 
                      </table>
                          </div>
                  </td>
              </tr>
                
                
                  <tr>
                   <td colspan="4">
                       <div id="kindStayTime" style="text-align:left">
<table>
    <tr>
       <th style="width:120px"> ����ͣ��ʱ��(����)��</th>
                    <td colspan="3"><input type="text" name="dwMinStayTime" id="dwMinStayTime"/></td>
        </tr>
</table>

                           </div>
                   </td>
                    
                     
                </tr> 
                      
               <tr>
                   <th>���ڷ��䣺</th>
                   <td colspan="3" style="width:200px;"><%=szRoomList %></td>
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
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[name='AttendKind']").click(function () {
                var chenked = $("input[name='AttendKind']:checked").val();
                if (chenked == "1") {
                    $("#kindStayTime").css("display", "none");
                    $(".divTimeIn").css("display", "block");
                }
                else {
                    $(".divTimeIn").css("display", "none");
                    $("#kindStayTime").css("display", "block");
                }
            });
            //$("#kindStayTime").css("display", "none");
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);
            $("select:not(.opentime)").css("width","70")
            $("#dwStartDate,#dwEndDate").datepicker({
                changeMonth: true,
                changeYear: true
            });
    });
    </script>
     <style>
              .ui-datepicker select.ui-datepicker-year { width: 43%;}
              .tb_infoInLine td input {
            width:120px;
            }
           
        </style>
</asp:Content>
