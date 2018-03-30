<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Sub_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="form1" runat="server">
        <div style="margin:auto auto;text-align:center;margin-top:50px">
          <h1 style="font-size:40px;">欢迎进入<%=ConfigConst.GCSysName %></h1>
      </div>
              <div style="margin-top:30px;width:99%;">
            <div style="text-align:center">
                <!--
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>监控对象</th>
                        <th>描述</th>
                        <th>状态</th>
                        <th>状态</th>                                             
                        <th>正常时间</th>                         
                                        
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
           -->
        </div>
               </div>
        <script type="text/javascript">
        
        </script>
    </form>
</asp:Content>
