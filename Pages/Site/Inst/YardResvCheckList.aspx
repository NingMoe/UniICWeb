<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="YardResvCheckList.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <input type="hidden" id="dwApplicantID" name="dwApplicantID" />
      
        <h2 style="margin-top: 10px; font-weight: bold">��˹���</h2>
        <div class="tb_info">
           <div id="tdDetail">

           </div>
        </div>
        <div style="margin-top: 30px; width: 99%;">
            <div class="tb_infoInLine" style="margin: 0 auto; text-align: center">
                <table style="margin: 5px; width: 95%">
                    <tr>
                           <th >��ʼ����:</th>
                            <td>
                                <input type="text" name="dwStartDate" id="dwStartDate" runat="server" /></td>
                            <th>��������:</th>
                            <td >
                                <input type="text" name="dwEndDate" id="dwEndDate" runat="server" /></td>
                    </tr>
                  
                    <tr>
                        <th>������������</th>
                         <td><input name="szTrueName" id="szTrueName" style="width:100px;" /></td>
                        <th>���ţ�</th>
                         <td><input name="dwResvGroupID" id="dwResvGroupID" style="width:100px;" /></td>
                        </tr>
                     <tr>
                        <th>����ƣ�</th>
                         <td colspan="3"><input name="szResvName" id="szResvName" style="width:100px;" /></td>
                          </tr>
                    <tr>
                         <th>
                            ����ͣ�
                        </th>
                        <td colspan="3">
                            <%=szCodeing %>
                        </td>
                    </tr>
                    <tr>
                        <th>״̬��</th>
                        <td colspan="3">
                            <label>
                                <input class="enum" value="16" type="radio" name="dwCheckStat" checked="checked">�����</label>
                            <label>
                                <input class="enum" value="2" type="radio" name="dwCheckStat">���ͨ��</label>
                            <label>
                                <input class="enum" value="5" type="radio" name="dwCheckStat">��˲�ͨ��</label>


                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <input type="submit" id="sub" value="��ѯ">
                            <input type="button" id="checkList" value="�������">
                         <!--   <input type="button" id="export" value="����">-->
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content" style="margin-top: 10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>�����</th>
                        <th name="dwResvGroupID">����</th>
                        <th name="szResvName">�����</th>
                        <th name="szApplicantName">������</th>
    <th>�ύʱ��</th>                        
