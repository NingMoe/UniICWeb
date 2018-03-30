<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Reservecalendar.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
          <div class="toolbar">
         
            <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="ReserveList.aspx">列表</a>
                     <a href="Reservecalendar.aspx">日历</a>
                    
                </div>

            </div>

        </div>

        <input type="hidden" id="roomid" name="roomid" />
       <div id="divsearch" class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:99%">   
                        <tr>
                 <th>校区:</th>
                   <td>
                        <select class="opt" id="szCampusIDs" name="szCampusIDs" style="width:auto">
                    <%=m_szCamp %>
                </select></td>
                   <th>楼宇:</th>
                   <td>
                         <select class="opt" id="szBuildingIDs" name="szBuildingIDs" style="width:auto">
                    <%=m_szBuilding %>
                </select>
                   </td>
                     <th>资源名称:</th>
                    <td><input type="text" name="szSearchKey" id="szSearchKey" /></td>

                    <td>
                        <input type="button" value="查询" id="search" />
                           <input type="button" value="打印" id="print" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divDigAll">
            <div id="divDig">
        </div>
        <div style="width:99%;height:30px;text-align:center"><input type="button" id="btnClose" value="关闭" />
        </div>
        </div>
        <div style="color:red">
            字体黑色表示当日审核通过
        </div>
       <div id='calendar'style="overflow-y:hidden;overflow-x:hidden;zoom:99%">

       </div>
        <script src="<%=MyVPath %>themes/fullcalendar-2.2.5/moment.min.js"></script>
        <script src="<%=MyVPath %>themes/fullcalendar-2.2.5/fullcalendar.min.js"></script>
        <script src="<%=MyVPath %>themes/js/jquery.PrintArea.js"></script>
        
        <script src="<%=MyVPath %>themes/fullcalendar-2.2.5/zh-cn.js"></script>
        <link href="<%=MyVPath %>themes/fullcalendar-2.2.5/fullcalendar.min.css" rel="stylesheet" />

        
        <script type="text/javascript">
            $(function () {
                var tabl = $(".UniTab").UniTab();
                $("#print").button().click(function () {
                    $("#calendar").printArea();
                });
                var vCampuid = $("#szCampusIDs").val();
                var vBuidlingID = $("#szBuildingIDs").val();
                $("#szSearchKey").click(function () { 
                $("#szSearchKey").autocomplete({
                    source: "../data/searchroom.aspx?campuid="+vCampuid+'&buildingid='+vBuidlingID,
                    minLength: 0,
                    select: function (event, ui) {
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
                $("#btnClose").button().click(function () {
                    $("#divDigAll").dialog("close");
                });
                $("#szBuildingIDs").change(function () {
                    vBuidlingID = $(this).val();
                });
                $("#search").button().click(function () {
                    $('#calendar').fullCalendar('destroy');
                    setCalu();
                });
                function ReplaceAll(str, sptr, sptr1) {
                    while (str.indexOf(sptr) >= 0) {
                        str = str.replace(sptr, sptr1);
                    }
                    return str;
                }
                $("#divDigAll").dialog({
                    autoOpen: false,
                    height: 250,
                    width: 450,
                    title: '查看详细信息',
                    modal: true,
                    show: {
                        effect: "blind",
                        duration: 200
                    },
                    hide: {
                        effect: "blind",
                        duration: 200
                    }
                });
                var myDate = new Date();
                var vMonth=myDate.getMonth() + 1;
                if ((vMonth) < 10)
                {
                    vMonth = "0" + vMonth;
                }
                setCalu();
                var sz = myDate.getFullYear() + '/' + vMonth + '/' + myDate.getDate();
                function setCalu() {
                  
                    var campidV = $("#szCampusIDs").val();
                    var buildingid = $("#szBuildingIDs").val();
                    var vRoomid = $("#roomid").val();

                    $('#calendar').fullCalendar({
                        header: {
                            left: 'prev,next today',
                            center: 'title',
                            right: 'month,basicWeek,basicDay'
                        },
                        height: 1800,
                        defaultView: 'basicWeek',
                        defaultDate: sz,
                        editable: true,
                        eventLimit: true, // allow "more" link when too many events
                        events: {
                            url: "../data/getYardResv.aspx?campuid=" + campidV + '&buildingid=' + buildingid + '&roomid=' + vRoomid,
                            error: function () {
                                $('#script-warning').show();
                            }
                        },
                        eventRender: function (event, element) {
                            var words = event.title.split(",");
                            var vdivDevName = "";
                            debugger;
                            //alert(words.length + words[0]);
                            if (words.length >= 5) {
                                if (words[4] == "true") {
                                    vdivDevName = "<div style='padding:5px;color:black;'>" + words[0] + "；" + words[1] + "<br />";
                                    vdivDevName += words[2] + "；" + words[3] + "</div>"
                                } else {
                                    vdivDevName = "<div style='padding:5px;'>" + words[0] + "；" + words[1] + "<br />";
                                    vdivDevName += words[2] + "；" + words[3] + "</div>"
                                }
                            }
                            else {
                                vdivDevName = "<div style='padding:5px;'>" + words[0] + "；" + words[1] + "<br />";
                                vdivDevName += words[2] + "；" + words[3] + "</div>"
                            }
                            vdivDevName = ReplaceAll(vdivDevName, 'undefined;', '');// vdivDevName.replace('undefined', '');
                            vdivDevName = ReplaceAll(vdivDevName, 'undefined', '');// vdivDevName.replace('undefined', '');
                            element.html(vdivDevName);
                        },
                        eventClick: function (event, element) {
                            debugger;
                            var id = event.id;
                            if (id == null || id==undefined||id=='0')
                            {
                                return;
                            }
                            var ActivityLevel = event.dwSecurityLevel;
                            var YardResvCheck = "YardResvCheckmeetcalget";
                            var bMoudel = false;
                            if ((ActivityLevel & 0x10000000) > 0) {
                                bMoudel = true;
                                YardResvCheck = "YardResvCheckcalget";
                            }
                            else if ((ActivityLevel & 0x20000000) > 0) {
                                bMoudel = false;
                                YardResvCheck = "YardResvCheckMeetcalget";
                            }
                            else if ((ActivityLevel & 0x800000) > 0) {
                                bMoudel = false;
                                YardResvCheck = "YardResvCheckSport";
                            }
                            else {
                                bMoudel = true;
                                YardResvCheck = "YardResvCheckcalget";
                            }
                            if (bMoudel) {
                                var checkIDs = event.id;
                                $.lhdialog({
                                    title: '查看详情',
                                    width: '800px',
                                    height: '600px',
                                    lock: true,
                                    data: Dlg_Callback,
                                    content: 'url:dlg/' + YardResvCheck + '.aspx?op=set&id=' + id
                                });

                            }
                            else {
                                $.lhdialog({
                                    title: '查看详情',
                                    width: '800px',
                                    height: '600px',
                                    lock: true,
                                    data: Dlg_Callback,
                                    content: 'url:dlg/' + YardResvCheck + '.aspx?op=set&id=' + id
                                });

                            }
                        }

                    });
                }
            
        });
        </script>


    </form>
</asp:Content>
