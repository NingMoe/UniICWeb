<%@ Page Language="C#" AutoEventWireup="true" CodeFile="space_Resv.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<%@ Register TagPrefix="Uni" TagName="calendar" Src="modules/calendar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="modules/include.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title>预约空间</title>
    <meta content="" name="keywords" />
    <meta content="" name="description" />
    <link rel="stylesheet" href="style/css/main.css">
    <link rel="stylesheet" href="style/css/calendar.css">

    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
    <script type="text/javascript" language="javascript" src="js/datepicker/WdatePicker.js"></script>

    <Uni:include runat="server" />
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.yellow.css" rel='stylesheet' />
</head>
<body>
    <div class="body">
        <Uni:sidebar runat="server" />
        <div class="content">
            <Uni:nav runat="server" />

            <div class="reservation">
                <h1>预约空间</h1>
                <form id="Form1" runat="server">
                    <div>
                        <div style="text-align: left; width: 500px">
                            <table style="width: 700px;">
                                <tr>
                                    <td style="width: 20%; height: 36px;">
                                        <asp:DropDownList ID="ddlDevClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDevClass_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 50%">
                                        <div style="background-color: rgb(13, 139, 213); width: 60px; text-align: center; float: right;color:#fff;">繁忙时段</div>
                                    </td>
                                    <td style="width: 30%"><span style="margin-left:20px;">请点击空闲时段预约</span></td>
                                </tr>
                            </table>
                        </div>
                        <div style="text-align: left;">
                            <Uni:calendar runat="server" ID="MyCld"/>
                        </div>
                    </div>
                </form>
            </div>
            <div class="copyright">版权说明</div>
        </div>
    </div>
    <Uni:dialog runat="server" />
    <script>
        $("select.tab").change(function () {
            $("table.orderstatus").hide();
            $("table.orderstatus[DevId=" + this.value + "]").show();
        });
    </script>
</body>
</html>

