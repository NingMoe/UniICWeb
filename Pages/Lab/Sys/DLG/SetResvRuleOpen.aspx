<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetResvRuleOpen.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="szConTimeHidden" name="szConTimeHidden" />
        <div class="formtable">
            <input id="szValue" name="szValue" type="hidden" />
            <table>
                <tr>
                    <th>
                        开始周次
                    </th>
                    <td>
                        
             <select id="weekDay" name="weekDay">
          <option value="1">第1周</option>
          <option value="2">第2周</option>
          <option value="3">第3周</option>
          <option value="4">第4周</option>
          <option value="5">第5周</option>
          <option value="6">第6周</option>
          <option value="7">第7周</option>
          <option value="8">第8周</option>
          <option value="9">第9周</option>
          <option value="10">第10周</option>
          <option value="11">第11周</option>
          <option value="12">第12周</option>
          <option value="13">第13周</option>
          <option value="14">第14周</option>
          <option value="15">第15周</option>
          <option value="16">第16周</option>
          <option value="17">第17周</option>
          <option value="18">第18周</option>
          <option value="19">第19周</option>
          <option value="20">第20周</option>
          <option value="21">第21周</option>
          <option value="22">第22周</option>
<option value="23">第23周</option>
<option value="24">第24周</option>
<option value="25">第25周</option>

        </select>
                    </td>
                    <th>
                        结束周次
                    </th>
                    <td>
                          <select id="weekDay2" name="weekDay2">
          <option value="1">第1周</option>
          <option value="2">第2周</option>
          <option value="3">第3周</option>
          <option value="4">第4周</option>
          <option value="5">第5周</option>
          <option value="6">第6周</option>
          <option value="7">第7周</option>
          <option value="8">第8周</option>
          <option value="9">第9周</option>
          <option value="10">第10周</option>
          <option value="11">第11周</option>
          <option value="12">第12周</option>
          <option value="13">第13周</option>
          <option value="14">第14周</option>
          <option value="15">第15周</option>
          <option value="16">第16周</option>
          <option value="17">第17周</option>
          <option value="18">第18周</option>
          <option value="19">第19周</option>
          <option value="20">第20周</option>
          <option value="21">第21周</option>
          <option value="22">第22周</option>
<option value="23">第23周</option>
<option value="24">第24周</option>
<option value="25">第25周</option>

        </select>
                    </td>
                </tr>
            </table>

      
        <br />
            <div style="margin:0px auto;text-align:center">
        <input type="button" onclick="add()" id="btn" value="添加开放时间" />
    </div>
            <div id="szLimitTime">
                <%=szConTimeDiv %>
            </div>
            <table>
                  
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>

        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
      
            $("#OK,#btn").button();
        $("#Cancel").button().click(Dlg_Cancel);
      
        });
        function add() {
            var bigdate = ["星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期天"];
            var bigSecs = ["零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八"];
            var bigWeeks = ["零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十", "二十一", "二十二", "二十三", "二十四", "二十五", "二十六", "二十七", "二十八", "二十九", "三十"];

            var szValue = document.getElementById("szValue");
            var weekDayIndex = document.getElementById("weekDay").selectedIndex;
            var weekDayValue = document.getElementById("weekDay").options[weekDayIndex].value;
            var weekDaysObj = document.getElementsByName("weekDays");

            var weekDayIndex2 = document.getElementById("weekDay2").selectedIndex;
            var weekDayValue2 = document.getElementById("weekDay2").options[weekDayIndex2].value;
            var weekDaysObj2 = document.getElementsByName("weekDay2");

            var weekDaysValue = "";
            var devid = "";
            var t = 2 * 100 + parseInt(weekDayValue);
            if (weekDayValue2 < 10) {
                devid = "T0" + t + "-0" + weekDayValue2;
            }
            else {
                devid = "T0" + t + "-" + weekDayValue2;
            }

            szValue.value = szValue.value + devid + ";";
            var szInfo = "第" + bigWeeks[weekDayValue] + "周到" + bigWeeks[weekDayValue2];
            var szAddHtml = "<a id=\"" + devid + "\" onclick=\"delA('" + devid + "')\" href=\"#\">" + szInfo + "(点击删除)" + "</a><br />";

            var divHtml = document.getElementById("szLimitTime");
            divHtml.innerHTML = divHtml.innerHTML + szAddHtml + "";

        }
        function delA(divid) {
            var delA = document.getElementById(divid);
            delA.parentNode.removeChild(delA);
            var szValue = document.getElementById("szValue");
            szValue.value = szValue.value.replace(divid + ";","");
        }
    </script>
</asp:Content>
