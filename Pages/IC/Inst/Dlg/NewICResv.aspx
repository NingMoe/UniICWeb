<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewICResv.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input name="szDevID" id="szDevID" type="hidden" />
        <input name="szowner" id="szowner" type="hidden" />
        <div class="formtable">
            <table>
                <tr>
                    <th>空间：</th>
                    <td colspan="3">
                        <input id="szDevName" name="szDevName" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>开始日期：</th>
                    <td>
                        <input id="szStartDate" name="szStartDate" class="validate[required]" /></td>
                    <th>结束日期：</th>
                    <td>
                        <input id="szEndDate" name="szEndDate" class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>星期：</th>
                    <td colspan="3">
                       <%=szWeek %>
                    </td>
                </tr>
                <tr>
                    <th>开始时间：</th>
                    <td>
                        <select name="startTimeHour" id="startTimeHour" style="width:40px">
                            <%=TimeHour %>
                        </select>时
                        <select  name="startTimeMin" id="startTimeMin" style="width:40px">
                            <%=TimeMin %>
                        </select>分
                     </td>
                    <th>结束时间：</th>
                    <td>
                         <select name="endTimeHour" id="endTimeHour" style="width:40px">
                            <%=TimeHour %>
                        </select>时
                        <select  name="endTimeMin" id="endTimeMin" style="width:40px">
                            <%=TimeMin %>
                        </select>分
                        </td>
                </tr>
                <tr>
                    <th>其他成员工号：</th>
                    <td>
                        <input id="szLogonName" name="szLogonName" title="多个工号用中文，隔开，【点击右侧箭头查询】" /><input type="button" id="btnCheckName" value="➜" style="width:20px;" /></td>
                    <th>姓名</th>
                    <td>
                        <label id="szTrueName"></label>
                    </td>
                </tr>
                <tr>
                    <th>备注：</th>
                    <td colspan="3">
                        <input id="szMemo" name="szMemo" /></td>
                </tr>

                <tr>
                    <td colspan="4" style="text-align: center">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">关闭</button></td>
                </tr>
            </table>
        </div>
        <div class="formtable">
            <%=szResOut %>

        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .enum {
            margin-left:18px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
       
            AutoDevice($("#szDevName"), 2, $("#szDevID"), 1, null, null, null);
            $("#szStartDate,#szEndDate").datepicker({
            });
            $('#szStaName').val($("#dwStaSN").find("option:selected").text());
            $("#dwStaSN").change(function () {
                $("#szStaName").val($(this).find("option:selected").text());
            });
            $("#OK").button().click(function () {
                $("#OK").html('已经提交请稍等');
                $("#OK").addClass("disabled");
                $("#OK").attr("disabled", true);
                debugger;
                $("#<%=Form1.ClientID%>").submit();
            });
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {

            }, 1);
            //$("#szLogonName").keyup(function () {
            $("#btnCheckName").click(function(){
                var vlist = $("#szLogonName").val().split('，');
                var vlistE = $("#szLogonName").val().split(',');
                if (vlist.length < vlistE.length) {
                    vlist = vlistE;
                }
                var vRes = "";
                var vOwner = "";
                var i = 0;
                for (i = 0; i < vlist.length; i++) {
                    var vTemp = vlist[i];
                    $.get(
                         "../../data/searchaccount.aspx",
                         { Type: "pid", term: vTemp },
                         function (data) {
                             var vJson = eval(data);
                             if (vJson[0] != null && vJson[0].szTrueName != null) {
                                 vRes = vRes + vJson[0].szTrueName + ";";
                                 vOwner = vOwner + vJson[0].id + ";";
                                 $("#szTrueName").html("");
                                 $("#szTrueName").html(vRes);
                                 $("#szowner").val();
                                 $("#szowner").val(vOwner);
                             }

                         }
                       );

                }

            });
        });
    </script>
</asp:Content>
