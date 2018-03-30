<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetRoomUrlMoniter.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">  
        <input id="dwID" name="dwID" type="hidden" runat="server" />
        <input id="IsNewCtl" dwCtrlSNt="server" dwsnname="bIsNew" type="hidden" />
        <table>
            <tr class="trInput">
                <td>监控模式:</td><td><select id="dwCtrlSN" name="dwCtrlSN"><%=m_dwCtrlMode%></select></td></tr>         
            <tr><td>结束日期:</td><td><input type="text" id="dwEndDate" name="dwEndDate" value="2030-01-01" class="validate[required]" readonly="readonly"  /></td></tr>  
           
           <tr><td>结束时间:</td><td><div class="TimePicker TimePicker-time">
                            <input name="dwEndTime" id="dwEndTime" value="17:30" class="TimePicker-time-input" /></div></td></tr>  
            
        <tr><td></td><td><button type="submit" id="OK">确定</button><button type="button" id="Cancel">取消</button></td></tr>
        </table>
       
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .formtitle {
            padding:6px;
            background: #d0d0d0;
            height:30px;
            color: #fff;
            font-size: 20px;
        }
        .formtable table{
            text-align:center;
            margin:auto;
        }
        td {
         padding:6px;
        }
        input, select {
            width: 200px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        var idPre = "ctl00_Content_";       
        function ChbTimeEable(tar, Time) {

            if (tar.checked) {
                var list = GetDDLByTime(Time, true);//true代表选中
                for (i = 0; i < list.length; i++) {
                    list[i].removeAttr("disabled");
                }
            }
            else {
                var list = GetDDLByTime(Time, false);
                for (i = 0; i < list.length; i++) {
                    list[i].attr("disabled","disabled");
                }
            }
        }
        function ChbWeekEable(tar, Week) {

            if (tar.checked) {
                var list = GetDDLByWeek(Week, true);//true代表选中
                for (i = 0; i < list.length; i++) {
                    list[i].removeAttr("disabled");
                }
            }
            else {
                var list = GetDDLByWeek(Week, false);
                for (i = 0; i < list.length; i++) {
                    list[i].attr("disabled", "disabled");
                }
            }
        }
        
        $(function () {                  
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        setTimeout(function () {
         
        }, 1);     
       
        var opt = {
                'time': {
                    preset: 'time'
                }
            }
            
        $("#dwEndDate").datepicker();
        //$("#dwEndTime").datepicker();
     
     
      $('#dwEndTime').scroller('destroy').scroller($.extend(opt["time"], {
                theme: "ios",
                mode: "scroller",
                lang: "zh",
                display: "bubble",
                animate: "flip"
            }));
            $('.TimePicker').hide();
            $('.TimePicker-time').show();
    });
     
     
    </script>
</asp:Content>
