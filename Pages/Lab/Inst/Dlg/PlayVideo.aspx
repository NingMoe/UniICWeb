<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="PlayVideo.aspx.cs" Inherits="_Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<form id="Form1" runat="server">
    <div class="formtitle"><%=m_Title %></div>
    <div class="formtable">
         <div style="margin-left: 10px">
             <input type="hidden" id="ip" runat="server" />
             <input type="hidden" id="ctrlSN" runat="server" />
             <input type="hidden" id="dwDate" runat="server" />
         <div style="text-align:center;margin-bottom:10px;"> <%=m_szButton %></div>   
             <div style="width: 99%">

                            <div style="text-align: center;">

                                <div id="videoDiv" style="text-align: center; vertical-align: middle">
                                    
                       <object name="ocx" style="width: 450px; height: 350px;" id="NetOCX"  classid="clsid:E7EF736D-B4E6-4A5A-BA94-732D71107808" codeBase="" standby="Waiting..."/>

                                </div>
                            </div>
                        </div>
             
                        </div>
    </div>
    <div style="margin:5px auto; text-align:center;">
                        <button type="button" id="Cancel">关闭</button>
                 </div>
</form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
     
    </style>
<script language="javascript" type="text/javascript" >
    $(function () {
        var iIpChanBase = 33; //私有取流通道初始值
        $("#Cancel").button().click(Dlg_Cancel);
        var snTest = 0;
        $(".btn").button().click(function () {
            var ip = $(this).attr("ip");
          
            var myDate = $("#<%=dwDate.ClientID%>").val();
            myDate = myDate.replace(/-/g, '/');
            var myDate = new Date(myDate);
            var vTime = myDate.getFullYear().toString() + '-' + (myDate.getMonth() + 1).toString() + '-' + myDate.getDate().toString() + ' ' + myDate.getHours().toString() + ':' + myDate.getMinutes().toString() + ':' + myDate.getSeconds().toString();

            var id = $(this).attr("id");
            onloadev(ip, id, 2, vTime, vTime);
            
        });
       
        {
            var myDate = $("#<%=dwDate.ClientID%>").val();
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
             setTimeout(function () {
                 nType = nlType;
                 var OCXobj = document.getElementById("NetOCX");
                 /*
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
                 */
                 debugger;
                 nType = nlType;

                 var nChanNo = parseInt(szChanNo);
                 var n = (iIpChanBase + nChanNo - 1);

                 var s = "";
                 var e = "";
                 if (nType == "1") {
                     url = "http://" + szServerip + ":80/SDK/play/" + (n * 100) + "/004";
                     s = "";
                     e = "";

                 } else {
                     url = "http://" + szServerip + ":80/SDK/playback/" + n;
                     szBeginTime = szBeginTime.replace(/-/g, "/");
                     szEndTime = szEndTime.replace(/-/g, "/");
                     var vBegin = new Date(szBeginTime);
                     var vEnd = new Date(szEndTime);
                     s = getDateFormat(vBegin);
                     e = getDateFormat(vEnd);
                 }
                 OCXobj.HWP_Stop(0);
                 var vinfo = OCXobj.HWP_Play(url, "YWRtaW46MTIzNDU", 0, s, e);

             }, 1000);

         }
         function getDateFormat(vThisDate) {
             var vYear = vThisDate.getFullYear();
             var vMonth = vThisDate.getMonth() + 1;
             if (vMonth < 10) {
                 vMonth = "0" + vMonth;
             }
             var vDate = vThisDate.getDate();
             if (vDate < 10) {
                 vDate = "0" + vDate;
             }
             var vHour = vThisDate.getHours();
             if (vHour < 10) {
                 vHour = "0" + vHour;
             }
             var vMint = vThisDate.getMinutes();
             if (vMint < 10) {
                 vMint = "0" + vMint;
             }
             var vSec = vThisDate.getSeconds();
             if (vMonth < 10) {
                 vSec = "0" + vSec;
             }
             return vYear + vMonth + vDate + "T" + vHour + vMint + vSec + "Z";

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
