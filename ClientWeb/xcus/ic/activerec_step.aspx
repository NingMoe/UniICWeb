<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activerec_step.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="modules/include.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title>活动申请</title>
    <meta content="" name="keywords" />
    <meta content="" name="description" />
    <link rel="stylesheet" href="style/css/main.css">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
    <script type="text/javascript" src="js/site.js"></script>
    <script type="text/javascript" src="js/datepicker/WdatePicker.js"></script>
    <Uni:include runat="server" />
    <script type="text/javascript">
        function isloginL() {
            if (!IsLogin()) {
                $("#logindialog").dialog('open');
                return false;
            }
            return true;
        }
        $(function () {
            isloginL();
        });
    </script>
</head>
<body>
    <div class="body">
        <Uni:sidebar runat="server" />
        <div class="content">
            <Uni:nav runat="server" />
            <div class="application">
                <h1>活动申1请</h1>
                <form id="form" runat="server" enctype="multipart/form-data">
                    <fieldset class="f1">
                        <table>
                            <tr>
                                <th>
                                    <p>空间说明</p>
                                </th>
                                <td>
                                    <div class="des"><%=szInfo %></div>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <th></th>
                                <td><font color="red">*必须填写内容</font></td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset class="f2">

                        <table>
                            <tr>
                                <th>
                                    <p>活动名称</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szActivityPlanName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>主办单位</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szHostUnit" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>承办单位</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szOrganizer" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>主持人</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szPresenter" runat="server" />
                                </td>
                            </tr>

                        </table>
                    </fieldset>
                    <fieldset class="f3">
                        <table>
                            <tr>
                                <th>
                                    <p>参与者要求</p>
                                </th>
                                <td>
                                    <textarea id="szDesiredUser" runat="server"></textarea>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset class="f4">
                        <table>
                            <tr>
                                <th>
                                    <p>联系人</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szContact" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>联系电话</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szHandPhone" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>电子邮箱</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szEmail" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset class="f5">
                        <table>
                            <tr>
                                <th>
                                    <p>最大限制人数</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" value="" id="dwMaxUsers" runat="server"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>最小限制人数</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" value="" id="dwMinUsers" runat="server"/>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset class="f6">
                        <table>
                            <tr>
                                <th>
                                    <p>申请加入截至日</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" onclick="WdatePicker({ readOnly: true })" id="dwEnrollDeadline" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>发布日期</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" onclick="WdatePicker({ readOnly: true })" id="dwPublishDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>活动日期</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" onclick="WdatePicker({ readOnly: true })" id="dwActivityDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>开始时间</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="dwBeginTime" onclick="WdatePicker({ readOnly: true, dateFmt: 'HH:mm' })" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>结束时间</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="dwEndTime" onclick="WdatePicker({ readOnly: true, dateFmt: 'HH:mm' })" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset class="f7">
                        <table>
                            <tr>
                                <th>
                                    <p>活动简介</p>
                                </th>
                                <td>
                                    <textarea id="szIntroInfo" runat="server"></textarea>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <table>
                            <tr>
                                <th>
                                    <p style="text-indent: 0px;">活动海报</p>
                                </th>
                                <td>
                                    <!--<textarea id="szActivityPlanURL"></textarea>-->
                                    <input type="file" id="szActivityPlanURL" style="cursor: pointer;" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <table>
                            <tr>
                                <th>
                                    <p style="text-indent: 0px;">申请材料<span style="color: red;">*</span></p>
                                </th>
                                <td>
                                    <input id="InputFile" style="width: 400px; cursor: pointer;" type="file" runat="server" />
                                    <%--<asp:Button ID="UploadButton" runat="server" Text="上传申请材料" OnClick="UploadButton_Click" OnClientClick="return isloginL();" />--%>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div class="submitarea">
                        <asp:Button ID="btnSubmint" class="input_submit" runat="server" OnClick="btnSubmint_Click" OnClientClick="return isloginL();" />
                    </div>
                </form>
            </div>
            <div class="copyright">版权说明</div>
        </div>

    </div>
    <Uni:dialog runat="server" />
</body>
</html>

