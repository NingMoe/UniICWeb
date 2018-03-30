<%@ Page Language="C#" AutoEventWireup="true" CodeFile="css.aspx.cs" Inherits="_Default" %>
html{
	padding:0px;margin:0px;overflow:visible;
}
body{
	padding:auto;margin:0px;overflow:visible;
}
table{
	border-collapse:collapse;border-spacing:0;
}
img{
	border:0px;
	vertical-align: text-bottom;
}
td
{
padding-right:3px;
padding-left:3px;
padding-top:3px;
padding-bottom:3px;
}

a,a:hover,a:visited,a:active{color: #039; outline:none; blur:expression(this.onFocus=this.blur());}
a:hover {
	text-decoration:none;
}

ins,a {
	text-decoration:none;
}


td.label
{
	text-align:right;
	font-size:14px;
}

.labelicon
{
	width:20px;
	vertical-align:text-bottom;
	margin-right:5px;
	
	filter: Alpha(Opacity=80);
    -moz-opacity: 0.8;
    opacity: 0.8;
}

.tblBody{
	height: 100%;
	text-align:center;
	vertical-align:top;
}
.tblBody td
{
	height: 100%;
}

.THead
{
	font-size: 26px;
	margin:auto;
	text-align:center;
}

.Head
{
	font-size: 14px;
	margin:auto;
	text-align:center;
	border-bottom: 1px dotted #ddd;
    margin-bottom: 3px;
    float:none;
    line-height: 30px;
    height: 30px;
    position: relative;
    color: #666;
}

.navback
{
    -webkit-border-radius: 6px;
	-moz-border-radius: 6px;
    border-radius: 6px;
        
    background: #ddd;
    padding:3px;
    margin: 0px;
    line-height: 20px;
    position: absolute;
    left: 0px;
    color: #06b;
    
    margin-left: 2px;
    padding-right: 6px;
    cursor:pointer;
    
    font-family: 微软雅黑;
    font-size: 16px;
    vertical-align: top;
}

.navarrow
{
    font-size: 16px;
    vertical-align:top;
}

.Head2
{
	font-size: 14px;
	margin:auto;
	text-align:center;
	border-bottom: 1px dashed #bbb;
    margin-bottom: 3px;
    float:none;
}

.Content
{
	text-align:center;
}

.tblContent
{
	margin:auto;
	width: 99%;
}

.tblEContent
{
	margin:auto;
	width: 99%;
}

.tr-even-bg{
		background:#eee;
}

.tr-odd-bg{
		background:#fff;
}

.FormBlock
{
    border-top: 1px outset #ccc;
    border-left: 1px outset #ccc;
    border-bottom: 2px outset #fff;
    border-right: 2px outset #fff;
    
    padding: 6px;
    margin: 3px;
    
    -webkit-border-radius: 6px;
	-moz-border-radius: 6px;
    border-radius: 6px;
}

.tblLoginContent
{
	margin:auto;
	width: 300px;
}

.msg
{
	text-align:center;
}

thead
{
	background: #dfdfdf;
}

.member
{
	letter-spacing: 1;
	text-align:left;
	margin-left: 8px;
	color: #666;
}

.date
{
	letter-spacing: 1;
	text-align:left;
	margin-left: 8px;
	color: #666;
}

fieldset{
	width:auto;
	display:inline;
}

.tail
{
	color: #fff;
	text-align:center;
	font-size:13px;
	text-shadow: 1px 2px 3px #000;
}

.little
{
	size:10px;
}

.valname
{
    text-align:center;
font-size:12px;
}

.inNumValue
{
    width: 80px;
    font-size:16px;
}

button
{
    background: #ddd;
    color: #039;
    font-size:16px;
    border:1px solid #039;
    -webkit-border-radius: 3px;
	-moz-border-radius: 3px;
    border-radius: 3px;
    vertical-align: middle;
    min-height:30px;
}

button.btnCancel
{
    /*background: #d60;
    color: #fff;
    border:1px solid #222;*/
}

.LBtn button
{
    background: #3c3;
    color: #000;
    border:1px solid #222;
    width:100px;
    height:50px;
}
.LTimeBtn button
{
    background: #3c3;
    color: #000;
    border:1px solid #222;
    width:100px;
    height:30px;
}
.ItemList
{
    background: #ccc;
    padding: 1px;
}

.ItemList .Item
{
    border:1px solid #666;
    
    -webkit-border-radius: 6px;
	-moz-border-radius: 6px;
    border-radius: 6px;
    background: white;
    padding: 0px;
    margin: 6px;
    color: #333;
}

.ItemList .Item>div
{
    border-top: 1px dashed #888;
}

.ItemList .LHead
{
    padding: 6px;
    font-weight: bold;
    text-align: left;
    border-top:0px !important;
}

.ItemList .KHead
{
    padding: 8px;
    font-weight: bold;
    text-align: left;
    border-top:0px !important;
    
    display:block;
    margin: 6px;
}
.ItemList .KHead .memo
{
    font-size: 12px;
    padding-left: 10px;
    color: #888;
}

.ItemList .KHead .stat1
{
    float:right;
    color: #0a0;
    font-size: 11px;
}

.ItemList .KHead .stat2
{
    float:right;
    color: #777;
    font-size: 11px;
}

.ItemList .LContent
{
    border-top:0px !important;
    padding: 6px;
    padding-top: 10px;
    padding-bottom: 10px;
    min-height:50px;
    text-align: left;
}

.LGraphics
{
    padding-top: 10px;
    padding-bottom: 10px;
    text-align: left;
}

.LGraphics span
{
    padding: 3px;
    font-size:12px;
    vertical-align: middle;
}

.LGraphics span.enableStat
{
    color:#0a0;
}

.LGraphics span.disableStat
{
    color:#999;
}

.LGraphics>canvas
{
    background: white;
    display:block;
    width: 99%;
    height:auto;
    margin: 2px;
}

.LBtn
{
    padding: 3px;
    text-align:right;
}
.LTimeBtn
{
    padding: 3px;
    width:99%;
}

.Placeholder
{
    padding: 0px;
    display: block;
    height: 50px;
}

.Menu
{
    padding: 6px;
    font-size:20px;
    margin:0px;

    z-index:100;
    position: fixed;
    top:0px;
    white-space:nowrap;
        
    width: 100%;
    background: #fff;
    border-bottom: 1px solid #999;
}

#Menu a
{
    outline:none;
    
    background: white;
    color: #06b;
    padding: 6px;
        
    font-size: 15px;
    border: 1.5px solid #06b;
    border-right-style: none;
}

#Menu a.current
{
    color: white;
    background: #06b;
    outline:none;
}

.itemfirst
{
    -webkit-border-top-left-radius: 4px;
	-moz-border-top-left-radius: 4px;
    border-top-left-radius: 4px; 

    -webkit-border-bottom-left-radius: 4px;
	-moz-border-bottom-left-radius: 4px;
    border-bottom-left-radius: 4px; 
}

.itemlast
{
    border-right-style: solid !important;
   
    -webkit-border-top-right-radius: 4px;
	-moz-border-top-right-radius: 4px;
    border-top-right-radius: 4px; 

    -webkit-border-bottom-right-radius: 4px;
	-moz-border-bottom-right-radius: 4px;
    border-bottom-right-radius: 4px;
}

.curUser
{
    float: right;
    font-size: 11px;
    color: #999;
    padding-right: 2px;
}

.copyright
{
    border-top: 1px dotted #eee;
    text-align:center;
    font-size: 11px;
    color: #999;
}
