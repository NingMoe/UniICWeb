<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ICIntro.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>�ռ�չʾ����</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
         <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
               <a  href="ICINTRONotice.ASPX" id="ICINTRONotice">֪ͨ</a>                
                <a  href="ICINTROClass.ASPX" id="ovclass">�ռ���������</a>                
                <a href="ICINTRO.ASPX" id="ov">�ռ����</a>           
                           
                </div>
            </div>
        </div> 
         
        <div  style="position:absolute; left:150px;">
           <div>
         <table>
             <tr>
                 <td><input type="button" value="�޸Ŀռ����" id="ov0" style="font-size:15px; " /></td>
                 <td><input type="button" value="�޸Ŀռ�����" id="ov1" style="font-size:15px; "  /></td>
                 <td><input type="button" value="�޸ķ�������" id="ov2" style="font-size:15px; "  /></td>
                 <td><input type="button" value="�޸�ʹ�ð���" id="ov3"  style="font-size:15px; " /></td>
                 <td><input type="button" value="�޸���ϵ����" id="ov4" style="font-size:15px; "  /></td>
                 </tr>  
                                         
         </table>
               </div> 
        </div>
        <style>
            table tr td input {
            width:120px;
            height:40px;
            margin:40px;
           
            }
             table tr td{          
            height:80px;
            }
        </style>
        <script type="text/javascript">
            $("#ov0,#ov1,#ov2,#ov3,#ov4").button();
            $(function () {              
                $(".UniTab").UniTab();
               
            });
            $("#ov0").click(function () {
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=0&type=ov")
            });
            $("#ov1").click(function () {
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=1&type=ov")
            });
            $("#ov2").click(function () {
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=2&type=ov")
            });
            $("#ov3").click(function () {
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=3&type=ov")
            });
            $("#ov4").click(function () {
                window.open("../../../ClientWeb/editContent.aspx?h=500&w=720&id=4&type=ov")
            });
        </script>
    </form>
</asp:Content>
