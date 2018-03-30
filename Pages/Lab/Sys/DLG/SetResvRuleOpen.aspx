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
                        ��ʼ�ܴ�
                    </th>
                    <td>
                        
             <select id="weekDay" name="weekDay">
          <option value="1">��1��</option>
          <option value="2">��2��</option>
          <option value="3">��3��</option>
          <option value="4">��4��</option>
          <option value="5">��5��</option>
          <option value="6">��6��</option>
          <option value="7">��7��</option>
          <option value="8">��8��</option>
          <option value="9">��9��</option>
          <option value="10">��10��</option>
          <option value="11">��11��</option>
          <option value="12">��12��</option>
          <option value="13">��13��</option>
          <option value="14">��14��</option>
          <option value="15">��15��</option>
          <option value="16">��16��</option>
          <option value="17">��17��</option>
          <option value="18">��18��</option>
          <option value="19">��19��</option>
          <option value="20">��20��</option>
          <option value="21">��21��</option>
          <option value="22">��22��</option>
<option value="23">��23��</option>
<option value="24">��24��</option>
<option value="25">��25��</option>

        </select>
                    </td>
                    <th>
                        �����ܴ�
                    </th>
                    <td>
                          <select id="weekDay2" name="weekDay2">
          <option value="1">��1��</option>
          <option value="2">��2��</option>
          <option value="3">��3��</option>
          <option value="4">��4��</option>
          <option value="5">��5��</option>
          <option value="6">��6��</option>
          <option value="7">��7��</option>
          <option value="8">��8��</option>
          <option value="9">��9��</option>
          <option value="10">��10��</option>
          <option value="11">��11��</option>
          <option value="12">��12��</option>
          <option value="13">��13��</option>
          <option value="14">��14��</option>
          <option value="15">��15��</option>
          <option value="16">��16��</option>
          <option value="17">��17��</option>
          <option value="18">��18��</option>
          <option value="19">��19��</option>
          <option value="20">��20��</option>
          <option value="21">��21��</option>
          <option value="22">��22��</option>
<option value="23">��23��</option>
<option value="24">��24��</option>
<option value="25">��25��</option>

        </select>
                    </td>
                </tr>
            </table>

      
        <br />
            <div style="margin:0px auto;text-align:center">
        <input type="button" onclick="add()" id="btn" value="��ӿ���ʱ��" />
    </div>
            <div id="szLimitTime">
                <%=szConTimeDiv %>
            </div>
            <table>
                  
                <tr>
                    <td class="btnRow" colspan="4">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
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
            var bigdate = ["����һ", "���ڶ�", "������", "������", "������", "������", "������"];
            var bigSecs = ["��", "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "ʮһ", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��"];
            var bigWeeks = ["��", "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "ʮһ", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "��ʮ", "��ʮһ", "��ʮ��", "��ʮ��", "��ʮ��", "��ʮ��", "��ʮ��", "��ʮ��", "��ʮ��", "��ʮ��", "��ʮ"];

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
            var szInfo = "��" + bigWeeks[weekDayValue] + "�ܵ�" + bigWeeks[weekDayValue2];
            var szAddHtml = "<a id=\"" + devid + "\" onclick=\"delA('" + devid + "')\" href=\"#\">" + szInfo + "(���ɾ��)" + "</a><br />";

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
