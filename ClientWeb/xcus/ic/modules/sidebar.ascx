<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sidebar.ascx.cs" Inherits="WebUserControl" %>
<!--�廪-->
<style>
    .othe_line { border-top: 1px solid #776054; margin-top: 10px; padding-top: 10px; margin-bottom: 10px; }
    #space a {white-space:nowrap;}
</style>
<!--//-->
<div class="aside">
    <a href="index.aspx" id="logo">�㽭��ѧ��Ϣ����ռ�</a>
    <div id="twitter">
        <a class="tweet_sina" href="">ת��������΢��</a>
        <a class="tweet_qq" href="">ת������Ѷ΢��</a>
        <a class="tweet_renren" href="">ת����������</a>
    </div>
    <div id="aboutus">
        <h2>��������</h2>
        <ul>
            <li>
                <a href="space_info.aspx">�ռ����</a>
            </li>

        </ul>
    </div>
    <div id="space" style="overflow:visible">
        <h2>�������</h2>
        <ul>
            <%=szDevCLS %>
        </ul>
    </div>
    <!--�廪-->
<script>
    var list = $("#space li");
    list.each(function () {
        var id = $(this).attr("class_id");
        if (id == "10333")//�ݷ��
        {
            $(this).addClass("othe_line");
            return false;
        }
    });
</script>
<!--//-->
</div>
