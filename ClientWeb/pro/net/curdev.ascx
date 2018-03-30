<%@ Control Language="C#" AutoEventWireup="true" CodeFile="curdev.ascx.cs" Inherits="ClientWeb_xcus_bsd_xl_net_curdev" %>
        <div class="hidden">
            <input type='hidden' id='cur_dev_id' value='<%=CurDevId %>' />
            <input type='hidden' id='cur_dev_name' value='<%=CurDevName %>' />
            <input type='hidden' id='cur_dev_cps' value='<%=CurDevCps %>' />
            <input type='hidden' id='cur_dev_dept' value='<%=CurDevDept %>' />
            <input type='hidden' id='cur_dev_lab' value='<%=CurDevLab %>' />
            <input type='hidden' id='cur_dev_url' value='<%=imgUrl %>' />
            <input type='hidden' id='cur_dev_pro' value='<%=CurDevPro %>' />
            <input type='hidden' id='cur_dev_status' value='<%=CurDevSta %>' />
            <input type='hidden' id='cur_dev_early' value='<%=CurDevEarly %>' />
            <input type='hidden' id='cur_dev_last' value='<%=CurDevLast %>' />
            <input type='hidden' id='cur_dev_max' value='<%=CurDevMax %>' />
            <input type='hidden' id='cur_dev_min' value='<%=CurDevMin %>' />
        </div>
<script type="text/javascript">
    var curDev = {};
    curDev.id = $("#cur_dev_id").val();
    curDev.name = $("#cur_dev_name").val();;
    curDev.cps = $("#cur_dev_cps").val();
    curDev.dept = $("#cur_dev_dept").val();
    curDev.lab = $("#cur_dev_lab").val();
    curDev.url = $("#cur_dev_url").val();
    curDev.pro = $("#cur_dev_pro").val();
    curDev.sta = $("#detail .dev_status").eq(0).html();
    curDev.early = $("#cur_dev_early").val();
    curDev.last = $("#cur_dev_last").val();
    curDev.max = $("#cur_dev_max").val();
    curDev.min = $("#cur_dev_min").val();
    pro.dev = curDev;
</script>
