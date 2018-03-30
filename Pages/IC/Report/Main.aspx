<%@ Page Language="C#" MasterPageFile="~/Templates/Main.master" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <div id="tabs">
        <ul>
           
             
            <li><a href="DeptusingStat.aspx"><%=ConfigConst.GCDeptName %>统计</a></li>
            <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DevUsingStat") > -1)
              {%>
            <li><a href="DevUsingStat.aspx">使用率统计</a></li>
             <%}%>
            <li><a href="IdentUsingStat.aspx">身份统计</a></li>
           <!-- <li><a href="DevClsUsingStat.aspx"><%=ConfigConst.GCClassName %>使用率统计</a></li>-->
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("DevKindUsingStat") > -1)
              {%>
            <li><a href="DevKindUsingStat.aspx"><%=ConfigConst.GCKindName %>使用率统计</a></li>
                  <%}%>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("PersonUsingStat") > -1)
              {%>
            <li><a href="PersonUsingStat.aspx">个人使用排行榜</a></li>
                  <%}%>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("LabUsingStat") > -1)
              {%>
            <li><a href="LabUsingStat.aspx"><%=ConfigConst.GCLabName %>使用率统计</a></li>
                  <%}%>
             <%if (nIsAdminSup == 1 || szAdminPar.IndexOf("RDevUsingTable") > -1)
              {%>
          <li><a href="RDevUsingTable.aspx">使用率统计图</a></li>
              <li><a href="RDiscore.aspx">违约率统计</a></li>
                <li><a href="ResvKindStat.aspx">预约/活动类型统计</a></li>
                <li><a href="ResvMoelStat.aspx">预约方式统计</a></li>
                  <%}%>
        </ul>
    </div>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>