<th>������Դ</th>
                         <th>����ʱ��</th>
                        <th name="dwCheckDeptID">��˲���</th>
                        <th>״̬</th>
                        <th  name="dwCheckTime">���ʱ��</th>
                        <th name="szAdminName">���Ա</th>
                        <!--<th>˵��</th>-->
                        <th style="width: 25px;" class="thCenter">����</th>
                    </tr>
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
         <!--<uc1:PageCtrl runat="server" ID="PageCtrl" />-->
        </div>
        <script type="text/javascript">
            $(function () {
                $("#checkList").button().click(function () {
                    var vSelectName = "";
                    $("input[name^='tblSelect']").each(function () {
                        if ($(this).prop("checked") == true) {
                            vSelectName = vSelectName + $(this).parent().data("id");
                        }
                    });
                    if (vSelectName == "")
                    {
                        return;
                    }
                    $.lhdialog({
                        title: '�������',
                        width: '400px',
                        height: '250px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/YardResvCheckList.aspx?op=set&id=' + vSelectName
                    }); 
                });
                $("[name = dwCheckStat]:radio").bind("click", function () {
                    var val = $('input:radio[name="dwCheckStat"]:checked').val();
                    if (val == 16)
                    {
                         $("#<%=dwStartDate.ClientID%>").val("");
                         $("#<%=dwEndDate.ClientID%>").val("");
                    }
                    else
                    {
                         var vdwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                        var vdwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                        if (vdwStartDate == "" && vdwEndDate == "")
                        {
                            var vDateNow = "";
                            var mydate = new Date();
                            var Month=mydate.getMonth() + 1;
                            if(Month<10)
                            {
                                Month="0"+Month;
                            }
                            var vDate = mydate.getDate(); //��ȡ��ǰ��(1-31)
                            if (vDate < 10)
                            {
                                vDateNow = mydate.getFullYear() + "-" + Month+ "-" + "0" + vDate;
                            }
                            else {
                                vDateNow = mydate.getFullYear() + "-" + Month + "-" + vDate;
                            }
                            $("#<%=dwEndDate.ClientID%>").val(vDateNow);

                            var vDateNowBef = "";
                            var mydatebef = new Date(mydate.getTime() - 24 * 60 * 60 * 1000*7);  //
                            var MonthBef = mydatebef.getMonth() + 1;
                            if (MonthBef < 10) {
                                MonthBef = "0" + MonthBef;
                            }

                            vDate = mydatebef.getDate(); //��ȡ��ǰ��(1-31)
                            if (vDate < 10)
                            {
                                vDateNowBef = mydatebef.getFullYear() + "-" + MonthBef + "-" + "0" + vDate;
                            }
                            else {
                                vDateNowBef = mydatebef.getFullYear() + "-" + MonthBef + "-" + vDate;
                            }
                            $("#<%=dwStartDate.ClientID%>").val(vDateNowBef);

                        }
                    }

                });

                $("#export").button().click(function () {
                    var vdwStartDate = $("#<%=dwStartDate.ClientID%>").val();
                    var vdwEndDate = $("#<%=dwEndDate.ClientID%>").val();
                    var vdwApplicantID = $("#dwApplicantID").val();
                    var vyardKind = $("input[name='yardKind']:checked").val();
                    var vdwCheckStat = $("input[name='dwCheckStat']:checked").val();
                    $.lhdialog({
                        title: '����',
                        width: '500px',
                        height: '100px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/YardResvExport.aspx?op=set&dwStartDate=' + vdwStartDate + '&dwEndDate=' + vdwEndDate + '&dwApplicantID=' + vdwApplicantID + '&yardKind=' + vyardKind + '&dwCheckStat=' + vdwCheckStat
                    }); 
                });
                var i = 0;
                $("#<%=dwStartDate.ClientID%>,#<%=dwEndDate.ClientID%>").datepicker({
                });
                $(".tdDetail").click(function () {
                    var vText = $(this).attr("text");
                    var vList = vText.split(';');
                    var vTextInfo = "";
                    for (var i = 0; i < vList.length; i++)
                    {
                        vTextInfo += vList[i]+"<br />";
                    }
                    $("#tdDetail").empty();
                    $("#tdDetail").html(vTextInfo);
                    $("#tdDetail").dialog("open");
                });
                $("#tdDetail").dialog({
                    autoOpen: false,
                    height: 450,
                    width: 350,
                    modal: true,
                    show: {
                        effect: "blind",
                        duration: 1000
                    },
                    hide: {
                        effect: "blind",
                        duration: 1000
                    }
                });
                var tabl = $(".UniTab").UniTab();
                AutoUserByName($("#szTrueName"), 1, $("#dwApplicantID"), null, null, null);
                AutoLab($("#szLabName"), 1, $("#dwLabID"), null, false);
                $("#sub").button();
                $("input[name='lab'],input[name='szRoom'],input[name='campus'],input[name='szDevCls'],input[name='dwRunStat']").click(function () {
                    ShowWait();
                    $('.PageCtrl').UIPageCtrl();
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });

                $(".OPTD").html('<div class="OPTDBtn">\
                      <a href="#" class="check" title="���"><img src="../../../themes/iconpage/check.png"/></a>\</div>');
                /*
                $(".OPTD2").html('<div class="OPTDBtn">\
                      <a href="#" class="get" title="�鿴��Ϣ"><img src="../../../themes/icon_s/10.png"/></a>\</div>');
                */
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
                $(".getInfo").click(function () {
                    var id = $(this).parents("tr").children().first().data("id");
                    var ActivityLevel = $(this).parents("tr").children().first().attr("data-activitylevel");//data-activitylevel
                    var YardResvCheck = "YardResvCheckGet";
                    if ((ActivityLevel & 0x10000000) > 0) {
                        bMoudel = true;
                        YardResvCheck = "YardResvCheckGet";
                    }
                    else if ((ActivityLevel & 0x20000000) > 0) {
                        bMoudel = false;
                        YardResvCheck = "YardResvCheckMeetGet";
                    }
                    else if ((ActivityLevel & 0x800000) > 0) {
                        bMoudel = false;
                        YardResvCheck = "YardResvCheckMeetSport";
                    }
                    else {
                        bMoudel = true;
                        YardResvCheck = "YardResvCheckGet";
                    }
                  
                    $.lhdialog({
                        title: '�鿴��Ϣ',
                        width: '800px',
                        height: '720px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/'+YardResvCheck+'.aspx?op=set&id=' + id
                    });
                });
                $(".check").click(function () {
                    var vData = $("input[name='dwCheckStat']:checked").val();
                    var id =$(this).parents("tr").children().first().data("id");
                    var ActivityLevel = $(this).parents("tr").children().first().attr("data-activitylevel");//data-activitylevel
                    var YardResvCheck = "YardResvCheckmeet";
                    var bMoudel = false;
                    if ((ActivityLevel &0x10000000) > 0)
                    {
                        bMoudel = true;
                        YardResvCheck = "YardResvCheck";
                    }
                    else if ((ActivityLevel &0x20000000 ) > 0) {
                        bMoudel = false;
                        YardResvCheck = "YardResvCheckMeet";
                    }
                    else if ((ActivityLevel & 0x800000) > 0) {
                        bMoudel = false;
                        YardResvCheck = "YardResvCheckSport";
                    }
                    else {
                        bMoudel = true;
                        YardResvCheck = "YardResvCheck";
                    }
                    if (bMoudel) {
                        var checkIDs =$(this).parents("tr").children().first().data("checkIDs");
                        $.lhdialog({
                            title: '���',
                            width: '800px',
                            height: '720px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:dlg/' + YardResvCheck + '.aspx?op=set&id=' + id + '&checkstate=' + vData + '&checkIDs=' + checkIDs
                        });
                      
                    }
                    else { 
                        $.lhdialog({
                            title: '���',
                            width: '800px',
                            height: '720px',
                            lock: true,
                            data: Dlg_Callback,
                            content: 'url:dlg/' + YardResvCheck + '.aspx?op=set&id=' + id + '&checkstate=' + vData
                        });
                       
                    }
                });
                $(".get").click(function () {
                    var id =$(this).parents("tr").children().first().data("id");
                    $.lhdialog({
                        title: '�鿴��Ϣ',
                        width: '720px',
                        height: '720px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:dlg/GetYardResvCheck.aspx?op=set&id=' + id
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
                $(".ListTbl").UniTable({ ShowCheck: true, HeaderIndex: false });
            });
        </script>
        <style>
            #tbSearch {
                border-width: 1px;
                border-color: #d1c1c1;
                cursor: hand;
            }

            .thCenter {
                text-align: center;
            }
            .getInfo {
            text-decoration:underline;
            }
            .tdDetail {
            text-decoration:underline;
            }
            #tbSearch td {
                font-family: "Trebuchet MS",Monospace,Serif;
                font-size: 12px;
                padding-top: 2px;
                padding-bottom: 2px;
                padding-left: 15px;
                padding-right: 15px;
                border-style: solid;
                border-width: 1px;
            }
            td input {
                margin-left: 8px;
            }
        </style>
    </form>
</asp:Content>
