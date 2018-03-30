<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevSeatCardUser.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC %>预约状况</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                       <a href="DevSeatList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindSeat %>使用状况</a> 
                <a href="DevSeatCardUser.aspx" id="DevSeatCardUser"><%=ConfigConst.GCSysKindSeat %>用户刷卡列表</a>  
                <a href="DevSeatUseRec.aspx" id="DevSeatUseRec"><%=ConfigConst.GCSysKindSeat %>使用记录</a>
                    <a href="DevSeatUseInfo.aspx" id="DevPCUseinfo"><%=ConfigConst.GCSysKindSeat %>用户签到记录</a>
               <!-- <a href="DevPCResvRec.aspx" id="DevPCResvRec"><%=ConfigConst.GCSysKindRoom %>预约记录</a>-->
                </div> 
    
    </div>
         
        </div> 
            <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                       
                       <tr>
                    <td style="text-align:right"><%=ConfigConst.GCSysKindSeat %>名称</td>
                    <td style="text-align:left"><input type="text" name="szDevName" id="szDevName" style="margin-left:5px" />
                        <input type="submit" value="查询" id="btnOK" />
                           <input type="button" value="暂时离开" id="btnLeavl" />
                    </td> </tr>
                    </table>
             
            </div>
       </div>   
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>刷卡号</th>
                        <th>刷卡人姓名</th>                        
                        <th>座位名称</th>                      
                        <th>所属<%=ConfigConst.GCLabName %></th>
                        <th>状态</th>
                        <th name="dwOccurTime">刷卡时间</th>
                        <th name="dwBeginTime">可使用时间</th>
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
                $("#btnOK,#btnLeavl").button();
                var tabl = $(".UniTab").UniTab();              
                AutoDevice($("#szDevName"), 1, $("#szGetKey"),8, null, null, null);
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delLabBtn" title="强制刷卡离座"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnLeavl").click(function () {
                   
                    ConfirmBox("确定暂时离开?", function () {
                        ShowWait();
                        var dwResvID = "";
                        $("input[name^='tblSelect']").each(function () {
                            if ($(this).prop("checked") == true) {
                                dwResvID = dwResvID + $(this).parent().attr("data-id")+',';
                            }
                        });
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=stopLeavl&stopAllID=" + dwResvID);
                    }, '提示', 1, function () { });
                });
                $(".OPTD1").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '审核',
                        width: '700px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkbg.aspx?op=set&id=' + dwResvID
                    });
                });                
                $(".delLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("强制刷卡离座,30秒后生效?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&ID=" + dwLabID);
                }, '提示', 1, function () { });
                });
                $("input[name='dwCheckStat']").click(function () {
                   // pForm.data("UniTab_tab1", "reserve.aspx?dwCheckStat=" + $("input[name='dwCheckStat']").val());
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                  });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '新建',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewLab.aspx?op=new'
                });
            });
           // $(".ListTbl").UniTable();
            $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
        });
        </script>
          <style>
            #tbSearch
            {
                border-width: 1px;                
                border-color: #d1c1c1;                
                cursor: hand;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:20px;
            }

        </style>
    </form>
</asp:Content>
