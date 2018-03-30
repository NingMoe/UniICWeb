<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upload.aspx.cs" MasterPageFile="../pageMaster.master" Inherits="ClientWeb_pro_page_testitem_upload" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
        <script src="../../../fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
    <link rel="stylesheet" href="../../../fm/jquery-ui/bootstrap/css/bootstrap.css" />
    <script>
        $(function () {

            $(".upload_file").uploadFile({ f_name: "<%=fileName%>" });
        });
    </script>
    </asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContent">
<form class="form" role="form" runat="server" style="margin-top:30px;">
    <div class="panel panel-default dialog" style="display:block;">
            <div class="panel-body list">
                        <table>
                    <tbody>
                        <tr>
                            <td>实验报告模版</td>
                            <td colspan="2">
                                <div class="input-group" style="width:100%;">
                    
                                                            <div style="margin-bottom:25px;">
                            <input type="file" name="report_file_name" id="report_file_name" class="click"/>
                        </div>
                                    <div>
                            <input type="button" class="upload_file btn btn-info btn-sm click" file="report_file_name" value="上传" /><span class="cur_file_name text-primary" style="padding-left:5px;"></span>
                </div>
                                </div>
                            </td>
                        </tr>
                        </tbody>
                            </table>
            </div>
        </div>
            <div class="text-center">
                <button type="button" class="btn btn-info" runat="server" id="submit_test" onserverclick="submit_test_ServerClick">保存</button>
            <button type="button" class="btn btn-default dlg_page_close">取消</button>
        </div>
    </form>
</asp:Content>