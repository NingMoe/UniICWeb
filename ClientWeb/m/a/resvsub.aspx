<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resvsub.aspx.cs" Inherits="ClientWeb_m_all_resvsub" %>

<html>
<body>
    <div class="navbar">
        <div class="navbar-inner">
            <div class="left"><a href="#" class="link back icon-only"><i class="icon icon-back"></i><span>返回</span></a></div>
            <div class="center sliding">提交预约</div>
        </div>
    </div>
    <div class="page" data-page="p-resvsub">

        <div class="page-content">
            <form id="resvsub_form">
                <div>
                    <input type="hidden" class="dev_id" name="dev_id" />
                    <input type="hidden" class="lab_id" name="lab_id" />
                    <input type="hidden" class="room_id" name="room_id" />
                    <input type="hidden" class="kind_id" name="kind_id" />
                    <input type="hidden" class="type" name="type" value="dev" />
                    <input type="hidden" class="prop" name="prop" />
                    <input type="hidden" class="test_id" name="test_id" />
                    <input type="hidden" class="resv_id" name="resv_id"/>
                    <input type="hidden" class="term" name="term" />
                    <input type="hidden" class="min_user" name="min_user" />
                    <input type="hidden" class="max_user" name="max_user" />
                    <input type='hidden' class="mb_list" name='mb_list' />
                </div>
                <div>
                    <div class="card metro resv_rule">
                        <div class="card-content">
                            <div class="card-content-inner resv_info_content theme-bg white">
                                <div class="text-ellipsis"><i class="icon icon-form" style="color:white"></i>&nbsp;<span class="apply_user"></span></div>
                                <div class="text-center apply_info font-bigger text-ellipsis"></div>
                            </div>
                        </div>
                        <ul style="margin:0;padding-left:0">
                            <li style=" background: #fcf8e3; font-size: 12px;color: #8e8e93;width:100%;float:left;list-style-type:none;" class="list-group-title">&nbsp&nbsp
                                灰色-时间已过，白色-可预约，蓝色-已被人预约</li>
                                 </ul>
                        <div class="card-content">
                            <div class="card-content-inner obj_resv_stat"></div>
                        </div>
                    </div>
                </div>
                <div class="list-block narrow_top">
                    <%if (ToUInt(GetConfig("resvTheme")) >= 0)
                        {%>

                     <%if (GetConfig("fixTheme") != "1")
                         { %>
                    <div class="list-group">
                        <ul>
                        <li class="tr_theme <%=ToUInt(GetConfig("resvTheme")) > 0 ? "" : "hidden" %>">
                            <div class="item-content">
                                <div class="item-inner">
                                    <div class="item-title label name_theme">主题</div>
                                    <div class="item-input">
                                <input type="text" placeholder="..." name="test_name" id="MainTitle" class="con_theme <%=ToUInt(GetConfig("resvTheme")) == 2 ? "must" : "" %>" data-msg="<%=Translate("必填内容不允许为空") %>" maxlength="32" />
                              
                                         </div>
                                </div>
                            </div>
                             </li>
                        </ul>
                         </div>
                          <%}
    else
    {%>  
                     <ul class="">
    <!-- Smart select item -->
    <li>
      <!-- Additional "smart-select" class -->
      <a href="#" class="item-link smart-select">
        <!-- select -->
          <%if ((ToUInt(GetConfig("resvKind")) & 1) > 0)
              {%>
        <select name="resv_kind" class="form-control con_theme <%=ToUInt(GetConfig("resvTheme")) == 2 ? "must" : "" %>">
                                    <%=themeKind %>
                                </select>
          <%}
    else
    { %>
          
        <select name="test_name" class="form-control con_theme <%=ToUInt(GetConfig("resvTheme")) == 2 ? "must" : "" %>">
                                    <option value="">未选择</option>
                                    <%=themeOptions %>
                                </select>
          <%} %>
        <div class="item-content">
          <div class="item-inner">
            <!-- Select label -->
            <div class="item-title">主题</div>
            <!-- Selected value, not required -->
            
          </div>
        </div>
      </a>
    </li>
  </ul>
                   <%} %>
                     <%} %>
                    <div class="list-group">
                        <ul class="ul_form">
                            <li class="list-group-title resv_info_time"></li>
                                                    <li class="item-content uni-date-picker" id="resv_sub_picker">
                            <div class="item-inner">
                                <div class="item-title label">预约日期</div>
                                <div class="item-title label arrow">
                                    <a href="#" class="link icon-only minus"><i class="icon icon-back"></i></a>
                                </div>
                                <div class="item-input">
                                    <input type="text" class="stat_date" />
                                </div>
                                <div class="item-title label arrow">
                                    <a href="#" class="link icon-only add"><i class="icon icon-forward"></i></a>
                                </div>
                            </div>
                        </li>
                        </ul>
                    </div>
                   <div class="list-group md_group hidden">
                    <ul>
                        <li class="list-group-title"><span class="uni_trans">人数限制</span>：<span class='resv_info_group'></span> <span class="uni_trans">人</span></li>
                        <li>
                            <a href="#" data-url="searchAccount.aspx&placeholder=<%=Translate(Server.UrlEncode(szSearchKey)) %>" class="item-link group_link">
                                <div class="item-content">
                                    <div class="item-inner">
                                        <div class="item-title label">成员</div>
                                        <div class="item-after">
                                            创建小组
                                        </div>
                                    </div>
                                </div>
                            </a>
                        </li>
                    </ul>
                    </div>
                   <div class="list-group">
                    <ul>
                        <li class="resv_fee_panel <%=GetConfig("resvBilling")!="1"?"hidden":"" %>">
                            <div class="item-content">
                                <div class="item-inner">
                                    <div class="item-title label">费用</div>
                                    <div class="item-input">
                                        <span class="uni_trans">单价</span>：<span class="unit_price"></span>；
                                <span class="uni_trans">总计</span>：<span class="total_price"></span>
                                    </div>
                                </div>
                            </div>
                        </li>
                        <li class="<%=(ToUInt(GetConfig("resvMemo"))&3)>0?"":"hidden" %>">
                            <div class="item-content">
                                <div class="item-inner box-align-start">
                                    <div class="item-title label">申请说明</div>
                                    <div class="item-input">
                                        <textarea rows="4" name="memo" placeholder="<%=Translate(GetConfig("memoTip")) %>(45)" class="memo  <%=ToUInt(GetConfig("resvMemo"))==2?"must":"" %>" data-msg="<%=Translate("申请说明必须填写") %>" maxlength="45"></textarea>
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                   </div>

                </div>
            </form>
            <div class="content-block">
                <a href="#" class="button button-big button-fill" id="sub_resvsub_form" style="display: none;">提交</a>
                <p class="resv_info_prop text-center grey"></p>
            </div>
        </div>
    </div>
</body>
</html>
