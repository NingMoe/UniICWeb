<%@ Page Language="C#" MasterPageFile="Templates/ClientMaster.master" AutoEventWireup="true" CodeFile="Check.aspx.cs" Inherits="Check" %>

<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        $(function () {
            $("#apply_close").click(function () {
                $(".tem_panel").slideUp();
            });
            $(".apply_check").click(function () {
                if ($(".tem_panel").css("display") == "none") {
                    $(".tem_panel").slideDown();
                }
                else {
                    $(".tem_panel").slideUp();
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="templatemo_main">
        <h1>预约审核</h1>
        <div id="" class="float_all">
            <div class="resv_list">
                <ul>
                    <li class="odd">1 <span style="width: 80px;">基因分析仪 </span><span style="width: 120px;">基因分析实验</span><span style="width: 40px;">陈浩</span><span style="width: 30px;"></span><span style="color: green;">2013-10-08</span> 至 <span style="color: green;">2013-10-12</span><span style="width: 30px;"></span><span style="width: 300px;">课题实验 </span><span style="width: 80px; color: red">未审核</span><a class="apply_check" style="width: 40px; cursor: pointer;">审核</a></li>
                    <li class="even">2 <span style="width: 80px;">基因分析仪 </span><span style="width: 120px;">基因分析实验</span><span style="width: 40px;">陈浩</span><span style="width: 30px;"></span><span style="color: green;">2013-10-08</span> 至 <span style="color: green;">2013-10-12</span><span style="width: 30px;"></span><span style="width: 300px;">课题实验 </span><span style="width: 80px; color: green">已审核</span><a class="apply_check" style="width: 40px; cursor: pointer;">审核</a></li>
                    <li class="odd">3 <span style="width: 80px;">基因分析仪 </span><span style="width: 120px;">基因分析实验</span><span style="width: 40px;">陈浩</span><span style="width: 30px;"></span><span style="color: green;">2013-10-08</span> 至 <span style="color: green;">2013-10-12</span><span style="width: 30px;"></span><span style="width: 300px;">课题实验 </span><span style="width: 80px; color: green">已审核</span><a class="apply_check" style="width: 40px; cursor: pointer;">审核</a></li>
                    <li class="even">4 <span style="width: 80px;">基因分析仪 </span><span style="width: 120px;">基因分析实验</span><span style="width: 40px;">陈浩</span><span style="width: 30px;"></span><span style="color: green;">2013-10-08</span> 至 <span style="color: green;">2013-10-12</span><span style="width: 30px;"></span><span style="width: 300px;">课题实验 </span><span style="width: 80px; color: green">已审核</span><a class="apply_check" style="width: 40px; cursor: pointer;">审核</a></li>
                </ul>
            </div>
            <div class="tem_panel" style="display: none;">
                <div class="apply_head"></div>
                <div class="resv_apply">
                    <div style="width: 600px; float: left; border-bottom: 1px dashed #666;">
                        <h2>基因分析仪  预约申请</h2>
                    </div>
                    <a class="apply_close" id="apply_close"></a>
                    <div class="app_tbl">
                        <h3>仪器信息</h3>
                        <div class="apply_item">
                            <p>仪器型号：3130 Genetic Analyzer</p>
                            <p>仪器类型：分子生物学设备</p>
                            <p>放置位置：紫金港校区农生组团C816</p>
                            <p>仪器状态：<span style="color: green;">正常</span></p>
                            <p>预约状态：<span style="color: red;">2</span> 人排队中 至 <span style="color: red;">2013-10-07</span></p>
                        </div>
                        <h3>申请信息</h3>
                        <div class="apply_item">
                            <p>课题名称：基因分析实验</p>
                            <p>导师姓名：李国强</p>
                            <p>申请人：陈浩 200809230001</p>
                            <p>预约时间：<span style="color: green;">2013-10-08</span> 至 <span style="color: green;">2013-10-12</span></p>
                            <p>预约人联系电话：13738142786</p>
                            <p>预约人邮箱：227834567@qq.com</p>
                            <p>是否委托检测：是</p>
                            <p>是否需要耗材：是  材料1 <span style="color: red;">10</span>g  材料2 <span style="color: red;">5</span>g</p>
                            <p>申请说明：课题实验</p>
                            <p>审核状态：<span style="color: red;">未审核</span></p>
                        </div>
                        <h3>费用预算</h3>
                        <div class="apply_item">
                            <p>基本费用：40X5 = 200元</p>
                            <p>委托检测费：20元</p>
                            <p>耗材费：10X10 + 80X5 =  500元</p>
                            <p>合计：<span style="color: red;">720</span>元   需预付 <span style="color: red;">144</span>元</p>
                        </div>
                        <h3>操作</h3>
                        <div>
                            <input type="button" title="审核通过" value="审核通过" />
                            <input type="button" title="调整日期" value="调整日期" />
                            <input type="button" title="确认预收费" value="确认预收费" />
                            <input type="button" title="确认收费" value="确认收费" />
                            <input type="button" title="忽略" value="忽略" />
                        </div>
                    </div>
                </div>
                <div class="apply_footer"></div>
            </div>
        </div>
    </div>
</asp:Content>
