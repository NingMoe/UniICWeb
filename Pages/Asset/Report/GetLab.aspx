<%@ Page Language="C#" MasterPageFile="~/Templates/Sub.master" AutoEventWireup="true" CodeFile="GetLab.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Modules/PageCtrl.ascx" TagPrefix="uc1" TagName="PageCtrl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="Server">
    <form id="formAdvOpts" runat="server">
       <div id="grid-example">

       </div>
        <script>
            Ext.Loader.setConfig({ enabled: true });

            Ext.require([
                'Ext.grid.*',
                'Ext.data.*',
                'Ext.util.*',
                'Ext.toolbar.Paging',
                'Ext.ModelManager'
            ]);



            Ext.onReady(function () {

                Ext.define('ForumThread', {
                    extend: 'Ext.data.Model',
                    fields: [
                        'title', 'forumtitle', 'forumid', 'username',
                        { name: 'replycount', type: 'int' },
                        { name: 'lastpost', mapping: 'lastpost', type: 'date', dateFormat: 'timestamp' },
                        'lastposter', 'excerpt', 'threadid'
                    ],
                    idProperty: 'threadid'
                });

                // create the Data Store
                var store = Ext.create('Ext.data.Store', {
                    pageSize: 50,
                    model: 'ForumThread',
                    remoteSort: true,
                    proxy: {
                        // load using script tags for cross domain, if the data in on the same domain as
                        // this page, an HttpProxy would be better
                        type: 'jsonp',
                        url: 'data/getlab.aspx',
                        reader: {
                            root: 'topics',
                            totalProperty: 'totalCount'
                        },
                        // sends single sort as multi parameter
                        simpleSortMode: true
                    },
                    sorters: [{
                        property: 'lastpost',
                        direction: 'DESC'
                    }]
                });

                // pluggable renders
                function renderTopic(value, p, record) {
                    return Ext.String.format(
                        '{0}',
                        value,
                        record.data.forumtitle,
                        record.getId(),
                        record.data.forumid
                    );
                }

                function renderLast(value, p, r) {
                    return Ext.String.format('{0}<br/>by {1}', Ext.Date.dateFormat(value, 'M j, Y, g:i a'), r.get('lastposter'));
                }
                Ext.grid.RowEditor.prototype.saveBtnText = "保存";
                Ext.grid.RowEditor.prototype.cancelBtnText = '取消';
                var rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
                    clicksToMoveEditor: 1,
                    autoCancel: false
                });
               
                var pluginExpanded = true;
                var grid = Ext.create('Ext.grid.Panel', {
                    width: 700,
                    height: 500,
                    title: 'ExtJS.com - Browse Forums',
                    store: store,
                    disableSelection: true,
                    loadMask: true,
                    viewConfig: {
                        id: 'gv',
                        trackOver: false,
                        stripeRows: false
                    },
                    // grid columns
                    columns: [{
                        // id assigned so we can apply custom css (e.g. .x-grid-cell-topic b { color:#333 })
                        // TODO: This poses an issue in subclasses of Grid now because Headers are now Components
                        // therefore the id will be registered in the ComponentManager and conflict. Need a way to
                        // add additional CSS classes to the rendered cells.
                        id: 'topic',
                        text: "Topic",
                        dataIndex: 'title',
                        flex: 1,
                        renderer: renderTopic,
                        sortable: false,
                        editor: {
                            xtype: 'textfield'
                        }
                    }, {
                        text: "Author",
                        dataIndex: 'username',
                        width: 100,
                        hidden: true,
                        sortable: true
                    }, {
                        text: "Replies",
                        dataIndex: 'replycount',
                        width: 70,
                        align: 'right',
                        sortable: true,
                        editor: {
                            xtype: 'textfield'
                        }
                    }, {
                        id: 'last',
                        text: "Last Post",
                        dataIndex: 'lastpost',
                        width: 150,
                        renderer: renderLast,
                        sortable: true,
                        editor: {
                            xtype: 'textfield'
                        }
                    }],
                    plugins: [rowEditing],
                    // paging bar on the bottom
                    bbar: Ext.create('Ext.PagingToolbar', {
                        store: store,
                        displayInfo: true,
                        displayMsg: 'Displaying topics {0} - {1} of {2}',
                        emptyMsg: "No topics to display",
                        items: [
                            '-', {
                                text: 'Show Preview',
                                pressed: pluginExpanded,
                                enableToggle: true,
                                toggleHandler: function (btn, pressed) {
                                    //var preview = Ext.getCmp('gv').getPlugin('preview');
                                    //preview.toggleExpanded(pressed);
                                }
                            }]
                    }),
                    renderTo: 'grid-example'
                });
                Ext.grid.on('edit', onEdit, this);
                function onEdit(e) {
                    //执行ajax请求将数据提交至服务器
                    // e.record.commit();
                    alert('t');
                };
                // trigger the data store load
                store.loadPage(1);
            });
    </script>
        <style>
            .ListTbl th
            {
                text-align:center;
            }
        </style>
    </form>
</asp:Content>