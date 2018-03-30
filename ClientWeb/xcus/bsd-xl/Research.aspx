<%@ Page Language="C#" MasterPageFile="net/Master.master" AutoEventWireup="true" CodeFile="Research.aspx.cs" Inherits="ClientWeb_xcus_bsd_xl_Research" %>

<%@ Register TagPrefix="Uni" TagName="curdev" Src="net/curdev.ascx" %>
<%@ Register TagPrefix="Uni" TagName="dlgrtest" Src="net/dlg_rtest.ascx" %>
<%@ Register TagPrefix="Uni" TagName="tblresearch" Src="net/tblresearch.ascx" %>
<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.js" type="text/javascript"></script>
    <link href="<%=ResolveClientUrl("~/ClientWeb/") %>md/unicalendar/unicalendar.green.css" rel='stylesheet' />
    <script type="text/javascript">
        //申请使用资格
        function applyLabUseRole(labid, roleid, applyid, title) {
            var upFile = $("#dlg_lab_use_role .upload_file").uploadFile();//上传文件
            $("#dlg_lab_use_role .lab_name").html(title);
            uni.dlgR($("#dlg_lab_use_role"), "实验室使用资格", 420, 260, function (dlg) {
                var file_name = $(".upload_file", dlg).attr("save_name");
                if (uni.isNull(file_name)) {
                    uni.msgBox("请上传申请报告");
                    return;
                }
                pro.j.acc.applyLabUseRole(labid, roleid, applyid, file_name, function () {
                    uni.msgBox("已提交申请，等待管理员审批！", "", function () {
                        $("#dlg_lab_use_role").dialog("close");
                    });

                });
            });
        }
        $(function () {
            var req = uni.getReq();
            var act = req["act"];
            //
            $(".get_item").clickLoad();
            //点击事件
            $(".get_item").click(function () {
                $(".get_item").removeClass("ui-state-default");
                $(this).addClass("ui-state-default");
            });
            $("#first_item").trigger("click");
        });
    </script>
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="dlg_lab_use_role" class="dialog">
        <div class="list">
            <table>
                <tr>
                    <td>实验室：</td>
                    <td>
                        <span class="lab_name"></span></td>
                </tr>
                <tr>
                    <td>申请报告：</td>
                    <td>
                        <div style="text-decoration: underline; margin-bottom: 3px;"><a href="心理学院实验室使用资格申请表.docx">下载资格申请报告模版</a></div>
                        <div>
                            <input type="file" name="lab_file_name" id="lab_file_name" /><span class="red">*</span>
                        </div>
                        <div style="height: 24px; line-height: 24px;">
                            <input type="button" style="cursor: pointer;" class="upload_file" file="lab_file_name" value="上传" /><span class="cur_file_name color1"></span>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dev_info">
        <Uni:curdev runat="server" />
        <Uni:dlgrtest runat="server" />
    </div>
    <div class="rt_tab">
        <div class="f-fl qzone ui-tabs ui-widget ui-corner-all" id="act_list">
            <ul class="ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all">
                <li class="ui-state-default ui-corner-top ui-tabs-active ui-state-active"><a class="ui-tabs-anchor">功能列表</a></li>
            </ul>
            <div>
                <ul class="menu">
                    <li><a href="javascript:void(0);" id="first_item" class="get_item" url="RTest.aspx" con="#act_qzone"><span class='ui-icon ui-icon-calculator'></span>管理实验项目</a></li>
                    <li><a href="javascript:void(0);" class="get_item" url="LabUseRole.aspx" con="#act_qzone"><span class='ui-icon ui-icon-calculator'></span>实验室使用资格</a></li>
                </ul>
                <div class="accordion">
                    <h3 onclick="location.reload();">参与的项目： <span class="color1" style="font-size: 12px; text-decoration: underline; font-weight: 100;">点击刷新</span></h3>
                    <div class="rt_tab">
                        <div class="pro_list">
                            <ul class="menu">
                                <%=selRtList %>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                var acd = $(".accordion").accordion({ heightStyle: "content" });
                var menu = $(".menu").menu();
                var req = uni.getReq();
                var devId = req["dev"];
                var classId = req["classId"];
                $("h3", acd).each(function () {
                    var pthis = $(this);
                    var id = pthis.attr("classId");
                    if (classId) {
                        if (id == classId) {
                            pthis.trigger("click");
                            return;
                        }
                    }
                });
                $("li a", menu).each(function () {
                    var pthis = $(this);
                    var id = pthis.attr("devId");
                    if (devId) {
                        if (id == devId) {
                            pthis.addClass("menu-state-focus");
                            return;
                        }
                    }
                    else {
                        if (id == "0") {
                            pthis.addClass("menu-state-focus");
                        }
                    }
                });
            </script>
        </div>
        <div class="f-fr qzone ui-tabs ui-widget ui-corner-all" id="act_qzone">
        </div>
    </div>
    <script type="text/javascript">
        $(".tabs").tabs();
    </script>
</asp:Content>
