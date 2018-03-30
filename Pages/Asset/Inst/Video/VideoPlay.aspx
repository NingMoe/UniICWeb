<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="VideoPlay.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
      
<input type="hidden" id="roomno" />
    <input type="hidden" id="ip" runat="server" />
        <input type="hidden" id="sn" runat="server" />
           <div id="videoDiv" style="text-align: center; vertical-align: middle">
        <object style="width: 450px; height: 350px;" id="NetOCX" codebase="VideoOCX.cab" classid="clsid:1FB5A5AA-3750-421C-BEA3-6E52FC5F7843" name="ocx"></object>
               </div>
    </form>
           
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">

        .formtitle {
            padding: 6px;
            height: 30px;
            font-size: 20px;
            border-radius:10px;
            margin-top:-10px;
            margin-bottom:10px;
            text-align:center;
            color: #0088ff;
        }

        .formtable
        {
            height:350px;
            text-align: center;
        }
    </style>
   <script type="text/javascript">

       $(function () {
           var tempIP = $("#<%=ip.ClientID%>").val();
           var tempCtrlsn = $("#<%=sn.ClientID%>").val();
           var OCXobj = null;
           function onloadev(szServerip, szChanNo, nlType, szBeginTime, szEndTime) {
               OnClose();
               setTimeout(function () {
                   nType = nlType;
                   OCXobj = document.getElementById("NetOCX");
                   var userid = OCXobj.Login(szServerip, 8000, "admin", "12345")
                   var nChanNo = parseInt(szChanNo);
                   tempIP = szServerip;
                   tempCtrlsn = szChanNo;
                   if (nType == "1") {
                       //      OCXobj.CheckTime(); 
                       OCXobj.StartRealPlay(nChanNo, 0, 0)
                   } else if (nType == "2") {
                       OCXobj.PlayBackByTime(nChanNo, szBeginTime, szEndTime);
                   }
               }, 1000);


           }
           $(window).unload(function () { OnClose(); });
           function OnClose() {
               try {
                   if (nType == "1") {
                       OCXobj.StopRealPlay();
                   } else if (nType == "2") {
                       OCXobj.StopPlayBack();
                   }
                   OCXobj.Logout();
                   OCXobj.ClearOCX();
               } catch (e) {
               }
           }
           setTimeout(function () {
               onloadev(tempIP, tempCtrlsn, 1, 0, 0);
           }, 100);
       });
        </script>
</asp:Content>
