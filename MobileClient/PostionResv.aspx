<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostionResv.aspx.cs" Inherits="_Default" %>
<div class="Div" id="postionResv">
    
    <div class="Head">
       <div><span class="navback"><font class="navarrow">◀</font> 选择时间</span></div> 
    </div>
     
     <input type="hidden" name="dwBeginTime" id="dwBeginTime" value="<%=dwBeginTime %>" />
    <input type="hidden" name="dwEndTime" id="dwEndTime" value="<%=dwEndTime %>" />
    <input type="hidden" name="szLabID" id="szLabID" value="<%=szLabID %>" />
    <input type="hidden" name="uNeedHour" id="uNeedHour" value="<%=uNeedhour %>" />
    <input type="hidden" name="timePart" id="timePart" value="<%=uTimePart %>" />
    <input type="hidden" name="resvDate" id="resvDate" value="<%=resvDate %>" />
    <input type="hidden" name="resvDevID" id="resvDevID" value="" />
    <div  id="divContant" style="position:relative">
      
        <table>
            <tr>
                <td style="width:100px;">
                   <img src="img/ResvStatePng/A100.png" width="20px" height="20px" />全部空闲
                </td>
                <td style="width:100px;">
                   <img src="img/ResvStatePng/A75.png"  width="20px" height="20px"  />75%空闲
                </td>
                <td style="width:100px;">
                    <img src="img/ResvStatePng/A50.png"  width="20px" height="20px"  />50%空闲
                </td>
                 <td style="width:100px;">
                    <img src="img/ResvStatePng/A25.png"  width="20px" height="20px"  />25%空闲
                </td>
                <td style="width:100px;">
                    <img src="img/ResvStatePng/A0.png"  width="20px" height="20px"  />无空闲
                </td>
            </tr>
        </table>
      
        
    </div>
  <div id="dragContainer" onselectstart="return false" style="position:relative;border:1px dashed blue;">
     <img src="../clientweb/upload/devimg/floorplan/rm<%=szLabID %>.jpg"  alt="" style="width:839px;height:442px" />
      <div id="dragDiv">
          
	  </div>
      
	</div>

	  <span id="span1"></span>
    <div class="content" style="position:relative">
        <div  style="position:absolute;left:20px;top:30px"></div>
        <div style="position:absolute;left:60px;top:60px">☆</div>
         
    </div>
    <div id="win">  
        <div id="title"><span id="dlgclose"></span></div>  
        <div id="content">
            <div style="margin:auto 0px;text-align:center">
            <div style="margin:10px">
                <span>名称：</span>
                <span id="resvDevName"></span>
            </div>
            <div style="margin:10px">
                <span>开始时间：</span>
                <select id="selectBeginTime"></select>
            </div>

            <div  style="margin:10px">
                <span>结束时间：</span>
                <select id="selectEndTime"></select>
            </div>
            <div  style="margin:10px">
<input type="button" id="btnResv" value="预约" class="btnTimeClass" />
                <input type="button" id="btnClose" value="关闭" class="btnTimeClass" onclick="hide();" />
            </div>
            
                 <div style="margin:10px;border-top: 1px dashed #888;">
                    <span>已预约信息：</span>
                    <div id="resvinfo"></div>
                </div>
           </div>
        </div>  
    </div> 
</div>

<head>

    <style>
        .cycleGreen {
            background-color:orange;
            border-radius: 10px;
            width: 15px;
            height: 15px;
            display: inline-block;
        }
        .cycleOrange {
            /* background-color:green;*/
            border-radius: 10px;
            width: 15px;
            height: 15px;
            display: inline-block;
        }
        #win{  
    /*边框*/  
    border:1px black solid;  
    /*窗口的高度和宽度*/  
    width : 200px;  
    height: 250px;  
    /*窗口的位置*/  
    position : absolute;  
    top : 100px;  
    left: 200px;  
    background-color:#6699cc;
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
#dlgclose{  
    margin-left: 390px;  
    /*当鼠标移动到X上时，出现小手的效果*/  
    cursor: pointer;  
} 
    </style>
</head>
