<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="WSetDevice.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwDevID" name="dwDevID" type="hidden" />
            <input id="dwKindID" name="dwKindID" type="hidden" />
            <input id="dwRoomID" name="dwRoomID" type="hidden" />
            <input id="kindProp" name="kindProp" type="hidden" value="<%=sz_Share %>" />
            <table>
                <tr>
                    <th>编号：</th>
                    <td>
                        <input id="szAssertSN" name="szAssertSN" /></td>
                    <th>名称：</th>
                    <td>
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>

                <tr>
                 
                    <th>容量：</th>
                    <td>
                        <input type="text" id="dwMaxUsers" name="dwMaxUsers" /></td>
                      <th>类型：</th>
                    <td>
                        <select id="dwClassID" name="dwClassID"><%=sz_DevCls %></select>
                       </td>
                </tr>      
                 <tr>
                    <th>所属楼宇：</th>
                    <td>
                        <select id="dwBuildingID" name="dwBuildingID"><%=sz_building %></select>
                       </td>
                    <th>物管部门：</th>
                    <td>
                        <select id="dwLabID" name="dwLabID"><%=sz_Lab %></select>
                        </td>
                </tr>       
                       <tr>
                </tr> 
                <tr>
                    <td colspan="4">
                        <label><input id="isOutDoor" name="isOutDoor" class="enum" type="checkbox" value="1" /> 室外预约模式</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div id="divNum">
                            容纳次数:<input type="text" name="num" id="num" value="0" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="btnRow" colspan="4">
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
        <%if (bSet)
          {%>
           <%}%>
   
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);
        if ($("#kindProp").val() == "0") {
            $("#divNum").hide();
        }
        $("#isOutDoor").click(function () {
            var vThis = $(this);
            if (vThis.prop("checked") == true) {
                $("#divNum").show();
            }
            else {
                $("#divNum").hide();
                
            }
        });
        setTimeout(function () {
            
        }, 1);
    });
    </script>
</asp:Content>
