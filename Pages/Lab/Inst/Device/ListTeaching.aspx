<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListTeaching.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>��ѧ�����豸</h2>
        <div class="toolbar">
            <div class="tb_info">
                <!--������5�����༶��5������������5�� ----->
                <%=m_szOpts %>
            </div>
            <div>
                <table border="1">
                    <tr>
                        <td>
                            <button type="button" id="Back">����</button></td>
                        <td>���ſη���:<%=m_szRoom %>|</td>
                        <td>�豸״̬:<label><input class="enum" type="radio" name="devRunState" value="1">����</label><input class="enum" type="radio" name="devRunState" value="4">
                            ��ԤԼ<label><input class="enum" type="radio" name="devRunState" value="2">
                                ʹ����
                            </label>
                        </td>
                        <td>�Ͽ��еĿγ�:<%=m_szResvTeaching %></td>
                          <td><button type="button" id="btnSerach">��ѯ</button></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <tdead>
                    <tr>
                        <th name="dwDevSN">�������</th>
                        <th name="szRoomName">����</th>
                        <th name="dwRunStat">״̬</th>
                        <th name="szCurTrueName">ʹ����</th>
                        <th name="szGroupName">�Ͽΰ༶</th>
                        <th name="szCourseName">�γ�</th>                       
                        <th name="szTeacherName">�Ͽν�ʦ</th>
                        <th name="dwTeachingTime">�Ͽ�ʱ��</th>
                        <th>��½ʱ��!</th>
                        <th width="25px">����</th>
                    </tr>
                </tdead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
            <fieldset style="width: 99%">
                <legend align="center">��ѧ�豸ʹ�����</legend>
                <div class="ColumnStat tblBottomStat" data-color="1">
                    <h1><span></span><strong>�����豸</strong><strong>ʹ���豸</strong><strong>�Ͽ�����</strong><strong>ʵ������</strong></h1>
                    <%=m_szPie %>
                </div>
            </fieldset>
        </div>
        <script type="text/javascript">
            $(function () {

                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $("#btnSerach").button;
                $(".OPTDUN").html('<div class="OPTDBtn">\
                    <a href="#" class="NOLOGIN" title="���½"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="NEEDLOGIN" title="��Ҫ��½"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" class="WAKEUP" title="Զ�̿���"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" class="SHUTDOWN"  title="Զ�̹ػ�"><img src="../../../themes/icon_s/13.png"/></a>\
                    <a href="#" class="RESTART" title="Զ������"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="ADMINMESSAGE" title="������Ϣ"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="SCREEN" title="�鿴��Ļ"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDUSING").html('<div class="OPTDBtn">\
                    <a href="#" class="ADMINMESSAGE" title="������Ϣ"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" class="SHUTDOWN" title="�ػ�"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="RESTART"  title="����"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" class="SCREEN" title="�鿴��Ļ"><img src="../../../themes/iconpage/edit.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
                var OldTabReload = null;

                if (OldTabReload == null) {
                    OldTabReload = TabReload;
                }
                $("[name='room']").click(function (event) {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $("[name='devRunState']").click(function (event) {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                    });
                    $("[name='resvid']").click(function (event) {
                        TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                    });
                    var pForm = $("form");
                    TabReload = function (fdata) {
                        var url = "device/ListTeaching.aspx";
                        if (url != null && url + "" != "undefined") {
                            $.ajax({
                                url: url,
                                data: fdata,
                                type: "POST",
                                timeout: 600000,
                                async: true,
                                dataType: "html",
                                success: function (data, status) {
                                    pForm.empty()
                                    var pData = $("<div>" + data + "</div>").appendTo(pForm);
                                    if (OnTabLoad) {
                                        OnTabLoad(null, { panel: pData });
                                    }
                                },
                                error: function (data, status, error) {
                                    MessageBox(status, "", 2);
                                }
                            });
                        } else {
                            if (OldTabReload != null) {
                                OldTabReload(fdata);
                            }
                        }
                    }

            });
                    setTimeout(function () {
                        $(".WAKEUP").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            $.lhdialog({
                                title: 'Զ�̿���',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=11&id=' + dwID
                            });
                        });
                        $(".SHUTDOWN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: 'Զ�̹ػ�',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=12&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".RESTART").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: 'Զ������',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=13&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".NOLOGIN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '���½',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=52&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".NEEDLOGIN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '��Ҫ��½',
                                width: '250px',
                                height: '160px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendCtrlDev.aspx?type=51&id=' + dwID + '&labid=' + labid
                            });
                        });
                        $(".SCREEN").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '�鿴��Ļ',
                                width: '800px',
                                height: '600px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/RomteScreen.aspx?id=' + dwID + '&labid=' + labid
                        });
                        });
                        $(".ADMINMESSAGE").on("click", function () {
                            var dwID = $(this).parents("tr").children().first().attr("data-id");
                            var labid = $(this).parents("tr").children().first().attr("data-labid");
                            $.lhdialog({
                                title: '������Ϣ',
                                width: '600px',
                                height: '200px',
                                lock: true,
                                data: Dlg_Callback,
                                content: 'url:<%=MyVPath %>Pages/Inst/Dlg/SendMessageCtrlDev.aspx?type=81&id=' + dwID + '&labid=' + labid
                        });
                        });


                    }, 1);

        </script>
    </form>
</asp:Content>
