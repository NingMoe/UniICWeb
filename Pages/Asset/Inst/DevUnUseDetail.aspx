<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevUnUseDetail.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>设备报废明细</h2>
         <div class="toolbar">
               
                    <div class="UniTab" id="tabl">
                <a href="DeviceList.aspx" id="devList"><%=ConfigConst.GCDevName %>总账</a>
                 <a href="devListUse.aspx" id="devListUse">资产变更</a>
                    <a href="DevDamage.aspx" id="DevDamage">设备报修</a>
                    <a href="DevUnUse.aspx" id="DevUnUse">设备报废计划</a>
                        <a href="DevUnUseDetail.aspx" id="DevUnUseDetail">设备报废明细</a>
                </div>
             </div>
        <div class="tb_infoInLine"  style="margin-top:40px">
            <table style="width:99%;margin-top:25px">
                <tr>
                    <th style="width:80px">开始日期:</th>
                    <td><input type="text" name="dwStartDate" id="dwStartDate" style="width:120px" /></td>
                    <th>结束日期:</th>
                   <td><input type="text" name="dwEndDate" id="dwEndDate" style="width:120px" /></td>
                
               </tr>
                 <tr>
                   
                    <th style="width:80px">状态:</th>
                    <td colspan="3">
                        <label><input class="enum" value="0" type="radio" name="dwOOSStat" checked="checked">全部</label>
                        <label><input class="enum" value="1" type="radio" name="dwOOSStat">已申请</label>
                        <label><input class="enum" value="2" type="radio" name="dwOOSStat">已批准</label>
                    </td>
                
               </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="btn" value="查询" />
                    </td>
                </tr>
           </table>
               </div>
                    
        

        <div class="content" style="margin-top:20px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szAssertSN">资产编号</th>
                        <th name="szDevName"><%=ConfigConst.GCDevName%>名称</th>
                        <th name="szModel">规格/型号</th>
                        <th name="dwUnitPrice">单价(元)</th>
                        <th name="dwPurchaseDate">采购日期</th>
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>
                        <th name="szDeptName">所属<%=ConfigConst.GCDeptName %></th>
                        <th>申请报废人</th>
                        <th>报废日期</th>
                        <th>状态</th>
                        <th width="25px">操作</th>
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
                $(".getStockDetail").click(function () {
                    var id =$(this).parents("tr").children().first().attr("data-id");
                    fdata = "dwSTID=" + id;
                    TabInJumpReload("stockakingDetail", fdata);
                });
                var tabl = $(".UniTab").UniTab();
                $("#btn,#btnExport").button();
                $(".OPTD").html('<div class="OPTDBtn">\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "75", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setFF").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '报废',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/SetDevUnUse.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
          
            $(".setExt").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '查看附件',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/GetDevExtUrl.aspx?op=set&opext=ff&id=' + dwLabID
                });
            });
            $("#btnDevKind").click(function () {
                $.lhdialog({
                    title: '<%=ConfigConst.GCDevName%>报废',
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
