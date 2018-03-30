<%@ Page Language="C#" AutoEventWireup="true" CodeFile="create.aspx.cs" MasterPageFile="../pageMaster.master" Inherits="ClientWeb_pro_page_testitem_create" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
        <script src="../../../fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="../../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <script>
        $(function () {
            $(".upload_file").uploadFile();
        });
    </script>
    </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
<form class="form" role="form" runat="server">
    <div class="panel panel-default dialog" style="display:block;">
            <div class="panel-body list">
                <table>
                    <tbody>
                        <tr>
                            <td>实验项目名称 </td>
                            <td><asp:TextBox runat="server" CssClass="form-control test_name" ID="testName"></asp:TextBox></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                        <tr><td style="height:20px;"></td><td style="height:20px;color:grey">剩余可安排：<span style="color:red"><%=usable %></span> 学时</td></tr>
                        <tr>
                            <td>安排学时 </td>
                            <td><asp:TextBox runat="server" CssClass="form-control test_hour" ID="testHour"></asp:TextBox></td>
                            <td><span style="color: red;">*</span></td>
                        </tr>
                                                                        <tr>
                            <td>实验人数 </td>
                            <td><asp:TextBox runat="server" CssClass="form-control user_num" ID="userNum"></asp:TextBox></td>
                            <td>(选填)</td>
                        </tr>
                        <%if (GetConfig("showResvAttach")=="1")
                          { %>
                        <tr>
                            <td>实验报告模版</td>
                            <td colspan="2">
                                <div class="input-group" style="width:100%;">
                    
                                                            <div style="margin-bottom:5px;">
                            <input type="file" name="report_file_name" id="report_file_name" class="click"/>
                        </div>
                                    <div>
                            <input type="button" class="upload_file btn btn-info btn-sm click" file="report_file_name" value="上传" /><span class="cur_file_name text-primary" style="padding-left:5px;"></span>
                </div>
                                </div>
                            </td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>

            </div>
        </div>
            <div class="text-center">
                <button type="button" class="btn btn-info" runat="server" id="submit_test" onserverclick="submit_test_ServerClick">提交</button>
            <button type="button" class="btn btn-default dlg_page_close">返回</button>
        </div>
    </form>
</asp:Content>
