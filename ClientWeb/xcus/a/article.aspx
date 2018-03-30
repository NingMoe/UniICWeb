<%@ Page Language="C#" AutoEventWireup="true" CodeFile="article.aspx.cs" Inherits="ClientWeb_xcus_all_article" %>
<html>
    <body>
        <style>
        /*#divContent table { border-collapse: collapse; }
        #divContent th, td { border-width: 1px; border-style: solid; }*/
        #divContent .info_date {text-align:right;font-size:12px;height:14px;line-height:14px;margin:3px;color:#999;border-bottom:1px solid #eee;}
        </style>
        <div class="click btn_back" style="display:<%=isBack%>" onclick="uni.hr.back();"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<%=Translate("返回")%></div>
        <div runat="server" id="divContent" class="article_content"></div>
    </body>
</html>
