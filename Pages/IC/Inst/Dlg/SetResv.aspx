<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="SetResv.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle">新建实验安排</div>
        <input name="IsSubmit" value="false" type="hidden"/>
        
        <div class="formtable">
            <table class="ListTbl2">
                <tbody>
                <tr>
                    <th>学期：</th>
                    <td><%=m_TermText%></td>
                    <th>时间日期：</th>
                    <td><span name="dwDate" ></span> ，第<span name="dwBeginSec" ></span>节 - 第<span name="dwEndSec" ></span>节
                    </td>
                </tr>
                <tr>
                    <th>实验室房间：</th>
                    <td> <input type="hidden" name="RoomID" /><input type="hidden" name="szRoomName" value="<%=m_szRoomName %>"/><div name="szRoomName" ><%=m_szRoomName %></div></td>
                    <th>教师：</th>
                    <td> <div class="UISelect" data-id="dwOwner" data-name="szOwnerName" data-depend="dwIdent" class="validate[required]" data-tip="输入教师姓名" data-single="true" data-source="../../Data/searchAccount.aspx">
                            <input name="dwOwner" type="hidden"/>
                            <input name="szOwnerName" type="hidden"/>
                        </div>
                        <input name="dwIdent" value="512" type="hidden"/><!--教师身份-->
                        </td>
                </tr>                
                 <tr>
                    <th>实验计划：</th>
                    <td> <div class="UISelect" data-id="dwTestPlanID" data-name="szTestPlanName" class="validate[required]" data-tip="输入实验计划" data-single="true" data-source="../../Data/searchTestPlan.aspx">
                            <input name="dwTestPlanID" type="hidden"/>
                            <input name="szTestPlanName" type="hidden"/>
                        </div>
                    </td>
                    <th>实验项目：</th>
                    <td> <div class="UISelect" data-id="dwTestItemID" data-name="szTestName" data-mode="select" data-depend="dwTestPlanID" class="validate[required]" data-tip="输入实验项目" data-single="true" data-source="../../Data/searchTestItem.aspx">
                            <input name="dwTestItemID" type="hidden"/>
                            <input name="szTestName" type="hidden"/>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="btnRow">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
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
            });
            
            $("#Cancel").button().click(Dlg_Cancel);

            var tbl = $(".formtable table.ListTbl2");
            tbl.find(">tbody>tr:even").addClass("tblEven");
            tbl.find(">tbody>tr:odd").addClass("tblOdd");
            
            $(".UISelect").UISelect();
        });
    </script>
</asp:Content>
