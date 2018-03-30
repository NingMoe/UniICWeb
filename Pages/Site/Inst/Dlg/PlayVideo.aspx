<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="PlayVideo.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
         <div style="margin-left: 10px">
             <input type="hidden" id="ip" runat="server" />
             <input type="hidden" id="ctrlSN" runat="server" />
             <input type="hidden" id="dwDate" runat="server" />
             <div style="width: 99%">
                            <div style="text-align: center;">

                                <div id="videoDiv" style="text-align: center; vertical-align: middle">
                                    <object style="width: 450px; height: 350px;" id="NetOCX" codebase="NetVideoOCX.cab" classid="clsid:1FB5A5AA-3750-421C-BEA3-6E52FC5F7843" name="ocx"></object>

                                </div>

                            </div>
                        </div>
             
                        </div>
    </div>
    <div style="margin:5px auto; text-align:center;">
                        <button type="button" id="Cancel">¹Ø±Õ</button>
                 </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
     
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        $("#Cancel").button().click(Dlg_Cancel);
        var snTest = 0;
        var OCXobj = null;
        {
            myDate = $("#<%=dwDate.ClientID%>").val();
            myDate = myDate.replace(/-/g, '/');
            var myDate = new Date(myDate);
            var txtStart = myDate.getFullYear().toString() + '-' + (myDate.getMonth() + 1).toString() + '-' + myDate.getDate().toString() + ' ' + myDate.getHours().toString() + ':' + myDate.getMinutes().toString() + ':' + myDate.getSeconds().toString();
            var DateNext = new Date(myDate.setDate(myDate.getDate() + 1));
            var txtEnd = DateNext.getFullYear().toString() + '-' + (DateNext.getMonth() + 1).toString() + '-' + DateNext.getDate().toString() + ' ' + DateNext.getHours().toString() + ':' + DateNext.getMinutes().toString();
            var ip = $("#<%=ip.ClientID%>").val();
            var crtlSN = $("#<%=ctrlSN.ClientID%>").val();          
            onloadev(ip, crtlSN, '2', txtStart, txtEnd);
        }
         function onloadev(szServerip, szChanNo, nlType, szBeginTime, szEndTime) {
             OnClose();
             nType = nlType;
             OCXobj = document.getElementById("NetOCX");

             var userid = OCXobj.Login(szServerip, 8000, "admin", "12345")
             var nChanNo = parseInt(szChanNo);
             if (nType == "1") {
                 //      OCXobj.CheckTime();
                 OCXobj.StartRealPlay(nChanNo, 0, 0)
             } else if (nType == "2") {
                 OCXobj.PlayBackByTime(nChanNo, szBeginTime, szEndTime);
             }

         }
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
         $("input[name='szRoom']").click(function () {
             var ip = $(this).data("ip");
             var crtlSN = $(this).data("ctrlSN");

             ip = "10.75.64.250";

             crtlSN = snTest;
             onloadev(ip, crtlSN, '1', 0, 0);
             if (snTest == 1) {
                 snTest = 0;
             } else {
                 snTest = 1;
             }
         });
         setTimeout(function () {
             $("#<%=dwDate.ClientID%>").css("z-index", 1000);
                }, 100);

     });
        </script>
</asp:Content>
