<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ICINTRODevice.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�ռ���������</h2>
        <div class="toolbar">
            <div class="tb_info">
                  <div class="UniTab" id="tabl">  
                <a href="ICINTRONotice.ASPX" id="notice">֪ͨ</a>        
                <a href="<%=DevIn%>.ASPX" id="ovclass">�ռ���������</a>                
                <a href="ICINTRO.ASPX" id="ov">�ռ����</a>
                </div>
            </div>         
        </div>
        <div>
            <div class="tb_infoInLine" style="margin: 5px 0px">                
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>����</th>                      
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <style>
            .tb_infoInLine table tr th {
                text-align: center;
            }

            .tb_infoInLine table tr td input {
                margin-left: 5px;
            }

            .tb_infoInLine table tr td select {
                margin-left: 5px;
            }
        </style>
        <script type="text/javascript">
            $(function () {
                var tabl = $(".UniTab").UniTab();
                $(".opt").css({ width: "150px" }).change(function (userChanged) {
                    if (TabReload && userChanged) {
                        TabReload($(this).parents("form").serialize());
                    }
                });
                $("#btn").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a class="setIntro" href="#" title="<%=ConfigConst.GCRoomName%>����"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a class="setslide" href="#" title="����λ��"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="sethard" href="#" title="ʹ�ð���"><img src="../../../themes/icon_s/17.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
              
                $(".setIntro").click(function () {
                    var classid = $(this).parents("tr").children().first().data("id");
                    window.open("../../clientweb/md/ueditor/net/editContent.aspx?h=500&w=975&id=" + classid + "&type=sy1")
                });
                $(".setslide").click(function () {
                    var classid = $(this).parents("tr").children().first().data("id");
                    window.open("../../clientweb/md/ueditor/net/editContent.aspx?h=500&w=975&sy=2&id=" + classid + "&type=sy2")
                });
                $(".sethard").click(function () {
                    var classid = $(this).parents("tr").children().first().data("id");
                    window.open("../../clientweb/md/ueditor/net/editContent.aspx?h=500&w=975&sy=3&id=" + classid + "&type=sy3")
                });                                   
            });
           
        </script>
    </form>
</asp:Content>
