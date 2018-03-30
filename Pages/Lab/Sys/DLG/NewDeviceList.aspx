<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewDeviceList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>        
          <input type="hidden" id="dwRoomID" name="dwRoomID" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
        <input type="hidden" id="dwKindID" name="dwKindID" />
        <input type="hidden" id="dwCtrlMode" name="dwCtrlMode" value="2" />
        <div class="formtable">
            <table>
                <tr>
                    <th>�������ǰ׺��</th>
                    <td> <input id="szPreName" name="szPreName" class="validate[required]" /></td>
             
                    <th>���������׺��ʼ��ţ�</th>
                    <td>
                        <input id="szNextName" name="szNextName" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
                <tr>
                    <th>��׺���ȣ�</th>
                    <td>
                        <input id="dwNextLen" name="dwNextLen"  class="validate[required,validate[custom[onlyNumber]]" style="width:100px"  />
                        <input type="checkbox" checked="checked" name="LenFix" id="LenFix"  value="true" style="width:20px" /><label style="width:80px">�����Ƿ�̶�</label>
                    </td>
               
                    <th>�½�������</th>
                    <td>
                         <input id="dwNum" name="dwNum"  class="validate[required,validate[custom[onlyNumber]]"  />
                       
                      </td>
                </tr> 
                 <tr>
                    <th>��ʼ��ţ�</th>
                    <td colspan="3">
                        <input id="dwStartNum" name="dwStartNum"  class="validate[required,validate[custom[onlyNumber]]" style="width:100px" title="���ڵ���Ĭ��ֵ"  />                        
                    </td>               
                </tr>                             
                <tr>
                      <td colspan="4">  <hr size="2" /></td>
                </tr>
                   <tr>
                    <th><%=ConfigConst.GCDevName %>���ǰ׺��</th>
                    <td> <input id="szPreDevName" name="szPreDevName" class="validate[required]" /></td>
             
                    <th><%=ConfigConst.GCDevName %>��ź�׺��ʼ��ţ�</th>
                    <td>
                        <input id="szNextDevName" name="szNextDevName" class="validate[required,validate[custom[onlyNumber]]" /></td>
                </tr>
                <tr>
                    <th>��׺���ȣ�</th>
                    <td>
                        <input id="dwNextDevLen" name="dwNextDevLen"  class="validate[required,validate[custom[onlyNumber]]" style="width:100px"  />
                        <input type="checkbox" name="DevLenFix" checked="checked" id="DevLenFix"  value="true" style="width:20px" /><label style="width:80px">�����Ƿ�̶�</label>
                    </td>
                </tr>
                     <tr>
                    <td colspan="4">  <hr size="2" /></td>
                </tr>                           
               <tr>                   
                    <th>��������</th>
                    <td>
                      <input type="text" name="szRoomName" id="szRoomName" class="validate[required]" /></td>
                     <th>����<%=ConfigConst.GCKindName %>��</th>
                    <td><input type="text" name="szKindName" id="szKindName" class="validate[required]" /></td>
                </tr>     
                <tr>    
                      <th>���ԣ�</th>
                    <td><%=m_szPorperty%></td>
                    <th></th>              
                    <td></td>
                </tr>
               
                <tr>
                      <td colspan="4">  <hr size="2" /></td>
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
        AutoDevKind($("#szKindName"), 2, $("#dwKindID"),null, false);
        AutoRoom($("#szRoomName"), 2, $("#dwRoomID"), null, $("#dwLabID"));
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
