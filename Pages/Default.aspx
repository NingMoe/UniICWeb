<%@ Page Language="C#"%>
<%string szSpliet = ""; string szUrlPath = HttpContext.Current.Request.Url.Query; string szUrl = ""; %>
<%if (szUrlPath != null && szUrlPath.StartsWith("?")) { szSpliet = ""; } else { szSpliet = "?"; }%> 
<%  /*  
    LoginUseInfo loginUserInfo = (LoginUseInfo)Session["LoginUseInfo"] ;
      if (loginUserInfo != null)
      {
          Response.Write(loginUserInfo.szLogoName + "::passwd=" + loginUserInfo.szPassword);
          Response.End();
      }*/
      %>
<%szUrl = ConfigConst.GCSysFrame + "/LoginForm.aspx" + szSpliet + szUrlPath; %>

<%if (szUrlPath != null && szUrlPath.IndexOf("logout") > -1) { szUrl += "&op=logout"; } %>
<%Response.Redirect(szUrl);%>
