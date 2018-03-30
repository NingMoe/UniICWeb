<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="_Default" %>
<html>
<head>
    <meta http-equiv=Content-Type content="text/html;charset=utf-8">
    <title></title>
	<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<link rel="stylesheet" type="text/css" media="screen" href="css.aspx" />
    <script type="text/javascript" src="zepto.min.js"></script>
    <script type="text/javascript" src="jquery-1.4.2.min.js"></script>
    
 <script type="text/javascript">
     (function ($) {
         $.extend({
             //获取鼠标当前坐标
             mouseCoords: function (ev) {
                 if (ev.pageX || ev.pageY) {
                     return { x: ev.pageX, y: ev.pageY };
                 }
                 return {
                     x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
                     y: ev.clientY + document.body.scrollTop - document.body.clientTop
                 };
             },
             //获取样式值
             getStyle: function (obj, styleName) {
                 return obj.currentStyle ? obj.currentStyle[styleName] : document.defaultView.getComputedStyle(obj, null)[styleName];
                 //				return obj.currentStyle ? obj.currentStyle[styleName] : document.defaultView.getComputedStyle(obj,null).getPropertyValue(styleName);
             }
         });

         // 元素拖拽插件
         $.fn.dragDrop = function (options) {
             var opts = $.extend({}, $.fn.dragDrop.defaults, options);

             return this.each(function () {

                 //是否正在拖动
                 var bDraging = false;
                 //移动的元素
                 var moveEle = $(this);
                 //点击哪个元素，以触发移动。
                 //该元素需要是被移动元素的子元素（比如标题等）
                 var focuEle = opts.focuEle ? $(opts.focuEle, moveEle) : moveEle;
                 if (!focuEle || focuEle.length <= 0) {
                     alert('focuEle is not found! the element must be a child of ' + this.id);
                     return false;
                 }
                 // initDiffX|Y : 初始时，鼠标与被移动元素原点的距离
                 // moveX|Y : 移动时，被移动元素定位位置 (新鼠标位置与initDiffX|Y的差值)
                 // 如果定义了移动中的回调函数，该对象将以参数传入回调函数。
                 var dragParams = { initDiffX: '', initDiffY: '', moveX: '', moveY: '' };
                 var vInist = opts.inist;
                 //被移动元素，需要设置定位样式，否则拖拽效果将无效。
                 var vInitPostionX = 0;
                 if (opts.inistx != null) {
                     vInitPostionX = Number(opts.inistx);
                 }
                 var vInitPostionY = 0;
                 if (opts.inisty != null) {
                     vInitPostionY = Number(opts.inisty);
                 }

                 moveEle.css({ 'position': 'absolute', 'left': vInitPostionX, 'top': vInitPostionY });
                 if (opts.isDrag) {
                     //点击时，记录鼠标位置
                     //DOM写法： getElementById('***').onmousedown= function(event);
                     focuEle.bind('mousedown', function (e) {
                         //标记开始移动
                         bDraging = true;
                         //改变鼠标形状
                         moveEle.css({ 'cursor': 'move' });
                         //捕获事件。（该用法，还有个好处，就是防止移动太快导致鼠标跑出被移动元素之外）
                         if (moveEle.get(0).setCapture) {
                             moveEle.get(0).setCapture();
                         }
                         //（实际上是鼠标当前位置相对于被移动元素原点的距离）
                         // DOM写法：(ev.clientX + document.body.scrollLeft - document.body.clientLeft) - document.getElementById('***').style.left;
                         dragParams.initDiffX = $.mouseCoords(e).x - moveEle.position().left;
                         dragParams.initDiffY = $.mouseCoords(e).y - moveEle.position().top;
                     });

                     //移动过程
                     focuEle.bind('mousemove', function (e) {
                         if (bDraging) {
                             //被移动元素的新位置，实际上鼠标当前位置与原位置之差
                             //实际上，被移动元素的新位置，也可以直接是鼠标位置，这也能体现拖拽，但是元素的位置就不会精确。
                             dragParams.moveX = $.mouseCoords(e).x - dragParams.initDiffX;
                             dragParams.moveY = $.mouseCoords(e).y - dragParams.initDiffY;

                             //是否限定在某个区域中移动.
                             //fixarea格式: [x轴最小值,x轴最大值,y轴最小值,y轴最大值]
                             if (opts.fixarea) {
                                 if (dragParams.moveX < opts.fixarea[0]) {
                                     dragParams.moveX = opts.fixarea[0]
                                 }
                                 if (dragParams.moveX > opts.fixarea[1]) {
                                     dragParams.moveX = opts.fixarea[1]
                                 }

                                 if (dragParams.moveY < opts.fixarea[2]) {
                                     dragParams.moveY = opts.fixarea[2]
                                 }
                                 if (dragParams.moveY > opts.fixarea[3]) {
                                     dragParams.moveY = opts.fixarea[3]
                                 }
                             }

                             //移动方向：可以是不限定、垂直、水平。
                             if (opts.dragDirection == 'all') {
                                 //DOM写法： document.getElementById('***').style.left = '***px'; 
                                 moveEle.css({ 'left': dragParams.moveX, 'top': dragParams.moveY });
                             }
                             else if (opts.dragDirection == 'vertical') {
                                 moveEle.css({ 'top': dragParams.moveY });
                             }
                             else if (opts.dragDirection == 'horizontal') {
                                 moveEle.css({ 'left': dragParams.moveX });
                             }

                             //如果有回调

                         }
                     });

                     //鼠标弹起时，标记为取消移动
                     focuEle.bind('mouseup', function (e) {
                         bDraging = false;

                         moveEle.css({ 'cursor': 'default' });
                         if (moveEle.get(0).releaseCapture) {
                             moveEle.get(0).releaseCapture();
                         }
                         if (opts.callback) {
                             //将dragParams作为参数传递
                             opts.callback.call(opts.callback, dragParams, moveEle.attr("id"));
                         }
                     });
                 }
             });
            
             };
         
         //默认配置
         $.fn.dragDrop.defaults =
         {
             focuEle: null,			//点击哪个元素开始拖动,可为空。不为空时，需要为被拖动元素的子元素。
             callback: null,			//拖动时触发的回调。
             dragDirection: 'all',    //拖动方向：['all','vertical','horizontal']
             fixarea: null,			//限制在哪个区域拖动,以数组形式提供[minX,maxX,minY,maxY]
             inistx: null,
             inisty: null,
             isDrag:true
         };

     })(jQuery);

     $.ajax({
         url: "../Ajax/AGetDevice.aspx?op=get",
         success: function (data) {
             var vDevList = eval(data);
             var vDivContant = $("#dragContainer");
             for (var i = 0; i < vDevList.length; i++)
             {
                 var x = Number(vDevList[i].x);
                 var y = Number(vDevList[i].y);
                 var vSigan = "☆";

                 var isDrag=<%=szDrag%>;
                 if(isDrag=="true")
                 {
                     isDrag=true;
                 }
                 else{
                     isDrag=false;
                 }
                 var vDivTemp = $("<div style='text-align:center;' id='" + vDevList[i].devID + "'></div>");
                 if(isDrag)
                 {
                 vDivTemp.click(function () {
                     alert(vDivTemp.attr("id"));
                 });
             }
                 vDivTemp.append("<div id='div" + vDevList[i].devID + "'>" + vSigan + "</div><div>" + vDevList[i].DevName + "</div>");
                 
                 vDivTemp.dragDrop({
                     fixarea: [0, $('#dragContainer').width() - 50, 0, $('#dragContainer').height() - 50],
                     inistx:x,
                     inisty: y,
                     isDrag:isDrag,
                     callback: function (params,eleid) {
                         $('#span1').text('X:' + params.moveX + ' Y:' + params.moveY);
                         var id = eleid;
                         var x = params.moveX;
                         var y = params.moveY;
                         $.ajax({
                             url: "Ajax/AGetDevice.aspx?op=save",
                             data: { devid: id,x: x, y: y },
                             success: function (data) {

                             }
                         });
                     }
                 });
                 vDivContant.append(vDivTemp);
             }
         }
     });
     $(function () {
         //限定区域，有回调函数。
         $('#dragDiv').dragDrop({
             fixarea: [0, $('#dragContainer').width() - 50, 0, $('#dragContainer').height() - 50], callback: function (params) {
                 
             }
         });
     });
 </script>
</head>
<body>
<form action="#" method="post" enctype="application/x-www-form-urlencoded">
    <div  id="divContant">
<span>  ★:有人</span>
        <span>:空闲</span>
    </div>
  <div id="dragContainer" onselectstart="return false" style="position:relative;border:1px dashed blue;">
     <img src="Labimg/<%=szLabID %>.jpg"  alt="" />
      <div id="dragDiv">
          
	  </div>
      
	</div>

	  <span id="span1"></span>
    <div class="content" style="position:relative">
        <div  style="position:absolute;left:20px;top:30px"></div>
        <div style="position:absolute;left:60px;top:60px">☆</div>
         
    </div>

</form>

</body>
</html>
