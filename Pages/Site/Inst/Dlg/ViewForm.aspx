<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="ViewForm.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <form id="Form1" runat="server">
        <div class="formtitle">������Ϣ</div>      
        <div>
            <hr style="color:#999" width="99%" size="1" />
             </div>
            <div style="margin-top:10px;">
                
                <div>
                    <div style="float:left;width:35%">
                        <div style="margin-top:10px;margin-left:80px;">
                        <img id="accpic" alt="" height="180px" src="../../../../themes/img/accHead.jpg" />
                       </div>
                    </div>
                    <div style="float:left;width:65%;">                        
                        <div style="font-size:16px;font-weight:bold">������Ϣ</div>
                         <hr style="color:#999" width="99%" size="1"/>
                        <div style="margin-top:10px;">
                            <table>
                                <tr align="center">
                                    <th class="th">ѧ����:</th>
                                    <td><div id="szLogonName"></div></td>                                      
                                </tr>
                               <tr>
                                   <th class="th">����:</th>
                                    <td><div id="szTrueName"></div></td>
                               </tr>
                                 <tr>
                                   <th class="th"><%=ConfigConst.GCDeptName %>:</th>
                                    <td><div id="szDeptName"></div></td>
                               </tr>
                                 <tr>
                                   <th class="th">�༶:</th>
                                    <td><div id="szClassName"></div></td>
                               </tr>
                                 <tr>
                                   <th class="th">���:</th>
                                    <td><div id="szQQ"></div></td>
                               </tr>                               
                            </table>
                        </div>
                        <div>

                        </div>
                    </div>
                </div>
                 
            </div> 
              
      <div style="width:99%;float:left;margin-top:0px">
          <div>
          <hr style="color:#999" width="99%" size="1"/>
     </div>
                    <div style="margin-top:10px;">
                        <div style="font-size:16px;font-weight:bold; margin-left:50px">��ϵ��ʽ</div>
                        </div>
                     <hr style="color:#999" width="99%" size="1"/>
                    <div>
                        <div style="width:550px;margin:0 auto">
                        <table>
                              <tr align="center">
                                    <th class="th">�ֻ�:</th>
                                    <td><div id="szHandPhone"></div></td>                                                                     
                                   <th class="th">����:</th>
                                    <td><div id="szTel"></div></td>
                               </tr>
                             <tr align="center">
                                    <th class="th">����:</th>
                                    <td><div id="szEmail"></div></td>                                                                     
                                   <th></th>
                                    <td></td>
                               </tr>
                        </table>
                            </div>
                    </div>
                </div>
      <div style="width:99%;float:left;margin-top:0px">
          <div>
          <hr style="color:#999" width="99%" size="1"/>
     </div>
          <div style="width:99%">
          <div style="margin:0px auto;text-align:center;margin-top:15px;">
                <button type="button" id="Cancel">�ر�</button>
          </div>
              </div>
          </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
<script language="javascript" type="text/javascript" >
    $(function () {  
        $("#Cancel").button().click(Dlg_Cancel);
    });
</script>
    <style>
        .th
        {
            font-size:14px;
            font-weight:bold;
            text-align:right;
            height:30px;
        }
        td
        {
            text-align:left;
            width:200px
        }
            td div
            {
                margin-left:15px;
            }
    </style>
</asp:Content>
