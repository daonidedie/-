var NovelNews = function (node) {

    var index = -1;
    var pageSize = 22;


    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });

    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        sm,
        { header: '新闻编号', dataIndex: 'NewsId', width: 80, align: 'center' },
        { header: '新闻标题', dataIndex: 'NewsTitle', width: 80, align: 'center' },
        { header: '新闻内容', dataIndex: 'NewsContens', align: 'center',width:250 },
        { header: '新闻图片', dataIndex: 'NewsImages', width: 80, align: 'center' },
        { header: '新闻来源', dataIndex: 'FromWhere', width: 80, align: 'center' },
        { header: '发布时间', dataIndex: 'AddTime',align:'center'}
    ]);

    var record = new Ext.data.Record.create([
        { name: 'NewsId', type: 'int' },
        { name: 'NewsTitle', type: 'string' },
        { name: 'NewsContens', type: 'string' },
        { name: 'NewsImages', type: 'string' },
        { name: 'FromWhere', type: 'string' },
        { name: 'AddTime', type: 'string'}
    ]);

    var dsNews = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/GetNewsList.aspx' }),
        reader: new Ext.data.JsonReader({
            totalProperty: 'recordCount',
            root: 'result'
        }, record),
        remoteStore: true
    });

    var grid = new Ext.grid.GridPanel({
        title: '新闻管理',
        sm: sm,
        cm: cm,
        store: dsNews,
        autoExpandColumn: 3,
        columnLines: true,
        stripeRows: true,
        loadMask: { msg: '数据加载中，请稍候...' },
        listeners: {
            rowClick: function (grid, rowIndex, event) {
                if (grid.getSelectionModel().getSelected() != null) {
                    index = rowIndex;
                    Ext.getCmp('del').enable();
                    sm.checked = false;
                } else {
                    index = -1;
                }
            }
        },
        tbar: new Ext.Toolbar({
            items: [
            { text: '添加',
                iconCls: '',
                handler: function () {
                    AddOrModifyNews(null);
                }
            }, '-',  {
                text: '删除',
                id: 'del',
                disabled: true,
                handler: function () {
                    var item = grid.getSelectionModel().getSelected();
                    var id = item.get('NewsId');
                    if (id == '') {
                        Ext.Msg.alert('系统提示', '请选择要删除的记录');
                        return;
                    }

                    Ext.Msg.confirm('系统提示', '确定要删除吗？', function (btn) {
                        if (btn == 'yes') {
                            Ext.Ajax.request({
                                url: 'Process/DelNews.aspx',
                                waitMsg: '数据处理中，请稍候...',
                                success: function (response) {
                                    var json = Ext.decode(response.responseText);
                                    if (json.success) {
                                        Ext.Msg.alert("系统提示", json.msg, function () {
                                            grid.getStore().reload();
                                        });
                                    } else {
                                        Ext.Msg.alert("系统提示", json.msg);
                                    }
                                },
                                failure: function () {
                                    Ext.Msg.alert('系统提示', '删除失败');
                                    grid.getStore().reload();
                                },
                                params: { id: id }
                            });
                        }
                    });
                }
            }]
        }),
        bbar: new Ext.PagingToolbar({
            pageSize: pageSize,
            store: dsNews,
            displayInfo: true,
            displayMsg: '显示第{0}条记录到第{1}条记录，总共{2}条记录',
            emptyMsg: '无记录',
            refreshText: '刷新',
            firstText: '第一页',
            prevText: '上一页',
            nextText: '下一页',
            lastText: '最末页',
            beforePageText: '当前页',
            afterPageText: '共{0}页'
        })
    });

    gridMain(node, grid);
    dsNews.load();
}