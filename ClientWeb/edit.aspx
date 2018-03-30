<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="ClientWeb_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>前端页面编辑</title>
    <script type="text/javascript" src="fm/jquery/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="fm/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript" src="fm/jquery-ui/bootstrap/js/bootstrap.js"></script>
   <link rel="stylesheet"  href="fm/jquery-ui/bootstrap/jquery-ui-1.10.3.custom.css" />
    <link rel="stylesheet" href="fm/jquery-ui/bootstrap/css/bootstrap.css" />

    <script type="text/javascript" src="fm/uni.lib.js"></script>
    <link rel="stylesheet" type="text/css" href="fm/uni.css" />
    <script type="text/javascript" src="pro/pro.lib.js"></script>
    <style>
        a {cursor:pointer;}
    </style>
</head>
<body style="margin-left:5px;">
    <h2>前端页面编辑</h2>
    <div id="panel_login" style="display:<%=islogin?"none":"" %>">
                        <script>
                            function login() {
                                var id = $.trim($("#username").val());
                                var pwd = $("#password").val();
                                if (id.length == 0) {
                                    uni.msgBox("请输入帐号和密码！");
                                    return;
                                }
                                pro.j.lg.login(id, pwd, function (rlt) {
                                        location.reload();
                                });
                            }
                            function uploadFile(para,title,suc) {
                                if (!para) para = {};
                                var dlg = $($("#upload_dlg").html());
                                var btn = dlg.find(".btn_upload");
                                btn.attr("ren", para.ren || "");
                                btn.attr("limit", para.limit || "");
                                btn.attr("dir", para.dir || "");
                                btn.uploadFile({},suc);
                                uni.dlg(dlg, title||"上传文件");
                            }
                            function upTemplate() {
                                uploadFile({ren:"*"}, '申请报告模版', function (rlt) {
                                    pro.j.xml.save(rlt.data.save, 'resv_file_template', 'other');
                                });
                            }
    </script>
        <div id="upload_dlg">
            <div style="display:none;">
                <form onsubmit="return false;">
                <input type="button" class="btn_upload" value="上传" />
                </form>
            </div>
        </div>
            <div id="login_info" style="width:260px;">
                <h3>用户登录</h3>
                <p class="input-group">
                    <span class="input-group-addon"><span>帐号</span></span>
                    <input type="text" class="form-control" name="id" id="username" placeholder="<%=GetConfig("idIntro") %>" />
                </p>
                <p class="input-group">
                    <span class="input-group-addon"><span>密码</span></span>
                    <input type="password" class="form-control" name="pwd" id="password" placeholder="<%=GetConfig("pwdIntro") %>" />
                </p>
                <p class="text-center">
                    <button type="button" class="btn btn-success default" onclick="login()">登录</button>
                </p>
            </div>
            </div>
    <div style="display:<%=islogin?"":"none" %>">
        <div>已登录，请确保你拥有编辑权限，且拥有upload目录以及upload/info/xmlData/下文件的操作系统写入权限。</div>
    <%=itemList %>
    <div class="line"></div>
    <h2>其它</h2>
    <ul>
        <li>
            <a onclick="upTemplate();">上传申请报告模版(IC空间/研修间系统)</a>
        </li>
        <%if (GetConfig("mIndexMode")=="2"){ %> 
        <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("主页相册") %>&type=other&id=slide" target="_blank">编辑主页相册</a>
        </li>
        <%} %>
        <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("介绍") %>&w=863&type=dft&id=intro" target="_blank">通用演示内容 介绍</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("硬件") %>&w=863&type=dft&id=hard" target="_blank">硬件</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("相册") %>&w=863&type=dft&id=slide" target="_blank">相册</a>/
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("移动端介绍") %>&w=863&type=dft&id=mIntro" target="_blank">(移动端)介绍</a>
        </li>
        <%if ((editForSubsys & 2) > 0)
          { %> 
        <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("介绍") %>&w=863&type=dft&id=cptintro" target="_blank">电子阅览室子系统通用 介绍</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("硬件") %>&w=863&type=dft&id=cpthard" target="_blank">硬件</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("相册") %>&w=863&type=dft&id=cptslide" target="_blank">相册</a>/
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("移动端介绍") %>&w=863&type=dft&id=cptmIntro" target="_blank">(移动端)介绍</a>
        </li>
        <%} %>
        <%if ((editForSubsys&8)>0)
          { %> 
        <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("介绍") %>&w=863&type=dft&id=seatintro" target="_blank">座位子系统通用 介绍</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("硬件") %>&w=863&type=dft&id=seathard" target="_blank">硬件</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("相册") %>&w=863&type=dft&id=seatslide" target="_blank">相册</a>/
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("预约须知") %>&w=863&type=dft&id=seatrule" target="_blank">预约须知</a>/
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("移动端介绍") %>&w=863&type=dft&id=seatmIntro" target="_blank">(移动端)介绍</a>
        </li>
        <%} %>
        <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("帮助") %>&type=other&id=help" target="_blank">编辑web端帮助</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("帮助") %>&type=other&id=helpEnglish" target="_blank">编辑web端帮助(英文版)</a> /
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("移动端帮助") %>&type=other&id=m_help" target="_blank"> 移动端帮助</a>
        </li>
        <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("规则/须知") %>&type=other&id=rule" target="_blank">编辑通用规则/须知</a>
        </li>
        <%if (GetConfig("proTab")=="jx"){ %>
         <li>
            <a href="editcontent.aspx?name=<%=Server.UrlEncode("教学预约规则") %>&type=other&id=resv_rule" target="_blank">教学预约规则</a>
        </li>
        <%} %>
        <%if((ToUInt(GetConfig("availMobile"))&9)>0){ %>
                 <li>
            <a href="coords.aspx" target="_blank">手机端坐标定位日志</a>
        </li>
        <%} %>
    </ul>        
    </div>
</body>
</html>
