<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ReserveList.aspx.cs" Inherits="Sub_Lab" %>

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
                            <th>开始日期:</th>
                             <td>
                                 <input type="text" id="start" name="start" value="<%=szDateNow %>"/>
                             </td>
                            <th>结束日期:</th>
                            <td>
                                <input type="text" id="end" name="end" value="<%=szDateNow %>"/>
                            </td>                       
                        </tr>
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
                            </tr>
                        <tr>
                     <th>资源名称:</th>
                    <td colspan="3"><input type="text" name="szSearchKey" id="szSearchKey" /></td>
                            <tr>
                    <td colspan="4" style="text-align:center">
                        <input type="button" value="查询" id="search" />
                        <!--   <input type="button" value="打印" id="print" />-->
                    </td>
                </tr>
            </table>
        </div>
        <div id="divContanct" style="height:400px;overflow-y:scroll">
            <table class="ListTbl UniTable">
                <thead>
                    <tr>
                        <th>预约号</th>
                        <th>申请人姓名</th>
                        <th>申请房间</th>
                       <th>申请日期</th>
                        <th>申请时间</th>
                        <th>说明</th>
                        <th width="25px" class="tblOPTH">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    </tbody>
        </div>
        <script src="<%=MyVPath %>themes/fullcalendar-2.2.5/moment.min.js"></script>
 
        
        <script type="text/javascript">
            $(function () {
                var tabl = $(".UniTab").UniTab();
               $("#start,#end").datepicker();
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
                    
                     var campidV = $("#szCampusIDs").val();
                    var buildingid = $("#szBuildingIDs").val();
                    var vRoomid = $("#roomid").val();
                    var start=$("#start").val();
                    var end=$("#end").val();
                    $.get(
                    "../data/getYardResv.aspx?campuid=" + campidV + '&buildingid=' + buildingid + '&roomid=' + vRoomid+'&start='+start+'&end='+end,
                    function (data) {
                     
                        var vJson = eval(data);
                        vJson= SortData(vJson);
                        var vBody = $("#ListTbl");
                        vBody.empty();
                        for (var i = 0; i < vJson.length; i++) {
                            var vTemp = vJson[i];
                            var vInfo = vTemp.title.split(',');
                            var vDevName = vInfo[0];
                            var vResvTime = vInfo[1];
                            var vResvName = vInfo[2];
                            var vResvTile = vInfo[3];
                            var vResvDate = vTemp.start;
                            var vResvID = vTemp.id;
                            var dwSecurityLevel = vTemp.dwSecurityLevel;
                            //var vTR = $("<tr><td>'+vResvID+'</td><td>'+vResvName+'</td><td>'+vDevName+'</td><td>'+vResvDate+'</td><td>'+vResvTime+'</td><td>'+vResvTile+'</td><td><input type='button' value='查看' class='btnGet' /></td></tr>")
                            var vTR = $("<tr><td id='" + vResvID + "' dwSecurityLevel='" + dwSecurityLevel + "'>" + vResvID + "</td><td>" + vResvName + "</td><td>" + vDevName + "</td><td>" + vResvDate + "</td><td>" + vResvTime + "</td><td>" + vResvTile + "</td><td><input type='button' value='查看' style='width:50px' class='btnGet ui-button ui-widget ui-state-default ui-corner-all' /></td></tr>")
                            vBody.append(vTR);
                        }
                        
                    }
                  );


                });
                function SortData(vData) {

                    for (var i = 0; i < vData.length; i++) {
                        vData[i].start = ChangeStr2Date(vData[i].start);
                    }

                    vData = vData.sort(sortprice);
                    for (var i = 0; i < vData.length; i++) {
                        vData[i].start = vData[i].start.Format("yyyy-MM-dd");
                    }
                    return vData;
                }
                Date.prototype.Format = function (fmt) { //author: meizz 
                    var o = {
                        "M+": this.getMonth() + 1, //月份 
                        "d+": this.getDate(), //日 
                        "h+": this.getHours(), //小时 
                        "m+": this.getMinutes(), //分 
                        "s+": this.getSeconds(), //秒 
                        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
                        "S": this.getMilliseconds() //毫秒 
                    };
                    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
                    for (var k in o)
                        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                    return fmt;
                }
                function sortprice(a, b) {
                    return a.start - b.start
                }
                function ChangeStr2Date(str) {
                    return new Date(Date.parse(str.replace(/-/g, "/")));
                }
                $("#search").click();
               
                $('#divContanct').on('click', '.btnGet', function () {
                    debugger;
                    var vEvent = $(this).parent().parent().children().first();
                    var id = vEvent.attr("id");
                    if (id == null || id == undefined || id == '0') {
                        return;
                    }
                    var ActivityLevel = vEvent.attr("dwSecurityLevel");
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
                        var checkIDs = id;//event.id;
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
                });
                function ReplaceAll(str, sptr, sptr1) {
                    while (str.indexOf(sptr) >= 0) {
                        str = str.replace(sptr, sptr1);
                    }
                    return str;
                }
                
                var myDate = new Date();
                var vMonth=myDate.getMonth() + 1;
                if ((vMonth) < 10)
                {
                    vMonth = "0" + vMonth;
                }
               
            
        });
        </script>


    </form>
</asp:Content>
