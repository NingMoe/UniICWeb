<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subResv.aspx.cs" Inherits="_Default" %>

<div class="Div" id="resvDiv">
    <div class="Head">
        <span class="navback"><font class="navarrow">◀</font>选择研修间</span>
        <span>预约登记</span>
    </div>
    <input type="hidden" id="devID" name="devID" runat="server" />
    <input type="hidden" id="date" name="date" runat="server" />
    <input type="hidden" id="szAccno" name="szAccno" />
 
    <div class="Content FormBlock">
        <div class="Head2">
        </div>
        <table class="tblEContent" style="font-size: 12px">
            <tr>
                <td class="label">学工号：</td>
                <td><%=user.szLogonName %></td>
                <td class="label">姓名：</td>
                <td><%=user.szTrueName %></td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="label"><%=ConfigConst.GCRoomName %>：</td>
                <td>
                    <div id="devName"><%=DevName %></div>
                </td>
                <td class="label">人数限制：</td>
                <td>
                    <div id="divPersonNumLimit"><%=personNumLimit %></div>
                </td>

            </tr>
            <tr>
                <td class="label">预约日期：</td>
                <td colspan="3">
                    <div style="height: 25px; vertical-align: middle; line-height: 25px;">
                        <font class="navarrow" id="datePre" style="font-size: 25px" onclick="resvPreDay()">◀</font>
                        <span id="resvDate"><%=resvDateIn %></span>
                        <font class="navarrow" id="dateNext" style="font-size: 25px" onclick="resvNextDay()">▶</font>
                    </div>

                </td>
            </tr>
            <%if((uClassKindTemp&2048)>0) { %>
            <tr>
                <td class="label">申请主题：</td>
                <td colspan="3"><input type="text" id="szTestName" name="szTestName" /></td>
            </tr>
            <%} %>
            <tr>
                <td class="label">开始时间：</td>
                <td>
                    <select name="dwBeginTime" id="beginTime">
                        <%=szStartTime %>
                    </select></td>
                <td class="label">结束时间：</td>
                <td>
                    <select name="dwEndTime" id="endTime">
                        <%=szStartTime %>
                    </select></td>
            </tr>
            <%if (uMinUser>1) {%>
            <tr>
                <td class="label">其他学工号：</td>
                <td colspan="3">
                    <input type="text" id="logonName" name="logonName" /><input type="button" value="添加" id="btnAdd" onclick="addUser()" /></td>
            </tr>
            <tr>
                <td class="label">其他成员姓名：</td>
                <td colspan="3">
                    <div id="divTrueName"></div>
                </td>
            </tr>
            <%} %>
            <tr>
                <td class="label">已预约信息：</td>
                <td colspan="3">
                    <div class="Head2" id="resvInfo" style="text-align: left">
                        <%=szResvInfo %>
                    </div>
                </td>
            </tr>
            <tr class="btnline">
                <td colspan="4" style="text-align: center; padding: 6px;">
                    <input type="button" onclick="BtnResv()" class="button" id="btnOK" value="确定预约" /></td>
            </tr>

        </table>
        <hr />
        <div class="Head2" style="text-align: left; color: red">

            <div id="divLimit1">
              开放时间：  <%=OpenTime %>
              
            </div>
            <br />
            <div id="divLimit2">
              可预约时长：  <%=szResvTime %>
                
            </div>
            <br />
            <div id="divLimit3">
              可预约时间范围：  <%=szPreResvTime %>
            </div>
            <br />
            <div id="divLimit4">
                违约不来取消时间：  <%=cancelTime %>分钟
            </div>
        </div>
        <div class="msg">
            <%=szMsg %>
        </div>
</div>
