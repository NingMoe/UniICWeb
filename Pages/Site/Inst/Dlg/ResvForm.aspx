<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ResvForm.aspx.cs" Inherits="_Default"%>

<%@ Register Src="~/Modules/ResvTable.ascx" TagPrefix="uc1" TagName="ResvTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">

<style type="text/css">
	.inputText {
		border:1px solid #808080;
		width:200px;
		height:18px;
	}
	.ClsItem { 
		list-style-type: none; margin: 0; padding: 0; 
		width:300px;
		min-height: 20px;
		max-height: 100px;
		overflow: visible;
		overflow-y:auto; 
		list-style: none;
	}
	.ClsItem li { margin: 3px; padding: 3px; font-size: 14px; height: 18px; line-height:18px;overflow:hidden;}
	.ClsItem li span { float:right;margin-top:1px; }
	.emptyCls {
		font-size:16px;
		text-align:left;
	}
	#resultCls {
		border: 0px solid #808080;
	}
	#trash {
		border:2px dashed #808080;
	}
	.miniTrash {
		width:20px;
		height:20px;
		visibility:hidden;
	}

	.InputStealth {
		width:1px;
		height:1px;
		border:0px;
		display:inline;
		margin:0px;
		padding:0px;
		visibility:hidden;
	}

	.tblResv {
		margin:10px;
	}

	.tblResv td {
		padding:6px;
	}
	.tblLabel {
		text-align:right;
		vertical-align:top;
	}
	.tblInput {
	}
	.tblBtn {
		text-align:center;
	}

	.recvintro {
		margin:10px;
		margin-top:30px;
	}
	.intro_title {
		font-size: 12px;
		font-weight:bold;
		background:#cccccc;
	}
	.recvintro_text {
		text-indent:22px;
	}
	.recvintro_img {
		margin:6px;
		float:right;
	}

	.tblEditCls {
		width:100%;
		height:100%;
	}
	 .tblEditCls td {
		vertical-align:top;
		height: 60px;
		width: 100px;
		padding:0px;

	 }
	#tdTrash {
		vertical-align:bottom;
	}
	.searchCls {
		height:20px;
		margin-bottom:6px;
	}
	.ui-autocomplete-loading {
		background: white url('./themes/img/ui-anim_basic_16x16.gif') right center no-repeat;
	}

	/*====combobox=====*/
	.ui-combobox {
		position: relative;
		display: inline-block;
	  }
	  .ui-combobox-toggle {
		position: absolute;
		top: 0;
		bottom: 0;
		margin-left: -1px;
		padding: 0;
		/* support: IE7 */
		*height: 1.7em;
		*top: 0.1em;
	  }
	  .ui-combobox-input {
		margin: 0;
		padding: 0.3em;
	  }


	.recvintro_text_tbl td{
		text-align:left;
		padding:3px;
	}

	.resv ul{
		list-style:none;
		float:none;
		clear:both;
	}
	.resv li{
		list-style:none;
		float:none;
		clear:both;
	}
	.resv .sec {
		display: block;
		border:1px solid #cccccc;
		width:120px;
		float:left;
	}
	.resv .memo {
		display:block;
		border:1px solid #cccccc;
		width:220px;
		float:left;
	}
