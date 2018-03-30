<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDeviceSeatList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>        
          <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwCtrlMode" name="dwCtrlMode" value="0" />
        <div class="formtable">
            <table>
                <tr>
                    <th><%=ConfigConst.GCSysKindSeat %>名前缀：</th>
                    <td> <input id="szPreName" name="szPreName" class="validate[required]" /></td>
             
                    <th><%=ConfigConst.GCSysKindSeat %>名后缀起始编号：</th>
                    <td>
                        <input id="szNextName" name="szNextName" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
                <tr>
                    <th>后缀长度：</th>
                    <td>
                        <input id="dwNextLen" name="dwNextLen"  class="validate[required,validate[custom[onlyNumber]]" style="width:100px"  />
                        <input type="checkbox" name="LenFix" checked="checked" id="LenFix"  value="true" style="width:20px" /><label style="width:80px">长度是否固定</label>
                    </td>
               
                    <th>新建数量：</th>
                    <td>
                         <input id="dwNum" name="dwNum"  class="validate[required,validate[custom[onlyNumber]]"  />
                       
                      </td>
                </tr>    
                <tr>
                    <th>起始编号：</th>
                    <td colspan="3">
                        <input id="dwStartNum" name="dwStartNum"  class="validate[required,validate[custom[onlyNumber]]" style="width:100px"  />                        
                    </td>               
                </tr>                                 
                <tr>
                     <td colspan="4">  <hr size="2" /></td>
                </tr>
                                     
               <tr>                   
                    <th>所属区域：</th>
                    <td>
                      <input type="text" name="szRoomName" id="szRoomName" class="validate[required]" /></td>
                     <th>所属<%=ConfigConst.GCKindName %>：</th>
                    <td><input type="text" name="szKindName" id="szKindName" class="validate[required]" /></td>
                </tr>                  
                <tr>
                    <td colspan="4">  <hr size="2" /></td>
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
      
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
        <%if (bSet)
          {%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        AutoDevKind($("#szKindName"), 2, $("#dwKindID"), 8, false);
        AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), 8, $("#dwLabID"));
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        $("#dwLabID").change(function () {
            var dwLabID = $("#dwLabID").val();
            $.ajax({
                type: "post",
                url: "../../data/GetRoomList.aspx?type=Lab&szkey=" + dwLabID,
                dataType: "json",
                success: function (data) {
                    if (data != "") {
                        $("#dwRoomID").empty();
                        var szTemp = "";
                       
                        bIsSet = false;
                        $(data).each(function () {
                            var vData = $(this);
                            if (bIsSet == false) {
                                $.ajax({
                                    type: "post",
                                    url: "../../data/GetDevMaxSNByRoom.aspx?roomid=" + vData.attr('id'),
                                    dataType: "json",
                                    success: function (data) {
                                        if (data != "") {
                                            $("#dwStartNum").val(data);
                                        }
                                        else {
                                            MessageBox(data, "", 2);
                                        }

                                    }
                                });
                            }
                           bIsSet=true;
                            $("#dwRoomID").append("<option value=" + $(this).attr('id') + ">" + $(this).attr('name') + "</option>");
                        });


                    }
                    else {
                        MessageBox(data, "", 2);
                    }

                }
            });
        });
        $("#dwRoomID").change(function () {
            var dwRoomID = $("#dwRoomID").val();
            $.ajax({
                type: "post",
                url: "../../data/GetDevMaxSNByRoom.aspx?roomid=" + dwRoomID,
                dataType: "json",
                success: function (data) {
                    if (data != "") {
                        $("#dwStartNum").val(data);
                    }
                    else {
                        MessageBox(data, "", 2);
                    }

                }
            });
        });
        setTimeout(function () {
            
        }, 1);
    });
    </script>
</asp:Content>
