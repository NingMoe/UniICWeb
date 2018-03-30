<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subSeatRoomList.aspx.cs" Inherits="_Default" %>
<div class="Div" id="selectTimeList">
      <input type="hidden" id="dwDate" name="dwDate" value="<%=uDate %>" />
    <div id="divResvDateSearch" style='font-size:12px;text-align:center;height: 25px; vertical-align: middle; line-height: 25px;'>
             查询日期：<font class="navarrow" id="datePreSearch" onclick="resvRoomStatePreDay()" style="font-size:25px">◀</font>
                    <span id="ResvDateSearch"><%=szSearchDate %></span>
                    <font class="navarrow" id="dateNextSearch" onclick="resvRoomStateNextDay()" style="font-size:25px">▶</font>
        </div>
    <div class="Content">
        <div class="ItemList">
            <%=m_szOut%>      
        </div>
    </div>
</div>
