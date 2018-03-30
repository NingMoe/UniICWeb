<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="AssertList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>资产管理</h2>
               <div class="tb_info">
                <div class="UniTab" id="tabl">
                    <a href="assertlist.aspx" id="assertlist">设备资产列表</a>
                   <a href="AssertCodeTable.aspx?dwCodeType=5" id="dwCodeType5">用途</a>
                     <%if (nIsAdminSup == 1){%><a href="assertDevCls.aspx" id="assertDevCls">类型</a><%} %>
                    <a href="company.aspx" id="company">供应商</a>
                </div>
           </div>
           
        <div>
           <div class="FixBtn"><!--<a id="btnNew">资产入库</a>--><a id="btnNewList">资产入库</a></div>
             
            </div>
          
                    
            <div  class="tb_infoInLine"  style="margin-top:35px;margin-left:0px; margin-right:0px;margin-bottom:10px;">
            <table style="margin:0px auto;border:1px solid #d1c1c1;width:99%">    
              <tr><td colspan="5" style="text-align:center">查询条件</td></tr>
                 <tr><td rowspan="7" style="text-align:center">查询分类</td></tr>
                <tr>
                    <tr>
                   <th><%=ConfigConst.GCRoomName %>:</th>
                   <td colspan="3">
                       <%=szRoom %>
                     </td>
                   </tr>
                <tr>
                    <th style="width:80px"><%=ConfigConst.GCDevName %>编号:</th>
                    <td><input type="text" name="szAssertSN" id="szAssertSN" style="width:180px" /></td>
                    <th><%=ConfigConst.GCDevName%>名称:</th>
                   <td><input type="text" name="szDevName" id="szDevName" style="width:180px" /></td>
               </tr>  
                <tr>
                    <th style="width:80px"><%=ConfigConst.GCDevName %>单价:</th>
                    <td><input type="text" name="dwMinUnitPrice" id="dwMinUnitPrice" style="width:80px" />到
                        <input type="text" name="dwMaxUnitPrice" id="dwMaxUnitPrice" style="width:80px" /></td>
                    <th>采购日期:</th>
                     <td><input type="text" name="dwSPurchaseDate" id="dwSPurchaseDate" style="width:80px" />到
                         <input type="text" name="dwEPurchaseDate" id="dwEPurchaseDate" style="width:80px" /></td>
               </tr>
                 <tr>
                   <th><%=ConfigConst.GCKindName %>:</th>
                   <td colspan="3">
                       <%=szCLS%>
                     </td>
                   </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="btn" value="查询" />
                        <input type="button" id="btnExport" value="导出明细" />
                    </td>
                </tr>
           </table>
               </div>
                 



        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th name="szAssertSN">资产编号</th>
                        <th name="szDevName"><%=ConfigConst.GCDevName%>名称</th>
                        <th name="szClassName">类型</th>
                        <th name="szModel">品牌型号</th>
                        <th name="dwUnitPrice">单价(元)</th>
                        <th name="dwPurchaseDate">采购日期</th>
                        <th name="szRoomName">所属<%=ConfigConst.GCRoomName %></th>
                        <th name="szDeptName">所属<%=ConfigConst.GCDeptName %></th>
                        <th>插图</th>
                        <th name="szTagID">FID卡状态</th>
                        <th width="25px">操作</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <style>
            .InfoSet {
            cursor:pointer;
            }
            .InfoSet2 {
            cursor:pointer;
            }
        </style>

        <script type="text/javascript">

            $(function () {
                var tabl = $(".UniTab").UniTab();
                $("#btn,#btnExport").button();
                $("#dwSPurchaseDate,#dwEPurchaseDate").datepicker({
                    changeMonth: true,
                    changeYear: true
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="修改"><img src="../../../themes/iconpage/edit.png"/></a>\
                       <a href="#"  class="setResvRule" title="修改预约规则"><img src="../../../themes/icon_s/11.png"/></a>\
                       <a href="#"  class="setEmpty" title="置空FID号"><img src="../../../themes/icon_s/15.png"/></a>\</div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "125", minHeight: "25", maxHeight: "25", speed: 50
            });
            $(".setLabBtn").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改资产',
                    width: '750px',
                    height: '500px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/newAssert.aspx?op=set&id=' + dwID
                });
            });
            $(".setResvRule").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '修改预约规则',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/setResvRule.aspx?op=set&devID=' + dwID
                });
            });
            
            $(".InfoSet").click(function () {
                var dwID = $(this).parents("tr").children().first().attr("data-kindid");
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=" + dwID + "&type=hard3")
            });
            $(".delLabBtn").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                $.lhdialog({
                    title: '删除资产',
                    width: '720px',
                    height: '400px',
                    lock: true,
                    content: 'url:Dlg/DelAssert.aspx?op=del&id=' + dwLabID
                });
            });

            $(".setEmpty").click(function () {
                var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                ConfirmBox("确定置空FID号?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID + '&op=setEmpty');
                }, '提示', 1, function () { });
            });
            
            $("#btnNew").click(function () {
                $.lhdialog({
                    title: '资产入库',
                    width: '720px',
                    height: '600px',
                    lock: true,
                    content: 'url:Dlg/NewAssert.aspx?op=new'
                });
            });
            $("#btnNewList").click(function () {
                $.lhdialog({
                    title: '资产批量入库',
                    width: '750px',
                    height: '550px',
                    lock: true,
                    content: 'url:Dlg/NewAssertList.aspx?op=new'
                });
            });
            $("#btnExport").click(function () {
                $.lhdialog({
                    title: '导出明细',
                    width: '150px',
                    height: '70px',
                    lock: true,
                    content: 'url:Dlg/DevListExport.aspx?op=new'
                });
            });
            $(".ListTbl").UniTable();
          
        });
        </script>
    </form>
</asp:Content>
