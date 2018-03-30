<%@ Page Language="C#" AutoEventWireup="true" CodeFile="seatYResv.aspx.cs" Inherits="_Default" %>

<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0 , user-scalable=no"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
    <link rel="stylesheet" href="seat/seatStyle.css">
    <script src="seat/Adaptive.js"></script>
    <script src="../../themes/jQuery/js/jquery-1.9.1.js" charset="utf-8"  type="text/javascript" ></script>
	<script language="javascript" type="text/javascript">
	</script>
  <script src="appui/Adaptive.js"></script>
    
    <script type="text/javascript"> 
        $(document).ready(function () {
            $("#btncheck").click(function () {
                alert('签到成功');
            });
            $("#btnLeval").click(function () {
                alert('暂时离开成功');
            });
            $("#btnOver").click(function () {
                alert('提前结束成功');
            });
        });
            /*
            $.post("../ajaxdata/user.aspx?op=getresv", function (data) {
                debugger;
                    var resData = eval(data);
                    if (resData.nStatus == 1) {
                       
                    }
                    else {
                     
                    }
            });

            $("#btnLogin").click(function () {
                
                $.post("../ajaxdata/user.aspx?op=scanlogin", { logonname: $("#logonname").val(), password: $("#password").val(), msn: vOpenid }, function (data) {
                    var resData =eval(data);
                });
            });
            */
       // });
        /*
        var domain = '网站域名'; 
        var url1 ='获取openId接口'; 
        var url2 = '跳转页面';
        function getQueryString(key) { //获取querystring } 
            BeaconAddContactJsBridge.ready(function () {
                //判断是否关注 
                BeaconAddContactJsBridge.invoke('checkAddContactStatus', {}
                    , function (apiResult) {
                        if (apiResult.err_code == 0) {
                            var status = apiResult.data; if (status == 1) {
                                //调用本地页面，通过ticket获取openId 
                                $.get(domain + url1 + getQueryString('ticket'), function (resp) {
                                    if (resp.data)
                                        openId = resp.data.openid; url2 += openId;
                                    location.href = url;
                                });
                            } else { //跳转到关注页
                                BeaconAddContactJsBridge.invoke('jumpAddContact');
                            }
                        }
                        else {
                            alert(apiResult.err_msg)
                        }
                    });
            });
        }
        */
    </script>


</head>
<body>
      <form method="post" runat="server">
        <input type="hidden" id="op" name="op" />
          <input type="hidden" id="openid" name="openid" runat="server" />
    <div class="wrap">
 
    <div class="login-content">
       
        
        <div class="btn-wrap">
            <input class="btn" type="button" id="btncheck" value="签到"/>
        </div>
        
        <div class="btn-wrap">
            <input class="btn" type="button" id="btnLeval" value="暂时离开"/>
        </div>
        <div class="btn-wrap">
            <input class="btn" type="button" id="btnOver" value="结束"/>
        </div>
    </div>
    <div class="background back-login"></div>
</div>
          </form>
</body>
</html>