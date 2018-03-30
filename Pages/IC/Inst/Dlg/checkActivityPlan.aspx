<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="checkActivityPlan.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwActivityPlanID" id="dwActivityPlanID" />
        <input type="hidden" name="dwResvID" id="dwResvID" />
        <div class="formtable">
            <table>
                <tr>
                    <th style="width:100px">活动名称：</th>
                    <td>
                        <div id="szActivityPlanName" /></td>
                    <th  style="width:100px">空间名称：</th>
                    <td>
                      <div id="devName"></div></td>
                </tr>
                <tr>
                    <th>主办单位：</th>
                    <td>
                        <div id="szHostUnit"  /></td>
                    <th>承办单位：</th>
                    <td>
                        <div id="szOrganizer" /></td>
                </tr>
                <tr>
                    <th>主持人：</th>
                    <td>
                        <div id="szPresenter"  /></td>
                     <th>参与者要求：</th>
                    <td>
                        <div id="szDesiredUser"  /></td>
                </tr>
                <tr>
                     <th>联系人：</th>
                    <td>
                        <div id="szContact" /></td>
                      <th>电话：</th>
                    <td>
                        <div id="szTel" /></td>
                </tr>
                <tr>
                    <th>邮箱：</th>
                    <td>
                        <div id="szEmail" /></td>
                    <th>手机：</th>
                    <td>
                        <div id="szHandPhone" /></td>
                </tr>
                 <tr>
                      <th>最少报名人数：</th>
                    <td>
                        <div id="dwMinUsers" /></td>
                    <th>最多报名人数：</th>
                    <td>
                        <div id="dwMaxUsers" /></td>
                </tr>
                <tr>
                        <th>截止报名日期：</th>
                    <td>
                        <div id="dwEnrollDeadline" /></td>
                    <th>活动日期：</th>
                    <td>
                        <div id="dwActivityDate" /></td>
                </tr>
                <tr>
                      <th>开始时间：</th>
                    <td>
                        <div id="dwBeginTime" /></td>
                    <th>结束时间：</th>
                    <td>
                        <div  id="dwEndTime" /></td>
                </tr>
                <tr>
                    <th>活动简介：</th>
                    <td colspan="3"><div id="szIntroInfo"></div></td>
                </tr>
                <tr>
                    <th>状态：</th>
                    <td>
                    <select id="dwStatus" name="dwStatus">
                        <option value="256">不开放</option>
                        <option value="512">开放</option>
                        <option value="1024">过期</option>
                    </select>
                        </td>
                    <th>
                        申请附件
                    </th>
                    <td>
                        <a target="_blank" style="color:blue;text-decoration:underline" href="..\..\..\..\ClientWeb\upload\UpLoadFile\<%=szFile %>">点击下载</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center">
                    <button type="submit" id="OK">审核通过</button>
                        <button type="button" id="Cancel">审核不通过</button></td>
                </tr>
            </table>
        </div>
         <div id="divNoOK">
           <table>
               <tr>
                   <td>审核意见：</td>
                   <td>
                        <input type="text" name="szCheckInfo" id="szCheckInfo" title="审核不通过必须输入原因" style="width:250px" />
                   </td>
                   <td>
                       <input type="button" id="btnNOOK" value="确定不通过" style="width:90px" />
                       <input type="button" id="btnClose" value="关闭" style="width:80px" />
                   </td>
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
            $("#divNoOK").hide();
            $("#OK").button();
            $("#Cancel").button();
            $("#Cancel").click(function () {
                $("#divNoOK").show();
            });
            $("#btnClose").button().click(Dlg_Cancel);
            $("#btnNOOK").button();
            $("#btnNOOK").click(function () {
                var szCheckInfo = $("#szCheckInfo").val();
                var dwstate = $("#dwStatus").val();
                if (szCheckInfo == "") {
                    return;
                }
                var id = $("#dwActivityPlanID").val();
                $.get(
                         "../../ajaxpage/checkActivityplan.aspx",
                         { szCheckInfo: szCheckInfo, id: id, statue: dwstate },
                         function (data) {
                             if (data == "success") {
                                 MessageBox("审核不通过", "提示", 3, function () { Dlg_OK() });
                             }
                             else {
                                 MessageBox("审核失败" + data, "提示", 3, function () { Dlg_OK() });
                             }

                         }
                       );

            });
        });
    </script>
</asp:Content>
