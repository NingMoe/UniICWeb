<%@ Page Language="C#" AutoEventWireup="true" CodeFile="second.aspx.cs" Inherits="ClientWeb_xcus_cg2_second" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <style>
        .second_list_tbl { width: 99%; margin-top: 20px; }
        .second_list_tbl .popover { max-width: 600px; }
        .second_list_tbl thead th { text-align: center; line-height: 40px; border-width: 0; border-bottom: 2px #31b0d5 solid; border-top: 1px #ddd solid; background-color: #f1f1f1; }
        .second_list_tbl td { border: 1px solid #ddd; color: #333; padding: 2px; }
        .second_list_tbl .head td { background-color: #fafafa; color: #777; vertical-align: bottom; padding: 5px 5px 2px 5px; }
        .second_list_tbl .head td span { padding: 0 5px; display: inline-block; }
        .second_list_tbl .head td h3 { padding: 0 5px; display: inline; }
        .second_list_tbl .content td { vertical-align: top; padding: 4px 3px; }
        .second_list_tbl .content td .part { color: #999; }
        .second_list_tbl .content td .primary { color: #31708f; }
        .second_list_tbl .content .popover .popover-content { min-height: 160px; }
        .second_list_tbl .box { min-height: 60px; }

        .second_list_tbl .detail_info td { line-height: 24px; padding: 0 3px; background: none; border-width: 0; }
        .second_list_tbl .detail_info td:first-child { color: #428bca; text-align: right; padding-right: 5px; }

        .second_list_tbl .state_info td { line-height: 24px; padding: 0 3px; background: none; font-size: 12px; }
        .second_list_tbl .state_info { width: 500px; }
        .second_list_tbl .state_info th { border-bottom: 2px #31b0d5 solid; text-align: center; line-height: 20px; }
        .second_list_tbl .state_info td:first-child { color: #428bca; }
        .second_list_tbl .time_span { border-bottom: 1px dashed #ccc; }
        .second_list_tbl .state_info tr:nth-child(2n) { background-color: #f7f7f7; }

        .second_list_tbl tr:nth-child(2n) { background-color: #f7f7f7; }
    </style>
    <script>
        $(function () {
            var act = $(".second_list_tbl").pctrl($(".second_pctrl"), 20);
            //预约操作
            $(".second_act").click(function () {
                var third_id = $(this).attr("third_id");
                var resv_id = $(this).attr("resv_id");
                if (resv_id) {
                    uni.confirm("删除旧预约并重新预约场地？", function () {
                        pro.j.rsv.delResv(resv_id, function () { goResv(); }, null, function (rlt) {
                            if (rlt.msg.indexOf("不存在") > 0) {
                                goResv();
                            }
                            else {
                                uni.msgBox(rlt.msg);
                            }
                        });
                    });
                }
                else {
                    goResv();
                }
                function goResv() {
                    var dlg = $("#dlg_rsv_third");
                    var idlg = $(dlg.html());
                    uni.dlg(idlg, "选择活动场景", 400, 230);
                    $(".btn_next", idlg).click(function () {
                        idlg.dialog("close");
                        var id = $(".sel_aty", idlg).val();
                        uni.hr.loadCache("apply.aspx?back=true&thirdId=" + third_id + "&activityId=" + id, null, $("#cache_con"));
                        uni.backTop();
                    });
                }
            });

        });
    </script>
    <div id="dlg_rsv_third"> 
           <div class="dialog">
        <div class="title"></div>
        <div class="list">
            <table>
                <tr>
                    <td>
                        <select class="form-control sel_aty">
                            <%=atyList %>
                        </select>
                    </td>
                    <td>
                        <button type="button" class="btn btn-info btn_next">下一步</button></td>
                </tr>
            </table>
        </div>
    </div>
</div>
    <div>
        <h1>第二课堂活动</h1>
        <div>
            <table class="second_list_tbl">
                <thead>
                    <tr>
                        <th>活动编号</th>
                        <th>活动名称</th>
                        <th>组织方</th>
                        <th>组织人</th>
                        <th>活动时间</th>
                        <th>状态</th>
                        <th class="no_sort">预约场地</th>
                    </tr>
                </thead>
                <tbody>
                    <%=secondList %>
                </tbody>
            </table>
            <div class="second_pctrl"></div>
        </div>

    </div>
</body>
</html>
