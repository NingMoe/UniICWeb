<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ICINTROClass.aspx.cs" Inherits="Sub_Device" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�ռ���������</h2>
        <div class="toolbar">
            <div class="tb_info">
                 <div class="UniTab" id="tabl">                
                <a  href="ICINTRONotice.ASPX" id="ICINTRONotice">֪ͨ</a>                
                <a  href="ICINTROClass.ASPX" id="ovclass">��������</a>                
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
                    <a class="setIntro" href="#" title="�ռ����"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a class="kind_rule" href="#" title="ԤԼ��֪"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a class="setslide" href="#" title="�ռ����"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="sethard" href="#" title="Ӳ������"><img src="../../../themes/icon_s/17.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".kind_rule").click(function () {
                    var classid = $(this).parents("tr").children().first().data("classid");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + classid + "&type=rule")
                });
                
                $(".setIntro").click(function () {
                    var classid = $(this).parents("tr").children().first().data("classid");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + classid + "&type=intro")
                });
                $(".setslide").click(function () {
                    var classid = $(this).parents("tr").children().first().data("classid");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + classid + "&type=slide")
                });
                $(".sethard").click(function () {
                    var classid = $(this).parents("tr").children().first().data("classid");
                    window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + classid + "&type=hard")
                });                                   
            });
           
        </script>
    </form>
</asp:Content>
