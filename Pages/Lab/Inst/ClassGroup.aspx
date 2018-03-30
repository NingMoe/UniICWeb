<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ClassGroup.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�γ̰�</h2>
        <div class="toolbar">
            <div class="tb_info"></div>
            <div class="FixBtn">
                <a id="btnImportGroup">����γ̰�</a>
               <!-- <a id="btnNewGroup">�½��γ̰�</a></div>-->
            <div class="tb_btn">
            </div>
        </div>
              </div>
         <div style="margin-top:5px;margin-bottom:5px; width: 99%;">
            <div style="text-align: center">
                <table id="tbSearch" style="margin: 0 auto; border: 1px solid #d1c1c1;width:99%">                       
               <tr>
                    <th style="text-align:right">ѧ�ڣ�</th>
                    <td style="text-align:left"><select id="dwDeadLine" name="dwDeadLine"><%=szYearTerm %></select></td>
                  
                    <th style="text-align:right">�༶���ƣ�</th>
                    <td style="text-align:left"><input type="text" name="szName" id="szName" /> </td>
                
                     <td style="text-align:center">
                        <input type="submit" value="��ѯ" id="sub" />
                    </td>
                </tr>
          
            </table>
                </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szName">����</th>
                        <th>��ע</th>
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
            #tbSearch {
                border-width: 1px;
                border-color: #d1c1c1;
                cursor: hand;
            }

            .thCenter {
                text-align: center;
            }

            #tbSearch td {
                font-family: "Trebuchet MS",Monospace,Serif;
                font-size: 12px;
                border-style: solid;
                border-width: 1px;
                height:35px;
            }
             #tbSearch th {
                font-family: "Trebuchet MS",Monospace,Serif;
                font-size: 12px;
                border-style: solid;
                border-width: 1px;
            }
            td input {
              
            }
        </style>
        <script type="text/javascript">
            $(function () {
                $("#sub").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a class="setGroupMember" title="�޸ĳ�Ա"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="setGroup" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a class="delGroup"  href="#" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnImportGroup").button().click(function () {
                    $.lhdialog({
                        title: '����γ̰�',
                        width: '750px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/importClassGroup.aspx?op=set'
                    });
                });
                $(".setGroupMember").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸ĳ�Ա',
                        width: '660px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?op=set&id=' + dwLabID
                    });
                });
                $(".setGroup").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '�޸�',
                        width: '660px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setGroup.aspx?op=set&dwID=' + dwLabID
                    });
                });
                $(".delGroup").click(function () {
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("ȷ��ɾ��?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                }, '��ʾ', 1, function () { });
            });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '660px',
                    height: '300px',
                    lock: true,
                    content: 'url:Dlg/NewLab.aspx?op=new'
                });
            });
            //$(".ListTbl").UniTable();

        });
        </script>
    </form>
</asp:Content>
