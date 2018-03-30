<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Sub_Plan" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�γ�ʵ��ƻ�</h2>
        <div class="toolbar">
            <div class="FixBtn"><a id="newPlan">�ֶ����ʵ��ƻ�</a></div>
            <div class="tb_btn">
                <!--
            <div class="AdvOpts"><div class="AdvLab">�߼�ѡ��</div>
                <fieldset><legend>ѧ��</legend>
                    <%if ((m_TermList & 1) != 0)
                      { %>
                    <label><input name="dwYearTerm" value="1" type="radio" />��ѧ��</label>
                    <%}
                      if ((m_TermList & 2) != 0)
                      { %>
                    <label><input name="dwYearTerm" value="0" type="radio" />��ѧ��</label>
                    <%}
                      if ((m_TermList & 4) != 0)
                      { %>
                    <label><input name="dwYearTerm" value="2" type="radio" />��ѧ��</label>
                    <%} %>
                </fieldset>
                <fieldset><legend>�γ�</legend>
                    <label><input name="room" value="1" type="checkbox" />�γ�1</label>  <label><input name="room" value="2" type="checkbox" />�γ�2</label>
                </fieldset>
                <fieldset><legend>״̬</legend>
                    <label><input name="room" value="1" type="checkbox" />�Ѱ���</label>  <label><input name="room" value="2" type="checkbox" />δ����</label>  <label><input name="room" value="2" type="checkbox" />���ְ���</label>
                </fieldset>
            </div>
            -->
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th width="60px">���</th>
                        <th width="60px">�ƻ�����</th>
                        <th width="60px">ѧ��</th>
                        <th width="60px" name="szTeacherName">��ʦ</th>
                        <th>�༶</th>
                        <th>�γ�</th>
                        <th name="dwTotalTestHour">ѧʱ</th>
                        <th>״̬</th>
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
                $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" class="DoItemSet" title="�޸�"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" class="DoItemDel" title="ɾ��"><img src="../../../themes/iconpage/del.png""/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "130", minHeight: "25", maxHeight: "25", speed: 50
                });


                $(".DoItemDel").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: 'ɾ��ʵ��ƻ�',
                        width: '250px',
                        height: '100px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/DelPlan.aspx?op=del&id=' + dwID
                    });
                });


                $(".DoItemSet").click(function () {
                    var dwID = $(this).parents("tr").data("id");

                    $.lhdialog({
                        title: '�޸�ʵ��ƻ�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=set&id=' + dwID
                    });
                });

                $("#newPlan").click(function () {
                    $.lhdialog({
                        title: '�ֶ����ʵ��ƻ�',
                        width: '800px',
                        height: '500px',
                        lock: true,
                        data: Dlg_Callback,
                        content: 'url:Dlg/SetPlan3.aspx?op=new'
                    });
                });
            });
        </script>
    </form>
</asp:Content>
