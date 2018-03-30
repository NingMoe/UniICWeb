<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveYard.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>ԤԼ״��</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                 <a href="reserveYard.aspx" id="rtreserve">ԤԼ״��</a>
                 <a href="resevdreserve.aspx" id="resevdreserve">����Առ��</a>
            </div>
            </div>

        </div>

        <div>
            <input type="hidden" id="roomid" name="roomid" />
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                    <tr>
                        <th>��ʼ����:</th>
                        <td>
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                        <th>��������:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>ѧ����:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th>��Դ����:</th>
                        
                        <td colspan="3"><input type="text" name="szSearchKey" id="szSearchKey" /></td>

                    </tr>

                      <tr>
                 <th>У��:</th>
                   <td>
                        <select class="opt" id="szCampusIDs" name="szCampusIDs" style="width:auto">
                    <%=szCamp %>
                </select></td>
                   <th>¥��:</th>
                   <td>
                         <select class="opt" id="szBuildingIDs" name="szBuildingIDs" style="width:auto">
                    <%=szBuilding %>
                </select>
                   </td>
                            </tr>
                       
                    <tr>
                        <th>״̬</th>
                        <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">ȫ��</label>
                          <label><input class="enum"  value="1" type="radio" name="dwCheckStat">�����</label>
                            <label><input class="enum"  value="4" type="radio" name="dwCheckStat">��˲�ͨ��</label>
                            <label><input class="enum"  value="2" type="radio" name="dwCheckStat">���ͨ��</label>
                            <label><input class="enum" id="idtest" value="512" type="radio" name="dwCheckStat">��Ч��</label>
                        
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">�ѽ���</label>
                            <label><input class="enum" value="32" type="radio" name="dwCheckStat">��ȡ��</label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="��ѯ" style="height: 25px" /></th>
                    </tr>
                </table>

            </div>
        </div>
         <div style="margin-top:8px;width:99%;">  
             <a class="button" id="outplan">����</a>
        </div>
        <div class="content" style="margin-top: 10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ԤԼ��</th>
                        <th>����������</th>
                        <th><%=ConfigConst.GCDevName %>����</th>
                       
                        <th>״̬</th>
                        <th name="dwOccurTime">�ύʱ��</th>
                        <th name="dwBeginTime">����ʱ��</th>
                          <th>¥��</th>
                        <th>����˵��</th>
                        <th width="25px">����</th>
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

                var vCampuid = $("#szCampusIDs").val();
                var vBuidlingID = $("#szBuildingIDs").val();
                $("#szSearchKey").click(function () {
                    
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


                $("#btnClose").button().click(function () {
                    $("#divDigAll").dialog("close");
                });
               

                $("#outplan").click(function () {
                    var dwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var dwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var szBuildingIDs = $("#szBuildingIDs").val();
                    var dwCheckStat =  $("input[name='dwCheckStat']:checked").val();
                    var szGetKey = $("#szGetKey").val();
                    var szPid = $("#dwPID").val();
                    $.lhdialog({
                        title: '����',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/YardResvListExport.aspx?op=set&dwStartDate=' + dwStartDate + "&dwEndDate=" + dwEndDate + "&szBuildingIDs=" + szBuildingIDs + "&dwPID=" + szPid + '&dwCheckStat=' + dwCheckStat + '&szGetKey=' + szGetKey
                    });

                });
                $("#btnOK,#outplan").button();
                var tabl = $(".UniTab").UniTab();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                AutoDevice($("#devName"), 1, $("#szGetKey"), 1, null, null, null);
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delBtn" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="setResvOut" title="���ó�ΥԼ"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });

                $(".delBtn").click(function () {
                    var delBtn =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: 'ȡ��',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/delResv.aspx?id=' + delBtn
                    });
                });
                $(".setResvOut").click(function () {
                    var delBtn =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: 'ȡ��',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/resvOut.aspx?id=' + delBtn
                    });
                });
                $(".ListTbl").UniTable();

            });
        </script>
        <style>
            #tbSearch {
                border-width: 1px;
                border-color: #d1c1c1;
                cursor: hand;
            }

                #tbSearch td {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;
                }

            td input {
                margin-left: 20px;
            }
        </style>
    </form>
</asp:Content>
