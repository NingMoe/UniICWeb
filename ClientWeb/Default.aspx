<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ClientWeb_Default"%>
<%string proTab = ConfigurationManager.AppSettings["proTab"];
  if (proTab == "" || proTab == "0")
      Response.Redirect("update.aspx");
  else
  {
      string qurey=HttpContext.Current.Request.Url.Query;
      string t=(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds.ToString();//(string.IsNullOrEmpty(qurey)?"?_t="+t:qurey+"&_t="+t)
      Response.Redirect(((ToUInt(GetConfig("availMobile")) & 3) > 0 && isMobile ? "m/" : "xcus/") + proTab + "/Default.aspx" +qurey);
  }%>
