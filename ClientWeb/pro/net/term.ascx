<%@ Control Language="C#" AutoEventWireup="true" CodeFile="term.ascx.cs" Inherits="ClientWeb_pro_net_term" %>
<script>
    var term = {};
    term.year="<%=year%>";
    term.name="<%=name%>";
    term.status = "<%=status%>";
    term.start = "<%=start%>";
    term.end = "<%=end%>";
    term.firstweek = <%=firstweek%>;
    term.totalweek = <%=totalweek%>;
    term.secnum = <%=secnum%>;
    term.cts1 = "<%=cts1%>";
    term.cts1start = "<%=cts1start%>";
    term.cts1end = "<%=cts1end%>";
    term.cts2 = "<%=cts2%>";
    term.cts2start = "<%=cts2start%>";
    term.cts2end = "<%=cts2end%>";
    pro.term = term;
</script>