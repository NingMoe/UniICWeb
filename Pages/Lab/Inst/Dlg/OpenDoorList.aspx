<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="OpenDoorList.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">       
        <div class="formtable">
           
            <div style="width:99%">
                 <div style="margin-top:30px; margin-left:120px">
                <%=sz_Doorctrl %>
            </div>
          <div style="margin-top:30px; margin-left:120px">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button>
             </div>
        </div>
            </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .formtitle {
            padding: 6px;
            background: #d0d0d0;
            height: 30px;
            color: #fff;
            font-size: 20px;
        }

        .formtable table {
            text-align: center;
            margin: auto;
        }

        td {
            padding: 6px;
        }

        input, select {
            width: 200px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {       
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
        
        setTimeout(function () {
           
        }, 1);
    });
    </script>
</asp:Content>
