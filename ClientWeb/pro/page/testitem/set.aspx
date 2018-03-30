<%@ Page Language="C#" AutoEventWireup="true" CodeFile="set.aspx.cs" MasterPageFile="../pageMaster.master" Inherits="ClientWeb_pro_page_testitem_set" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
        <script src="../../../fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="../../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <script>
        
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
                        <tr>
                            <td>安排学时 </td>
                            <td><asp:TextBox runat="server" CssClass="form-control test_hour" ID="testHour"></asp:TextBox></td>
                            <td>/小时<span style="color: red;">*</span></td>
                        </tr>
                                                <tr>
                            <td>实验人数 </td>
                            <td><asp:TextBox runat="server" CssClass="form-control user_num" ID="userNum"></asp:TextBox></td>
                            <td></td>
                        </tr>
                                                                        <tr>
                            <td>实验类别 </td>
                            <td><select runat="server" class="form-control test_class" id="testClass">
                                <option value="0">其它</option>
                                </select></td>
                            <td></td>
                        </tr>
                                                                        <tr>
                            <td>实验类型 </td>
                            <td>
                                <select runat="server" class="form-control test_kind" id="testKind">
                                <option value="0">其它</option>
                                </select>
                            </td>
                            <td></td>
                        </tr>
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