<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<link rel="stylesheet" type="text/css" media="screen" href="../css.aspx" />
    <script type="text/javascript" src="../zepto.min.js"></script>
    
 <script type="text/javascript">
     $(function (){
         hide();
         var vAajxBeginTime=$("#dwBeginTime").val();
         var vAajxEndTime=$("#dwEndTime").val();
         var vAajxszLabID=$("#szLabID").val();
         $.ajax({
             url: "../Ajax/AGetDevice.aspx?op=get&dwBeginTime="+vAajxBeginTime+"&dwEndTime="+vAajxEndTime+"&szLabID="+vAajxszLabID,
             success: function (data) {
                 var vDevList = eval(data);
                 var vDivContant = $("#dragContainer");
                 for (var i = 0; i < vDevList.length; i++)
                 {
                     var x = Number(vDevList[i].x);
                     var y = Number(vDevList[i].y);
                     var vSigan = "☆";
                     if(vDevList[i].uStatus==1)
                     {
                         vSigan = "☆";
                     }
                     else{
                         vSigan = "★";//有人
                     }

                     var isDrag=<%=szDrag%>;
                     if(isDrag=="true")
                     {
                         isDrag=true;
                     }
                     else if(isDrag=="false"){
                         isDrag=false;
                     }
                     var vDivTemp = $("<div opentime= "+vDevList[i].openTime+" devName='"+vDevList[i].DevName+"' style='text-align:center;' id='" + vDevList[i].devID + "'></div>");

                     vDivTemp.css({ 'position': 'absolute', 'left': x, 'top': y,'border-radius:59px':50});
                     if(!isDrag)
                     {
                         if(vSigan=="☆")
                         {
                             vDivTemp.click(function () {
                                 $("#resvDevName").html($(this).attr("devName"));
                                 var opentime=Number($(this).attr("opentime"));
                                 debugger;
                                 var opentimeBegin=parseInt(opentime/10000);
                                 var opentimeEnd=parseInt(opentime%10000);
                                 for(var i=opentimeEnd;i>=opentimeBegin;i=i-100)
                                 {
                                     var min=parseInt(i%100);
                                     if(min<10)
                                     {
                                         min="0"+min;
                                     }
                                     var selectTime=parseInt(i/100)+':'+min;
                                     $("#selectBeginTime").prepend("<option value='"+i+"'>"+selectTime+"</option>");
                                     $("#selectEndTime").prepend("<option value='"+i+"'>"+selectTime+"</option>");
                                 }
                                 showWin();
                             });
                         }
                     }
                     vDivTemp.append("<div id='div" + vDevList[i].devID + "'>" + vSigan + "</div><div>" + vDevList[i].DevName + "</div>");
                     vDivContant.append(vDivTemp);
                 }
             }
         });
     });
     function showWin(){  
         /*找到div节点并返回*/  
         var winNode = $("#win");  
         winNode.css("display", "block"); 
     }  
     function hide(){  
         var winNode = $("#win");  
         winNode.css("display", "none");  
     }
 </script>
    <style>
        #win{  
    /*边框*/  
    border:1px black solid;  
    /*窗口的高度和宽度*/  
    width : 500px;  
    height: 400px;  
    /*窗口的位置*/  
    position : absolute;  
    top : 100px;  
    left: 350px;  
    background-color:#d3bcbc;
    /*开始时窗口不可见*/  
}  
/*控制背景色的样式*/  
#title{  
    background-color:#d3bcbc;  
    color : black;  
    /*控制标题栏的左内边距*/  
    padding-left: 3px;  
}  
#cotent{  
    padding-left : 3px;  
    padding-top :  5px;  
}  
/*控制关闭按钮的位置*/  
#close{  
    margin-left: 390px;  
    /*当鼠标移动到X上时，出现小手的效果*/  
    cursor: pointer;  
} 
    </style>
    <script>
       
        
    </script>
</head>
<body>
<form action="#" method="post" enctype="application/x-www-form-urlencoded">
    <input type="hidden" name="dwBeginTime" id="dwBeginTime" value="<%=dwBeginTime %>" />
    <input type="hidden" name="dwEndTime" id="dwEndTime" value="<%=dwEndTime %>" />
    <input type="hidden" name="szLabID" id="szLabID" value="<%=szLabID %>" />
  
      <div class="Head">
       <div><span class="navback1"><font class="navarrow">◀</font> 选择空闲时间</span></div> 
    </div>

    <div  id="divContant">
<span>  ★:有人</span>
        <span>☆:空闲</span>
    </div>

    

  <div id="dragContainer" onselectstart="return false" style="position:relative;border:1px dashed blue;">
     <img src="../Labimg/100453746.jpg"  alt="" />
      <div id="dragDiv">
          
	  </div>
      
	</div>

	  <span id="span1"></span>
    <div class="content" style="position:relative">
        <div  style="position:absolute;left:20px;top:30px"></div>
        <div style="position:absolute;left:60px;top:60px">☆</div>
         
    </div>
    <div id="win">  
        <div id="title">预约<span id="close" onclick="hide()">X</span></div>  
        <div id="content">
            <table>
                <tr>
                    <th>名称：</th>
                    <td><div id="resvDevName"></div></td>
                </tr>
                   <tr>
                    <th>开始时间:</th>
                    <td><select id="selectBeginTime"></select></td>
                </tr>
                   <tr>
                    <th>结束时间:</th>
                    <td><select id="selectEndTime"></select></td>
                </tr>
                <tr>
                    <td><input type="button" value="确定" /></td>
                    <td><input type="button" value="关闭" onclick="hide();" /></td>
                </tr>
            </table>
        </div>  
    </div> 
</form>

</body>
</html>
