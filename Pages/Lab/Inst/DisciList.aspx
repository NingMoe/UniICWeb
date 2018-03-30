<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DisciList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>违约状况</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
        <div class="tb_info">
            <div class="UniTab" id="tabl">               
             <a href="DisciList.aspx">违约状况</a>       
            <a href="punishlist.aspx">处罚状况</a>
            </div>
    
    </div>         
        </div> 
            <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">
                        <tr>
                            <th>开始日期:</th>
                            <td><input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>结束日期:</th>
                            <td> <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                        </tr>
                        <tr>
                            <th>学工号:</th>
                            <td><input type="text" name="dwPID" id="dwPID" /></td>
                            <th></th>
                            <td></td>
                          
                        </tr>                        
                        <tr>
                            <th colspan="4"><input type="submit" id="btnOK" value="查询" style="height:25px" /></th>
                        </tr>
                    </table>
             
            </div>
       </div>   
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>违约号</th>
                        <th>违约人</th>                        
                        <th>违约对象</th>                                              
                        <th>本次积分</th>
                        <th>剩余积分</th>
                        <th>违约时间</th>                                                
                        <th>处罚时间</th> 
                        <th>状态</th>
                        <th>说明</th>                                               
                        <th>操作</th>                                                
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
                $("#btnOK").button();
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                var tabl = $(".UniTab").UniTab();
                AutoUserByLogonname($("#dwPID"), 1, $("#szGetKey"), null, null, null);
                $("#dwPID").on("autocompleteselect", function (event, ui) {
                    setTimeout(function () { $("#dwPID").val(ui.item.szLogonName); }, 10);
                });

                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delLabBtn" title="取消违约"><img src="../../../themes/iconpage/del.png""/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                         
                $(".delLabBtn").click(function () {                    
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定取消违约?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&delID=" + dwLabID);
                    }, '提示', 1, function () { });
                });
                $("input[name='dwCheckStat']").click(function () {
                   // pForm.data("UniTab_tab1", "reserve.aspx?dwCheckStat=" + $("input[name='dwCheckStat']").val());
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                  });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '新建',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewLab.aspx?op=new'
                });
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
                margin-left:20px;
            }

        </style>
    </form>
</asp:Content>
