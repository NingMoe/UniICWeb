<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="YardResvCheckbak.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
         <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <input id="dwActivityLevel" name="dwActivityLevel" type="hidden" value="10" />
            <input type="hidden" name="dwCheckID" id="dwCheckID" />
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
                    <th>���������:</th>
                    <td>
                        <%if (szDevList == "")
                          {%>
                        <div id="szDevName"><%=szResvDevName %></div>
                        <%}
                          else
                          { %>
                        <select id="devID" name="devID">
                            <%=szDevList %>
                        </select>
                        <%} %>
                    </td>
                    <th>��Դ���ڲ���:</th>
                    <td>
                        <div id=""><%=szResvDevDept %></div>
                    </td>
                </tr>
                 <tr>
                      <th>��������:</th>
                    <td>
                        <div id="szApplyInfo"><%=szApplyName %></div>
                    </td>
            
               <th>����
                    </th>
                    <td colspa="3">
                        <input type="checkbox" id="ser" name="ser">ʹ�ö�ý��
                                <input type="checkbox" id="Checkbox1" name="ser">�������Ԥװ
                                <input type="checkbox" id="Checkbox2" name="ser">��Ҫ����                         
                    </td>
                      
                 
                </tr>
                <tr>
                    <th>ʹ������
                    </th>
                    <td>
                        <select name="mb_num" style="width:100px" >
                            <option value="0-50">50������</option>
                            <option value="50-100">50��-100��</option>
                            <option value="100-100000">100������</option>
                        </select>
                    </td>
                    <th>ʹ�ö���
                    </th>
                    <td>
                        <input type="text" style="width:200px" />
                    </td>
                </tr>
                <tr>
                       <th>������:</th>
                    <td>
                        <div id="szOrganiger"><%=szOrganiger %></div>
                    </td>
                     <th>�����˱�������:</th>
                    <td>
                        <div id="Div1">�����˱������Ͻ�����Ϣ</div>
                    </td>
                </tr>
                <tr>
                    <th>�����         
                    </th>
                    <td>
                          <select style="width:150px">
                                    <option>���Ϳ���</option>
                                    <option>��ҵ�</option>
                                    <option>��������</option>
                                    <option>����</option>
                                    <option>����</option>
                                    <option>���Ż</option>
                                    <option>�����</option>
                                    <option>��ѵ</option>
                                    <option>����</option>
                                </selec>
                           </td> 
                    <td> <input type="checkbox" id="Checkbox11" name="ser" checked="checked">���ӵ���</td>
                    <td>   <input type="checkbox" id="Checkbox10" name="ser" />Ӫ���</td>
                </tr>
              
                    <!--
                <tr>

                    <th>Ԥ���μ�����:</th>
                    <td>
                        <div><%=szPeople %></div>
                    </td>
                    <th>�����:</th>
                    <td>
                        <div><%=szActivity %></div>
                    </td>
                    
                    <th>�Ƿ�����:</th>
                    <td>
                        <div id="Div2"><%=szNeedCameor %></div>
                    </td>
                   
                </tr> 
                        -->
                <!--
                <tr>

                    <th>���ܲ���:</th>
                    <td>
                         <%if (szDirectors == "")
                           {%>

                        <label><input type="checkbox" name="dwDirectors" value="1024" class="enum" />��ί</label>
                         <label><input type="checkbox" name="dwDirectors" value="2048" class="enum" />ѧ����</label>

                        <%}
                           else
                           {%>
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
                        <%}
                             else
                             {%>
                        <%=szSecurityLevel %>
                         <%}%>
                       

                    </td>
                </tr>
                -->
                <tr>

                    <th>����ʱ��:</th>
                    <td colspan="3">
                        <div id="dwApplyUseTime"><%=szResvTime %></div>
                    </td>
                  
  </tr>
               
                <tr>
                    <th>����������:</th>
                    <td colspan="3">
                        <div><%=szMemo %></div>
                    </td>
                </tr>
                 <tr>
                    <th>����˵��:</th>
                    <td colspan="3">
                      
                    </td>
                </tr>
             <tr>
                    <th>����������</th>
                    <td></td>
                    <th>������</th>
                    <td><input type="text" style="width:150px" /></td>
                    </tr>
             
                <tr>
                    <td colspan="4" style="text-align: center">
                        <%//if (szFileName != "")
                          { %>
                        <a style="color: blue" target="_blank" href="<%=szFileName %>">����������뱨��</a>
                        <%} %>
                    </td>
                </tr>
             
                <tr>
                    <td colspan="4" style="text-align: center">
                        <button type="submit" id="OK">���ͨ��</button>
                        <button type="button" id="Cancel">��˲�ͨ��</button></td>
                </tr>
            </table>
            <div id="divNoOK">
                <table>
                    <tr>
                        <td>��������
                        <input type="text" name="szCheckInfo" id="szCheckInfo" title="��˲�ͨ����������ԭ��" />

                            <input type="button" id="btnNOOK" value="ȷ����ͨ��" style="width: 90px" />
                            <input type="button" id="btnClose" value="�ر�" style="width: 80px" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .DlgListTbl tr {
            height: 40px;
            border: 1px solid #777777;
        }
       .formtable input, select, .input {
width: 10px;
}
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
            $("#Cancel").click(function () {
                $("#divNoOK").show();
            });
            $("#divNoOK").hide();
            $("#OK").button();
            $("#btnClose").button().click(Dlg_Cancel);
            $("#btnNOOK").button();
            $("#btnNOOK").click(function () {
                var szCheckInfo = $("#szCheckInfo").val();
                if (szCheckInfo == "") {
                    return;
                }
                var id = $("#dwCheckID").val();
                var vApplyAgain = "1";
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
