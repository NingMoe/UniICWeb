<%@ Page Language="C#"%>
<%string szUrl = "http://115.236.69.115:8188/unitestwx/pay.aspx" + HttpContext.Current.Request.Url.Query; %>
<%Response.Redirect(szUrl);%>
