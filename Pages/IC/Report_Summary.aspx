<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Report_Summary.aspx.cs" Inherits="Sub_Summary"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="tabs-summary">
    <h2>ͳ������</h2>
    <fieldset class="ChartFD"><legend>ʵ�鰲��ͳ��</legend>
        <div><label>�ܰ���������</label><a>30��</a>�� <label>δ����������</label><a>10��</a>�� <label>�Ѱ���������</label><a>16��</a>�� <label>��ѧʱ��</label><a>4��</a>�� <label>�Ѱ���ѧʱ��</label><a>4��</a>�� <label>δ����ѧʱ��</label><a>4��</a></div>
        <div id="ResvStat" class="BarStat" data-color="3">
            <h1><span>--------</span><strong>�Ѱ���</strong><strong>δ����</strong></h1>
            <p><span>����ͳ��</span><strong>30</strong><strong>2</strong></p>
            <p><span>ѧʱͳ��</span><strong>60</strong><strong>8</strong></p>
        </div>
    </fieldset>
    <fieldset class="ChartFD"><legend>�����ϻ�</legend>
        <div><label>��ǰ��������</label><a>30��</a></div>
        <div id="FreeUserStat" class="LineStat" data-color="0" data-name="������" data-unit="��">
            <p><span>1ʱ</span><span>7</span></p>
            <p><span>2ʱ</span><span>6</span></p>
            <p><span>3ʱ</span><span>9</span></p>
            <p><span>4ʱ</span><span>14</span></p>
            <p><span>5ʱ</span><span>26</span></p>
            <p><span>6ʱ</span><span>9</span></p>
            <p><span>7ʱ</span><span>1</span></p>
        </div>
    </fieldset>
    <fieldset class="ChartFD"><legend>�豸ά��ͳ��</legend>
        <div><label>��ǰ�����豸����</label><a>0��</a></div>
        <div id="Div1" class="LineStat" data-color="0" data-name="�豸��" data-unit="̨">
            <p><span>1��</span><span>1</span></p>
            <p><span>2��</span><span>0</span></p>
            <p><span>3��</span><span>0</span></p>
            <p><span>4��</span><span>2</span></p>
            <p><span>5��</span><span>0</span></p>
            <p><span>6��</span><span>0</span></p>
            <p><span>7��</span><span>0</span></p>
        </div>
    </fieldset>
</div>
</asp:Content>