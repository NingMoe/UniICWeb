<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="DoorCardRec.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
        <div class="toolbar" style="height: auto;">
            <table>
                <tr>
                    <td class="Searchtd" style="text-align:right"><label style="font-weight: bolder">勾选查询<%=ConfigConst.GCRoomName %></label> :</td>
                    <td class="Searchtd"><%=m_szRoom %></td>
                </tr>
                <tr>
                    <td class="Searchtd" style="text-align:right"><label style="font-weight: bolder">查询日期:</label></td>
                    <td class="Searchtd"><input type="text" id="dwStartDate" name="dwStartDate" readonly="readonly" class="validate[required]" />到
                        <input type="text" id="dwEndDate" name="dwEndDate" readonly="readonly" class="validate[required]" /></td>
                </tr>
                <tr>
                    <td class="Searchtd" style="text-align:right"><label style="font-weight: bolder">查询人学号:</label></td>
                    <td class="Searchtd"><input type="text" id="Text1" name="dwStartDate"/></td>
                </tr>
                <tr>
                    <td class="Searchtd"></td>
                    <td class="Searchtd"><button type="submit" id="OK">查询</button><button type="button" id="Cancel">关闭</button></td>
                </tr>
            </table>
        </div>
        <div class="content" style="margin-top:20px;">
            <table class="ListTbl">
                <thead>
                    <tr>
                        <th>流水号</th>
                        <th>学号</th>
                        <th>姓名</th>
                        <th>刷卡时间</th>
                        <th><%=ConfigConst.GCRoomName %></th>
                        <th>备注</th>
                    </tr>
                </thead>
                <tbody id="ListTbl">
                    <%=m_szOut %>
                </tbody>
            </table>
        </div>
        <uc1:PageCtrl runat="server" ID="PageCtrl" />
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
        .Searchtd {
            height:15px;
            padding:0px;
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
            setTimeout(function () {
                
            }, 1);
            $("#Cancel").button().click(Dlg_Cancel);
            $("#dwStartDate").datepicker();
            $("#dwEndDate").datepicker();

        });
        var dateNow = new Date();
        var Month = dateNow.getMonth() + 1;
        if (Month < 10) {
            Month = "0" + Month;
        }
        var date = dateNow.getDate();
        if (date < 10) {
            date = "0" + date;
        }
        var dateNowFor = dateNow.getFullYear() + "-" + Month + "-" + date;
        $("#dwStartDate").val(dateNowFor);
        $("#dwEndDate").val(dateNowFor);
    </script>
</asp:Content>
