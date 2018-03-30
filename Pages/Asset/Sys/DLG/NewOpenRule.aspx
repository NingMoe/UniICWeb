<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewOpenRule.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">  
        <input id="dwRuleSN" name="dwRuleSN" type="hidden" runat="server" />
        <input id="IsNewCtl" name="bIsNew" type="hidden" runat="server" />
        <table>
            <tr class="trInput">
                <th>规则名称:</th><td><input id="szRuleName" runat="server" name="szRuleName" class="validate[required]" /></td><th>备注:</th><td><input id="szMemo" name="szMemo" runat="server" /></td>
            </tr>
            <tr class="trInput">
                <th>开放对象:</th><td style="text-align:left"> <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList></td><th>优先级:</th><td  style="text-align:left"><asp:DropDownList ID="dwPriority" runat="server"></asp:DropDownList></td>
            </tr>            
             <tr>
                <th>开放时间段固定</th><td align="left"><input type="checkbox" class="chb" ID="chbLimit" runat="server" /></td>
            </tr>
        </table>     
         <table id="table2" style="border:1px solid #000000;">
            <tr>
             <th class="tdFirst"></th>
                <th style="text-align:center">时间段一<input type="checkbox" class="chb" ID="chbTime1" runat="server" onclick="ChbTimeEable(this,1)" /></th>
                <th  style="text-align:center">时间段二<input type="checkbox" class="chb" ID="chbTime2" runat="server" onclick="ChbTimeEable(this, 2)" /></th>
                <th  style="text-align:center">时间段三<input type="checkbox" class="chb" ID="chbTime3" runat="server" onclick="ChbTimeEable(this, 3)" /></th>                
            </tr>
            <tr>
                <th class="tdFirst">周一<input type="checkbox" class="chb" ID="chbWeek1" runat="server" onclick="ChbWeekEable(this, 1)"  /></th><td><asp:DropDownList ID="ddlWeek1Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek1Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek1Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek1Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek1Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek1Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek1Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek1Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek1Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek1Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek1Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek1Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">周二<input type="checkbox" class="chb" ID="chbWeek2" runat="server" onclick="ChbWeekEable(this, 2)" /></th><td><asp:DropDownList ID="ddlWeek2Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek2Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek2Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek2Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek2Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek2Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek2Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek2Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek2Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek2Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek2Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek2Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">周三<input type="checkbox" class="chb" ID="chbWeek3" runat="server" onclick="ChbWeekEable(this, 3)" /></th><td><asp:DropDownList ID="ddlWeek3Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek3Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek3Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek3Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek3Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek3Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek3Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek3Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek3Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek3Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek3Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek3Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">周四<input type="checkbox" class="chb" ID="chbWeek4" runat="server" onclick="ChbWeekEable(this, 4)" /></th><td><asp:DropDownList ID="ddlWeek4Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek4Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek4Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek4Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek4Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek4Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek4Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek4Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek4Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek4Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek4Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek4Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">周五<input type="checkbox" class="chb" ID="chbWeek5" runat="server" onclick="ChbWeekEable(this, 5)"  /></th><td><asp:DropDownList ID="ddlWeek5Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek5Time1StartMin" runat="server" class="ddl" Enabled="false" ></asp:DropDownList>到<asp:DropDownList ID="ddlWeek5Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek5Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek5Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek5Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek5Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek5Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek5Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek5Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek5Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek5Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">周六<input type="checkbox" class="chb" ID="chbWeek6" runat="server" onclick="ChbWeekEable(this, 6)"  /></th><td><asp:DropDownList ID="ddlWeek6Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek6Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek6Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek6Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek6Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek6Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek6Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek6Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek6Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek6Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek6Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek6Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">周日<input type="checkbox" class="chb" ID="chbWeek7" runat="server" onclick="ChbWeekEable(this, 7)"  /></th><td><asp:DropDownList ID="ddlWeek7Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek7Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek7Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek7Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek7Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek7Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek7Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek7Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek7Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek7Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek7Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek7Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>
             <tr>
                <th class="tdFirst">节假日<input type="checkbox" class="chb" ID="chbWeek0" runat="server"  onclick="ChbWeekEable(this, 0)" /></th><td><asp:DropDownList ID="ddlWeek0Time1StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek0Time1StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek0Time1EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek0Time1EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek0Time2StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek0Time2StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek0Time2EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek0Time2EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td><td><asp:DropDownList ID="ddlWeek0Time3StartHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek0Time3StartMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList>到<asp:DropDownList ID="ddlWeek0Time3EndHour" runat="server" class="ddl" Enabled="false"></asp:DropDownList>:<asp:DropDownList ID="ddlWeek0Time3EndMin" runat="server" class="ddl" Enabled="false"></asp:DropDownList></td>
            </tr>       
        </table>  
        <table>
            <tr>
                  <td colspan="4" class="btnRow"> <asp:Button ID="btnOk" runat="server" Text="确定" OnClick="btnOk_Click" Width="70px" />
                      <button type="button" id="Cancel">取消</button></td>
            </tr>
        </table>
       
    </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
       
        #table2 td {
         padding:6px;
         border:1px solid #000000;
        }
       #table2 th {
         padding:6px;
         border:1px solid #000000;
        }
        .tdheadRight
        {
            width:200px;
            text-align:right;
        }
        .tdContextLeft
        {
            width:180px;
            text-align:left;
        }
        .ddl
        {
        width:40px;
        }
        .chb
        {
           width:20px;
        }
         .tdFirst
        {
           width:80px;
        }
        .formtable input, select, .input {
            width: 40px;
        }
        .trInput input, select, .input {
            width: 120px;
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
        function GetDDLByWeek(Week, b) {
            var list = new Array();
            for (i = 1; i <= 3; i++) {
                var TempStartHour = $("#" + idPre + "ddlWeek" + Week + "Time" + i + "StartHour");
                var TempStartMin = $("#" + idPre + "ddlWeek" + Week + "Time" + i + "StartMin");
                var TempEndHour = $("#" + idPre + "ddlWeek" + Week + "Time" + i + "EndHour");
                var TempEndMin = $("#" + idPre + "ddlWeek" + Week + "Time" + i + "EndMin");

                if ($("#" + idPre + "chbTime" + i)[0].checked && b) {
                    list.push(TempStartHour);
                    list.push(TempStartMin);
                    list.push(TempEndHour);
                    list.push(TempEndMin);
                }
                else if ((!b)) {
                    list.push(TempStartHour);
                    list.push(TempStartMin);
                    list.push(TempEndHour);
                    list.push(TempEndMin);
                }

            }
            return list;
        }
        function GetDDLByTime(Time, b) {
            var list = new Array();
            for (i = 1; i <= 8; i++) {
                temp = i;
                if (i == 8)
                {
                    temp = 0;
                }
                var TempStartHour = $("#" + idPre + "ddlWeek" + temp + "Time" + Time + "StartHour");
                var TempStartMin = $("#" + idPre + "ddlWeek" + temp + "Time" + Time + "StartMin");
                var TempEndHour = $("#" + idPre + "ddlWeek" + temp + "Time" + Time + "EndHour");
                var TempEndMin = $("#" + idPre + "ddlWeek" + temp + "Time" + Time + "EndMin");
                if ($("#" + idPre + "chbWeek" + temp)[0].checked && b) {
                    list.push(TempStartHour);
                    list.push(TempStartMin);
                    list.push(TempEndHour);
                    list.push(TempEndMin);
                }
                else if ((!b)) {
                    list.push(TempStartHour);
                    list.push(TempStartMin);
                    list.push(TempEndHour);
                    list.push(TempEndMin);
                }
            }
            return list;
        }
        $(function () {      
            $("#<%=btnOk.ClientID%>").button();
            $("#Cancel").button().click(Dlg_Cancel);

        });
       
    </script>
</asp:Content>
