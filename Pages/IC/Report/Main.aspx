<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
           
             
            <li><a href="DeptusingStat.aspx"><%=ConfigConst.GCDeptName %>ͳ��</a></li>
            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DevUsingStat") > -1)
              {%>
            <li><a href="DevUsingStat.aspx">ʹ����ͳ��</a></li>
             <%}%>
            <li><a href="IdentUsingStat.aspx">���ͳ��</a></li>
           <!-- <li><a href="DevClsUsingStat.aspx"><%=ConfigConst.GCClassName %>ʹ����ͳ��</a></li>-->
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DevKindUsingStat") > -1)
              {%>
            <li><a href="DevKindUsingStat.aspx"><%=ConfigConst.GCKindName %>ʹ����ͳ��</a></li>
                  <%}%>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("PersonUsingStat") > -1)
              {%>
            <li><a href="PersonUsingStat.aspx">����ʹ�����а�</a></li>
                  <%}%>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("LabUsingStat") > -1)
              {%>
            <li><a href="LabUsingStat.aspx"><%=ConfigConst.GCLabName %>ʹ����ͳ��</a></li>
                  <%}%>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("RDevUsingTable") > -1)
              {%>
          <li><a href="RDevUsingTable.aspx">ʹ����ͳ��ͼ</a></li>
              <li><a href="RDiscore.aspx">ΥԼ��ͳ��</a></li>
                <li><a href="ResvKindStat.aspx">ԤԼ/�����ͳ��</a></li>
                <li><a href="ResvMoelStat.aspx">ԤԼ��ʽͳ��</a></li>
                  <%}%>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