</style>
<form id="Form1" runat="server">
<div id="PanelContent">
<div class="Box" theme="box960x550white.png" borderWeight="10px" width="900" height="440px">
	<div class="title">新建预约  ---  <%=m_szRoomName %></div>

	<div class="resv">
		<table class="tblResv" border="0">
			<tr>
				<td class="tblLabel">日期：</td>
				<td class="tblInput"><p><%=m_szDate %>
					<%--<input name="m_szDate" id="m_szDate" value="<%=m_szDate %>"/> --%>
				</p></td>
			</tr>
			<tr>
				<td class="tblLabel">时间：</td>
				<td class="tblInput"><p><select name="dwBeginSec" id="dwBeginSec" class="validate[required,funcCall[checkSec]]"></select>&nbsp;至&nbsp;
						<select name="dwEndSec" id="dwEndSec" class="validate[required,funcCall[checkSec]]"></select></p>
				</td>
			</tr>
			<tr>
				<td class="tblLabel">实验计划：</td>
				<td class="tblInput">
					<input name="szCourseName" id="szCourseName" class="inputText validate[required]"  value="选择实验计划"/>
				</td>
			</tr>
			<tr>
				<td class="tblLabel">实验项目：</td>
				<td class="tblInput">
					<input name="szResvName" id="szResvName" class="inputText validate[required]"  value="选择实验项目"/>
				</td>
			</tr>
			<!--<tr>
				<td class="tblLabel">日期：</td>
				<td class="tblInput"><p><input name="dwDate" id="dwDate" class="validate[required,custom[date],future[NOW]]"/></p></td>
			</tr>-->
			<tr>
				<td class="tblLabel">参与学生班级：</td>
				<td class="tblInput">
					<table class="tblEditCls"><tr><td>                            
						<input id="searchCls" class="searchCls" style="width:200px"/>
						  <input name="GroupList" id="GroupList" class="InputStealth validate[required]" /><input name="GroupListName" id="GroupListName" class="InputStealth" />
						<ul id="resultCls" class="ClsItem">
						  <div class="ui-state-disabled emptyCls">未选择班级</div>
						</ul>
					</td><td id="tdTrash">
						<ul id="trash" class="ClsItem miniTrash">
						  <div class="ui-state-disabled emptyCls">未选择班级</div>
						</ul>
					</td></tr></table>
			   </td>
			</tr><!--
			<tr>
				<td class="tblLabel">申请报告：</td>
				<td class="tblInput"><p><a href="#">点击下载申请模板</a></p><p><input type="file" name="report" /></p></td>
			</tr>-->
			<tr>
				<td class="tblLabel">申请说明：</td>
				<td class="tblInput"><p><textarea rows="5" cols="70" name="szMemo" id="szMemo" class="validate[required]">计划内教学预约</textarea></p></td>
			</tr>
			<tr>
				<td class="tblBtn" colspan="2">
					<asp:Button ID="Button_OK" runat="server" Text="确定预约" OnClick="Button_OK_Click" /><button type="button" id="btnBack">取消</button>
				</td>
			</tr>
		</table>
	</div>
</div>
<div id="tabIntroResv" class="Box" theme="box960x550.png" width="900" height="220px">
	<ul>
		<li><a href="#tabIntro">实验室简介</a></li>
		<li><a href="#tabResvStat">实验室预约状态</a></li>
	</ul>
	<div id="tabIntro">
		<div class="recvintro_img">
			<img src="../Upload/a.jpg" />
		</div>
		<div id="recvintro_text" class="recvintro_text">
			<p>
				实验室主要职能是承担全院计算机基础课程的教学上机实验和自由上机，是各类计算机课程教学、计算机辅助教学及上机实验的理想场所。
			</p>
			<p><table class="recvintro_text_tbl">
				<tr><td>硬件配置：</td><td>DELL 显示器，DELL 双臂支架</td></tr>
				<tr><td>软件配置：</td><td>Windows Server 2008</td></tr>
				<tr><td>预约安排：</td><td><%=m_szResvInfo %></td></tr>

			   </table></p>
			<p></p>
		</div>
		<div class="clear"></div>
	</div>
	<div id="tabResvStat">
		<uc1:ResvTable runat="server" ID="ResvTable" />
	</div>
