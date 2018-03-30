<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="YardResvCheckbak.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
         <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwLabID" name="dwLabID" type="hidden" />
            <input id="dwActivityLevel" name="dwActivityLevel" type="hidden" value="10" />
            <input type="hidden" name="dwCheckID" id="dwCheckID" />
            <table class="DlgListTbl">
                <tr>
                    <th>申请人姓名:</th>
                    <td>
                        <div id="szTrueName"></div>
                    </td>
                    <th>申请人所在部门:</th>
                    <td>
                        <div id="szDeptName"></div>
                    </td>
                </tr>
                <tr>
                    <th>手机:</th>
                    <td>
                        <div id="szHandPhone"></div>
                    </td>
                    <th>邮件:</th>
                    <td>
                        <div id="szEmail"></div>
                    </td>
                </tr>
                <tr>
                    <th>申请会议室:</th>
                    <td>
                        <%if (szDevList == "")
                          {%>
                        <div id="szDevName"><%=szResvDevName %></div>
                        <%}
                          else
                          { %>
                        <select id="devID" name="devID">
                            <%=szDevList %>
                        </select>
                        <%} %>
                    </td>
                    <th>资源所在部门:</th>
                    <td>
                        <div id=""><%=szResvDevDept %></div>
                    </td>
                </tr>
                 <tr>
                      <th>会议名称:</th>
                    <td>
                        <div id="szApplyInfo"><%=szApplyName %></div>
                    </td>
            
               <th>服务
                    </th>
                    <td colspa="3">
                        <input type="checkbox" id="ser" name="ser">使用多媒体
                                <input type="checkbox" id="Checkbox1" name="ser">常用软件预装
                                <input type="checkbox" id="Checkbox2" name="ser">需要安保                         
                    </td>
                      
                 
                </tr>
                <tr>
                    <th>使用人数
                    </th>
                    <td>
                        <select name="mb_num" style="width:100px" >
                            <option value="0-50">50人以内</option>
                            <option value="50-100">50人-100人</option>
                            <option value="100-100000">100人以上</option>
                        </select>
                    </td>
                    <th>使用对象
                    </th>
                    <td>
                        <input type="text" style="width:200px" />
                    </td>
                </tr>
                <tr>
                       <th>主讲人:</th>
                    <td>
                        <div id="szOrganiger"><%=szOrganiger %></div>
                    </td>
                     <th>主讲人背景资料:</th>
                    <td>
                        <div id="Div1">主讲人背景资料介绍信息</div>
                    </td>
                </tr>
                <tr>
                    <th>活动类型         
                    </th>
                    <td>
                          <select style="width:150px">
                                    <option>大型考试</option>
                                    <option>就业活动</option>
                                    <option>讲座报告</option>
                                    <option>文体活动</option>
                                    <option>会议</option>
                                    <option>社团活动</option>
                                    <option>招生活动</option>
                                    <option>培训</option>
                                    <option>其他</option>
                                </selec>
                           </td> 
                    <td> <input type="checkbox" id="Checkbox11" name="ser" checked="checked">服从调配</td>
                    <td>   <input type="checkbox" id="Checkbox10" name="ser" />营利活动</td>
                </tr>
              
                    <!--
                <tr>

                    <th>预估参加人数:</th>
                    <td>
                        <div><%=szPeople %></div>
                    </td>
                    <th>活动类型:</th>
                    <td>
                        <div><%=szActivity %></div>
                    </td>
                    
                    <th>是否摄像:</th>
                    <td>
                        <div id="Div2"><%=szNeedCameor %></div>
                    </td>
                   
                </tr> 
                        -->
                <!--
                <tr>

                    <th>主管部门:</th>
                    <td>
                         <%if (szDirectors == "")
                           {%>

                        <label><input type="checkbox" name="dwDirectors" value="1024" class="enum" />团委</label>
                         <label><input type="checkbox" name="dwDirectors" value="2048" class="enum" />学工部</label>

                        <%}
                           else
                           {%>
                        <%=szDirectors %>
                         <%}%>

                      </td>
                    <th>安保级别:</th>
                    <td>
                           <%if (szSecurityLevel == "")
                             {%>
                        <select id="dwSecurityLevel" name="dwSecurityLevel">
                            <option value="1">不需要安保</option>
                            <option value="6">需要安保</option>
                        </select>
                        <%}
                             else
                             {%>
                        <%=szSecurityLevel %>
                         <%}%>
                       

                    </td>
                </tr>
                -->
                <tr>

                    <th>申请时间:</th>
                    <td colspan="3">
                        <div id="dwApplyUseTime"><%=szResvTime %></div>
                    </td>
                  
  </tr>
               
                <tr>
                    <th>会议室内容:</th>
                    <td colspan="3">
                        <div><%=szMemo %></div>
                    </td>
                </tr>
                 <tr>
                    <th>申请说明:</th>
                    <td colspan="3">
                      
                    </td>
                </tr>
             <tr>
                    <th>部门审核意见</th>
                    <td></td>
                    <th>审核意见</th>
                    <td><input type="text" style="width:150px" /></td>
                    </tr>
             
                <tr>
                    <td colspan="4" style="text-align: center">
                        <%//if (szFileName != "")
                          { %>
                        <a style="color: blue" target="_blank" href="<%=szFileName %>">点击下载申请报告</a>
                        <%} %>
                    </td>
                </tr>
             
                <tr>
                    <td colspan="4" style="text-align: center">
                        <button type="submit" id="OK">审核通过</button>
                        <button type="button" id="Cancel">审核不通过</button></td>
                </tr>
            </table>
            <div id="divNoOK">
                <table>
                    <tr>
                        <td>审核意见：
                        <input type="text" name="szCheckInfo" id="szCheckInfo" title="审核不通过必须输入原因" />

                            <input type="button" id="btnNOOK" value="确定不通过" style="width: 90px" />
                            <input type="button" id="btnClose" value="关闭" style="width: 80px" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .DlgListTbl tr {
            height: 40px;
            border: 1px solid #777777;
        }
       .formtable input, select, .input {
width: 10px;
}
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {

        <%if (bSet)
          {%>
            $("#dwSN").attr("readonly", "readonly").addClass("disabled");
        <%}%>
            $("#dwOwnerDept").change(function () {
                $("#szDeptName").val($(this).find("option:selected").text());
            });
            $("#Cancel").click(function () {
                $("#divNoOK").show();
            });
            $("#divNoOK").hide();
            $("#OK").button();
            $("#btnClose").button().click(Dlg_Cancel);
            $("#btnNOOK").button();
            $("#btnNOOK").click(function () {
                var szCheckInfo = $("#szCheckInfo").val();
                if (szCheckInfo == "") {
                    return;
                }
                var id = $("#dwCheckID").val();
                var vApplyAgain = "1";
                $.get(
                         "../../ajaxpage/YearResvCheck.aspx",
                         { szCheckInfo: szCheckInfo, id: id, vApplyAgain: vApplyAgain },
                         function (data) {
                             if (data == "success") {
                                 MessageBox("审核不通过", "提示", 3, function () { Dlg_OK() });
                             }
                             else {
                                 MessageBox("审核失败" + data, "提示", 3, function () { Dlg_OK() });
                             }

                         }
                       );

            });
            $("#Cancel").button();
            $("#szManName2").autocomplete({
                source: "../../data/searchAccount.aspx",
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#szManName").val(ui.item.label);
                            $("#szManName2").val(ui.item.label);
                            $("#dwManagerID").val(ui.item.id);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {
                        $("#dwManagerID").val("");
                        $("#szManName").val("");
                        ui.content.push({ label: " 未找到配置项 " });
                    }
                }
            }).blur(function () {
                if ($("#dwManagerID").val() == "") {
                    $(this).val("");
                } else {
                    $(this).val($("#szManName").val());
                }
            });

            $("#szAttendantName2").autocomplete({
                source: "../../data/searchAccount.aspx",
                select: function (event, ui) {
                    if (ui.item) {
                        if (ui.item.id && ui.item.id != "") {
                            $("#szAttendantName").val(ui.item.label);
                            $("#szAttendantName2").val(ui.item.label);
                            $("#dwAttendantID").val(ui.item.id);
                        }
                    }
                    return false;
                },
                response: function (event, ui) {
                    if (ui.content.length == 0) {
                        $("#dwAttendantID").val("");
                        $("#szAttendantName").val("");
                        ui.content.push({ label: " 未找到配置项 " });
                    }
                }
            }).blur(function () {
                if ($("#dwAttendantID").val() == "") {
                    $(this).val("");
                } else {
                    $(this).val($("#szAttendantName").val());
                }
            });

            setTimeout(function () {
                if ($("#dwManagerID").val() == "") {
                    $("#szManName2").val("");
                } else {
                    $("#szManName2").val($("#szManName").val());
                }

                if ($("#dwAttendantID").val() == "") {
                    $("#szAttendantName2").val("");
                } else {
                    $("#szAttendantName2").val($("#szAttendantName").val());
                }
            }, 1);
        });
    </script>
</asp:Content>
