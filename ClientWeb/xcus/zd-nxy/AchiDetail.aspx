<%@ Page Language="C#" MasterPageFile="Modules/Master.master" AutoEventWireup="true" CodeFile="AchiDetail.aspx.cs" Inherits="ClientWeb_xcus_zd_nxy_AchiDetail" %>

<asp:Content runat="server" ID="MyHead" ContentPlaceHolderID="HeadContent">
    <style>
        .tbl { width: 800px; }
        .tbl table { width: 100%; }
        .tbl td { padding: 3px; height: 30px; border: 1px solid #ccc; vertical-align: middle; }
        .tbl td.title { background: #f9f9f9; width: 90px; font-size: 12px; line-height: 25px; font-family: 微软雅黑 !important; color: #666; font-weight: bold; text-align: center; margin: 5px 0 10px 0; overflow: hidden; }
    </style>
    <script>
        $(function () {
        })
    </script>
</asp:Content>
<asp:Content runat="server" ID="MyContent" ContentPlaceHolderID="MainContent">
    <div class="g-b-m">
        <div id="content">
            <div id="position">当前位置：<a href="Default.aspx">首页</a> > <a href="DevList.aspx">仪器共享</a> > <span class="u_pri_f"><%=pagePosition %></span></div>
            <div style="margin: 20px auto;" class="tbl">
                <table class="achi_apply_tbl detail">
                    <tbody>
                        <tr>
                            <td rowspan="3" class="title">仪器概况</td>
                            <td class="title">当前仪器</td>
                            <td colspan="3"><%=CurDevName %></td>
                        </tr>
                        <tr>
                            <td class="title">研究方向</td>
                            <td style="width: 232px;"><%=devLab %></td>
                            <td class="title">管理人</td>
                            <td><%=devMan %></td>
                        </tr>
                        <tr>
                            <td class="title">成果相关仪器</td>
                            <td colspan="3"><%=devs %></td>
                        </tr>
                        <tr>
                            <td rowspan="2" class="title">提交人概况</td>
                            <td class="title">姓  名</td>
                            <td><%=acc.szTrueName %></td>
                            <td class="title">身 份</td>
                            <td><%=ConvertIdent(acc.dwIdent) %></td>
                        </tr>
                        <tr>
                            <td class="title">所属院系</td>
                            <td><%=acc.szDeptName %></td>
                            <td class="title">导师姓名</td>
                            <td><%=acc.szTutorName %></td>
                        </tr>
                    </tbody>
                    <tbody class="achi_kind" style="display:<%=rec.dwRewardKind==8?"":"none"%>">
                        <tr>
                            <td rowspan="5" class="title">发表论文</td>
                            <td class="title">论文名称</td>
                            <td colspan="3">
                                <span class="achi_name"><%=rec.szRewardName %></span></td>
                        </tr>
                        <tr>
                            <td class="title">期刊名称</td>
                            <td colspan="3">
                                <span class="org"><%=rec.szAuthOrg %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">论文作者(前三)</td>
                            <td colspan="3">
                                <span class="member"><%=rec.szMemberNames %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">期数、页码</td>
                            <td colspan="3">
                                <span class="cert_id"><%=rec.szCertID %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">期刊等级</td>
                            <td>
                                <span class="level"><%=ConvertLevel(rec.dwRewardLevel)%></span>
                            </td>
                            <td class="title">影响因子</td>
                            <td>
                                <span class="ext"><%=rec.szExtInfo %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">论文附件</td>
                            <td colspan="4">
                                <a href="<%=ToUploadUrl("UploadFile/" + rec.szMemo)%>"><%=rec.szMemo %></a>
                            </td>
                        </tr>
                    </tbody>
                    <tbody class="achi_kind" style="display:<%=rec.dwRewardKind==1?"":"none"%>">
                        <tr>
                            <td rowspan="4" class="title">获奖情况</td>
                            <td class="title">获奖名称</td>
                            <td colspan="3">
                                <span class="achi_name"><%=rec.szRewardName %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">获奖人员</td>
                            <td colspan="3">
                                <span class="member"><%=rec.szMemberNames %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">获奖等级</td>
                            <td>
                                <span class="level"><%=ConvertLevel(rec.dwRewardLevel) %></span>
                            </td>
                            <td class="title">颁奖部门</td>
                            <td>
                                <span class="org"><%=rec.szAuthOrg %></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">获奖截图</td>
                            <td colspan="3">
                                            <div>
                <div class="img_large">
                    <%=szPicZoom %>
                </div>
                <div class="img_thumb">
                    <ul class="clear">
                        <%=szPicPath %>
                    </ul>
                </div>
                <script>
                    $(".img_thumb a").click(function () {
                        var thumbimgurl = $(this).children().attr('src');
                        var largeimagenurl = thumbimgurl.replace("", "");
                        $(".img_large img").attr('src', largeimagenurl);
                        $(".img_thumb a").each(function () {
                            if ($(this).hasClass('cur')) {
                                $(this).removeClass('cur');
                            };
                        })
                        $(this).addClass('cur');
                        return false;
                    })
                </script>
            </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="title">主要涉及论文</td>
                            <td colspan="4">
                                <span class="memo"><%=rec.szExtInfo %></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

</asp:Content>
