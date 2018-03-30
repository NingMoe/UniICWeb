<%@ Page Language="C#" MasterPageFile="Templates/ClientMaster.master" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="DevWeb_test" %>

<asp:Content ID="MyHead" runat="server" ContentPlaceHolderID="HeadContent">
<script type="text/javascript">
    $(function () {
        $("#dialog").dialog();
    });
</script>
</asp:Content>
<asp:Content ID="MyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="dialog" title="Basic dialog">
  <p>This is the default dialog which is useful for displaying information. The dialog window can be moved, resized and closed with the 'x' icon.</p>
</div>
</asp:Content>
