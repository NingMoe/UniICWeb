<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
         <!-- 
            <li><a href="RDevList.aspx">��ѧ���������豸��</a></li>
            <li><a href="RDevChange.aspx" style="font-size: 12px" title="��ѧ���������豸�����䶯�����">��ѧ���������豸����..</a></li>
            <li><a href="RBigDev.aspx">���������豸��</a></li>
            <li><a href="RTestItemStat.aspx">��ѧʵ����Ŀ��</a></li>
            <li><a href="RStaff.aspx">ר��ʵ������Ա��</a></li>
            <li><a href="RLabInfo.aspx">ʵ���һ��������</a></li>
            <li><a href="RLabCostInfo.aspx">ʵ���Ҿ��������</a></li>
            <li><a href="RLABSUMMARY.aspx" style="font-size: 12px" title="�ߵ�ѧУʵ�����ۺ���Ϣ��һ">�ߵ�ѧУʵ������..��һ</a></li>
            <li><a href="RLABSUMMARYII.aspx" style="font-size: 12px" title="�ߵ�ѧУʵ�����ۺ���Ϣ���">�ߵ�ѧУʵ������..���</a></li>
          
             
            <li><a href="DevFarTotal.aspx"><%=ConfigConst.GCDevName %>���ѷ���ͳ��</a></li>
            <li><a href="DevFarDetail.aspx"><%=ConfigConst.GCDevName %>���ѷ�����ϸ</a></li>
            <li><a href="DevRtResvTotal.aspx"><%=ConfigConst.GCDevName %>����ͳ��</a></li>
            <li><a href="DevRtResvDetail.aspx"><%=ConfigConst.GCDevName %>������ϸ</a></li>  
                   --> 
            <li><a href="DevUsingStat.aspx"><%=ConfigConst.GCDevName %>ʹ����ͳ��</a></li>
           <!-- <li><a href="DevClsUsingStat.aspx"><%=ConfigConst.GCClassName %>ʹ����ͳ��</a></li>-->
            <li><a href="DevKindUsingStat.aspx"><%=ConfigConst.GCKindName %>ʹ����ͳ��</a></li>
            <li><a href="PersonUsingStat.aspx">����ʹ�����а�</a></li>
            <li><a href="LabUsingStat.aspx"><%=ConfigConst.GCLabName %>ʹ����ͳ��</a></li>
          <li><a href="RDevUsingTable.aspx"><%=ConfigConst.GCDevName %>ʹ����ͳ��ͼ</a></li>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

