<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetResv.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle">�½�ʵ�鰲��</div>
        <input name="IsSubmit" id="IsSubmit" value="false" type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>ѧ�ڣ�</th>
                    <td><%=m_TermText%></td>
                    <th>ʱ�����ڣ�</th>
                    <td><span name="dwDate" ></span> ����<span name="dwBeginSec" ></span>�� - ��<span name="dwEndSec" ></span>��
                    </td>
                </tr>
                <tr>
                    <th>ʵ���ҷ��䣺</th>
                    <td> <input type="hidden" name="RoomID" /><input type="hidden" name="szRoomName" value="<%=m_szRoomName %>"/><div name="szRoomName" ><%=m_szRoomName %></div></td>
                    <th>��ʦ��</th>
                    <td> <div class="UISelect" data-id="dwOwner" data-name="szOwnerName" data-depend="dwIdent" class="validate[required]" data-tip="�����ʦ����" data-single="true" data-source="../../Data/searchAccount.aspx">
                            <input name="dwOwner" type="hidden"/>
                            <input name="szOwnerName" type="hidden"/>
                        </div>
                        <input name="dwIdent" value="512" type="hidden"/><!--��ʦ���-->
                        </td>
                </tr>                
                 <tr>
                    <th>ʵ��ƻ���</th>
                    <td> <div class="UISelect" data-id="dwTestPlanID" data-name="szTestPlanName" class="validate[required]" data-tip="����ʵ��ƻ�" data-single="true" data-source="../../Data/searchTestPlan.aspx">
                            <input name="dwTestPlanID" type="hidden"/>
                            <input name="szTestPlanName" type="hidden"/>
                        </div>
                    </td>
                    <th>ʵ����Ŀ��</th>
                    <td> <div class="UISelect" data-id="dwTestItemID" data-name="szTestName" data-mode="select" data-depend="dwTestPlanID" class="validate[required]" data-tip="����ʵ����Ŀ" data-single="true" data-source="../../Data/searchTestItem.aspx">
                            <input name="dwTestItemID" type="hidden"/>
                            <input name="szTestName" type="hidden"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">ȷ��</button>
                        <button type="button" id="Cancel">ȡ��</button></td>
                </tr>
                    </tbody>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
<link href="<%=MyVPath %>themes/UISelect/UISelect.css" rel="stylesheet" type="text/css" />
<script src="<%=MyVPath %>themes/UISelect/UISelect.js" charset="utf-8"  type="text/javascript" ></script>

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
            border-radius:5px;
            height:350px;
        }
        
        .formtable table {
            text-align: center;
            width:100%;
            margin: auto;
        }        
        .tblOdd {
            background: white;
            border:1px solid #f5f5f5;
        }
        .tblEven {
            background: #f5f5f5;
            border:1px solid #f5f5f5;
        }
        td {
            padding: 6px;
            text-align:left;
        }
        th {
            text-align:right;
        }
        .btnRow {
            text-align:center;
        }

        input, select {
            width: 200px;
        }
        .tblBtn {
            text-align:center;
        }

        /***********************/
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {            
            $("#OK").button().click(function(){
                ShowWait();
                $("#OK").button("disable");
                $("#Cancel").button("disable");
                $("#IsSubmit").val("true");
                $("#<%=Form1.ClientID%>").submit();
            });
            
            $("#Cancel").button().click(Dlg_Cancel);

            var tbl = $(".formtable table.ListTbl2");
            tbl.find(">tbody>tr:even").addClass("tblEven");
            tbl.find(">tbody>tr:odd").addClass("tblOdd");
            
            $(".UISelect").UISelect();
        });
    </script>
</asp:Content>
