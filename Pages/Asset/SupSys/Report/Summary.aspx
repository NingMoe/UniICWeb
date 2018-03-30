<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Sub_Summary"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="tabs-summary">
    <h2>统计总览</h2>
    <fieldset class="ChartFD"><legend>实验安排统计</legend>
        <div><label>总安排数量：</label><a>30条</a>， <label>未安排数量：</label><a>10条</a>， <label>已安排数量：</label><a>16条</a>， <label>总学时：</label><a>4个</a>， <label>已安排学时：</label><a>4个</a>， <label>未安排学时：</label><a>4个</a></div>
        <div id="ResvStat" class="BarStat" data-color="3">
            <h1><span>--------</span><strong>已安排</strong><strong>未安排</strong></h1>
            <p><span>安排统计</span><strong>30</strong><strong>2</strong></p>
            <p><span>学时统计</span><strong>60</strong><strong>8</strong></p>
        </div>
    </fieldset>
    <fieldset class="ChartFD"><legend>自由上机</legend>
        <div><label>当前总人数：</label><a>30人</a></div>
        <div id="FreeUserStat" class="LineStat" data-color="0" data-name="总人数" data-unit="人">
            <p><span>1时</span><span>7</span></p>
            <p><span>2时</span><span>6</span></p>
            <p><span>3时</span><span>9</span></p>
            <p><span>4时</span><span>14</span></p>
            <p><span>5时</span><span>26</span></p>
            <p><span>6时</span><span>9</span></p>
            <p><span>7时</span><span>1</span></p>
        </div>
    </fieldset>
    <fieldset class="ChartFD"><legend>设备维护统计</legend>
        <div><label>当前报修设备数：</label><a>0人</a></div>
        <div id="Div1" class="LineStat" data-color="0" data-name="设备数" data-unit="台">
            <p><span>1月</span><span>1</span></p>
            <p><span>2月</span><span>0</span></p>
            <p><span>3月</span><span>0</span></p>
            <p><span>4月</span><span>2</span></p>
            <p><span>5月</span><span>0</span></p>
            <p><span>6月</span><span>0</span></p>
            <p><span>7月</span><span>0</span></p>
        </div>
    </fieldset>
</div>
</asp:Content>