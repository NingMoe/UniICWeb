<%@ Page Language="C#"%>
<%string szSpliet = ""; string szUrlPath = HttpContext.Current.Request.Url.Query; string szUrl = ""; %>
<%if (szUrlPath != null && szUrlPath.StartsWith("?")) { szSpliet = ""; } else { szSpliet = "?"; }%> 
<%szUrl = ConfigConst.GCSysFrame + "/LoginForm.aspx" + szSpliet + szUrlPath; %>
<%Response.Redirect(szUrl);%>
