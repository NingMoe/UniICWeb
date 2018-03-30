<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetFeeDetailList.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <script src="<%=MyVPath %>themes/js/MainJScript.js" charset="utf-8"  type="text/javascript" ></script>
    <form id="formAdvOpts" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <input type="hidden" id="detail" name="detail" />
        <input type="hidden" id="FeeSN" name="FeeSN" runat="server" />
        <div>
            <div class="content">
                <div style="margin:10px auto;width:90%;">
                 <button type="button" id="btnAdd">�����շ����</button>
                    </div>
                <div style="margin:10px auto;width:90%;">
                <table class="ListTbl">
                    <thead>
                        <tr>
                            <th>�շ����</th>
                            <th>����(��0.01Ԫ)</th>
                            <th>�Ʒ�ʱ��(����)</th>
                            <th>�շѷ�ʽ</th>
                            <th>�Ƿ���Ҫ���</th>
                            <th style="width:25px">����</th>
                        </tr>
                    </thead>
                    <tbody id="ListTbl">
                      <%=m_szOut %>
                    </tbody>
                </table>
                    </div>
            </div>

            <div>
                <table style="margin: 10px auto">
                    <tr>
                        <td class="btnRow">
                            <button type="submit" id="OK">ȷ��</button>
                            <button type="button" id="Cancel">ȡ��</button></td>
                    </tr>
                </table>
            </div>

        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .ui-accordion .ui-accordion-header
        {
            padding-top: 2px !important;
            padding-bottom: 2px !important;
            height: 25px;
        }

        .ui-accordion h3.ui-accordion-header
        {
            background: #bdd9f2;
        }

        .ui-accordion h3.newTemp
        {
            background: #f2d9bd;
        }

        .accHeadText
        {
            float: left;
            line-height: 25px;
            margin-right: 10px;
        }

        .accHeadOP
        {
            float: right;
        }

        .tblBtn
        {
            text-align: center;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            var icons = {
                header: "ui-icon-plusthick",
                activeHeader: "ui-icon-circle-arrow-s"
            };
            $("#btnSaveTemp").button();
            $("#OK").button();
            $("#btnAdd").button();
            $("#Cancel").button().click(Dlg_Cancel);
            $("#accordion").accordion({
                collapsible: true,
                icons: icons
            });
            $(".accDel").button({ icons: { primary: "ui-icon-trash" }, text: true });
            setTimeout(function () {
            }, 1);
            $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="set" href="#" title="�޸�"><img src="../../../../themes/iconpage/edit.png"/></a>\
                            <a href="#" class="del" title="ɾ��"><img src="../../../../themes/iconpage/del.png""/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "defaultbg.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".set").click(function () {
                var FeeType = $(this).parents("tr").children().first().attr("data-id");
                var FeeSN=$("input[id='<%=FeeSN.ClientID%>']").val();    
                $.lhdialog({
                    title: '�޸�',
                    width: '720px',
                    height: '250px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:SetFeeDetail.aspx?op=set&FeeType=' + FeeType + '&FeeSN=' + FeeSN
                });
            });
            $("#btnAdd").click(function () {             
                var FeeSN = $("input[id='<%=FeeSN.ClientID%>']").val();    
                $.lhdialog({
                    title: '�½��շ����',
                    width: '720px',
                    height: '250px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:NewFeeDetail.aspx?op=new&FeeSN=' + FeeSN
                });
            });
            $(".del").click(function () {
                var FeeSN = $("input[id='<%=FeeSN.ClientID%>']").val();
                var FeeType = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("ȷ��ɾ��?", function () {
                    ShowWait();
                    $.ajax({
                        data: { "op": 'del', "FeeType": FeeType, "FeeSN": FeeSN },
                        success: function (data) {
                            $("#<%=formAdvOpts.ClientID%>").submit();
                        }
                    });
                }, '��ʾ', 1, function () { });
                
              
             });          
        });
    </script>
</asp:Content>
