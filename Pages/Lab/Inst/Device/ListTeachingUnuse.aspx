<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListTeachingUnuse.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>空闲中设备</h2>
        <div class="toolbar">
            <div class="tb_info">
                <!--总数：5条，班级：5个，房间数：5个 ----->
                <%=m_szOpts %>
            </div>
            <button type="button" id="Back">返回</button>
            <div class="tb_btn">
                <div class="AdvOpts">
                    <div class="AdvLab">高级选项</div>
                    <fieldset>
                        <legend>机房</legend>
                        <label>
                            <input name="room" value="1" type="checkbox" />机房1</label>
                        <label>
                            <input name="room" value="2" type="checkbox" />机房2</label>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="content">
            <table class="ListTbl">
                <tdead>
                    <tr>
                       
                        <th>机器编号!</th>
                         <th>房间!</th>
                        <th>状态!</th>
                        <th>上课班级!</th>
                        <th>课程</th>
                        <th>实验项目名</th>
                        <th>上课教师</th>
                        <th>上课时间</th>
                        <th width="25px">操作</th>
                    </tr>
                     <tr>
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="开机中" />Dev001</td>
                          <td>机房1</td>
                        <td>开机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                     <tr>
                       
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="开机中" />Dev001</td>
                          <td>机房1</td>
                        <td>开机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                    <tr>
                    
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="开机中" />Dev001</td>
                            <td>机房1</td>
                        <td>开机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                     <tr>
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="关机中" />Dev001</td>
                         
                        <td>机房1</td>
                        <td>关机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                     <tr>
                       
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="关机中" />Dev001</td>
                          <td>机房1</td>
                        <td>关机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                      <tr>
                     
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="关机中" />Dev001</td>
                             <td>机房1</td>
                        <td>关机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                     <tr>
                      
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="关机中" />Dev001</td>
                           <td>机房1</td>
                        <td>关机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                    <tr>
                     
                        <td><img src="../../../themes/icon_s/devShutDown.ico" class="imgico" title="关机中" />Dev001</td>
                           <td>机房1</td>
                        <td>关机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDOFF">
                            </div>
                        </td>
                    </tr>
                     <tr>
                       
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="开机中" />Dev001</td>
                          <td>机房1</td>
                        <td>开机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                     <tr>
                      
                        <td><img src="../../../themes/icon_s/devPowerOn.ico" class="imgico" title="开机中" />Dev001</td>  
                         <td>机房1</td>
                        <td>开机中</td>
                      
                        <td>会计0605</td>
                        <td>会计基础</td>
                        <td>会计软件第一次课</td>
                        <td>赵红亮</td>
                        <td>第一二节</td>
                      
                        <td>
                            <div class="OPTD OPTDON">
                            </div>
                        </td>
                    </tr>
                </tdead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
              <fieldset style="width:99%">
                  <legend  align="center" >教学设备空闲情况</legend>
            <div class="LineStat tblBottomStat" data-color="1" data-unit="台" data-name="空闲设备数">
                <h1><span></span><strong>使用设备</strong><strong>所需设备</strong></h1>
                <p><span>第一节</span><span>3</span></p>
                <p><span>第二节</span><span>5</span></p>
                <p><span>第三节</span><span>8</span></p>
                <p><span>第四节</span><span>10</span></p>
                <p><span>第五节</span><span>12</span></p>
                <p><span>第六节</span><span>15</span></p>
                <p><span>第七节</span><span>7</span></p>
                <p><span>第八节</span><span>7</span></p>
                <p><span>第九节</span><span>9</span></p>
                <p><span>第十节</span><span>15</span></p>
                <p><span>第十一节</span><span>15</span></p>
                <p><span>第十二节</span><span>15</span></p>
            </div>
              </fieldset>
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTDON").html('<div class="OPTDBtn">\
                    <a href="#" title="免登陆"><img src="../../../themes/icon_s/11.png"/></a>\
                    <a href="#" title="需要登陆"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" title="关机"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" title="重启"><img src="../../../themes/icon_s/11.png"/></a></div>');

                $(".OPTDOFF").html('<div class="OPTDBtn">\
                    <a href="#" title="开机"><img src="../../../themes/icon_s/15.png"/></a>\
                    <a href="#" title="免登陆"><img src="../../../themes/iconpage/edit.png"/></a>\
                    <a href="#" title="需要登陆"><img src="../../../themes/icon_s/17.png"/></a></div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "150", minHeight: "25", maxHeight: "25", speed: 50
                });
            });
        </script>
    </form>
</asp:Content>
