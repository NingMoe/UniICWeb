<%@ Page Language="C#" AutoEventWireup="true" CodeFile="autocomplete.aspx.cs" Inherits="ClientWeb_m_all_auto" %>

<html>
<body>
        <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back"><i class="icon icon-back"></i><span>OK</span></a></div>
            <div class="center sliding"><%=title %></div>
        </div>
    </div>
    <div class="page" data-page="p-autocomplete">
    <form class="searchbar searchbar-init" data-search-list=".list-block-search">
        <div class="searchbar-input">
            <input type="search" class="auto_key" placeholder="<%=placeholder %>">
            <a href="#" class="searchbar-clear"></a>
        </div>
        <a href="#" class="searchbar-cancel uni_trans">取消</a>
    </form>
    <div class="searchbar-overlay"></div>
    <div class="page-content">
        <div class="list-block autocomplete-block auto_result">
        </div>
        <div class="auto_selected">
        <div style="line-height:18px;position: relative;overflow: hidden;font-size: 14px;text-transform: uppercase;color: #6d6d72;margin: 35px 15px 10px;"><span class="uni_trans">已选</span> <span class="badge bg-green cur_num">0</span>&nbsp;&nbsp;&nbsp;<span class="uni_trans">范围</span><span class="num_range"></span></div>
        <div class="list-block" style="margin-top:10px"><ul></ul></div>
        </div>
        <div class="list-block list-block-search" style="display:none;"></div>
    </div>
    </div>
</body>
</html>
