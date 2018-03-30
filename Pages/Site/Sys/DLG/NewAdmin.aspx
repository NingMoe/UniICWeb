<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewAdmin.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
    <input type="hidden" id="dwAccNo" name="dwAccNo" />    
    <input type="hidden" id="hiddenRoomID" name="hiddenRoomID" />       
     <!--   <input type="hidden" id="szLogonName" name="szLogonName" />   -->
    <input type="hidden" id="hidenManrole" name="hidenManrole" runat="server" />      
        <div class="formtable">
            <table>
                <tr>
                    <th>���ţ�</th>
                    <td style="vertical-align: top;" colspan="3">
                        <input id="szLogonName" name="szLogonName" class="validate[required]" /></td>
                </tr>
              <tr>
                    <th>����Ա���ͣ�</th>
                    <td>
                      <%=m_adminKind %></td>
                    <th>����Ա����</th>
                    <td>
                      <select id="dwManLevel" name="dwManLevel"><%=m_adminLevle %></select></td>
                </tr>
                <tr>
                    <th>�����ʼ���</th>
                    <td>
                        <input type="text" id="szEmail" name="szEmail" /></td>
                    <th>�绰��</th>
                    <td>
                        <input type="text" id="szTel" name="szTel" /></td>
                </tr>
                <tr>
                     <th>�ֻ���</th>
                    <td>
                        <input type="text" id="szHandPhone" name="szHandPhone" style="text-decoration:underline" class="validate[custom[handphone]" /></td>
                    <th>��ע��</th>
                    <td>
                        <input type="text" name="szMemo" id="szMemo" /></td>

                </tr>
                <!--
                   <tr>
                    <td colspan="4"><div id="checkLab">����<%=ConfigConst.GCLabName %>��<%=m_checkLab %></div></td>
                </tr> 
            <tr>
                  
                    <td colspan="4"><div id="radLab">����<%=ConfigConst.GCLabName %>��<%=m_szLab %></div></td>
                </tr> 
                <tr>
                    <td colspan="4"><div id="divRoom"><%=m_szRoom %></div></td>
                </tr>
                -->
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
        .formtable table th {
        width:80px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            var vManRole = 132097;
            SetManagerArea(vManRole);
            $("#dwManRole").change(function () {
                SetManagerArea($("#dwManRole").val());
            });
            $(".labClass").each(function () {
                $(this).hide();
                $("input[name='labList']:checked").each(function () {
                    var obj = $(this);
                    $("#divLab" + obj.val()).show();
                });
            });

            function SetManagerArea(vManRole) {
                vManRole = 524801;
                var vdivcheckLab = $("#checkLab");
                var vdivradLab = $("#radLab");
                var vdivRoom = $("#divRoom");
                if (vManRole == 132097) {
                    vdivcheckLab.hide();
                    vdivradLab.hide();
                    vdivRoom.hide();
                }
                else if (vManRole == 524801) {
                    vdivcheckLab.show();
                    vdivradLab.hide();
                    vdivRoom.hide();
                }
                else if (vManRole == 1049089) {
                    vdivradLab.show();
                    vdivcheckLab.hide();
                    vdivRoom.show();
                }
            }
            $("input[name='labList']").click(function () {
                var labid = $(this).val();
                $(".labClass").each(function () {
                    var obj = $(this);
                    if (obj.attr("id") == "divLab" + labid) {
                        obj.show();
                    }
                    else { obj.hide(); }
                });

            });

            setTimeout(function () {
                var dateNow = new Date();
                var Month = dateNow.getMonth() + 1;
                if (Month < 10) {
                    Month = "0" + Month;
                }
                var date = dateNow.getDate();
                if (date < 10) {
                    date = "0" + date;
                }
                var dateNowFor = dateNow.getFullYear() + "-" + Month + "-" + date;
                $("#dwEndDate").val(dateNowFor);
                $("#dwBeginDate").val(dateNowFor);
            }, 1);
            AutoUserByLogonname($("#szLogonName"), 2, $("#szHandPhone"), $("#szTel"), $("#szEmail"), $("#dwAccNo"));
            /*
            $("#szTrueName").on("autocompleteselect", function (event, ui) {
                setTimeout(function () {
                    $("#szTrueName").val(ui.item.szTrueName);
                    $("#szHandPhone").val(ui.item.szHandPhone);
                    $("#szTel").val(ui.item.szTel);
                    $("#szEmail").val(ui.item.szEmail);
                    $("#dwAccNo").val(ui.item.id);
                    $("#szLogonName").val(ui.item.szLogonName);
                }, 10);
            });
            */
        });
    </script>
</asp:Content>
