<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="offlinecheck.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">

        <input type="hidden" id="szidh" runat="server" />
        <input type="hidden" id="szOwnerID" runat="server" />
        <input type="hidden" id="szOwnerNameH" runat="server" />
        <div style="width: 100%">
            <div style="width: 120px; margin: 0 auto;">
                <label style="font-size: 18px; font-weight: bold;">离线签到</label>
            </div>
        </div>
        <div style="margin:0px auto;width:99%">
            <div style="margin:0px auto;">
              <button type="submit" id="OK">提交离线签到</button>
        </div>
            </div>
             <div class="content" style="margin-top:10px">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>学号</th>
                       <th>姓名</th>
                        <th>刷卡时间</th>
                    </tr>          
                </thead>
                <tbody id="Tbody1">
                    <%=m_szOut %>
                </tbody>
            </table>
        </div>
        
 
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">             
         .resvth{
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
                text-align: right;
                height: 35px;
         }
         .resvtd{
                border-left: 1px solid #000;
                border-top: 1px solid #000;
                border-bottom: 1px solid #000;
                text-align: left;
                height: 35px;
         }
            .resvtd label {
            margin-left:10px;
            }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
     
                $("#OK").button();
                  
            setTimeout(function () {

            }, 1);
        });
    </script>
</asp:Content>
