<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUnUse.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�豸����</h2>
         <div class="toolbar">
               
                    <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>����</a>
                 <a href="devListUse.aspx" id="devListUse">�ʲ����</a>
                    <a href="DevDamage.aspx" id="DevDamage">�豸����</a>
                    <a href="DevUnUse.aspx" id="DevUnUse">�豸���ϼƻ�</a>
                        <a href="DevUnUseDetail.aspx" id="DevUnUseDetail">�豸������ϸ</a>
                </div>
                   
             
                      <div id="btnDevKind" class="FixBtn"><a>���ñ���<%=ConfigConst.GCDevName %>�ƻ�</a></div>
             </div>
        <div class="tb_infoInLine"  style="margin-top:40px">
            <table style="margin-top:25px;width:99%">
                <tr>
                    <th style="width:80px">��ʼ����:</th>
                    <td><input type="text" name="dwStartDate" id="dwStartDate" style="width:120px" /></td>
                    <th>��������:</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" style="width:120px" /></td>
                
               </tr>
                 <tr>
                   
                    <th style="width:80px">״̬:</th>
                    <td colspan="3">
                        <label><input class="enum" value="0" type="radio" name="dwOOSStat" checked="checked">ȫ��</label>
                        <label><input class="enum" value="1" type="radio" name="dwOOSStat">������</label>
                        <label><input class="enum" value="2" type="radio" name="dwOOSStat">����׼</label>
                    </td>
                
               </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="btn" value="��ѯ" />
                    </td>
                </tr>
           </table>
               </div>
              

        <div class="content" style="margin-top:20px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>��������</th>
                        <th>����������</th>
                        <th>��������</th>
                        <th>�����</th>
                        <th>�������</th> 
                        <th>״̬</th>
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
                $("#btn,#btnExport").button();
                $("#dwStartDate,#dwEndDate").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setExt" title="���"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a class="setDetail" title="�鿴��ϸ"><img src="../../../themes/icon_s/17.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setDetail").click(function () {
                var id =$(this).parents("tr").children().first().attr("data-id");
                fdata = "id=" + id;
                TabInJumpReload("DevUnUseDetail", fdata);
            });
            $(".setFF").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '���ϼƻ�',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/SetDevUnUse.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
            $(".setExt").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '���',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/CheckDevUnuse.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
            $("#btnDevKind").click(function () {
                $.lhdialog({
                    title: '<%=ConfigConst.GCDevName%>����',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/NewDevUnuse.aspx?op=new'
                    });
             });
            $(".ListTbl").UniTable();
          
        });
        </script>
    </form>
</asp:Content>
