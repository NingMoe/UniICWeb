<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveYard.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>预约状况</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                 <a href="reserveYard.aspx" id="rtreserve">预约状况</a>
                 <a href="resevdreserve.aspx" id="resevdreserve">管理员占用</a>
            </div>
            </div>

        </div>

        <div>
            <input type="hidden" id="roomid" name="roomid" />
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 70%">
                    <tr>
                        <th>开始日期:</th>
                        <td>
                            <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                        <th>结束日期:</th>
                        <td>
                            <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>学工号:</th>
                        <td>
                            <input type="text" name="dwPID" id="dwPID" /></td>
                        <th>资源名称:</th>
                        
                        <td colspan="3"><input type="text" name="szSearchKey" id="szSearchKey" /></td>

                    </tr>

                      <tr>
                 <th>校区:</th>
                   <td>
                        <select class="opt" id="szCampusIDs" name="szCampusIDs" style="width:auto">
                    <%=szCamp %>
                </select></td>
                   <th>楼宇:</th>
                   <td>
                         <select class="opt" id="szBuildingIDs" name="szBuildingIDs" style="width:auto">
                    <%=szBuilding %>
                </select>
                   </td>
                            </tr>
                       
                    <tr>
                        <th>状态</th>
                        <td colspan="3">
                            <label><input class="enum" value="0" type="radio" name="dwCheckStat" checked="checked">全部</label>
                          <label><input class="enum"  value="1" type="radio" name="dwCheckStat">待审核</label>
                            <label><input class="enum"  value="4" type="radio" name="dwCheckStat">审核不通过</label>
                            <label><input class="enum"  value="2" type="radio" name="dwCheckStat">审核通过</label>
                            <label><input class="enum" id="idtest" value="512" type="radio" name="dwCheckStat">生效中</label>
                        
                            <label><input class="enum" value="1073741824" type="radio" name="dwCheckStat">已结束</label>
                            <label><input class="enum" value="32" type="radio" name="dwCheckStat">已取消</label>
                        </td>
                    </tr>
                    <tr>
                        <th colspan="4">
                            <input type="submit" id="btnOK" value="查询" style="height: 25px" /></th>
                    </tr>
                </table>

            </div>
        </div>
         <div style="margin-top:8px;width:99%;">  
             <a class="button" id="outplan">导出</a>
        </div>
        <div class="content" style="margin-top: 10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>预约号</th>
                        <th>申请人姓名</th>
                        <th><%=ConfigConst.GCDevName %>名称</th>
                       
                        <th>状态</th>
                        <th name="dwOccurTime">提交时间</th>
                        <th name="dwBeginTime">申请时间</th>
                          <th>楼宇</th>
                        <th>申请说明</th>
                        <th width="25px">操作</th>
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
                                ui.content.push({ label: " 未找到配置项 " });
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
                        title: '导出',
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
                       <a class="delBtn" title="删除"><img src="../../../themes/iconpage/del.png""/></a>\
                       <a class="setResvOut" title="设置成违约"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });

                $(".delBtn").click(function () {
                    var delBtn =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '取消',
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
                        title: '取消',
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
