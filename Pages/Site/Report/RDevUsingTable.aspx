<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RDevUsingTable.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="roomid" name="roomid" />
        <h2 style="margin-top: 10px; margin-bottom: 10px; font-weight: bold">ʹ����ͳ��ͼ</h2>
       <div>
               <div id="divsearch" class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:99%">
                           <tr>
                            <th class="thHead">��ʼ����:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th class="thHead">��������:</th>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                           <th class="thHead">У��:</th>
                   <td>
                        <select class="opt" id="szCampusIDs" name="szCampusIDs" style="width:auto">
                    <%=szCamp %>
                </select></td>
                   <th class="thHead">¥��:</th>
                   <td>
                         <select class="opt" id="szBuildingIDs" name="szBuildingIDs" style="width:auto">
                    <%=szBuilding %>
                </select>
                   </td>
                            </tr>
                       <tr>
                             <th class="thHead">��Դ����:</th>
                        
                        <td colspan="3"><input type="text" name="szSearchKey" id="szSearchKey" /></td>

                        </tr>
                        <tr>
                             <td class="tdHead" colspan="4" style="text-align:center">
                                <input type="submit" id="btnOK" value="��ѯ" />
                             
                                
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

     <div id="szResv" class="LineStat" data-color="1" style="min-width:0px" data-unit="����" data-name="ʹ�ô���">
                   <h1><span></span><strong>ʹ���豸</strong><strong>�����豸</strong></h1>
         <%=szResvRate %>
                </div>
        <script type="text/javascript">
            $(function () {
                $('#btnOK').button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                var vCampuid = $("#szCampusIDs").val();
                var vBuidlingID = $("#szBuildingIDs").val();
                $("#szSearchKey").click(function () {
                    debugger;
                    $("#szSearchKey").autocomplete({
                        source: "../data/searchroom.aspx?campuid=" + vCampuid + '&buildingid=' + vBuidlingID,
                        minLength: 0,
                        select: function (event, ui) {
                            debugger;
                            if (ui.item) {
                                if (ui.item.id && ui.item.id != "") {

                                    $("#szSearchKey").val(ui.item.label);
                                    $("#roomid").val(ui.item.id);
                                }
                            }
                            return false;
                        },
                        response: function (event, ui) {
                            if (ui.content.length == 0) {
                                ui.content.push({ label: " δ�ҵ������� " });
                            }
                        }
                    }).blur(function () {
                        if ($(this).val() == "") {
                            $("#roomid").val("");

                        } else {

                        }
                    }).click(function () {
                        $("#szSearchKey").autocomplete("search", "");
                    });
                });
                $("#szCampusIDs").change(function () {
                    var campidV = $("#szCampusIDs").val();
                    vCampuid = campidV;
                    $.get(
               "../data/searchBuilding.aspx",
               { campid: campidV },
               function (data) {

                   var vCamp = eval(data);
                   $('#szBuildingIDs').empty();
                   for (var i = 0; i < vCamp.length; i++) {
                       $('#szBuildingIDs').append($("<option value='" + vCamp[i].id + "'>" + vCamp[i].label + "</option>"));
                   }
               }
               );
                });
                $("#szBuildingIDs").change(function () {
                    vBuidlingID = $(this).val();
                });
           });
		</script>
        <style>
            .tb_info table           
            .thHead
            {
                width: 80px;
                text-align: center;
            }
            .context2 input
            {
                margin-right: 20px;
            }

            .context input
            {
                margin-left: 15px;
            }

            .context select
            {
                margin-left: 15px;
            }
        </style>
    </form>
</asp:Content>

