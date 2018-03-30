<%@ Page Language="C#"%>
<%Response.Redirect("index.aspx" + HttpContext.Current.Request.Url.Query); %>
