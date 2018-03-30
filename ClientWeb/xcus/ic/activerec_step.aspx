<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activerec_step.aspx.cs" Inherits="Page_" %>

<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx" %>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx" %>
<%@ Register TagPrefix="Uni" TagName="include" Src="modules/include.ascx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8" />
    <title>�����</title>
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
                <h1>���1��</h1>
                <form id="form" runat="server" enctype="multipart/form-data">
                    <fieldset class="f1">
                        <table>
                            <tr>
                                <th>
                                    <p>�ռ�˵��</p>
                                </th>
                                <td>
                                    <div class="des"><%=szInfo %></div>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <th></th>
                                <td><font color="red">*������д����</font></td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset class="f2">

                        <table>
                            <tr>
                                <th>
                                    <p>�����</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szActivityPlanName" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>���쵥λ</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szHostUnit" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>�а쵥λ</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szOrganizer" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>������</p>
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
                                    <p>������Ҫ��</p>
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
                                    <p>��ϵ��</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szContact" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>��ϵ�绰</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="szHandPhone" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>��������</p>
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
                                    <p>�����������</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" value="" id="dwMaxUsers" runat="server"/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>��С��������</p>
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
                                    <p>������������</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" onclick="WdatePicker({ readOnly: true })" id="dwEnrollDeadline" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>��������</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" onclick="WdatePicker({ readOnly: true })" id="dwPublishDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>�����</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" onclick="WdatePicker({ readOnly: true })" id="dwActivityDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>��ʼʱ��</p>
                                </th>
                                <td>
                                    <input type="text" class="input_text" id="dwBeginTime" onclick="WdatePicker({ readOnly: true, dateFmt: 'HH:mm' })" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    <p>����ʱ��</p>
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
                                    <p>����</p>
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
                                    <p style="text-indent: 0px;">�����</p>
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
                                    <p style="text-indent: 0px;">�������<span style="color: red;">*</span></p>
                                </th>
                                <td>
                                    <input id="InputFile" style="width: 400px; cursor: pointer;" type="file" runat="server" />
                                    <%--<asp:Button ID="UploadButton" runat="server" Text="�ϴ��������" OnClick="UploadButton_Click" OnClientClick="return isloginL();" />--%>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <div class="submitarea">
                        <asp:Button ID="btnSubmint" class="input_submit" runat="server" OnClick="btnSubmint_Click" OnClientClick="return isloginL();" />
                    </div>
                </form>
            </div>
            <div class="copyright">��Ȩ˵��</div>
        </div>

    </div>
    <Uni:dialog runat="server" />
</body>
</html>

