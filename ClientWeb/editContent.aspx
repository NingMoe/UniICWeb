<%@ Page Language="C#"%>
<%string tab = ConfigurationManager.AppSettings["proTab"];if (tab=="bsd-xl"){tab = "xcus/" + tab;} else{ tab = "pro/page";} %>
<%string szUrl = "/editContent.aspx" + HttpContext.Current.Request.Url.Query; %>
<%Response.Redirect(tab+szUrl);%>