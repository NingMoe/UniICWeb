<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="NewActitityPlan.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" name="dwActivityPlanID" id="dwActivityPlanID" />
        <input type="hidden" name="dwResvID" id="dwResvID" />
        <input type="hidden" name="dwOwner" id="dwOwner" />
        <input type="hidden" name="dwGroupID" id="dwGroupID" />
        <div class="formtable">
            <table>
                <tr>
                    <th>活动主题：</th>
                    <td>
                        <input type="text" id="szActivityPlanName" name="szActivityPlanName" class="validate[required]" /></td>
                    <th>空间名称：</th>
                    <td>
                        <select id="dwDevID" name="dwDevID"><%=szDevList %></select></td>
                </tr>
                <tr>
                    <th>主讲人：</th>
                    <td>
                        <input type="text" id="szPresenter" name="szPresenter" class="validate[required]" /></td>
                    <th>联系人：</th>
                    <td>
                        <input type="text" id="szContact" name="szContact"/></td>
                </tr>
                 <tr>
                    <th>主办单位：</th>
                    <td>
                        <input type="text" id="szHostUnit" name="szHostUnit" /></td>
                    <th>承办单位：</th>
                    <td>
                        <input type="text" id="szOrganizer" name="szOrganizer" /></td>
                </tr>
                <tr>
                    <th>参与者要求：</th>
                    <td>
                        <input type="text" id="szDesiredUser" name="szDesiredUser" /></td>
                    <th>主办地点：</th>
                    <td>
                        <input type="text" id="szSite" name="szSite" class="validate[required]" /></td>
                </tr>
                
                
                <tr>
                    <th>电话：</th>
                    <td>
                        <input type="text" id="szTel" name="szTel" /></td>
                    <th>手机：</th>
                    <td>
                        <input type="text" id="szHandPhone" name="szHandPhone" /></td>
                </tr>
                 <tr>
                      <th>最少报名人数：</th>
                    <td>
                        <input type="text" id="dwMinUsers" name="dwMinUsers" /></td>
                    <th>最多报名人数：</th>
                    <td>
                        <input id="dwMaxUsers" name="dwMaxUsers" /></td>
                </tr>
                <tr>
                        <th>截止报名日期：</th>
                    <td>
                        <input type="text" id="dwEnrollDeadline" name="dwEnrollDeadline" /></td>
                    <th>活动日期：</th>
                    <td>
                        <input type="text" id="dwActivityDate" name="dwActivityDate"  class="validate[required]" /></td>
                </tr>
                <tr>
                      <th>开始时间：</th>
                    <td>
                        <input type="text" id="dwBeginTime" name="dwBeginTime"  class="validate[required]" /></td>
                    <th>结束时间：</th>
                    <td>
                        <input id="dwEndTime" name="dwEndTime"  class="validate[required]" /></td>
                </tr>
                <tr>
                    <th>状态：</th>
                    <td colspan="3">
                    <select id="dwStatus" name="dwStatus">
                        <option value="256">不开放</option>
                        <option value="512">开放</option>
                        <option value="1024">过期</option>
                    </select>
                        </td>
                </tr>
<tr>
<th>活动介绍：</th>
<td colspan="3"><textarea cols="75" rows="6" id="szIntroInfo" name="szIntroInfo"></textarea></td>
</tr>
                <tr>
                    <td colspan="4" style="text-align:center">
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
         
            $("#dwEnrollDeadline,#dwActivityDate").datepicker({
            });
            $("#dwBeginTime,#dwEndTime").datetimepicker({
                timeOnly: true
            });

            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            setTimeout(function () {
                
            }, 1);
        });
    </script>
</asp:Content>
