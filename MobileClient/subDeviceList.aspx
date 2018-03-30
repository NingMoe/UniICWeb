<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subDeviceList.aspx.cs" Inherits="_Default" %>
<div class="Div" id="listDiv">
    <input type="hidden" id="dwDate" name="dwDate" value="<%=uDate %>" />
    <div class="Head">
       <div><span class="navback"><font class="navarrow">◀</font> 选择类型</span></div> 
    </div>
     <div id="divResvDateSearch" style='font-size:12px;text-align:center;height: 25px; vertical-align: middle; line-height: 25px;'>
             查询日期：<font class="navarrow" id="datePreSearch" onclick="resvStatePreDay()" style="font-size:25px">◀</font>
                    <span id="ResvDateSearch"><%=szSearchDate %></span>
                    <font class="navarrow" id="dateNextSearch" onclick="resvStateNextDay()" style="font-size:25px">▶</font>
        </div>
    <div class="Content">
        <div class="ItemList">
            <%=m_szOut %>
            <!--
            <div class="Item">
                <div class="LHead">二楼研修单人间101</div>
                <div class="LContent">研修间介绍内容 研修间介绍内容 研修间介绍内容 研修间介绍内容 研修间介绍内容 研修间介绍内容 研修间介绍内容</div>
                <div class="LGraphics"><span>预约状态图，</span><span class="enableStat">■可预约</span><canvas id="Bar1"></canvas></div>
                <div class="LBtn"><button>预约→</button></div>
            </div>
            -->
        </div>
    </div>
</div>
