<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="DevSeatCardUser.aspx.cs" Inherits="Sub_Lab" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCSysKindPC %>ԤԼ״��</h2>
        <div class="toolbar">
            <input type="hidden" id="szGetKey" name="szGetKey" />
        <div class="tb_info">
            <div class="UniTab" id="tabl">
                       <a href="DevSeatList.aspx" id="DevRoomList"><%=ConfigConst.GCSysKindSeat %>ʹ��״��</a> 
                <a href="DevSeatCardUser.aspx" id="DevSeatCardUser"><%=ConfigConst.GCSysKindSeat %>�û�ˢ���б�</a>  
                <a href="DevSeatUseRec.aspx" id="DevSeatUseRec"><%=ConfigConst.GCSysKindSeat %>ʹ�ü�¼</a>
                    <a href="DevSeatUseInfo.aspx" id="DevPCUseinfo"><%=ConfigConst.GCSysKindSeat %>�û�ǩ����¼</a>
               <!-- <a href="DevPCResvRec.aspx" id="DevPCResvRec"><%=ConfigConst.GCSysKindRoom %>ԤԼ��¼</a>-->
                </div> 
    
    </div>
         
        </div> 
            <div>
            <div class="tb_infoInLine" style="margin:0 auto;text-align:center">
                    <table style="margin:5px;width:70%">                       
                       <tr>
                    <td style="text-align:right"><%=ConfigConst.GCSysKindSeat %>����</td>
                    <td style="text-align:left"><input type="text" name="szDevName" id="szDevName" style="margin-left:5px" />
                        <input type="submit" value="��ѯ" id="btnOK" />
                           <input type="button" value="��ʱ�뿪" id="btnLeavl" />
                    </td> </tr>
                    </table>
             
            </div>
       </div>   
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>ˢ����</th>
                        <th>ˢ��������</th>                        
                        <th>��λ����</th>                      
                        <th>����<%=ConfigConst.GCLabName %></th>
                        <th>״̬</th>
                        <th name="dwOccurTime">ˢ��ʱ��</th>
                        <th name="dwBeginTime">��ʹ��ʱ��</th>
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
                $("#btnOK,#btnLeavl").button();
                var tabl = $(".UniTab").UniTab();              
                AutoDevice($("#szDevName"), 1, $("#szGetKey"),8, null, null, null);
                $(".OPTD").html('<div class="OPTDBtn">\
                       <a class="delLabBtn" title="ǿ��ˢ������"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "50", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("#btnLeavl").click(function () {
                   
                    ConfirmBox("ȷ����ʱ�뿪?", function () {
                        ShowWait();
                        var dwResvID = "";
                        $("input[name^='tblSelect']").each(function () {
                            if ($(this).prop("checked") == true) {
                                dwResvID = dwResvID + $(this).parent().attr("data-id")+',';
                            }
                        });
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&op=stopLeavl&stopAllID=" + dwResvID);
                    }, '��ʾ', 1, function () { });
                });
                $(".OPTD1").click(function () {
                    var dwResvID = $(this).parents("tr").children().first().attr("data-id");
                    $.lhdialog({
                        title: '���',
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
                    ConfirmBox("ǿ��ˢ������?", function () {
                        ShowWait();
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize() + "&ID=" + dwLabID);
                }, '��ʾ', 1, function () { });
                });
                $("input[name='dwCheckStat']").click(function () {
                   // pForm.data("UniTab_tab1", "reserve.aspx?dwCheckStat=" + $("input[name='dwCheckStat']").val());
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                  });
            $("#btnNewLab").click(function () {
                $.lhdialog({
                    title: '�½�',
                    width: '660px',
                    height: '400px',
                    lock: true,
                    data: Dlg_Callback,
                    content: 'url:Dlg/NewLab.aspx?op=new'
                });
            });
           // $(".ListTbl").UniTable();
            $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
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
