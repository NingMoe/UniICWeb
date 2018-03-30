<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resvRec.aspx.cs" Inherits="ClientWeb_xcus_zd_nxy_resvRec" %>

                    <div class="resv_stat_list tbl_list" style="display:none;">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th>预约人</th>
                                    <th>实验次数</th>
                                    <th tp="strong_number">使用总时长</th>
                                    <th tp="strong_number">总应收费</th>
                                    <th tp="strong_number">总实收费</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=statList %>
                            </tbody>
                        </table>
                    </div>
                    <div class="resv_list tbl_list resv_rec_list">
                        <table class="tblResvs" cellpadding="0" cellspacing="0" width="100%">
                            <thead>
                                <tr class="title tbl_head">
                                    <th style="width: 120px;">仪器</th>
                                    <th style="width: 80px;">预约人</th>
                                    <th style="">实验信息</th>
                                    <th style="">导师</th>
                                    <th style="width: 180px;" class="sort_desc">预约时间</th>
                                    <th style="width:60px;" tp="strong_number">实际使用</th>
                                    <th style="width: 60px;" tp="strong_number">应收费</th>
                                    <th style="width: 60px;" tp="strong_number">实收费</th>
                                </tr>
                            </thead>
                            <tbody>
                                <%=recordList %>
                            </tbody>
                        </table>
                    </div>
