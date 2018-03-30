<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
         <!--
            <li><a href="RDevList.aspx">教学科研仪器设备表</a></li>
            <li><a href="RDevChange.aspx" style="font-size: 12px" title="教学科研仪器设备增减变动情况表">教学科研仪器设备增减..</a></li>
            <li><a href="RBigDev.aspx">贵重仪器设备表</a></li>
         
            <li><a href="RStaff.aspx">专任实验室人员表</a></li>
            <li><a href="RLabInfo.aspx">实验室基本情况表</a></li>
            <li><a href="RLabCostInfo.aspx">实验室经费情况表</a></li>
            <li><a href="RLABSUMMARY.aspx" style="font-size: 12px" title="高等学校实验室综合信息表一">高等学校实验室综..表一</a></li>
            <li><a href="RLABSUMMARYII.aspx" style="font-size: 12px" title="高等学校实验室综合信息表二">高等学校实验室综..表二</a></li>
          -->
             <!-- 
            <li><a href="DevFarTotal.aspx"><%=ConfigConst.GCDevName %>经费分配统计</a></li>
            <li><a href="DevFarDetail.aspx"><%=ConfigConst.GCDevName %>经费分配详细</a></li>
            <li><a href="DevRtResvTotal.aspx"><%=ConfigConst.GCDevName %>经费统计</a></li>
            <li><a href="DevRtResvDetail.aspx"><%=ConfigConst.GCDevName %>经费详细</a></li>  
                   --> 
            <li><a href="DevUsingStat.aspx"><%=ConfigConst.GCDevName %>使用率统计</a></li>
			 <li><a href="TestItemStat.aspx">实验室课程项目统计</a></li>
               <li><a href="RTestItemStat.aspx">教学实验项目表</a></li>
           <!-- <li><a href="DevClsUsingStat.aspx"><%=ConfigConst.GCClassName %>使用率统计</a></li>-->
            <li><a href="DevKindUsingStat.aspx"><%=ConfigConst.GCKindName %>使用率统计</a></li>
            <li><a href="PersonUsingStat.aspx">个人使用排行榜</a></li>
            <li><a href="LabUsingStat.aspx"><%=ConfigConst.GCLabName %>使用率统计</a></li>
            <li><a href="RoomUsingStat.aspx"><%=ConfigConst.GCRoomName %>使用率统计</a></li>
          <li><a href="RDevUsingTable.aspx"><%=ConfigConst.GCDevName %>使用率统计图</a></li>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

