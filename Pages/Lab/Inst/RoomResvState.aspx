<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="RoomResvState.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=szDevNameURL %>排课情况</h2> 
		
         <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="roomresvstate.aspx" id="devList"><%=szDevNameURL %>排课情况</a>
                <a href="roomresvstateCalc.aspx" id="devUseRec">周安排表</a>
            </div>
    
    </div>
        <INPUT type="hidden" id="testplanid" name="testplanid" />	
        <div style="margin-top:30px;width:99%;">  
        </div>
      
        <div class="content">
            <div style="width:99%">
            <div style="margin-top:10px;text-align:center; MARGIN-RIGHT: auto; MARGIN-LEFT: 50px; ">
          <div id="roomResvState" style="height:450px;overflow-y:scroll;overflow-x:hidden;"></div>
                </div>
                </div>
        </div>
        <script type="text/javascript">
            $(function () {
                var vResvForm="resvForm";
                var vModule= <%=ConfigConst.GCscheduleMode%>;
                var TeachResvMode=<%=ConfigConst.GCTeacResvMode%>;
                if(vModule==4&&TeachResvMode==1)
                {
                    vResvForm="resvForm2";//计量专用预约窗口
                }
                if(vModule==4&&TeachResvMode==0)
                {
                    vResvForm="resvForm3";//普通实验计划与项目分离窗口
                }
                var tabl= $(".UniTab").UniTab();
                $("#roomResvState").TeachingResvState({
                    "url": "../data/roomresvstate.aspx",
                    "urlWeek": "../data/getteachweekordate.aspx",
                    "devClassKind": <%=uClassKind%>-1,
                    "purpose": 11,
                    "selectDate":<%=selectDate%>
                    });
                var vDiv = $("#roomResvState");
                vDiv.on('click', ".aNewResv",function (event) {
                        var vA = event.target;
                        var date = $(vA).attr("data-resvDate");
                        var devid = $(vA).data("devid");
                        var sec = $(vA).data("sec");
						var testplanid=$("#testplanid").val();
						var vWeek=$("#resvTeachselectWeek").val();
                        $.lhdialog({
                            title: '预约',
                            width: '750px',
                            height: '400px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:Dlg/'+vResvForm+'.aspx?op=set&date=' + date + '&devid=' + devid + '&sec=' + sec+'&testplanid='+testplanid+'&vWeek='+vWeek
                        });
                    });
            });
            
        </script>
        <style>
          
        </style>
            <script src="<%=MyVPath %>themes/TeacherResvState/TeacherResvState.js"></script>
<link href="<%=MyVPath %>themes/TeacherResvState/TeacherResvState.css" rel="stylesheet" />
    </form>
</asp:Content>
