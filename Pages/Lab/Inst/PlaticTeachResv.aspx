<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PlaticTeachResv.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
          <h2 style="margin-top:10px;font-weight:bold">当前考勤情况</h2>
           <div class="tb_info">
            <div class="UniTab" id="tabl">
                <a href="PlaticTeachResv.aspx" id="PlaticTeachResv">当前考勤情况</a>
                <a href="PlaticTeachResvRec.aspx" id="PlaticTeachResvRec">考勤记录</a>
                <a href="PlaticCourseResvRec.aspx" id="PlaticCourseResvRec">课程考情统计表</a>
                <a href="PlaticLabResvRec.aspx" id="PlaticLabResvRec">实验室到课率统计</a>
            </div>
    
    </div>
         
         <div style="margin-top:30px;width:99%;">
             <div style="margin-top:15px;width:99%;">  
             <a class="button" id="outplan">导出</a>
        </div>
            <div style="text-align:center">
            <table class="ListTbl">
                <thead>
                    <tr>
                       <th>课程名</th>
                        <th>教师</th>
                        <th>房间</th>
                        <th>班级</th>
                        <th>应到人数</th>
                        <th>目前人数</th>
                        <th>上课时间</th>
                      <!--  <th>实验名称</th>-->
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
               </div>
        <script type="text/javascript">
           
            $(function () {
                $(".UniTab").UniTab();
                $("#outplan").button();
                $(".doorCar").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="远程开门"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="doorCardRec" title="刷卡记录"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".door").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="远程开门"><img src="../../../themes/iconpage/edit.png"/></a>\</div>');
                $(".Car").html('<div class="OPTDBtn">\
                            <a  class="openDoor" href="#" title="刷卡记录"><img src="../../../themes/icon_s/13.png"/></a>\</div>');
                $(".none").html('<div class="OPTDBtn">\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "defaultbg.png", borderWidth: 0, minWidth: "50", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });              
                $(".ListTbl").UniTable({});
            });
             $("#outplan").click(function () {
                   
                    $.lhdialog({
                        title: '导出',
                        width: '200px',
                        height: '90px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/platicResvOutExport.aspx?op=set'
                    });

                });
            
            $(".doorCardRec").click(function () {
                var devID = $(this).parents("tr").children().first().attr("data-id");
                var roomName = $(this).parents("tr").children().first().attr("data-roomName");
                fdata = "szGetKey=" + devID + '&roomName=' + roomName;
                TabInJumpReload("doorCardRec", fdata);
            });          
        </script>
    </form>
</asp:Content>
