<%@ Page Language="C#" AutoEventWireup="true" CodeFile="openaty.aspx.cs" Inherits="ClientWeb_m_all_openaty" %>

<html>
<body>
        <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">活动中心</div>
        </div>
    </div>
    <div class="page" data-page="p-openaty">
        <div class="page-content">
            <div class="content-block" style="margin:20px 0;">
                <div class="buttons-row">
      <a href="#tab_open_new" class="tab-link active button">活动预告</a>
      <a href="#tab_open_old" class="tab-link button">活动回顾</a>
    </div>
            </div>
 <div class="tabs">
    <div id="tab_open_new" class="tab active">
            <div class="list-block touch_top">
                <ul>
                    <%=newList %>
                </ul>
            </div>
    </div>
    <div id="tab_open_old" class="tab">
            <div class="list-block touch_top">
                <ul>
                    <%=oldList %>
                </ul>
            </div>
    </div>
  </div>
        </div>
    </div>
</body>
</html>
