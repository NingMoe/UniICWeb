<%@ Page Language="C#"%>
<%string szUrl = "ClientWeb/default.aspx" + HttpContext.Current.Request.Url.Query; %>
<%Response.Redirect(szUrl);%>
