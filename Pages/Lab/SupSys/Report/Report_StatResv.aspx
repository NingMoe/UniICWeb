<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Report_StatResv.aspx.cs" Inherits="Sub_Course"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>XXͳ��</h2>
    <div class="toolbar">
        <div class="tb_info">������5����XX��5��</div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">����ѡ��</div>
                <fieldset><legend>����</legend>
                    <label><input name="room" value="1" type="checkbox" />����1</label>  <label><input name="room" value="2" type="checkbox" />����2</label>
                </fieldset>
            </div>
        </div>
    </div>
    <div class="content">
        <table class="ListTbl">
            <thead>
                <tr><th width="60px">��ʼʱ��</th><th width="60px">��ʦ</th><th>�γ�</th><th>�༶</th><th>����</th><th width="90px">ʵ��/Ӧ��(����)</th><th width="60px">ʣ��ʱ��</th></tr>
            </thead>
            <tbody id="ListTbl">
                <tr><td>08:30</td><td class="lnkTeacher">����</td><td class="lnkCourse">�γ�1</td><td>�༶A</td><td>����1</td><td>50/50</td><td>1Сʱ</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">����</td><td class="lnkCourse">�γ�1</td><td>�༶A</td><td>����1</td><td>50/50</td><td>1Сʱ</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">����</td><td class="lnkCourse">�γ�1</td><td>�༶A</td><td>����1</td><td>50/50</td><td>1Сʱ</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">����</td><td class="lnkCourse">�γ�1</td><td>�༶A</td><td>����1</td><td>50/50</td><td>1Сʱ</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">����</td><td class="lnkCourse">�γ�1</td><td>�༶A</td><td>����1</td><td>50/50</td><td>1Сʱ</td></tr>
                <tr><td>08:30</td><td class="lnkTeacher">����</td><td class="lnkCourse">�γ�1</td><td>�༶A</td><td>����1</td><td>50/50</td><td>1Сʱ</td></tr>
            </tbody>
        </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
		<div class="BarStat tblBottomStat" data-color="3">
			<h1><span>--------</span><strong>�Ѱ���</strong><strong>δ����</strong></h1>
			<p><span>����ͳ��</span><strong>30</strong><strong>2</strong></p>
			<p><span>ѧʱͳ��</span><strong>60</strong><strong>8</strong></p>
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