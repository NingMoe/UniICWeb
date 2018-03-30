<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="GetYardResvCheck.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
        <input id="dwLabID" name="dwLabID" type="hidden" />
        <input type="hidden" name="dwApplyID" id="dwApplyID" />
          <table class="DlgListTbl">
           <tr>
                    <th>����������:</th>
                    <td>
                        <div id="szTrueName"></div>
                    </td>
                    <th>���������ڲ���:</th>
                    <td>
                        <div id="szDeptName"></div>
                    </td>
                </tr>
                <tr>
                    <th>�ֻ�:</th>
                    <td>
                        <div id="szHandPhone"></div>
                    </td>
                    <th>�ʼ�:</th>
                    <td>
                        <div id="szEmail"></div>
                    </td>
                </tr>
                <tr>
                    <th>������Դ:</th>
                    <td>
                        <div id="szDevName"><%=szResvDevName %></div>
                    </td>
                    <th>��Դ���ڲ���:</th>
                    <td>
                        <div id=""><%=szResvDevDept %></div>
                    </td>
                </tr>
               <tr>

                    <th>Ԥ���μ�����:</th>
                    <td>
                        <div><%=szPeople %></div>
                    </td>
                    <th>�����:</th>
                    <td>
                        <div><%=szActivity %></div>
                    </td>
                    <!--
                    <th>�Ƿ�����:</th>
                    <td>
                        <div id="Div2"><%=szNeedCameor %></div>
                    </td>
                    -->
                </tr>
                <!--
                <tr>

                    <th>���ܲ���:</th>
                    <td>
                         <%if (szDirectors == "")
                           {%>

                        <label><input type="checkbox" name="dwDirectors" value="1024" class="enum" />��ί</label>
                         <label><input type="checkbox" name="dwDirectors" value="2048" class="enum" />ѧ����</label>

                        <%} else {%>
                        <%=szDirectors %>
                         <%}%>

                      </td>
                    <th>��������:</th>
                    <td>
                           <%if (szSecurityLevel == "")
                             {%>
                        <select id="dwSecurityLevel" name="dwSecurityLevel">
                            <option value="1">����Ҫ����</option>
                            <option value="6">��Ҫ����</option>
                        </select>
                        <%} else {%>
                        <%=szSecurityLevel %>
                         <%}%>
                       

                    </td>
                </tr>
                -->
                 <tr>
                   
                      <th>����ʱ��:</th>
                    <td>
                        <div id="dwApplyUseTime"><%=szResvTime %></div>
                    </td>
                    <th>�����:</th>
                    <td>
                        <div id="szApplyInfo"><%=szApplyName %></div>
                    </td>
                </tr>
              
                <tr>
                   
                      <th>�����:</th>
                    <td>
                        <div id="szOrganization"><%=szOrganization %></div>
                    </td>
                    <th>������:</th>
                    <td>
                        <div id="szOrganiger"><%=szOrganiger %></div>
                    </td>
                </tr>
                <tr>
                    <th>�����:</th>
                    <td colspan="3">
                        <div><%=szMemo %></div>
                    </td>
                </tr>
            <tr><td colspan="4" style="text-align:center">  <input type="button" id="btnClose" value="�ر�" style="width:80px" /></td></tr>
        </table>
       
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       
    </style>
<script language="javascript" type="text/javascript" > 
    $(function () {
      
        <%if(bSet){%>
        $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
        $("#dwOwnerDept").change(function () {
            $("#szDeptName").val($(this).find("option:selected").text());
        });
        $("#btnClose").button().click(Dlg_Cancel);
       
        $("#btnNOOK").click(function () {
            var szCheckInfo=$("#szCheckInfo").val();
            if(szCheckInfo=="")
            {
                return;
            }
            var id = $("#dwApplyID").val();
            var vApplyAgain=$("#applayAggin").val();
            $.get(
                     "../../ajaxpage/YearResvCheck.aspx",
                     { szCheckInfo: szCheckInfo, id: id, vApplyAgain: vApplyAgain },
                     function (data) {
                         if (data == "success") {
                             MessageBox("��˲�ͨ��", "��ʾ", 3, function () { Dlg_OK() });
                         }
                         else {
                             MessageBox("���ʧ��" + data, "��ʾ", 3, function () { Dlg_OK() });
                         }

                     }
                   );

        });
        $("#Cancel").button();
        $("#szManName2").autocomplete({
            source: "../../data/searchAccount.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szManName").val(ui.item.label);
                        $("#szManName2").val(ui.item.label);
                        $("#dwManagerID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwManagerID").val("");
                    $("#szManName").val("");
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($("#dwManagerID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szManName").val());
            }
        });

        $("#szAttendantName2").autocomplete({
            source: "../../data/searchAccount.aspx",
            select: function (event, ui) {
                if (ui.item) {
                    if (ui.item.id && ui.item.id != "") {
                        $("#szAttendantName").val(ui.item.label);
                        $("#szAttendantName2").val(ui.item.label);
                        $("#dwAttendantID").val(ui.item.id);
                    }
                }
                return false;
            },
            response: function (event, ui) {
                if (ui.content.length == 0) {
                    $("#dwAttendantID").val("");
                    $("#szAttendantName").val("");
                    ui.content.push({ label: " δ�ҵ������� " });
                }
            }
        }).blur(function () {
            if ($("#dwAttendantID").val() == "") {
                $(this).val("");
            } else {
                $(this).val($("#szAttendantName").val());
            }
        });

        setTimeout(function () {
            if ($("#dwManagerID").val() == "") {
                $("#szManName2").val("");
            } else {
                $("#szManName2").val($("#szManName").val());
            }

            if ($("#dwAttendantID").val() == "") {
                $("#szAttendantName2").val("");
            } else {
                $("#szAttendantName2").val($("#szAttendantName").val());
            }
        }, 1);
    });
</script>
</asp:Content>
