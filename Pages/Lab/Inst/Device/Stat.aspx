<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Stat.aspx.cs" Inherits="Sub_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="form1" runat="server">
        <div class="tabs-summary">
            <h2>设备管理</h2>
            <fieldset>
                <legend>教学相关设备</legend>
                <div id="DevTeachingUse" class="PieStat" data-color="1">
                    <%=m_DevTeaching %>
                </div>
            </fieldset>
            <fieldset>
                <legend>课外自由上机</legend>
                <div id="DevPersonUse" class="PieStat" data-color="1">
                    <p data-value="10">使用中：10台</p>
                    <p data-value="7">预约且未使用：7台</p>
                    <p data-value="7">空闲中：7台</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>设备状态</legend>
                <div id="devRunState" class="PieStat" data-color="1">
                    <p data-value="10">关机状态：10台</p>
                    <p data-value="10">开机状态：10台</p>
                    <p data-value="10">免登录状态：10台</p>
                    <p data-value="7">故障报修中：7台</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>实验室</legend>
                <div id="lab" class="PieStat" data-color="1">
                    <p data-value="10">实验室1：10台</p>
                    <p data-value="7">实验室2：7台</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>房间</legend>
                <div id="room" class="PieStat" data-color="1">
                    <p data-value="10">房间1：10台</p>
                    <p data-value="7">房间2：7台</p>
                </div>
            </fieldset>
        </div>
        <script type="text/javascript">
            function OnClickPie(grp, type) {
                if (grp == "DevTeachingUse") {
                    if (type == "教学所需数") {
                        TabJump("Device/ListTeaching.aspx?state=total");
                    } else if (type == "使用中") {
                        TabJump("Device/ListTeaching.aspx?state=use");
                    }
                    else if (type == "空闲中") {
                        TabJump("Device/ListTeaching.aspx?state=unUse");
                    }
                } else if (grp == "DevPersonUse") {
                    if (type == "使用中") {
                        TabJump("Device/ListPersonal.aspx");
                    }
                    else if (type == "预约且未使用") {
                        TabJump("Device/ListPersonal.aspx");
                    }
                    else if (type == "空闲中") {
                        TabJump("Device/ListPersonal.aspx");
                    }

                } else if (grp == "devRunState" && type == "关机状态") {
                    TabJump("Device/DeviceListRunState.aspx?state=1");
                } else if (grp == "devRunState" && type == "开机状态") {
                    TabJump("Device/DeviceListRunState.aspx?state=2");
                }
                else if (grp == "devRunState" && type == "免登录状态") {
                    TabJump("Device/DeviceListRunState.aspx?state=3");
                }
                else if (grp == "devRunState" && type == "故障报修中") {
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
                return OnClickPie(p, this.name.split("：")[0]);
            }
        </script>
    </form>
</asp:Content>
