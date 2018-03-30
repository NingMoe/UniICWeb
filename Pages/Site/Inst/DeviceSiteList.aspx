<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DeviceSiteList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
            <h2 style="margin-top:10px;font-weight:bold"><%=ConfigConst.GCDevName %>使用状况管理</h2>
       
        <div class="tb_info">
      
    </div>
        <div style="margin-top:30px;width:99%;">
            <div style="text-align:center">
            <table id="tbSearch" style="margin:0 auto;border:1px solid #d1c1c1">    
                <tr>
                    <td style=" text-align:center">校区</td>
                    <td style="text-align:left"><%=m_szCamp %></td>
                </tr> 
                <!--
                <tr>
                    <td  style=" text-align:center"><%=ConfigConst.GCLabName %></td>
                    <td style="text-align:left"><%=m_szLab %></td>
                </tr>
                -->
                <tr>
                    <td><%=ConfigConst.GCDevName %>名称：</td>
                    <td style="text-align:left;padding:6px;"><input type="text" name="devName" id="devName" /></td>
                </tr>
                <tr>
                    <td colspan="2"><input type="submit" value="查询" id="btnSearch" /></td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th></th>
                        <th name="szAssertSN">编号</th>
                        <th><%=ConfigConst.GCDevName %>名</th>    
                        <th>所在楼宇</th>                    
                        <th>所在校区</th>                    
                        <th>使用者</th>      
                      <th>状态</th> 
                        <th style="width:25px;" class="thCenter">操作</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
               var tabl= $(".UniTab").UniTab();
                $("#Back").button().click(function () {                  
                    TabJump("Device/Stat.aspx");
                });
                $("#btnSearch").button();
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="setUnUse" title="禁用"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="setUable" title="启用"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="setResv" title="管理员占用"><img src="../../../themes/icon_s/17.png"/></a>\
                    <a href="#" class="setOpenGroupMember" title="设置白名单"><img src="../../../themes/icon_s/18.png"/></a>\
                    <a href="#" class="SetOpenGroupCopy" title="复制白名单"><img src="../../../themes/icon_s/18.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "125", minHeight: "25", maxHeight: "25", speed: 50
                });
               
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".setUnUse").click(function () {
                    var devid =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '禁用',
                        width: '700px',
                        height: '300px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/setUnUse.aspx?id=' + devid
                    });
                });
                $(".setResv").click(function () {
                    var devid =$(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '管理员占用',
                        width: '700px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/newICresv.aspx?id=' + devid
                    });
                });
                $(".setOpenGroupMember").click(function () {
                    var devid =$(this).parents("tr").children().first().attr("data-openrulesn");
                    $.lhdialog({
                        title: '设置白名单',
                        width: '700px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetOpenGroup.aspx?op=set&id=' + devid
                    });
                });
                $(".SetOpenGroupCopy").click(function () {
                    var devid = $(this).parents("tr").children().first().attr("data-openrulesn");
                    $.lhdialog({
                        title: '复制白名单',
                        width: '700px',
                        height: '400px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetOpenGroupCopy.aspx?op=set&id=' + devid
                    });
                });
                $(".setUable").click(function () {
                    var devid =$(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定启用?", function () {
                    ShowWait();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=usable&id=" + devid);
                    }, '提示', 1, function () { });

            });
                $(".ListTbl").UniTable();
            });
        </script>
        <style>
           #tbSearch
            {
                border-width: 1px;                
                border-color: #d1c1c1;                
                cursor: hand;
            }
            .thCenter
            {
                text-align:center;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:8px;
            }
        </style>
    </form>
</asp:Content>
