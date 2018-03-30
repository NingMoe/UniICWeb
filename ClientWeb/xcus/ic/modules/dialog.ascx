<%@ Control Language="C#" AutoEventWireup="true" CodeFile="dialog.ascx.cs" Inherits="WebUserControl" %>
<!-- ��½�������ȫվģ�� -->
<div id="logindialog" class="dialog">
    <form name='login' onsubmit="return false;">
        <h6>��¼</h6>
        <!-- ��½�� begin -->
<span style='color:#aaa;font-size:12px;line-height:28px;'>ʹ��<%=GetConfig("idIntro")%>��¼��<%=GetConfig("pwdIntro")%>��</span>
        <table>
            <tr>
                <th><p>ѧ��</p></th>
                <td><input name="id"  id="id"type="text" class="input_txt" placeholder="<%=GetConfig("idIntro")%>"/>
								<span class="error" id="reg_number_msg" style="display:none;">ѧ�Ŵ���</span></td>
            </tr>        
            <tr>
                <th><p>����</p></th>
                <td>
                  <input name="pwd" id="pwd" type="password" onBlur="blurPassword(this)" onfocus="focusPassword()" class="input_txt" style="display:none;">
                   <input name="pwd_text" id="pwd_text" type="text" onFocus="focusPasswordText()" class="input_txt" placeholder="<%=GetConfig("pwdIntro")%>"/></td>
            </tr>
           
        </table>
        <div class="submitarea clear">
            <input type="submit" class="input_submit" value="��¼" /><a class="reg" style="cursor:pointer;display:<%=GetConfig("mustAct")=="1"?"":"none"%>">���û����ȼ���</a>
        </div>
        <!-- ��½�� end-->
        <div class="fail">
            <p id="login_msg"><!--��½ʧ����ʾ--></p>
        </div>
        <a href="" class="close">�ر�</a>
    </form>
</div>
<div id="regdialog" class="dialog">
    <form name='act' onsubmit="return false;">
        <h6>����</h6>
        <!-- ����� begin -->
        <table>
            <tr>
                <th><p>ѧ��</p></th>
                <td><input name="id" id="rid" type="text" class="input_txt" />
								<span class="error" id="reg_number_msg" style="display:none;">ѧ�Ŵ���</span></td>
            </tr>
            <tr>
                <th><p>����</p></th>
                <td><input name="pwd" id="rpwd" type="password" class="input_txt" /><%=GetConfig("pwdIntro")%>            
                </td>
            </tr>           
            <tr>
                <th><p>�ֻ�</p></th>
                <td><input name="phone" type="text" class="input_txt" />��֤�ֻ���ȷ<span class="error" id="reg_mobile_msg" style="display:none;">�ֻ���������</span></td>
            </tr>
            <tr>
                <th><p>����</p></th>
                <td><input name="mail" type="text" class="input_txt" />��֤������ȷ<span class="error" id="reg_mail_msg" style="display:none;">������������</span></td>
            </tr>
        </table>
        <div class="submitarea clear">
            <input type="submit" class="input_submit" value="����" />
        </div>
        <div class="fail">
            <p id="reg_msg"><!--�˿����Ѵ��ڼ���״̬�������ظ�����--></p>
        </div>
        <a href="" class="close">�ر�</a>
    </form>
