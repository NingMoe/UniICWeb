<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
      <form id="Form1" runat="server">
        <div class="formtitle">内容编辑</div>
         <input type="hidden" runat="server" id="editContent" />
            <input type="hidden" runat="server" id="infoId" />
            <input type="hidden" runat="server" id="infoType" />
        <div class="formtable">
            <table>
             <tr>
                 <td style="text-align:center">     
                  <script id="editor" type="text/plain" style="width: 700px; height: 320px; margin: 0 auto;">
                </script>
                     </td>
          </tr>
                <tr>
                  <td style="text-align:center">                      
                       <button type="button" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                   
                </tr>
            </table>
        </div>
    </form> 
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="<%=MyVPath %>Ueditor/ueditor.all.js" type="text/javascript"></script>
    <script src="<%=MyVPath %>Ueditor/ueditor.config.js" type="text/javascript"></script>
    <script src="<%=MyVPath %>Ueditor/usercustom.js" type="text/javascript"></script>
    <script src="<%=MyVPath %>Ueditor/showmsg.js" type="text/javascript"></script>
    <style type="text/css">
        .imgClass
        {
            width: 40px;
            height: 40px;
        }
    </style>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript">
        $(function (){
            var edit = UE.getEditor('editor');
            var con = $("#<%=editContent.ClientID %>").val();
            if (con != null || con != "") {
                edit.ready(function () {
                    this.setContent(con);
                })
            }
            $("#OK").button().click(function () {
                var id = $("#<%=infoId.ClientID %>").val();
                var type = $("#<%=infoType.ClientID %>").val();
                var path = $("#<%=infoType.ClientID %>").val();
                ajaxSubmit(id, type, path);
                var id = $("#<%=infoId.ClientID %>").val();
                var type = $("#<%=infoType.ClientID %>").val();
                var path = $("#<%=infoType.ClientID %>").val();

                var postData = "\"" + UE.getEditor("editor").getContent() + "\"";
                $.ajax({
                    type: "POST",
                    url: "../Ueditor/net/editContent.ashx",
                    dataType: 'json',
                    data: { "content": postData, "id": id, "type": type },
                    success: function (msg) {
                        if (msg != null && msg.responseText == "ok") {
                            MessageBox('保存成功', '提示', 3);
                        }
                        else {
                            alert("保存失败！");
                            return "error";
                        }
                    },
                    error: function (er) {
                        if (er != null && er.responseText == "ok") {
                            {
                                MessageBox('保存成功', '提示', 3, function () { Dlg_Cancel() });
                            }

                        }
                        else {

                        }
                    }
                });
            });
            $("#Cancel").button().click(Dlg_Cancel);
        });
       
    </script>
</asp:Content>
