<%@ Page Language="C#"%>
<%string proTab = ConfigurationManager.AppSettings["proTab"]; 
      Response.Redirect(proTab + "/Default.aspx" + HttpContext.Current.Request.Url.Query); 
      %>