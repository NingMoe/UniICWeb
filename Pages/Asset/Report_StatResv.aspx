<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Report_StatResv.aspx.cs" Inherits="Sub_Course"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>XX统计</h2>
    <div class="toolbar">
        <div class="tb_info">总数：5条，XX：5个</div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">过滤选项</div>
                <fieldset><legend>机房</legend>
                    <label><input name="room" value="1" type="checkbox" />机房1</label>  <label><input name="room" value="2" type="checkbox" />机房2</label>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">开始时间</th><th width="60px">教师</th><th>课程</th><th>班级</th><th>房间</th><th width="90px">实到/应到(人数)</th><th width="60px">剩余时长</th></tr>
            </thead>
            <tbody id="ListTbl">
                <tr><td>08:30</td><td class="lnkTeacher">张三</td><td class="lnkCourse">课程1</td><td>班级A</td><td>房间1</td><td>50/50</td><td>1小时</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">张三</td><td class="lnkCourse">课程1</td><td>班级A</td><td>房间1</td><td>50/50</td><td>1小时</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">张三</td><td class="lnkCourse">课程1</td><td>班级A</td><td>房间1</td><td>50/50</td><td>1小时</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">张三</td><td class="lnkCourse">课程1</td><td>班级A</td><td>房间1</td><td>50/50</td><td>1小时</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">张三</td><td class="lnkCourse">课程1</td><td>班级A</td><td>房间1</td><td>50/50</td><td>1小时</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">张三</td><td class="lnkCourse">课程1</td><td>班级A</td><td>房间1</td><td>50/50</td><td>1小时</td></tr>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
		<div class="BarStat tblBottomStat" data-color="3">
			<h1><span>--------</span><strong>已安排</strong><strong>未安排</strong></h1>
			<p><span>安排统计</span><strong>30</strong><strong>2</strong></p>
			<p><span>学时统计</span><strong>60</strong><strong>8</strong></p>
		</div>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
            });
        });
    </script>
</form>    
</asp:Content>