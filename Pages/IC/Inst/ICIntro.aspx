<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="ICIntro.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2>空间展示内容</h2>
        <input type="hidden" id="AllOp" name="AllOp" />
       
         <div class="toolbar">
            <div class="tb_info">
                <div class="UniTab" id="tabl">
               <a  href="ICINTRONotice.ASPX" id="ICINTRONotice">通知</a>                
                <a  href="ICINTROClass.ASPX" id="ovclass">空间内容描述</a>                
                <a href="ICINTRO.ASPX" id="ov">空间概述</a>           
                           
                </div>
            </div>
        </div> 
         
        <div  style="position:absolute; left:150px;">
           <div>
         <table>
             <tr>
                 <td><input type="button" value="修改空间概述" id="ov0" style="font-size:15px; " /></td>
                 <td><input type="button" value="修改空间详情" id="ov1" style="font-size:15px; "  /></td>
                 <td><input type="button" value="修改服务内容" id="ov2" style="font-size:15px; "  /></td>
                 <td><input type="button" value="修改使用帮助" id="ov3"  style="font-size:15px; " /></td>
                 <td><input type="button" value="修改联系我们" id="ov4" style="font-size:15px; "  /></td>
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
