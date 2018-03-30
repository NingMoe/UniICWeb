<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Stat.aspx.cs" Inherits="Sub_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="form1" runat="server">
        <div class="tabs-summary">
            <h2>�豸����</h2>
            <fieldset>
                <legend>��ѧ����豸</legend>
                <div id="DevTeachingUse" class="PieStat" data-color="1">
                    <%=m_DevTeaching %>
                </div>
            </fieldset>
            <fieldset>
                <legend>���������ϻ�</legend>
                <div id="DevPersonUse" class="PieStat" data-color="1">
                    <p data-value="10">ʹ���У�10̨</p>
                    <p data-value="7">ԤԼ��δʹ�ã�7̨</p>
                    <p data-value="7">�����У�7̨</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>�豸״̬</legend>
                <div id="devRunState" class="PieStat" data-color="1">
                    <p data-value="10">�ػ�״̬��10̨</p>
                    <p data-value="10">����״̬��10̨</p>
                    <p data-value="10">���¼״̬��10̨</p>
                    <p data-value="7">���ϱ����У�7̨</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>ʵ����</legend>
                <div id="lab" class="PieStat" data-color="1">
                    <p data-value="10">ʵ����1��10̨</p>
                    <p data-value="7">ʵ����2��7̨</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>����</legend>
                <div id="room" class="PieStat" data-color="1">
                    <p data-value="10">����1��10̨</p>
                    <p data-value="7">����2��7̨</p>
                </div>
            </fieldset>
        </div>
        <script type="text/javascript">
            function OnClickPie(grp, type) {
                if (grp == "DevTeachingUse") {
                    if (type == "��ѧ������") {
                        TabJump("Device/ListTeaching.aspx?state=total");
                    } else if (type == "ʹ����") {
                        TabJump("Device/ListTeaching.aspx?state=use");
                    }
                    else if (type == "������") {
                        TabJump("Device/ListTeaching.aspx?state=unUse");
                    }
                } else if (grp == "DevPersonUse") {
                    if (type == "ʹ����") {
                        TabJump("Device/ListPersonal.aspx");
                    }
                    else if (type == "ԤԼ��δʹ��") {
                        TabJump("Device/ListPersonal.aspx");
                    }
                    else if (type == "������") {
                        TabJump("Device/ListPersonal.aspx");
                    }

                } else if (grp == "devRunState" && type == "�ػ�״̬") {
                    TabJump("Device/DeviceListRunState.aspx?state=1");
                } else if (grp == "devRunState" && type == "����״̬") {
                    TabJump("Device/DeviceListRunState.aspx?state=2");
                }
                else if (grp == "devRunState" && type == "���¼״̬") {
                    TabJump("Device/DeviceListRunState.aspx?state=3");
                }
                else if (grp == "devRunState" && type == "���ϱ�����") {
                    TabJump("Device/DeviceListRunState.aspx?state=4");
                }
                else if (grp == "lab") {
                    TabJump("Device/ListByLab.aspx?id=4");
                }
                else if (grp == "room") {
                    TabJump("Device/ListByRoom.aspx?state=4");
                }
                return false;
            }
            function OnClickChart(e) {
                var p = $(this.graphic.element).parents(".PieStat").attr("id");
                return OnClickPie(p, this.name.split("��")[0]);
            }
        </script>
    </form>
</asp:Content>
