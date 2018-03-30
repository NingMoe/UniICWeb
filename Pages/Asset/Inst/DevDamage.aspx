<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevDamage.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>���޼�¼</h2>
      <input type="hidden" name="dwDevID" id="dwDevID" />
        <div class="toolbar">
              <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>����</a>
                 <a href="devListUse.aspx" id="devListUse">�ʲ����</a>
                    <a href="DevDamage.aspx" id="DevDamage">�豸����</a>
                    <a href="DevUnUse.aspx" id="DevUnUse">�豸���ϼƻ�</a>
                        <a href="DevUnUseDetail.aspx" id="DevUnUseDetail">�豸������ϸ</a>
                </div>
             
            <div id="btnDevKind" class="FixBtn"><a>����<%=ConfigConst.GCDevName %></a></div>
            <div class="tb_btn">               
            </div>
        </div>  

         <div  class="tb_infoInLine"  style="margin-top:30px;margin-bottom:10px;">
            <table style="width:99%">
               <tr style="height:45px">
                   <th>��ʼ����</th>
                   <td>
                    <input type="text" name="dwStartDate" id="dwStartDate" runat="server" />   
                   </td>
                   <th>��ʼ����</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" runat="server"  /></td>
                    <th>�ʲ����</th>
                   <td><input type="text" name="dwDevSN" id="dwDevSN" /></td>
                    <th>״̬:</th>
                    <td><select name="dwStatus" id="dwStatus">
                        <%=szStatus %>
                        </select></td>
                  <th><input type="submit" id="btn" value="��ѯ" /></th>
               </tr>
           </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szAssertSN">�ʲ����</th>
                        <th name="szDevName">�ʲ�����</th>
                        <th><%=ConfigConst.GCDevName %>����<%=ConfigConst.GCDeptName %></th>
                        <th name="dwDamageDate">��ʱ��</th>
                         <th>������</th>
                        <th name="dwStatus">״̬</th>
                        <th>ά��˵��</th>
                        <th name="dwRepareDate">�޸�ʱ��</th>
                        <th name="szRepareCom">ά�޵�λ</th>
                        <th>ά�޵�λ�绰</th>
                        <th name="dwRepareCost">ά�޷���</th>    
                        <th name="szManName">������Ա</th>     
                        <th width="25px">����</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
           
            $(function () {
                var tabl = $(".UniTab").UniTab();
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });
                $("#btn").button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker();
                $(".OPTD").html('<div class="OPTDBtn">\
                            <a  class="setDevkindBtn" href="#" title="�޸�����"><img src="../../../themes/iconpage/edit.png"/></a>\
                            <a  class="del" href="#" title="��������"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "175", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#dwDevSN").autocomplete({
                    source: "../data/searchdevice.aspx?Type=assertsn",
                    minLength: 0,
                    select: function (event, ui) {
                        if (ui.item) {
                            if (ui.item.id && ui.item.id != "") {
                                $("#dwDevSN").val(ui.item.szAssertSN);
                                $("#dwDevID").val(ui.item.id);
                            }
                        }
                        return false;
                    },
                    response: function (event, ui) {
                        if (ui.content.length == 0) {
                            ui.content.push({ label: " δ�ҵ������� " });
                        }
                    }
                }).blur(function () {
                    if ($(this).val() == "") {
                        $("#dwDevID").val("");
                    } else {

                    }
                }).click(function () {
                    $("#dwDevSN").autocomplete("search", "");
                });
                $("#btnDevKind").click(function () {
                    $.lhdialog({
                        title: '<%=ConfigConst.GCDevName%>����',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevDemage.aspx?op=new'
                    });
                });
                $(".InfoDevKindBtn").click(function () {                    
                    var dwDevKind = $(this).parents("tr").children().first().children().first().val();
                    $.lhdialog({
                        title: '����',
                        width: '760px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:../../ueditor/default.aspx?id=' + dwDevKind + "&type=DevKindInfo"
                    });
                });
                $(".setDevkindBtn").click(function () {
                    var dwDevKind = $(this).parents("tr").children().first().data("id");
                    var devid = $(this).parents("tr").children().first().data("devid");
                    $.lhdialog({
                        title: '�޸�����',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setDevDemage.aspx?op=set&dwSID=' + dwDevKind + '&devid=' + devid
                    });
                });
                $(".del").click(function () {
                    var sid = $(this).parents("tr").children().first().data("id");
                    ConfirmBox("ȷ����������?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + sid);
                }, '��ʾ', 1, function () { });
                 });
                $(".ListTbl").UniTable({ ShowCheck: false, HeaderIndex: true });
            });
            function SelectAllCheck() {
                $("#ListTbl :checkbox").attr("checked", "checked");
                $("#ListTbl :checkbox").parent().parent().addClass("selected");
            }
        </script>
    </form>
</asp:Content>