</div>
</div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" >
    //combobox
        (function ($) {
            $.widget("ui.combobox", {
                input: null,
                _create: function () {
                    this.wrapper = $("<span>")
                      .addClass("ui-combobox")
                      .insertAfter(this.element);

                    this._createAutocomplete();
                    this._createShowAllButton();
                },

                _createAutocomplete: function () {
                    var selected = this.element.children(":selected"),
                      value = selected.val() ? selected.text() : "";

                    this.input = $("<input>")
                      .appendTo(this.wrapper)
                      .val(value)
                      .attr("title", "")
                      .addClass("ui-state-default ui-combobox-input ui-widget ui-widget-content ui-corner-left")
                      .autocomplete({
                          delay: 0,
                          minLength: 0,
                          source: $.proxy(this, "_source")
                      })
                      .tooltip({
                          tooltipClass: "ui-state-highlight"
                      });

                    this._on(this.input, {
                        autocompleteselect: function (event, ui) {
                            ui.item.option.selected = true;
                            this._trigger("select", event, {
                                item: ui.item.option
                            });
                        },

                        autocompletechange: "_removeIfInvalid"
                    });
                },

                _createShowAllButton: function () {
                    var wasOpen = false;

                    $("<a>")
                      .attr("tabIndex", -1)
                      //.attr("title", "Show All Items")
                      .tooltip()
                      .appendTo(this.wrapper)
                      .button({
                          icons: {
                              primary: "ui-icon-triangle-1-s"
                          },
                          text: false
                      })
                      .removeClass("ui-corner-all")
                      .addClass("ui-corner-right ui-combobox-toggle")
                      .mousedown(function () {
                          if (this.input) {
                              wasOpen = this.input.autocomplete("widget").is(":visible");
                          }
                      })
                      .click(function () {
                          if (this.input) {
                              this.input.focus();

                              // Close if already visible
                              if (wasOpen) {
                                  return;
                              }

                              // Pass empty string as value to search for, displaying all results
                              this.input.autocomplete("search", "");
                          }
                      });
                },

                _source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response(this.element.children("option").map(function () {
                        var text = $(this).text();
                        if (this.value && (!request.term || matcher.test(text)))
                            return {
                                label: text,
                                value: text,
                                option: this
                            };
                    }));
                },

                _removeIfInvalid: function (event, ui) {

                    // Selected an item, nothing to do
                    if (ui.item) {
                        return;
                    }

                    // Search for a match (case-insensitive)
                    var value = this.input.val(),
                      valueLowerCase = value.toLowerCase(),
                      valid = false;
                    this.element.children("option").each(function () {
                        if ($(this).text().toLowerCase() === valueLowerCase) {
                            this.selected = valid = true;
                            return false;
                        }
                    });

                    // Found a match, nothing to do
                    if (valid) {
                        return;
                    }

                    // Remove invalid value
                    this.input
                      .val("")
                      //.attr("title", value + " didn't match any item")
                      .tooltip("open");
                    this.element.val("");
                    this._delay(function () {
                        this.input.tooltip("close").attr("title", "");
                    }, 2500);
                    this.input.data("ui-autocomplete").term = "";
                },

                _destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                }
            });
        })(jQuery);
    //combobox

    //datepicker
    jQuery(function ($) {
        $.datepicker.regional['zh-CN'] = {
            closeText: '关闭',
            prevText: '&#x3C;上月',
            nextText: '下月&#x3E;',
            currentText: '今天',
            monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
            '七月', '八月', '九月', '十月', '十一月', '十二月'],
            monthNamesShort: ['一月', '二月', '三月', '四月', '五月', '六月',
            '七月', '八月', '九月', '十月', '十一月', '十二月'],
            dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
            dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
            dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
            weekHeader: '周',
            dateFormat: 'yy/mm/dd',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: true,
            yearSuffix: '年'
        };
        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
    });
    </script>
