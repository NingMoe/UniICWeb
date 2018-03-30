<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginForm.aspx.cs" Inherits="_Default"%>
<%@ Register Src="~/Modules/HeadInclude.ascx" TagPrefix="unifound" TagName="HeadInclude" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=ConfigConst.GCSysName %></title>
    <unifound:HeadInclude runat="server" ID="HeadInclude" />
    <style type="text/css">
        body {
            background: #f0f0f7;
        }
        .msg {
            width:300px;
            overflow:hidden;
        }
    </style>
</head>
<body>
<form id="Form1" runat="server">
    <div style="text-align:center; margin:15px auto;width:99%"><h1 style="font-size:40px;Font-style:initial;color:#3388bb"><%=ConfigConst.GCSysName %> </h1></div>  
    <div class="LoginContent">
        <div id="PanelContent">
            <div class="Box" theme="login.png" borderWeight="20px" width="400px" height="250px">
                <div class="title"><%=ConfigConst.GCSysName %> - µÇÂ¼</div>
                <div class="LoginForm">
                    <table>
                        <tr><td><div class="label" title="Ñ§¹¤ºÅ">ÕËºÅ£º</div><asp:TextBox class="input" ID="szLogonName" runat="server"></asp:TextBox><div class="clear"></div></td></tr>
                        <tr><td><div class="label" title="ÃÜÂë">ÃÜÂë£º</div><asp:TextBox class="input" ID="szPassword" runat="server" TextMode="Password"></asp:TextBox><div class="clear"></div></td></tr>
                        <tr><td><br /><asp:Button class="button enabled" title="" ID="Button_Logon" runat="server" OnClick="Button_Logon_Click" Text="µÇÂ¼" /> <input type="button" class="button enabled" value="Ö÷Ò³" onclick="location = '../default.aspx'"/></td></tr>
                        <tr><td><div class="msg"><asp:Label ID="MSG" runat="server" Text=""></asp:Label></div></td></tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</form>
</body>
<script language="javascript" type="text/javascript" >
    if(location.href.toUpperCase().indexOf("LOGINFORM.ASPX")<0)
    {
        location.href = "<%=MyVPath %>Pages/<%=ConfigConst.GCSysFrame%>/LoginForm.aspx";
    }
    $(function () {
        $('#PanelContent').UIBox({ width: "400px" });

        var g_bSubmited = false;
        $("#<%=Button_Logon.ClientID %>").click(
        function () {
            if (g_bSubmited) {
                return false;
            }
            g_bSubmited = true;
            $(this).tooltip({ content: "µÇÂ¼ÖÐ¡£¡£¡£" });
            $(this).tooltip("close");
            $(this).tooltip("open");
            $(this).removeClass("enabled");
            $(this).addClass("disabled");
        });
    });
</script>
</html>