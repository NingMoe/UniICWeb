<%@ Page Language="C#" AutoEventWireup="true" CodeFile="space_LeaseResv.aspx.cs" Inherits="Page_" %>
<%@ Register TagPrefix="Uni" TagName="sidebar" Src="modules/sidebar.ascx"%>
<%@ Register TagPrefix="Uni" TagName="nav" Src="modules/nav.ascx"%>
<%@ Register TagPrefix="Uni" TagName="dialog" Src="modules/dialog.ascx"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="UTF-8" />
<title></title>
<meta content="" name="keywords"/>
<meta content=""  name="description" />
<link rel="stylesheet" href="style/css/main.css" />
<link rel="stylesheet" href="style/css/calendar.css" />

<script type="text/javascript" src="js/jquery.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.20.custom.min.js"></script>
<script type="text/javascript" src="js/site.js"></script>
<script type="text/javascript" language="javascript" src="js/datepicker/WdatePicker.js"></script>
<style>
.seat{display:none;}
 .hidden
   { display:none;} 
   .HeaderStyle
{
  text-align:center;
}
</style>
<script type="text/javascript" language="javascript">
function myload() {

if(!IsLogin()){
			    
				$("#logindialog").dialog('open');				
			}  
}			
</script>
</head>
<body onload="myload()">
<Uni:dialog ID="Dialog1" runat="server" />
<form runat="server">
<div class="body">


<Uni:sidebar runat="server" />

    <div class="content clear">
		<Uni:nav runat="server" />	
        <div class="reservation">
            <h1>ԤԼ�ռ�</h1>
          
                <table>
                    <tr>
                        <th><p style="font-size:14px;">ԤԼ���ƣ�</p></th>
                        <td>
						<div id="divUserLimit" runat="server" style="margin-bottom:10px;">						
						</div>
						</td>
                    </tr>
						<tr >
                        <th><p>ѡ�����ڣ�</p></th>
                        <td>
                            <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th><p>ѡ��ʱ�䣺</p></th>
                        <td>
                        <div id="divFreeTime" runat="server">
                            <asp:DropDownList ID="ddlHourStart" runat="server" CssClass="hour" AutoPostBack="True" OnSelectedIndexChanged="ddlHourStart_SelectedIndexChanged1" >
                            </asp:DropDownList>&nbsp;��&nbsp;
                             <asp:DropDownList ID="ddlHourEnd" runat="server" CssClass="hour" >
                            </asp:DropDownList>                             						
                         </div>
                         <div id="divLimit" runat="server">
                          <asp:DropDownList ID="ddlPartTime" runat="server" CssClass="hour" Width="200px" >
                            </asp:DropDownList>
                         </div>
                         <div id="divLongTime" runat="server">
                          <input type="text" class="input_text"  onClick="WdatePicker()"  id="startDate" runat="server"/>
                          ��
                           <input type="text" class="input_text"  onClick="WdatePicker()"  id="endDate" runat="server"/>
                         </div>
						</td>
                    </tr>
					<tr>
					<th><div id="divMemberAdd1" runat="server"><p>������Աѧ�ţ�</p></div></th>
				    <td>
				    <div id="divMemberAdd2" runat="server">
				    <input class="input_txt _LoginName" type="text" runat="server" id="txtPerson"/>
				    <input type="button" class="addMember" value="���" runat="server" id="Button2" onserverclick="Button2_ServerClick" />
				    <input class="input_txt groupid" type="hidden" id='groupIDHidden' value="" runat="server"/>
				    </div>
					</td>
                    </tr>   
                    <tr>
                        <th><p style="font-size:14px;"></p></th>
                        <td>
						<asp:Label ID="szMemo" runat="server"></asp:Label>		
						
						</td>
                    </tr>                
					
<tr>
<th></th>
<td>
<div id="divUpLoadFile" runat="server">
<div>    
<a href="Innovation-mould.doc" target="_blank" title="�����������ģ��">�����������ģ��</a>
</div>
<div>
 <input id="InputFile" style="width: 400px" type="file" runat="server" />
 <asp:Button ID="UploadButton" runat="server" Text="�ϴ����뱨��" OnClick="UploadButton_Click"  />
 </div>
