<%@ Control Language="C#" AutoEventWireup="true" CodeFile="acc.ascx.cs" Inherits="WebUserControl" %>
    <script type="text/javascript">
        var acc = {};
        acc.id = "<%=proacc.id %>";
        acc.accno = "<%=proacc.accno %>";
        acc.name = "<%=proacc.name %>";
        acc.phone = $.trim("<%=proacc.phone %>");
        acc.msn = "<%=proacc.msn %>";
        acc.email = $.trim("<%=proacc.email%>");
        acc.ident = "<%=proacc.ident %>";
        acc.dept = "<%=proacc.dept %>";
        acc.tutor = "<%=proacc.tutor %>";
        acc.tutorid = "<%=proacc.tutorid %>";
        acc.tsta = "<%=proacc.tsta %>";
        acc.rtsta = "<%=proacc.rtsta %>";
        acc.score = "<%=proacc.score %>";
        acc.credit = <%=credit%>;
        acc.pro = "<%=proacc.pro %>";
        acc.role = "<%=proacc.role%>";
        pro.acc = acc;
    </script>
