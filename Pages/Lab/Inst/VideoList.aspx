<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="VideoList.aspx.cs" Inherits="Sub_Room" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <h2 style="margin-top: 10px; font-weight: bold"><%=ConfigConst.GCRoomName %>��Ƶ��ز�ѯ</h2>
        <div style="margin-top: 5px;">
            <div id="szRoomList" class="clsRoomList">
                <%=m_szOut %>
            </div>
            <div style="margin:5px auto;text-align:center;width:99%">
              
            <table style="margin:5px auto;text-align: center;margin-top:10px;">
                <tr>
                  
                    <td style="width: 65%">
                        <div style="width: 99%">
                            <div style="text-align: center;">
                                <div id="videoDiv" style="text-align: center; vertical-align: middle">
                                    <object name="ocx" style="width: 450px; height: 350px;" id="NetOCX"  classid="clsid:E7EF736D-B4E6-4A5A-BA94-732D71107808" codeBase="" standby="Waiting..."/>

                                </div>

                            </div>
                        </div>
                    </td>
                    <td>
                          <a style="color: blue; font-size: 16px;" href="dlg/help.rar">����˵���ĵ�</a>
                        <div style="margin-left: 10px">
                            <!--��ʷʱ��:-->
                            <div id="cal" style="width:200px"></div>
                            <input type="hidden" name="dwDate" id="dwDate" runat="server" style="width:150px;" />
                    <!-- <input type="button" id="btnOK" value="�鿴��ʷ" />-->
                        </div>
                    </td>
                </tr>
            </table>
                </div>
        </div>
        <style>
            #szRoomList label {
                margin-left: 3px;
                margin-bottom:3px;
                width:120px;
                height:28px;
            }         
        </style>
        <script type="text/javascript">

            $(function () {
                var iIpChanBase = 33; //˽��ȡ��ͨ����ʼֵ
                var tempIP = 0;
                var tempCtrlsn = 0;
                var snTest = 0;
                var OCXobj = null;
                $("#btnOK").button();
                $("#szRoomList").buttonset();
                $("#<%=dwDate.ClientID%>").datetimepicker({
                    stepHour: 1,
                    stepMinute: 10,
                    minDate: -15,
                    maxDate: 0,
                    onClose: PlayHistory
                });
                $("#cal").datetimepicker({
                    minDate: -10,
                    maxDate: 0,
                    onSelect: PlayHistory
                });
                $("#btnOK").click(function () {                   
                    myDate = $("#<%=dwDate.ClientID%>").val();
                       myDate = myDate.replace(/-/g, '/');
                       var myDate = new Date(myDate);
                       var txtStart = myDate.getFullYear().toString() + '-' + (myDate.getMonth() + 1).toString() + '-' + myDate.getDate().toString() + ' ' + myDate.getHours().toString() + ':' + myDate.getMinutes().toString() + ':' + myDate.getSeconds().toString();
                       var DateNext = new Date(myDate.setDate(myDate.getDate() + 1));
                       var txtEnd = DateNext.getFullYear().toString() + '-' + (DateNext.getMonth() + 1).toString() + '-' + DateNext.getDate().toString() + ' ' + DateNext.getHours().toString() + ':' + DateNext.getMinutes().toString();                     
                       crtlSN = snTest;                     
                       onloadev(tempIP, crtlSN, '2', txtStart, txtEnd)
                });
                function PlayHistory() {
                    $("#<%=dwDate.ClientID%>").val($(this).val());
                    var myDate1 = $("#<%=dwDate.ClientID%>").val().replace(/-/g, '/');
                    var myDate = new Date(myDate1);
                    var txtStart = myDate.getFullYear().toString() + '-' + (myDate.getMonth() + 1).toString() + '-' + myDate.getDate().toString() + ' ' + myDate.getHours().toString() + ':' + myDate.getMinutes().toString() + ':' + myDate.getSeconds().toString();
                    var DateNext = new Date(myDate.setDate(myDate.getDate() + 1));
                    var txtEnd = DateNext.getFullYear().toString() + '-' + (DateNext.getMonth() + 1).toString() + '-' + DateNext.getDate().toString() + ' ' + DateNext.getHours().toString() + ':' + DateNext.getMinutes().toString();
                  
                    onloadev(tempIP, tempCtrlsn, '2', txtStart, txtEnd)
                    
                }
                function onloadev(szServerip, szChanNo, nlType, szBeginTime, szEndTime) {
                  
                    setTimeout(function () {
                        nType = nlType;
                        OCXobj = document.getElementById("NetOCX");
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
                    tempIP = ip;
                    var crtlSN = $(this).data("ctrlsn");
                    tempCtrlsn = crtlSN;
                    onloadev(ip, crtlSN, '1', 0, 0);                   
                });
                setTimeout(function () {
                    $("#<%=dwDate.ClientID%>").css("z-index", 1000);
                   }, 100);

            });
        </script>
    </form>
</asp:Content>
