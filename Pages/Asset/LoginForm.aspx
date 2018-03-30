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
<body style="background:url(../../themes/img/assertBG.jpg) fixed center center no-repeat;background-size:cover;width:100%">
<form id="Form1" runat="server">
  
    <div class="LoginContent">
        <div id="PanelContent" style="width:500px;">
             
            <div class="Box" theme="login.png" borderWeight="20px" width="550px" height="350px">
                 <div style="text-align:left;margin:1px 10px">
                     <img src="../../themes/img/syslogo.png" width="50px" height="50px" />
                     <img src="../../themes/img/deptname.png" width="225px" height="50px" />
        </div>
                
                <div class="LoginForm" style="margin-top:2px;padding-top:0px;">
                    <table>
                        <tr>
                            <td>
                                <div class="title" style="text-align:center;margin:1px auto; color:red;font-size:40px;font-family:'Microsoft YaHei';font-weight:1200">实验室及资产管理系统</div>
                            </td>
                        </tr>
                        <tr><td>账号：<asp:TextBox class="input" ID="szLogonName" runat="server"></asp:TextBox><div class="clear"></div></td></tr>
                        <tr><td>密码：<asp:TextBox class="input" ID="szPassword" runat="server" TextMode="Password"></asp:TextBox><div class="clear"></div></td></tr>
                        <tr><td><br /><asp:Button class="button enabled" title="" ID="Button_Logon" runat="server" OnClick="Button_Logon_Click" Text="登录" /> <input type="button" class="button enabled" value="主页" onclick="location = '../default.aspx'"/></td></tr>
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
        $('#PanelContent').UIBox({ width: "550px" });
   
        var g_bSubmited = false;
        $("#<%=Button_Logon.ClientID %>").click(
        function () {
            if (g_bSubmited) {
                return false;
            }
            g_bSubmited = true;
            $(this).tooltip({ content: "登录中。。。" });
            $(this).tooltip("close");
            $(this).tooltip("open");
            $(this).removeClass("enabled");
            $(this).addClass("disabled");
        });
    });
</script>
    <style>
        .UIBOX_title {
        height:25px;
        top:80px;
        }
        .UIBOX_boxct {
        width:550px;
        text-align:center;
        margin:0 auto;
        }
        
    </style>
</html>