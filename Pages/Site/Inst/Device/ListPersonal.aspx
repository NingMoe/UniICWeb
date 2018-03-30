<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListPersonal.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>个人使用中设备</h2>
        <div class="toolbar">
            <div class="tb_info">
                <!--总数：5条，班级：5个，房间数：5个 ----->
                <%=m_szOpts %></div>
          
            <div class="tb_btn" style="float:left">
               <table border="1">
                    <tr>
                        <td>
                            <button type="button" id="Back">返回</button></td>
                        <td>使用中房间:| 
                            <label><input class="enum" type="checkbox" name="devRunState" value="1">机房1</label>
                            <label><input class="enum" type="checkbox" name="devRunState" value="1">机房2</label>
                        </td>
                        <td>设备状态:
                            <label><input class="enum" type="radio" name="devRunState" value="1">开机</label>
                            <label><input class="enum" type="radio" name="devRunState" value="4">被预约</label>
                            <label><input class="enum" type="radio" name="devRunState" value="2">使用中</label>
                        </td>
                        <td>设备类型:
                              <label><input class="enum" type="checkbox" name="devRunState" value="1">DELL电脑</label>
                            <label><input class="enum" type="checkbox" name="devRunState" value="1">苹果电脑</label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>编号</th>
                        <th>所属房间</th>
                        <th>所属设备类型</th>
                        <th>使用者</th>
                        <th>联系方式</th>
                        <th>控制方式</th>
                        <th>开始时间</th>
                        <th>已使用时长</th>
                        <th>计费单位</th>
                        <th width="25px">操作</th>
                    </tr>
                    <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                     <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                       <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>
                       <tr>
                        <td>DellPC001</td>
                        <td>房间1</td>
                        <td>Dell电脑</td>
                        <td>张三(201100972)</td>
                        <td>15967104608</td>
                        <td>禁止网站库/禁止游戏库</td>
                        <td>17:45</td>
                        <td>45分钟</td>
                        <td>1</td>
                        <td><div class="OPTD"></div>
                        </td>
                    </tr>

                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
           <div class="ColumnStat tblBottomStat" data-color="1">
                <h1><span>--------</span><strong>使用数目</strong></h1>
                <p><span>机房1</span><strong>12</strong></p>
                <p><span>机房2</span><strong>21</strong></p>
                <p><span>机房3</span><strong>17</strong></p>
               <p><span>机房4</span><strong>17</strong></p>
               <p><span>机房5</span><strong>17</strong></p>
            </div>
             <div><label>设备使用数目：</label><a>30人</a></div>
        <div id="FreeUserStat" class="LineStat" data-color="0" data-name="使用数" data-unit="人">
            <p><span>8:00</span><span>7</span></p>
               <p><span>8:00</span><span>6</span></p>
               <p><span>9:00</span><span>9</span></p>
               <p><span>10:00</span><span>11</span></p>
               <p><span>11:00</span><span>20</span></p>
               <p><span>12:00</span><span>15</span></p>
               <p><span>13:00</span><span>21</span></p>
            <p><span>14:00</span><span>24</span></p>
            <p><span>15:00</span><span>25</span></p>
            <p><span>16:00</span><span>26</span></p>
            <p><span>17:00</span><span>7</span></p>
        </div>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" title="发消息"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="查看屏幕"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="锁屏"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="设置控制方式"><img src="../../../themes/icon_s/11.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
        </script>
    </form>
</asp:Content>
