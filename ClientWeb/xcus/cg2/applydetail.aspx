<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="applydetail.aspx.cs" Inherits="ClientWeb_xcus_cg2_applydetail" %>
<html>
<body>
        <style media="print">
        .apply_table { width: 100%; background-color: #fff;border-collapse:collapse; }
        .apply_table .title { background-color: #fafafa; }
        .apply_table td { border: 1px solid #ddd; padding: 3px 4px; }
        .apply_table select { height: 28px; max-width: 200px; }
        .apply_table .sel_time { width: 60px; }
        .apply_table .p_dev_tmp .p_dev_info .close { display: none; }
        .apply_table .p_dev_info { margin: 5px; border: 1px solid #ccc; width: 99%; }
        .apply_table .head { color: #000; }
        .apply_table .cycle_tm_panel .sel_time_panel { min-height: 30px; }
        .apply_table .cycle_tm_panel tr td:first-child { width: 84px; }
        .text-center { text-align:center;}
        .text-right {text-align:right;}
        .sub_act { display:none;}
            .hidden {display:none;}
    </style>
    <style>
        .apply_table { width: 100%; background-color: #fff; border-collapse:collapse; }
        .apply_table .title { background-color: #fafafa; }
        .apply_table td { border: 1px solid #ddd; padding: 3px 4px; }
        .apply_table select { height: 28px; max-width: 200px; }
        .apply_table .sel_time { width: 60px; }
        .apply_table .p_dev_tmp .p_dev_info .close { display: none; }
        .apply_table .p_dev_info { margin: 5px; border: 1px solid #ccc; width: 99%; }
        .apply_table .head {color: #000; }
        .apply_table .cycle_tm_panel .sel_time_panel { min-height: 30px; }
        .apply_table .cycle_tm_panel tr td:first-child { width: 84px; }
        .text-center { text-align:center;}
        .text-right {text-align:right;}
        .hidden {display:none;}
    </style>
    <script>
        $(function () {
            
        });
    </script>
    <div id="apply_panel">
        <h1 style="text-align:center;"></h1>
            <table class="apply_table apply_print apply_dft" style="display:<%=applyTbl=="dft"?"":"none"%>">
                <tbody>
                    <tr class="thead">
                        <td colspan="5" class="text-center head"><strong class="apply_aty">场地申请表</strong></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="2" style="width: 96px;">申请人信息</td>
                        <td class="title text-right" style="width: 18%;">姓名：</td>
                        <td colspan="3">
                            <span class="p_user"><%=resv.szApplicantName %></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right">&nbsp;手机：</td>
                        <td>
                            <span><%=resv.szHandPhone %></span></td>
                        <td class="title text-right">&nbsp;E-mail：</td>
                        <td>
                            <span><%=resv.szEmail %></span></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="<%=isSports?"5":"6" %>" style="text-align: center">用途/性质</td>
                        <td class="title text-right">&nbsp;活动名称：</td>
                        <td colspan="3">
                            <span><%=resv.szResvName %></span>
                    </tr>
                                        <tr>
                        <td class="text-right title">组织方：</td>
                        <td colspan="3">
                            <span><%=resv.szOrganization %></span></td>
                    </tr>
                    <tr class="<%=isSports?"hidden":""%>">
                        <td class="title text-right">&nbsp;活动类型：</td>
                        <td colspan="3">
                            <%=atyType %>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right">&nbsp;是否有偿使用：</td>
                        <td colspan="3">
                           <span> <%=profit%></span>
                        </td>
                    </tr>
                                        <tr>
                        <td class="title text-right">&nbsp;是否使用多媒体：</td>
                        <td colspan="3">
                           <span> <%=media%></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right">&nbsp;是否公开活动：</td>
                        <td colspan="3">
                            <span><%=open %></span>
                        </td>
                    </tr>
                    <tr class="<%=isSports?"hidden":""%>">
                        <td class="title text-center" rowspan="2" style="text-align: center">主讲人情况</td>
                        <td class="text-right title">主讲人姓名：</td>
                        <td colspan="3">
                            <span><%=resv.szPresenter %></span></td>
                    </tr>
                    <tr class="<%=isSports?"hidden":""%>">
                        <td class="text-right title">背景介绍：</td>
                        <td colspan="3">
                            <span><%=resv.szIntroInfo %></span></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="<%=isShare?"1":"2"%>">出席者情况</td>
                        <td class="title text-right">参与条件：</td>
                        <td colspan="3">
                            <span"><%=require %></span>
                        </td>
                    </tr>
                    <tr style="display:<%=isShare?"none":""%>">
                        <td class="title text-right">&nbsp;参与人数：</td>
                        <td colspan="3">
                            <%--<span><%=resv.dwMinAttendance %></span>&nbsp;到&nbsp;--%>
                            <span><%=isSports?resv.szPresenter:resv.dwMaxAttendance.ToString() %></span></td>
                    </tr>
                    <tr class="<%=isSports?"hidden":""%>">
                        <td class="title text-center">服务</td>
                        <td colspan="4">
                            <div class='service'>
                                <%=checkService %>
                            </div>
                        </td>
                    </tr>
                    <tr> 
                        <td class="title text-center">预约场地</td>
                                            <td colspan="4"><span><%=resv.szDevName %></span></td></tr>
                                        <tr> 
                        <td class="title text-center">预约时间</td>
                                            <td colspan="4"><span><%=resvTime %></span></td></tr>
                                        <tr>
                        <td class="title text-center">归口审核部门</td>
                        <td colspan="4">
                            <span><%=checkDirector %></span></td>
                    </tr>
                    <tr>
                        <td class="title text-center">物管审核部门</td>
                        <td colspan="4">
                            <span><%=checkMan %></span></td>
                    </tr>
                    <tr>
                        <td class="title text-center">备注信息</td>
                        <td colspan="4">
                            <span><%=resv.szMemo %></span></td>
                    </tr>
<%--                    <tr>
                        <td class="title text-center">申请人承诺</td>
                        <td colspan="4">(1)遵守学校教室场所使用管理要求，保持环境整洁，不吸烟、不乱抛口香糖等杂物。
                            <br />
                            (2)遵守学校治安管理规定，确保安全使用。若因借用人管理和使用不当造成安全事故，借用人自行承担责任。<br />
                            (3)遵守学校财产物资规定，损坏设备设施按原值赔偿。
                        </td>
                    </tr>--%>
                    <tr>
<%--                        <td class="title text-right">申请人签名：</td>
                        <td>
                            </td>--%>
                        <td class="title text-center">填表时间</td>
                        <td colspan="4">
                                  <span class="p_apply_time"><%=Get1970Date((int)ToUInt(resv.dwOccurTime)) %></span></td>
                    </tr>
<%--                    <tr>
                        <td class="title text-center" rowspan="2">归口审核</td>
                        <td colspan="4">1.各院系学术讲座、办班等院长或系主任负责审批；<br />
                            2.各院系学生活动由各院系总支副书记负责审批(学生社团活动除外)；<br />
                            3.校团委、校学生会、社团联合会以及所有学生社团活动归口团委审批。<br />
                            4.后勤管理处直接审批(适应于“就业指导中心”等特殊部门)。
                        </td>
                    </tr>--%>
                </tbody>
            </table>
        <table class="apply_table apply_print apply_hy" style="display:<%=applyTbl=="hy"?"":"none"%>">
                <tbody>
                    <tr class="thead">
                        <td colspan="5" class="text-center head"><strong class="apply_aty">会议申请表</strong></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="4" style="text-align: center">会议信息</td>
                        <td class="title text-right">&nbsp;会议名称：</td>
                        <td colspan="3">
                            <span><%=resv.szResvName %></span>
                    </tr>
                                        <tr>
                        <td class="title text-right">&nbsp;会议介绍：</td>
                        <td colspan="3">
                            <span><%=resv.szOrganiger %></span>
                    </tr>
                                                            <tr>
                        <td class="text-right title">组织方：</td>
                        <td colspan="3">
                            <span><%=resv.szOrganization %></span></td>
                    </tr>
                                        <tr>
                        <td class="title text-right">&nbsp;会议类型：</td>
                        <td colspan="3">
                            <%=atyType %>
                        </td>
                    </tr>
                                                            <tr>
                        <td class="title text-right">&nbsp;是否使用多媒体：</td>
                        <td colspan="3">
                           <span> <%=media%></span>
                        </td>
                    </tr>
                                        <tr>
                        <td class="title text-right">&nbsp;是否公开活动：</td>
                        <td colspan="3">
                            <span><%=open %></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="2" style="text-align: center">主讲人情况</td>
                        <td class="text-right title">主讲人姓名：</td>
                        <td colspan="3">
                            <span><%=resv.szPresenter %></span></td>
                    </tr>
                    <tr>
                        <td class="text-right title">背景介绍：</td>
                        <td colspan="3">
                            <span><%=resv.szIntroInfo %></span></td>
                    </tr>
                    <tr>
                        <td class="title text-center" rowspan="4">预约信息</td>
                        <td class="title text-right">预约场地：</td>
                                            <td colspan="3"><span><%=resv.szDevName %></span></td></tr>
                                        <tr> 
                        <td class="title text-right">预约时间：</td>
                        <td colspan="3"><span><%=resvTime %></span></td></tr>
                                       <tr>
                        <td class="title text-right">归口审核部门：</td>
                        <td colspan="4">
                            <span><%=checkDirector %></span></td>
                    </tr>
                                                            <tr>
                        <td class="title text-right">物管审核部门：</td>
                        <td colspan="4">
                            <span><%=checkMan %></span></td>
                    </tr>
                                                                                    <tr>
                        <td class="title text-center">出席者情况</td>
                        <td class="title text-right">&nbsp;参会人员：</td>
                        <td>
                            <span><%=resv.szDesiredUser %></span>
                        </td>
                        <td class="title text-right">&nbsp;参会人数：</td>
                        <td>
                            <span><%=resv.dwMaxAttendance %></span></td>
                    </tr>
                                        <tr>
                        <td class="title text-center" rowspan="2" style="width: 96px;">申请人信息</td>
                        <td class="title text-right" style="width: 18%;">姓名：</td>
                        <td colspan="3">
                            <span><%=resv.szContact %></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="title text-right">&nbsp;手机：</td>
                        <td>
                            <span><%=resv.szHandPhone %></span></td>
                        <td class="title text-right">&nbsp;E-mail：</td>
                        <td>
                            <span><%=resv.szEmail %></span></td>
                    </tr>
                    <tr>
                        <td class="title text-center">备注信息</td>
                        <td colspan="4">
                            <span><%=resv.szMemo %></span></td>
                    </tr>
<%--                    <tr>
                        <td class="title text-center">申请人承诺</td>
                        <td colspan="4">(1)遵守学校教室场所使用管理要求，保持环境整洁，不吸烟、不乱抛口香糖等杂物。
                            <br />
                            (2)遵守学校治安管理规定，确保安全使用。若因借用人管理和使用不当造成安全事故，借用人自行承担责任。<br />
                            (3)遵守学校财产物资规定，损坏设备设施按原值赔偿。
                        </td>
                    </tr>--%>
                    <tr>
<%--                        <td class="title text-right">申请人签名：</td>
                        <td>
                            </td>--%>
                        <td class="title text-center">填表时间</td>
                        <td colspan="4">
                                  <span class="p_apply_time"><%=Get1970Date((int)resv.dwOccurTime) %></span></td>
                    </tr>
                </tbody>
            </table>
        <div class="text-center" style="margin-top: 10px;">
            <button type="button" class="btn btn-default sub_act<%=(status&2)>0?"":" hidden" %>" onclick="window.print();">打印申请表</button>
            <%--<button type="button" class="btn btn-info copy_apply">复制申请</button>--%>
        </div>
    </div>
</body>
</html>
