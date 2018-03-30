<%@ Page Language="C#" AutoEventWireup="true" CodeFile="votedetail.aspx.cs" Inherits="ClientWeb_xcus_all_votedetail" %>

<html>
<body>
    <style>
        .vote_items { width: 70%; margin-left: 15%; margin-right: 15%; margin-top: 30px; padding: 20px; border: solid 1px #ddd; }
        .vote_items label {font-weight:300;}
        .vote_items label.avail { cursor: pointer; }
        .progress { height: 12px; }
        .progress .progress-bar { line-height: 12px; font-size: 12px; }
        
    </style>
    <script>
        function castVote(id) {
            var dlg = $("#vote_" + id);
            var its = dlg.find(".vote_item:checked");
            var v = [];
            its.each(function () {
                v.push($(this).val());
            })
            if (v.length > 0) {
                pro.j.util.setVote(id, v.join(), function () {
                    uni.msgBoxR("投票成功");
                });
            }
            else {
                uni.msgBox("未选择");
            }
        }
    </script>
    <div class="click btn_back" style="display: <%=isBack%>" onclick="uni.hr.back();"><span class="glyphicon glyphicon-chevron-left"></span>&nbsp;<%=Translate("返回")%></div>
    <div class="header"><%=voteHeader %></div>
    <div runat="server" id="divDetail" class="article_content"></div>
    <div class="content"><%=voteItems %></div>
</body>
</html>
