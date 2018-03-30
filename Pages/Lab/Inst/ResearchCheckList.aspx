<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResearchCheckList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwAccNo" name="dwAccNo" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
            <h2 style="margin-top:10px;font-weight:bold">科研项目审核</h2>       
        <div class="tb_info">
             <div class="UniTab" id="tabl">
                <a href="ResearchCheckList.aspx" id="DevLendList">科研项目审核</a>                
                <a href="UseAuthCheckList.aspx" id="DevLendUseRec">资格审核</a>
                </div> 
    </div>
        <div style="margin-top:30px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                   <th>申请人学工号：</th>
                   <td><input type="text" name="szLogonName" id="szLogonName" /></td>
                   <th><%=ConfigConst.GCLabName %>：</th>
                   <td><input type="text" name="szLabName" id="szLabName" /></td>
               </tr>
                <tr>
                    <th>状态：</th>
                    <td colspan="3">
                            <label><input class="enum" value="1" type="radio" name="dwStatus" checked="checked">未审核</label>
                            <label><input class="enum" value="2" type="radio" name="dwStatus">审核通过</label>
                            <label><input class="enum" value="4" type="radio" name="dwStatus">审核不通过</label>
                            
                 
                       </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="sub" value="查询"></td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>                                     
                    <th name="szTrueName">申请人</th>
                        <th>所在部门</th>
                        <th>手机</th>
                        <th>导师</th>
                        <th>项目名称</th>
                        <th>实验名称</th>
                        <th>申请提交</th>
                        <th>申请时长（分钟）</th>
                        <th>可用时间(分钟)</th>
                        <th name="dwApplyUseTime">申请<%=ConfigConst.GCLabName %></th>
                        <th nae="dwStatus">状态</th>
                        <th>审核时间</th>
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

                var tabl = $(".UniTab").UniTab();
                AutoUserByName($("#szTrueName"), 1, $("#dwAccNo"), null, null, null);
                AutoLab($("#szLabName"), 1, $("#dwLabID"), null, false);
                $("#sub").button();
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });

                $(".OPTD").html('<div class="OPTDBtn">\
                      <a href="#" class="check" title="审核"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="get" title="查看信息"><img src="../../../themes/icon_s/10.png"/></a>\</div>');
                $(".OPTD2").html('<div class="OPTDBtn">\
                      <a href="#" class="get" title="查看信息"><img src="../../../themes/icon_s/10.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "60", maxWidth: "400", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("input[name='lab'],input[name='szRoom'],input[name='szDevKinds'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $(".check").click(function () {
                    var id =$(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '审核',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/researchCheck.aspx?op=set&id=' + id
                    });
                });
                $(".get").click(function () {
                    var id =$(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '查看信息',
                        width: '660px',
                        height: '450px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/researchCheckGet.aspx?op=set&id=' + id
                    });
                });
                $(".DevUseGroup").click(function () {
                    var groupID =$(this).parents("tr").children().first().attr("data-groupid");
                    $.lhdialog({
                        title: '免预约使用人员',
                        width: '700px',
                        height: '600px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetUseGroup.aspx?id=' + groupID
                    });
                });
                $("table").delegate(".DevUseGroup", "click", function () {

                });


                $(".DevUseRec").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");

                    fdata = "szGetKey=" + devID + '&devName=' + devName;
                    TabInJumpReload("devUseRec", fdata);
                });
                $(".DevTestData").click(function () {
                    var devID =$(this).parents("tr").children().first().attr("data-id");
                    var devName =$(this).parents("tr").children().first().attr("data-name");
                    urlPar = [["szGetKey", devID], ['devName', (devName)]];
                    fdata = "szGetKey=" + devID + '&devName=' + devName;
                    TabInJumpReload("devTestData", fdata);
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
