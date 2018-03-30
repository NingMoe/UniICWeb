<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ListPersonal.aspx.cs" Inherits="Sub_Course" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2><%=ConfigConst.GCDevName %>日常管理</h2>
        
        <div>
            <table id="tbSearch" style="border:1px solid #d1c1c1">
                <tr>
                    <td style="width:20%; text-align:center">校区</td>
                    <td style="width:75%;text-align:left"><%=m_szCamp %></td>
                    </tr>
                <tr>
                    <td  style="width:20%; text-align:center">实验室</td>
                    <td style="width:75%;text-align:left"><%=m_szLab %></td>
                </tr>
                 <tr>
                    <td  style="width:20%; text-align:center"><%=ConfigConst.GCDevName %>状态</td>
                    <td style="width:75%;text-align:left">
                        <LABEL><INPUT class="enum" value="1" type="checkbox" name="dwRunStat" > 开机</LABEL>
                        <LABEL><INPUT class="enum" value="2" type="checkbox" name="dwRunStat" > 使用中</LABEL>
                        <LABEL><INPUT class="enum" value="4" type="checkbox" name="dwRunStat" > 被预约</LABEL>
                    </td>
                    </tr>
               
            </table>
        </div>
        <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th></th>
                        <th>编号</th>
                        <th><%=ConfigConst.GCDevName %>名</th>
                        <th>控制方式</th>
                        <th>仪器状态</th>                      
                        <th>仪器类型</th>                        
                        <th>所属实验室</th>
                        <th>使用者</th>
                         <th><%=ConfigConst.GCDeptName %></th>                        
                         <th>管理员</th>  
                         <th>管理员电话</th>  
                        <th>登录时长</th>                     
                        <th>预约时间</th>  
                        <th width="25px">操作</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
            <uc1:PageCtrl runat="server" ID="PageCtrl" />
        </div>
        <script type="text/javascript">
            $(function () {
                $("#Back").button().click(function () {
                    TabJump("Device/Stat.aspx");
                });
                $(".OPTD").html('<div class="OPTDBtn">\
                    <a href="#" class="RemoveMessage" title="发消息"><img src="../../../themes/icon_s/9.png"/></a>\
                    <a href="#" class="devCtrlNoLogin" title="免登陆"><img src="../../../themes/icon_s/10.png"/></a>\
                    <a href="#" class="devCtrlNeedLogin" title="需要登陆"><img src="../../../themes/icon_s/8.png"/></a>\</div>');
                $(".OPTDBtn").UIAPanel({
                    theme: "none.png", borderWidth: 0, minWidth: "25", maxWidth: "100", minHeight: "25", maxHeight: "25", speed: 50
                });
                $("input[name='lab']").click(function () {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $("input[name='campus']").click(function () {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                });
                $("input[name='dwRunStat']").click(function () {
                    TabReload($("#<%=formAdvOpts.ClientID%>").serialize());
                  });
                
            });
        </script>
        <style>
            #tbSearch
            {
                border-width: 1px;                
                border-color: #d1c1c1;                
                cursor: hand;
            }
                #tbSearch td
                {
                    font-family: "Trebuchet MS",Monospace,Serif;
                    font-size: 12px;               
                    padding-top: 2px;
                    padding-bottom: 2px;
                    padding-left: 15px;
                    padding-right: 15px;
                    border-style: solid;
                    border-width: 1px;                    
                }
            td input
            {
                margin-left:20px;
            }

        </style>
    </form>
</asp:Content>
