<%@ Page Language="C#" MasterPageFile="~/Templates/Dlg.master" AutoEventWireup="true" CodeFile="setSpecaliDate.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="Form1" runat="server">
        <div class="formtitle"><%=m_Title %></div>
        <div class="formtable">
            <input id="dwID" name="dwID" type="hidden" />
            <table class="ListTbl">
                <tr>
                    <th></th>
                      <th>开始日期：</th>
                      <th>结束日期：</th>
                      <th>间段一：</th>
                      <th>间段二：</th>
                      <th>间段三：</th>
                </tr>
                <tr>
                    <td>特殊日期一：</td>
                    <td>
                        <input class="date" type="text"  id="dwStartDay1" name="dwStartDay1" /></td>
                    <td>
                        <input class="date"  type="text"  id="dwEndDay1" name="dwEndDay1"  /></td>

                     
                    <td>
                    <select id="starth11" name="starth11"><%=m_H %></select> 
                        <select id="startm11" name="startm11"><%=m_M %></select>  到
                         <select id="endh11" name="endh11"><%=m_H %></select>  
                        <select id="endm11" name="endm11"><%=m_M %></select>  
                    </td>
                     
                    <td>
                     <select id="starth12" name="starth12"><%=m_H %></select> 
                        <select id="startm12" name="startm12"><%=m_M %></select>到  
                         <select id="endh12" name="endh12"><%=m_H %></select>  
                        <select id="endm12" name="endm12"><%=m_M %></select>  
                    </td>
                     
                    <td>
                     <select id="starth13" name="starth13"><%=m_H %></select> 
                        <select id="startm13" name="startm13"><%=m_M %></select>  到
                         <select id="endh13" name="endh13"><%=m_H %></select>  
                        <select id="endm13" name="endm13"><%=m_M %></select>  
                    </td>
                </tr>
              
                 <tr>
                     <td>特殊日期二：</td>
                    <td>
                        <input class="date" type="text" id="dwStartDay2" name="dwStartDay2" /></td>
                  
                    <td>
                        <input class="date" type="text"   id="dwEndDay2" name="dwEndDay2"  /></td>

                    <td>
                    <select id="starth21" name="starth21"><%=m_H %></select> 
                        <select id="startm21" name="startm21"><%=m_M %></select>到  
                         <select id="endh21" name="endh21"><%=m_H %></select>  
                        <select id="endm21" name="endm21"><%=m_M %></select>  
                    </td>
                    <td>
                     <select id="starth22" name="starth22"><%=m_H %></select> 
                        <select id="startm22" name="startm22"><%=m_M %></select>  到
                         <select id="endh22" name="endh22"><%=m_H %></select>  
                        <select id="endm22" name="endm22"><%=m_M %></select>  
                    </td>
                    <td>
                     <select id="starth23" name="starth23"><%=m_H %></select> 
                        <select id="startm23" name="startm23"><%=m_M %></select>  到
                         <select id="endh23" name="endh23"><%=m_H %></select>  
                        <select id="endm23" name="endm23"><%=m_M %></select>  
                    </td>
                </tr>
               
                 <tr>
                     <td>特殊日期三：</td>
                    <td>
                        <input class="date"  type="text" id="dwStartDay3" name="dwStartDay3" /></td>
                  
                    <td>
                        <input class="date"  type="text"  id="dwEndDay3" name="dwEndDay3"  /></td>

                    <td>
                    <select id="starth31" name="starth31"><%=m_H %></select> 
                        <select id="startm31" name="startm31"><%=m_M %></select>  到
                         <select id="endh31" name="endh31"><%=m_H %></select>  
                        <select id="endm31" name="endm31"><%=m_M %></select>  
                    </td>
                    <td>
                     <select id="starth32" name="starth32"><%=m_H %></select> 
                        <select id="startm32" name="startm32"><%=m_M %></select>  到
                         <select id="endh32" name="endh32"><%=m_H %></select>  
                        <select id="endm32" name="endm32"><%=m_M %></select>  
                    </td>
                    <td>
                     <select id="starth33" name="starth33"><%=m_H %></select> 
                        <select id="startm33" name="startm33"><%=m_M %></select>  到
                         <select id="endh33" name="endh33"><%=m_H %></select>  
                        <select id="endm33" name="endm33"><%=m_M %></select>  
                    </td>
                </tr>
              
                <tr>
                    <td class="btnRow" colspan="10">
                        <button type="submit" id="OK">确定</button>
                        <button type="button" id="Cancel">取消</button></td>
                </tr>
            </table>
        </div>
    </form>
</asp:Content>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        
    </style>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".date").datepicker();
            $(".date").css("width", "80");
            $("select").css("width", "45");
            $("th").css("text-align", "center");
        $("#OK").button();
        $("#Cancel").button().click(Dlg_Cancel);
          setTimeout(function () {
           
        }, 1);
    });
    </script>
</asp:Content>
