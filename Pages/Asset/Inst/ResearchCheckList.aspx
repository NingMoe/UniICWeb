<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ResearchCheckList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwAccNo" name="dwAccNo" />
        <input type="hidden" id="dwLabID" name="dwLabID" />
            <h2 style="margin-top:10px;font-weight:bold">������Ŀ���</h2>       
        <div class="tb_info">
             <div class="UniTab" id="tabl">
                <a href="ResearchCheckList.aspx" id="DevLendList">������Ŀ���</a>                
                <a href="UseAuthCheckList.aspx" id="DevLendUseRec">�ʸ����</a>
                </div> 
    </div>
        <div style="margin-top:30px;width:99%;">
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                          
               <tr>
                   <th>������ѧ���ţ�</th>
                   <td><input type="text" name="szLogonName" id="szLogonName" /></td>
                   <th><%=ConfigConst.GCLabName %>��</th>
                   <td><input type="text" name="szLabName" id="szLabName" /></td>
               </tr>
                <tr>
                    <th>״̬��</th>
                    <td colspan="3">
                            <label><input class="enum" value="1" type="radio" name="dwStatus" checked="checked">δ���</label>
                            <label><input class="enum" value="2" type="radio" name="dwStatus">���ͨ��</label>
                            <label><input class="enum" value="4" type="radio" name="dwStatus">��˲�ͨ��</label>
                            
                 
                       </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center"><input type="submit" id="sub" value="��ѯ"></td>
                </tr>
            </table>
                </div>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>                                     
                    <th name="szTrueName">������</th>
                        <th>���ڲ���</th>
                        <th>�ֻ�</th>
                        <th>��ʦ</th>
                        <th>��Ŀ����</th>
                        <th>ʵ������</th>
                        <th>�����ύ</th>
                        <th>����ʱ�������ӣ�</th>
                        <th>����ʱ��(����)</th>
                        <th name="dwApplyUseTime">����<%=ConfigConst.GCLabName %></th>
                        <th nae="dwStatus">״̬</th>
                        <th>���ʱ��</th>
                        <th style="width:25px;" class="thCenter">����</th>
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
               AutoUserByName($("#szTrueName"), 1, $("#dwAccNo"),null, null, null);
               AutoLab($("#szLabName"), 1, $("#dwLabID"), null, false);
                $("#sub").button();
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });

                $(".OPTD").html('<div class="OPTDBtn">\
                      <a href="#" class="check" title="���"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="get" title="�鿴��Ϣ"><img src="../../../themes/icon_s/10.png"/></a>\</div>');
                $(".OPTD2").html('<div class="OPTDBtn">\
                      <a href="#" class="get" title="�鿴��Ϣ"><img src="../../../themes/icon_s/10.png"/></a>\</div>');
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
                        title: '���',
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
                        title: '�鿴��Ϣ',
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
                        title: '��ԤԼʹ����Ա',
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
