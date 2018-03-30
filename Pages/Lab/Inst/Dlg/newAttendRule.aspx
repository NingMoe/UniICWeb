<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="newAttendRule.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwAttendID" name="dwAttendID" type="hidden" />
            <table>

                <tr>
                    <th>名称：</th>
                    <td colspan="3">
                        <input id="szAttendName" name="szAttendName" class="validate[required]" /></td>
                </tr>   
                     <tr>
                    <th>开始日期：</th>
                    <td>
                        <input id="dwStartDate" name="dwStartDate" /></td>
                           <th>结束日期：</th>
                    <td>
                        <input id="dwEndDate" name="dwEndDate" /></td>
                </tr>  
                <tr>
                    <th>开放规则：</th>
                    <td colspan="3">
                        <select class="opentime" id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=szOpenRule %>
                        </select>
                    </td>
                
                </tr>
                <!--   
                <tr>
                    <th>考勤方式：</th>
                    <td colspan="3">
                            <label><input class="enum" value="1" type="radio" name="AttendKind" checked="checked" />时间点考勤</label>
                            <label><input class="enum" value="2" type="radio" name="AttendKind" />时长考勤</label>
                    </td>
                </tr>
             -->
              <tr>
                  <td colspan="4">
                      <div class="divTimeIn" style="text-align:left">
                      <table>
                             <tr>
                    <th>最早进入时间：</th>
                    <td>
                        <select id="dwEarlyInTimeH" name="dwEarlyInTimeH"><%=szHour %></select>时
                        <select id="dwEarlyInTimeM" name="dwEarlyInTimeM"><%=szMin %></select>分
                    </td>
                    <th>最晚进入时间：</th>
                    <td>
                         <select id="dwLateInTimeH" name="dwLateInTimeH"><%=szHour %></select>时
                        <select id="dwLateInTimeM" name="dwLateInTimeM"><%=szMin %></select>分
                    </td>
                </tr> 
                           <tr>
                    <th>最早离开时间：</th>
                    <td>
                         <select id="dwEarlyOutTimeH" name="dwEarlyOutTimeH"><%=szHour %></select>时
                        <select id="dwEarlyOutTimeM" name="dwEarlyOutTimeM"><%=szMin %></select>分
                    </td>
                    <th>最晚离开时间：</th>
                    <td>
                        <select id="dwLateOutTimeH" name="dwLateOutTimeH"><%=szHour %></select>时
                        <select id="dwLateOutTimeM" name="dwLateOutTimeM"><%=szMin %></select>分
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
       <th style="width:120px"> 最少停留时间(分钟)：</th>
                    <td colspan="3"><input type="text" name="dwMinStayTime" id="dwMinStayTime"/></td>
        </tr>
</table>

                           </div>
                   </td>
                    
                     
                </tr> 
                      
               <tr>
                   <th>考勤房间：</th>
                   <td colspan="3" style="width:200px;"><%=szRoomList %></td>
               </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
