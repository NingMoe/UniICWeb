<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="PunishList.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>处罚状况</h2>
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
                            <th>学工号:</th>
                            <td><input type="text" name="dwPID" id="dwPID" /></td>
                            <td colspan="2"><input type="submit" id="btnOK" value="查询" style="height:25px" /></td>
                        </tr>                        
                      
                    </table>
             
            </div>
       </div>   
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>预约目的</th>
                        <th>最大积分</th>                                              
                        <th>剩余积分</th>                                              
                        <th>处罚日期</th>                        
                        <th>说明</th>                        
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
        
        </div>
        <script type="text/javascript">

            $(function () {
                $("#btnOK").button();
             
                var tabl = $(".UniTab").UniTab();
                AutoUserByLogonname($("#dwPID"), 1, $("#szGetKey"), null, null, null);
                $("#dwPID").on("autocompleteselect", function (event, ui) {
                    setTimeout(function () { $("#dwPID").val(ui.item.szLogonName); }, 10);
                });

                $(".OPTD1").html('<div class="OPTDBtn">\
                       <a class="setLabBtn" title="审核"><img src="../../../themes/iconpage/edit.png"/></a></div>');              
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $(".OPTD1").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '审核',
                        width: '700px',
                        height: '650px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/checkbg.aspx?op=set&id=' + dwResvID
                    });
                });                
                $(".delLabBtn").click(function () {
                    var szLabSN = $(this).parents("tr").children().first().text();
                    var dwLabID = $(this).parents("tr").children().first().attr("data-id");
                    ConfirmBox("确定删除?", function () {
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
