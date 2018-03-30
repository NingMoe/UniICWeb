<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevListUse.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�ʲ����</h2>
 
           <input type="hidden" id="dwDevID" name="dwDevID" />
         <div class="toolbar">
          
               <div class="tb_info">
                    <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>����</a>
                 <a href="devListUse.aspx" id="devListUse">�ʲ����</a>
                    <a href="DevDamage.aspx" id="DevDamage">�豸����</a>
                    <a href="DevUnUse.aspx" id="DevUnUse">�豸���ϼƻ�</a>
                        <a href="DevUnUseDetail.aspx" id="DevUnUseDetail">�豸������ϸ</a>
                </div>
                   </div>
               </div>
              
        <div  class="tb_infoInLine"  style="margin-top:5px;margin-left:0px; margin-right:0px;margin-bottom:10px;">
            <table style="margin:0px auto;border:1px solid #d1c1c1;width:99%">  
                  <tr>
                    <th style="width:80px">��ʼ����:</th>
                    <td><input type="text" name="dwStartDate" id="dwStartDate" style="width:120px" /></td>
                    <th>��������:</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" style="width:120px" /></td>
                
               </tr>  
                    <tr>
                   <th>�ʲ�����:</th>
                   <td>
                     <input type="text" id="szDevName" name="szDevName" style="width:120px" />
                     </td>
                       
                        <th>
                            ����
                        </th>
                        <td>
                            <select id="dwOpKind" name="dwOpKind" style="width:120px">
                               <%=szKind %>
                            </select>
                        </td>
                         
                   </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="btn" value="��ѯ" />   </td>
                </tr>
                </table>
               </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>�ʲ����</th>
                        <th>�ʲ�����</th>
                        <th>����</th>
                        <th>�䶯˵��</th>
                        <th name="dwOpTime">�䶯ʱ��</th>
                        <th>������</th>
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
                $("#dwStartDate,#dwEndDate").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                AutoDevice($("#szDevName"), 1, $("#dwDevID"),null, null, null, null);
                var tabl = $(".UniTab").UniTab();
                $("#btn,#btnExport").button();
                $(".OPTD").html('<div class="OPTDBtn">\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setBF").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '����',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/SetDevUnUse.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
            $(".setDev").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�޸��ʲ�',
                    width: '720px',
                    height: '550px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:../sys/Dlg/newAssert.aspx?op=set&id=' + dwID
                });
            });

            $(".InfoSet").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + dwID + "&type=hard3")
            });
            $(".setExt").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�鿴����',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/GetDevExtUrl.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
            $(".setFF").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '�ַ�',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/setAttend.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
            
            $(".setLC").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '��ת',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/SetAttend2.aspx?op=set&opext=lc&id=' + dwLabID
                });
            });
            $("#btnNewList").click(function () {
                $.lhdialog({
                    title: '�ʲ��������',
                    width: '750px',
                    height: '550px',
                    lock: true,
                    content: 'url:Dlg/NewAssertList.aspx?op=new'
                });
            });
            $(".ListTbl").UniTable();
          
        });
        </script>
    </form>
</asp:Content>
