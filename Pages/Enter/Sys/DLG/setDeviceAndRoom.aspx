<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="setDeviceAndRoom.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwDevID" />        
        <input type="hidden" id="dwRoomID" name="dwRoomID" />        
        <input type="hidden" name="dwCtrlMode" id="dwCtrlMode" value="1" />
        <input type="hidden" name="dwKindID" id="dwKindID" />
        <div class="formtable">
            <table>
                <tr>
                    <th><%=ConfigConst.GCSysKindRoom %>编号(*)：</th>
                    <td> <input id="szRoomNo" name="szRoomNo" class="validate[required]" title="管理员请不要修改" /></td>
               
                    <th><%=ConfigConst.GCSysKindRoom %>名称(*)：</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>               
                <tr>
                    <th>所属<%=ConfigConst.GCLabName %>：</th>
                    <td>
                        <select id="dwLabID" name="dwLabID">
                            <%=m_szLab %>
                        </select></td>
                           <th>所属类型：</th>
                    <td>
                       <input type="text" id="szKindName" name="szKindName" /></td>       
                </tr>              
                <tr>
                  <th>开放规则：</th>
                    <td>
                        <select id="dwOpenRuleSN" name="dwOpenRuleSN">
                            <%=m_szOpenRule %>
                        </select></td>
              
                     <th>监控方式：</th> 
                   <td> <%=m_szRoomMode %></td>
                </tr>
                 <%if((ConfigConst.GCSysKind & 16) > 0) {%>
             
                <tr>
                  <th>是否支持活动安排：</th>
                    <td colspan="3"><label><input class="enum" type="checkbox" id="dwProperty2" name="dwProperty2" value="65536">是</label></td>
                </tr>
                <%} %>

                  
                  <tr>
                     <th>管理员：</th>
                    <td><select id="dwAttendantID" name="dwAttendantID"><%=m_szManager %></select></td>
                <th>联系方式:</th>
                    <td>
                       <input type="text" id="szAttendantTel" name="szAttendantTel" /></td>     
                </tr>
                 <tr>
                     <th>
                         硬件配置
                     </th>
                        <td>
                       <input type="text" id="szExtInfo" name="szExtInfo" /></td>   
                     <th></th> 
                    <td colspan="" style="text-align:left"><label><input class="enum" type="checkbox" id="chkopen" name="chkopen" value="1" >不对外开放</label></td>
                </tr>
                <tr>
                    <th>描述信息:</th>
                    <td colspan="3">
                       <input type="text" id="szDevURL" name="szDevURL" /></td>     
                </tr>
                <tr>
                    <td colspan="4" class="tblBtn">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
