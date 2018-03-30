<%@ Page Language="C#" AutoEventWireup="true" CodeFile="castvote.aspx.cs" Inherits="ClientWeb_xcus_cg2_castvote" %>

<html>
<body>
    <style>
        .list_style_tbl { width: 99%; margin-top: 20px; }
        .list_style_tbl .popover { max-width: 600px; }
        .list_style_tbl thead td { text-align: center; padding: 5px 0; border-width: 0; border-bottom: 2px #31b0d5 solid; border-top: 1px #ddd solid; background-color: #f1f1f1; line-height: 16px; }
        .list_style_tbl td { border: 1px solid #ddd; color: #333; padding: 2px; }
        .list_style_tbl .head td { background-color: #fafafa; color: #777; vertical-align: bottom; padding: 5px 5px 2px 5px; }
        .list_style_tbl .head td span { padding: 0 5px; display: inline-block; }
        .list_style_tbl .head td h3 { padding: 0 5px; display: inline; }
        .list_style_tbl .content td { vertical-align: top; padding: 4px 3px; }
        .list_style_tbl .content td .part { color: #999; }
        .list_style_tbl .content td .primary { color: #31708f; }
        .list_style_tbl .content .popover .popover-content { min-height: 160px; }
        .list_style_tbl .box { min-height: 60px; }

        .list_style_tbl .detail_info td { line-height: 24px; padding: 0 3px; background: none; border-width: 0; }
        .list_style_tbl .detail_info td:first-child { color: #428bca; text-align: right; padding-right: 5px; }

        .list_style_tbl .state_info td { line-height: 24px; padding: 0 3px; background: none; font-size: 12px; }
        .list_style_tbl.uni_list tbody tr:nth-child(2n) td { background: #edf2f5; }
        .list_style_tbl .state_info { width: 500px; }
        .list_style_tbl .state_info th { border-bottom: 2px #31b0d5 solid; text-align: center; }
        .list_style_tbl .state_info td:first-child { color: #428bca; }
        .list_style_tbl .time_span { border-bottom: 1px dashed #ccc; }
    </style>
    <script>
        $(".to-cast").clickLoad();
    </script>
    <div class="user_center_panel" style="position: relative;">

        <h1 class="h_title"><%=Translate("网上投票")%></h1>
        <div class="line"></div>
        <div class="" style="min-height: 400px;">

            <div class="vote_con">
                <div id="vote_list" class="list_style_tbl" style="margin-top: 0;">
                    <div class="cur_status text-right" style="padding: 2px;"></div>
                    <table class="tab_con" style="width: 100%;">
                        <thead>
                            <tr>
                                <td>主题</td>
                                <td style="width: 40%">介绍</td>
                                <td>截止日期</td>
                                <td>投票人数</td>
                                <td>状态</td>
                                <td>操作</td>
                            </tr>
                        </thead>
                        <tbody>
                            <%=voteList %>
                        </tbody>
                    </table>
                </div>
                <div class="vote_pctrl"></div>
            </div>
            <script>
                $("#vote_list").pctrl($(".vote_pctrl"), 10);
            </script>
        </div>
    </div>
</body>
</html>
