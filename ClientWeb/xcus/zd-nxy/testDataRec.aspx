<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testDataRec.aspx.cs" Inherits="ClientWeb_xcus_zd_nxy_testDataRec" %>
                                <table cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class='title tbl_head'>
                                    <th>名称</th>
                                    <th>实验人</th>
                                    <th>日期</th>
                                    <th>仪器</th>
                                    <th tp="strong_number">文件大小</th>
                                    <th>下载状态</th>
                                    <th>下载</th>
                                </tr>
                            </thead>
                            <tbody>
<%=testData%>
                            </tbody>
                        </table>