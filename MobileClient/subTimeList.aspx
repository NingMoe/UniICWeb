<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subTimeList.aspx.cs" Inherits="_Default" %>
<div class="Div" id="selectTimeList">
    <div class="Head">
       <div><span class="navback"><font class="navarrow">◀</font> 选择区域</span></div> 
    </div>
   
    <input type="hidden" id="TimeResvDateSearch" value="<%=szSearchDate %>" />
        <input type="hidden" id="selectBeginTime" value="<%=szSelectBeginTime %>" />
    <input type="hidden" id="selectEndTime" value="<%=szSelectEndTime %>" />
    <input type="hidden" id="roomid" value="<%=roomid %>" />
    <div class="Content">
           <div class="ItemList">
         <%=m_szOutResv %>
                     </div>
         
        <div class="ItemList">
            <%=m_szOut %>
        </div>
    </div>
</div>
