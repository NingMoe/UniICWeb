<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomResvStateCalc.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">


    <form id="formAdvOpts" runat="server">
                        <script src="<%=MyVPath %>themes/TeacherResvState/TeacherResvState.js"></script>
        <script type="text/javascript" src="<%=this.ResolveClientUrl("~/ClientWeb/") %>fm/uni.lib.js"></script>
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.sch.css" rel='stylesheet' />
<link href="<%=MyVPath %>themes/TeacherResvState/TeacherResvState.css" rel="stylesheet" />

            <h2 style="margin-top:10px;font-weight:bold"><%=szDevNameURL %>排课情况</h2> 
		<input type="hidden" id="dwStartDate" name="dwStartDate" />
        <input type="hidden" id="dwEndDate" name="dwEndDate" />
         <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="roomresvstate.aspx" id="devList"><%=szDevNameURL %>排课情况</a>
                <a href="roomresvstateCalc.aspx" id="devUseRec">周安排表</a>
            </div>
    
    </div>
        <INPUT type="hidden" id="testplanid" name="testplanid" />	
        <div style="margin-top:30px;width:99%;">  
        </div>
        <div>
            <input type="button" value="打印" id="print" />
        </div>
        <div class="content">
            <div style="width:99%">
            <div style="margin-top:2px;text-align:center; MARGIN-RIGHT: auto; ">
          <div id="calendar" style="overflow-y:hidden;overflow-x:hidden;zoom:99%"></div>
                </div>
                </div>
        </div>
        <script type="text/javascript">
            $(function () {
                var tabl = $(".UniTab").UniTab();
                $("#print").button().click(function () {
                    $("#calendar").jqprint();
                });
                var vStartDate ="<%=szStartDate%>";
                var vEndDate ="<%=szEndDate%>";
                    var cld = $("#calendar").uniCalendar({
                        mode: "m",
                        modes: "m",
                        style: "cld",
                        width: 1100,
                        cellHeight: 45,
                        borderWidth: 1,
                        relative: true,
                        schedule: true,
                        secnum: 8,
                        dateStart: vStartDate,
                        dateEnd: vEndDate,
                        evFinishDraw: function (dt, idate, opt, iqz) {
                        }
                    });
               // }, 100);
                //初始化数据
                $.get(
                       "../data/searchTestItemWeek.aspx",
                       {},
                       function (data) {
                           debugger;
                           var vData = eval(data);
                           callback(vData, cld);

                       }
                     );
               
            });
            function callback(list, cld) {
                var list = $(list);
                list.each(function () {
                    var ltch = this.ltch;
                    var start = parseInt(ltch / 100),
                    end = parseInt(start / 100) * 100 + (ltch % 100);
                    this.start = start;
                    this.end = end;
                });
                cld.uploadCld(null, {
                    plans: list
                });
            }


            
        </script>
        <style>
          
        </style>

    </form>
</asp:Content>
