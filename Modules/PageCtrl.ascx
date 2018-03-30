<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageCtrl.ascx.cs" Inherits="_PageCtrl" %>
<div class="PageCtrl">
	<div class="info"><label>总</label><input name="_dwTotolLines" type="hidden" Readonly="Readonly"/><span name="_dwTotolLines">0</span><label>条，　开始于第</label><input name="_dwStartLine" type="hidden"/><span name="_dwStartLine">0</span><label>条</label><label>，　每页条数：</label><select name="_dwNeedLines"><option value="10">10</option><option value="20">20</option><option value="50">50</option><option value="100">100</option></select></div>
	<div class="ctrl"><button class="firstPage">首页</button> | <button class="prevPage">上一页</button> | <button class="nextPage">下一页</button> | <button class="lastPage">尾页</button></div>
</div>
<script language="javascript" type="text/javascript" >
    $(function () {
        $('.PageCtrl').UIPageCtrl();
        setTimeout(function () {
            $('.PageCtrl').UIPageCtrl(true);
        }, 1);
    });
</script>