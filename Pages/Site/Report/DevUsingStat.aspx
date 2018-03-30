<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUsingStat.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top:5px; margin-bottom: 10px; font-weight: bold">ʹ����ͳ��</h2>
        <input type="hidden" value="2" name="dwPurpose" />
           <input type="hidden" id="roomid" name="roomid" />
        <div>
               <div id="divsearch" class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:99%">
                        <%if(ConfigConst.GCICTypeMode==1) { %>
                         <tr>
                            <th class="thHead"></th>
                            <td class="tdHead" colspan="3" style="text-align:center;height:35px">
                            <select id="dwYearTerm" name="dwYearTerm">
                                <%=m_TermList %>
                            </select>    
                            </td>
                            
                           
                        </tr>

                     
                        <%} else {%>
                           <tr>
                            <th class="thHead">��ʼ����:</th>
                            <td class="tdHead">
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th class="thHead">��������:</th>
                            <td class="tdHead">
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                               <!--
                           <th>
                               ����
                           </th>
                               <td>
                                   <select id="szRoom" name="szRoom">
                                       <%=sz_Room %>
                                   </select>
                               </td>
                               -->
                        </tr>
                        <%} %>
                        <!--
                         <tr>
                            <th class="thHead">����:</th>
                            <td class="tdHead">
                            <select id="dwClassKind" name="dwClassKind">
                                <option value="1">���޼�</option>
                                <option value="8">��λ</option>
                                <option value="2">����������</option>
                                </select> 
                            </td>
                             </tr>
                        -->
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
                             <!--   <input type="button" id="btnExport" value="����" />-->
                                
                            </td>
                        </tr>
                    </table>
            
            </div>

        </div>
        <div style="margin-top: 10px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>����</th>    
                        <th><%=ConfigConst.GCClassName%></th>       
                        <th>����<%=ConfigConst.GCLabName %></th>
                         <th name="dwTotalUseTime">ʹ����ʱ��</th>
                         <th name="dwPIDNum">ʹ������</th>
                        <th name="dwUseTimes">ʹ���˴���</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />

        </div>
        <script type="text/javascript">
            $(function () {
                //$(".ListTbl").UniTable();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
            });
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


            $(".ListTbl").UniTable();
            $("#btnOK,#btnExport").button();
            $("#btnExport").click(function () {
                var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                $.lhdialog({
                    title: '����',
                    width: '200px',
                    height: '50px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/DevUsingStatExport.aspx?dwEndDate=' + dwEndDate + '&dwStartDate=' + dwStartDate
                });
            });
        </script>
        <style>
            .thHead
            {
                background: #e5f1f4;
                text-align: right;
            }

            .tdHead
            {
                text-align: left;
            }

            td input
            {
                margin-left: 10px;
            }
        </style>
    </form>
</asp:Content>