</div>
<script>
var HrefPage="";
$(function(){
	$('form[name=login]').submit(function(){
		var data = $(this).serialize();
		$.ajax({
			type:"GET",
				url:"Ajax_Code/account.aspx?act=login&"+data,
				dataType:"json",
				success:function(object){
					if(Number(object.MsgId)>0)
						document.getElementById("login_msg").innerText=object.Message;
					else if(Number(object.MsgId)<0){
						alert(object.Message);
						$("#logindialog").dialog('close');
						$("#regdialog").dialog('open');
						if(object.Message.length>0)
							document.getElementById("login_msg").innerText=object.Message;
					}	else {
					    if(HrefPage=="")
						{
						    location.reload();
						}
						else
						{						  
						    location=HrefPage;
						}
					}					
				}
			});
		});



	$('form[name=act]').submit(function(){
		var value = $('input[name=mail]').val();
		if(!CheckEmailBox(value,$(this)))
			return false;
		
		value = $('input[name=phone]').val();
		if(!CheckPhoneBox(value,$(this)))
			return false;
		
		var data = $(this).serialize();
		MemberAjax({
			Prm:"act=act&"+data,
			Function:function(object)	{ 
				if(Number(object.MsgId)!=0){
					//alert("��������˺Ż����벻��ȷ�������ԡ�");
				//}
				//else if(Number(object.MsgId)<0){
					if($("#reg_msg").length>0){
						document.getElementById("reg_msg").innerText=object.Message;
					}
				} else {
					alert("����ɹ�������������ҳ��");
					location.reload();
					//$('#nav ul').toggle();
					//$('#nav ul li[class=welcome] span').html(object.Name);
					//document.getElementById("reg_msg").innerText="����ɹ����밴���Ͻǹرհ�ť";
				}
			}
		});
	});
			

	$("form[name=act] input[name=phone]").change(function(){
		CheckPhoneBox(this.value,$("form[name=act]"));
	});

	$("form[name=act] input[name=mail]").change(function(){
		CheckEmailBox(this.value,$("form[name=act]"));
	});

	$('li[class=logout] a').click(function () {
	    if (typeof(pro)=="undefined") {
	        MemberAjax({
	            Prm: 'act=logout',
	            Function: function () {
	                location.reload();
	            }
	        });
	    }
	});
});

function MemberAjax(Action){
	$.ajax({
			type:"GET",
				buf:Action.Buffer,
				url:"Ajax_Code/account.aspx?"+Action.Prm,
				dataType:"json",
				success: Action.Function
		});
}


function CheckEmailBox(taget,table){
	var IsReady = check_email(taget);
	
	if(IsReady)
		table.find('#reg_mail_msg').hide();
	else
		table.find('#reg_mail_msg').show();
	
	return IsReady;
	
}
function CheckPhoneBox(taget,table){
    var IsReady = (taget.length == 11);//CheckMobile(taget);
	
	if(IsReady)
		table.find('#reg_mobile_msg').hide();
	else
		table.find('#reg_mobile_msg').show();
	
	return IsReady;
}
function blurPassword(temp)
{
	//�Ƿ�����������
	if(isNull(temp.value))
	{
		document.getElementById('pwd').style.display="none";
		document.getElementById('pwd_text').style.display="inline-block";	
	}else{
		document.getElementById('pwd').style.color="#666";
		document.getElementById('pwd').style.opacity="0.75";//��͸����
	}
}
function focusPassword()
{
	document.getElementById('pwd').style.color="#000";
	document.getElementById('pwd').style.opacity="1";
}

function focusPasswordText()
{
	document.getElementById('pwd_text').style.display="none";
	document.getElementById('pwd').style.display="inline-block";
	document.getElementById('pwd').focus();	
}

//�ж��ַ����Ƿ�Ϊ��
function isNull(obj)
{
	if(obj==null || obj.length==0)
		return true;	
	else
		return false;
}

$("#logindialog").dialog({width:383,autoOpen:false,modal:true,minHeight:248,bgiframe: true,open:function()
{
    var dig=document.getElementById('commanResvStat');if(dig!=null) dig.style.display='none';
},beforeclose:function()
{
     HrefPage="";
     var dig=document.getElementById('commanResvStat');if(dig!=null) dig.style.display='block';
 }});
$("#logindialog .close").click(function(){
	$("#logindialog").dialog('close');
	return false;
});

$("#logindialog .reg").click(function(){
	$("#logindialog").dialog('close');
	$("#regdialog").dialog('open');
	return false;
	});

$("#regdialog").dialog({width:435,autoOpen:false,modal:true,minHeight:302,bgiframe: true});
$("#regdialog .close").click(function(){
	$("#regdialog").dialog('close');
	return false;
});

$("#nav .login a").click(function(){
	$("#logindialog").dialog('open');
	return false;
});

$("#nav .active a").click(function(){
	$("#regdialog").dialog('open');
	
	return false;
});

$("a.btn_activate").click(function(){
	$("#regdialog").dialog('open');
	return false;
});

$("a.reg").click(function(){
    $("#rid")[0].value=$("#id")[0].value;
    $("#rpwd")[0].value=$("#pwd")[0].value;
    
	$("#regdialog").dialog('open');
	return false;
});

$("a.btn_login").click(function(){
	$("#logindialog").dialog('open');
	return false;
});

</script>