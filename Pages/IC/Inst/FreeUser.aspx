<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="FreeUser.aspx.cs" Inherits="Sub_FreeUser"%>
<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="formAdvOpts" runat="server">
    <h2>课外自由上机</h2>
    <div class="toolbar">
        <div class="tb_info">当前总数：5人，空闲设备数：10台</div>
        <div class="FixBtn"></div>
        <div class="tb_btn">
            <div class="AdvOpts"><div class="AdvLab">高级选项</div>
                <fieldset><legend>机房</legend>
                    <label><input name="room" value="1" type="checkbox" />机房1</label>  <label><input name="room" value="2" type="checkbox" />机房2</label>
                </fieldset>
            </div>
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
                
                </tbody>
            </table>
		<uc1:PageCtrl runat="server" ID="PageCtrl"/>
    </div>
    <script type="text/javascript">
        $(function () {
            $(".OPTD").html('<div class="OPTDBtn">\
                        <a href="#" title="截屏"><img src="../../../themes/iconpage/edit.png"/></a>\
                        <a href="#" title="查看进程"><img src="../../../themes/icon_s/13.png"/></a>\
                        <a href="#" title="发消息"><img src="../../../themes/iconpage/del.png""/></a></div>');
            $(".OPTDBtn").UIAPanel({
                theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
            });
        });
    </script>
</form>
</asp:Content>