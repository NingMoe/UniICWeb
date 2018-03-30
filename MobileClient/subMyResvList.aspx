<%@ Page Language="C#" AutoEventWireup="true" CodeFile="subMyResvList.aspx.cs" Inherits="_Default" %>
<div class="Div" id="tableDiv">
    <div class="Head" style="">
        <table style="width:99%;font-size:12px;">
            <tr>
                <td>
                    <select id="searchDate" name="searchDate" style="width:99%;font-size:12px;">
                    <option value="<%=m_szMonthIn %>">一个月以内</option>
                    <option value="<%=m_szMonthOut %>">一个月之前</option>
                    </select>

                </td>
                <td>
                      <label><input class="enum" value="512" type="checkbox" name="resvStatus" />生效中</label>
        <label><input class="enum" value="262144" type="checkbox" name="resvStatus" />违约</label>
        <label><input class="enum" value="131072" type="checkbox" name="resvStatus" />已签到</label>
      
                </td>
                <td>
                     <input type="button" id="btnSearch" value="查询" />
                </td>
            </tr>
        </table>
      

    </div>
    <div class="Content" style="font-size:12px">
        <table class="tblContent" border="1"><thead><tr><td class="valname">时间</td><td class="valname">名称</td><td>状态</td><td class="valname"> </td></tr></thead>
            <tbody>
            <%=m_szOut %>
            <!--<tr data-id="1"><td>5月6日10时-11时</td><td class="valname">研修间1</td><td><button class="btnCancel">取消预约</button></td></tr>-->
            </tbody>
        </table>
    </div>
</div>
