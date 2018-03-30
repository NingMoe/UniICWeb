<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetAdminPer.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">

            <table>
                <tr>
                    <th>
                        管理员：
                    </th>
                    <td colspan="2">
                        <div id="divName"></div>
                    </td>
                </tr>
                <tr>
                    <th style="text-align: center;font-size:large;font-weight:bolder">日常管理</th>
                    <th style="text-align: center;font-size:large;font-weight:bolder">报表统计</th>
                    <th style="text-align: center;font-size:large;font-weight:bolder">管理设置</th>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <div style="text-align: center;">
                            <label>
                                <input type="checkbox" name="LV1" value="DevRoomResvState.aspx" class="enum" /><%=ConfigConst.GCSysKindRoom %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DevPCList.aspx" class="enum" /><%=ConfigConst.GCSysKindPC %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DevLendList.aspx" class="enum" /><%=ConfigConst.GCSysKindLend %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DevSeatList.aspx" class="enum" /><%=ConfigConst.GCSysKindSeat %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="Activityplan.aspx" class="enum" />活动安排</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="ReserveRoomList.aspx" class="enum" />预约状况</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="DisciList.aspx" class="enum" />违约与处罚</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV1" value="ICINTROClass.aspx" class="enum" />空间展示内容</label>
                            <br />
                        </div>
                    </td>
                    <td style="text-align: center">
                        <div>
                            <label>
                                <input type="checkbox" name="LV2" value="DevUsingStat.aspx" class="enum" /><%=ConfigConst.GCDevName %>使用率统计</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="DevKindUsingStat.aspx" class="enum" /><%=ConfigConst.GCKindName %>使用率统计</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="PersonUsingStat.aspx" class="enum" />个人使用排行榜</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="LabUsingStat.aspx" class="enum" /><%=ConfigConst.GCLabName %>使用率统计</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV2" value="RDevUsingTable.aspx" class="enum" /><%=ConfigConst.GCDevName %>使用率统计图</label>
                            <br />

                        </div>

                    </td>
                    <td style="text-align: center">

                          <label>
                                <input type="checkbox" name="LV3" value="SysKindRoom.aspx" class="enum" /><%=ConfigConst.GCSysKindRoom %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="SysKindPC.aspx" class="enum" /><%=ConfigConst.GCSysKindPC %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="SysKindLend.aspx" class="enum" /><%=ConfigConst.GCSysKindLend %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="SysKindSeat.aspx" class="enum" /><%=ConfigConst.GCSysKindSeat %>管理</label>
                            <br />
                            <label>
                                <input type="checkbox" name="LV3" value="DevKind.aspx" class="enum" /><%=ConfigConst.GCKindName %>管理</label>
                            <br />
                        <label>
                                <input type="checkbox" name="LV3" value="Control.aspx" class="enum" />控制台管理</label>
                            <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center" colspan="3">
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
            <%if (bSet)
              {%>
           <%}%>

            $("#OK").button();
            $("#Cancel").button().click(Dlg_Cancel);
            AutoDept($("#szDeptName"), 2, $("#dwDeptID"), false);


            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