</div>
</td>
</tr>
                </table>
                 <div>
             <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" Font-Size="14px" Width="400px" BorderColor="Black" BorderWidth="1px" CssClass="GridViewStyle" OnRowCommand="GV_RowCommand">
          <HeaderStyle CssClass="GridViewHeaderStyle" HorizontalAlign="Center" BorderColor="Black" BorderWidth="1px" />
          <RowStyle CssClass="GridViewRowStyle" HorizontalAlign="Center" />
          
            <Columns>              
          
             <asp:BoundField HeaderText="��Ա���" DataField="dwMemberID">                 
                    <ItemStyle BorderColor="Black" BorderWidth="1px" CssClass="hidden" />
                    <HeaderStyle BorderColor="Black" BorderWidth="1px"  CssClass="hidden" />                      
                </asp:BoundField>
             <asp:BoundField HeaderText="���" DataField="dwGroupID">                
                    <ItemStyle BorderColor="Black" BorderWidth="1px" CssClass="hidden" />
                      <HeaderStyle BorderColor="Black" BorderWidth="1px"  CssClass="hidden" />
                  </asp:BoundField>
                    <asp:BoundField HeaderText="���" DataField="szMemo">                
                    <ItemStyle BorderColor="Black" BorderWidth="1px" />
                      <HeaderStyle BorderColor="Black" BorderWidth="1px" CssClass="HeaderStyle" />
                  </asp:BoundField>        
                <asp:BoundField HeaderText="����" DataField="dwKind" >                 
                    <ItemStyle BorderColor="Black" BorderWidth="1px" CssClass="hidden" />
                    <HeaderStyle BorderColor="Black" BorderWidth="1px"  CssClass="hidden" />                      
                </asp:BoundField>                              
                <asp:BoundField HeaderText="����" DataField="szName">                
                    <ItemStyle BorderColor="Black" BorderWidth="1px" />
                      <HeaderStyle BorderColor="Black" BorderWidth="1px" CssClass="HeaderStyle" />
                  </asp:BoundField>              
           
                 
                 <asp:TemplateField HeaderText="">
                     <ItemTemplate>
                          <asp:LinkButton ID="linkDel" runat="server" CommandArgument="<%# GV.Rows.Count %>" >ɾ��</asp:LinkButton>
                     </ItemTemplate>
                      <ItemStyle BorderColor="Black" BorderWidth="1px" />
                      <HeaderStyle BorderColor="Black" BorderWidth="1px"  CssClass="HeaderStyle" />
                 </asp:TemplateField>
            </Columns>
              </asp:GridView>
        </div>
        <div>
        <div style="margin-top:10px;">
        <div id="divMemo" runat="server">
                <table>
                 <tr>
					<th><div id="div1" runat="server"><asp:Label ID="lblszMemo" runat="server" Text="����˵����"></asp:Label></div></th>
				    <td>
				    <div id="div2" runat="server">
				   <asp:TextBox ID="txtMemo" runat="server" class="input_txt groupid" TextMode="MultiLine" Height="50" Width="250" Text="��������д�������ݣ����������ͨ��" onfocus="this.value='';"></asp:TextBox>
				    </div>
					</td>
                    </tr>
               </table>    
               </div> 
                <div class="submitarea">
                    <input type="button" class="input_submit" value="ȷ��ԤԼ" runat="server" id="Button1" onserverclick="Button1_ServerClick" style="margin-left:100px;" />
                      <input type="button" class="input_submit" value="����" runat="server" id="btnBack" style="margin-left:50px;background:url(style/img/back.jpg);" onserverclick="btnBack_ServerClick"  />
                     
                </div>
          
        </div>
        <div class="copyright">��Ȩ˵��</div>
    </div>
</div>
</form>
</body>


</html>

