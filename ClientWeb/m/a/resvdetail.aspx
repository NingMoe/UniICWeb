<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resvdetail.aspx.cs" Inherits="ClientWeb_m_all_resvdetail" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">{{title}}</div>
        </div>
    </div>
    <div class="page" data-page="p-simple">
        <div class="page-content">
            <div class="content-block-title">{{name}}</div>
            <div class='line'></div>
            <ul class="info-list">
                <li class="info-item">
                    <div class="item-title">预约号</div>
                    <div class="item-content">{{id}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">预约人</div>
                    <div class="item-content">{{owner}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">预约对象</div>
                    <div class="item-content">{{devName}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">所在位置</div>
                    <div class="item-content">{{labName}} {{roomName}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">预约时间</div>
                    <div class="item-content">{{timeDesc}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">提交日期</div>
                    <div class="item-content">{{occur}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">组成员</div>
                    <div class="item-content">{{members}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">当前状态</div>
                    <div class="item-content">{{state}}</div>
                </li>
                <li class="info-item">
                    <div class="item-title">详细状态</div>
                    <div class="item-content">{{states}}</div>
                </li>
            </ul>
        </div>
    </div>
</body>
</html>