<script language="javascript" type="text/javascript" >
    $(function () {
        $('#PanelContent').UIBox({ width: "900"});

        $("#tabIntroResv").tabs();

        $("#m_szDate").datepicker().val(function () { return GetDateStr($(this).val());});

        $("#Course").combobox().toggle();

        $("#dwDate").datepicker({
            minDate: +1, maxDate: "+1M +10D",
            onClose: function () {
                $(this).validationEngine('validate');
            }
        });

        var szSecData = <%=m_szSecData%>;
        var htmlBeginSec = "";
        for(var i = 0; i < szSecData.length; i++)
        {
            htmlBeginSec += "<option value='"+szSecData[i][0].i+"'>"+szSecData[i][0].v+"</option>";
        }

        function OnBeginChg(){
            var BeginV = $("#dwBeginSec").val();
            for(var i = 0; i < szSecData.length; i++)
            {
                if(szSecData[i][0].i == BeginV)
                {
                    var htmlEndSec = ""
                    for(var j = 1; j < szSecData[i].length; j++)
                    {
                        htmlEndSec += "<option value='"+szSecData[i][j].i+"'>"+szSecData[i][j].v+"</option>";
                    }
                    $("#dwEndSec").html(htmlEndSec)
                    break;
                }
                
            }
        }
        $("#dwBeginSec").html(htmlBeginSec).change(OnBeginChg).val(function(){return $(this).attr("value");});
        OnBeginChg();
        $("#dwEndSec").val(function(){return $(this).attr("value");});

        $("#dwBeginSec").attr("disabled","disabled");
        $("#dwEndSec").attr("disabled","disabled");

        function ShowEmptyCls() {
            
            var clsItems = $("#resultCls li");
            if (clsItems.length == 0) {
                $("#resultCls .emptyCls").show();
                $("#GroupList").val("");
                $("#GroupListName").val("");
            } else {
                $("#resultCls .emptyCls").hide();
                var newv = "";
                var newv_name = "";
                for(var i = 0; i < clsItems.length; i++)
                {
                    newv += $(clsItems[i]).data("id") + ",";
                    var snvame = $(clsItems[i]).text();
                    snvame = snvame.replace(/\,/g," ");
                    newv_name += snvame + ",";
                }
                $("#GroupList").val(newv);
                $("#GroupListName").val(newv_name);
            }
            if ($("#trash li").length == 0) {
                $("#trash .emptyCls").show();
            } else {
                $("#trash .emptyCls").hide();
            }
        }

        function ClsItemClick() {
            var pThis = $(this);
            if (pThis.parent().attr("id") == "resultCls") {
                pThis.addClass("ui-state-disabled");
                pThis.appendTo($("#trash"));
            } else {
                pThis.removeClass("ui-state-disabled");
                pThis.appendTo($("#resultCls"));
            }
            ShowEmptyCls();
        }

        function AddClsItem(tid,tvalue)
        {
            if ($("li[data-id='" + tid + "']", $("#resultCls")).length == 0) {
                $('<li class="ui-widget-content" data-id="' + tid + '"><span class="ui-icon ui-icon-check"></span>' + tvalue + '</li>')
                    .click(ClsItemClick)
                    .appendTo($("#resultCls"))
                    .hover(function () {
                        var icon = $("span", this);

                        $(this).addClass("ui-state-highlight");
                                
                        icon.removeClass("ui-icon-check");
                        icon.addClass("ui-icon-circle-close");
                    }, function () {
                        var icon = $("span", this);

                        $(this).removeClass("ui-state-highlight");

                        icon.addClass("ui-icon-check");
                        icon.removeClass("ui-icon-circle-close");
                    });
                ShowEmptyCls();
            }
        }

        var szDefGrpLst = $("#GroupList").val();
        if(szDefGrpLst)
        {
            var arrayGrp = szDefGrpLst.split(",");
            var arrayGrpName = $("#GroupListName").val().split(",");
            for(var i = 0; i < arrayGrp.length && i < arrayGrpName.length;i++)
            {
                if(arrayGrp[i] && arrayGrpName[i])AddClsItem(arrayGrp[i],arrayGrpName[i]);
            }
        }

        var searchTip = "输入班级名称，增加预约班级";
        $("#searchCls").focus(function () {
            $(this).val("").css("color", "#000000");
        }).blur(function () {
            $(this).val(searchTip).css("color", "#aaaaaa");
        }).val(searchTip).css("color", "#aaaaaa").autocomplete({
            source: "Data_searchCls.aspx",
            select: function (event, ui) {
                if(ui.item)
                {
                    if(ui.item.id && ui.item.id != "")
                    {
                        AddClsItem(ui.item.id,ui.item.label);                        
                    }
                }
                $(this).val("");
                $(this).blur();
                return false;
            },
            response: function( event, ui ) {
                if(ui.content.length == 0)
                {
                    ui.content.push({label:" 未找到配置项 "});
                }
            }
        });
                
        var memoTip = "请认真填写研讨内容，否则不予审核通过，不超过40个字";
        $("#memo").focus(function () {
            if ($(this).val() == memoTip) {
                $(this).val("").css("color", "#000000");
            }
        }).blur(function () {
            if ($(this).val() == "" || $(this).val() == memoTip) {
                $(this).val(memoTip).css("color", "#aaaaaa");
            }
        }).val(memoTip).css("color", "#aaaaaa");


        $("#btnBack").button().click(function () {
            Dlg_Cancel();
        });
        $("#<%=Button_OK.ClientID%>").button();
    });

    function checkSec(field, rules, i, options) {
        if (field.attr("id") == "dwBeginSec") {
            if (parseInt($("#dwBeginSec").val()) > parseInt($("#dwEndSec").val())) {
                return "开始节次 不能大于 结束节次";
            } else {
                $("#dwEndSec").validationEngine("hide");
            }
        } else {
            if (parseInt($("#dwBeginSec").val()) > parseInt($("#dwEndSec").val())) {
                return "结束节次 不能小于 开始节次";
            } else {
                $("#dwBeginSec").validationEngine("hide");
            }
        }
    }
</script>
</asp:Content>
