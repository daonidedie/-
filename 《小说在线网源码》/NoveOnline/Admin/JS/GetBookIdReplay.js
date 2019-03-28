var GetBookIdReplay = function (item) {
    var index = -1;
    var pageSize = 22;

    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: false });

    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        sm,
        { header: '书评编号', dataIndex: 'ReplayId', width: 80, align: 'center' },
        { header: '书评内容', dataIndex: 'ReplayContext', width: 80, align: 'center' },
        { header: '评论时间', dataIndex: 'ReplayTime', align: 'center'},
        { header: '评论会员', dataIndex: 'UserName', width: 80, align: 'center' }
    ]);

    var record = new Ext.data.Record.create([
        { name: 'ReplayId', type: 'int' },
        { name: 'ReplayContext', type: 'string' },
        { name: 'ReplayTime', type:'string'},
        { name: 'UserName', type: 'string' }
    ]);

    var dsReplay = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/GetBookIdReplay.aspx?bookId=' + item }),
        reader: new Ext.data.JsonReader({
            totalProperty: 'recordCount',
            root: 'result'
        }, record),
        remoteStore: true
    });

    var grid = new Ext.grid.GridPanel({
        title: '删除书评',
        sm: sm,
        cm: cm,
        width: 800,
        height: 400,
        store: dsReplay,
        autoExpandColumn: 3,
        columnLines: true,
        stripeRows: true,
        loadMask: { msg: '数据加载中，请稍候...' },
        listeners: {
            rowClick: function (grid, rowIndex, event) {
                if (grid.getSelectionModel().getSelected() != null) {
                    index = rowIndex;
                } else {
                    index = -1;
                }
            }
        },
        tbar: new Ext.Toolbar({
            items: [
            { text: '删除书评',
                iconCls: '',
                handler: function () {
                    var selectedRows = grid.getSelectionModel().getSelections();
                    var ReplayIds = "";
                    var UserNames = "";
                    if (selectedRows.length == 0) {
                        Ext.Msg.alert('系统提示', '请选择要删除的书评');
                        return;
                    }

                    for (var i = 0; i < selectedRows.length; i++) {
                        ReplayIds += selectedRows[i].data.ReplayId + ",";
                        UserNames += selectedRows[i].data.UserName + ",";
                    }

                    Ext.Msg.confirm('系统提示', '确定要删除' + UserNames.substring(0, UserNames.length - 1) + '的书评记录?', function (btn) {
                        if (btn == 'yes') {
                            Ext.Ajax.request({
                                url: 'Process/DelBookReplay.aspx',
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
                                params: { id: ReplayIds }
                            });
                        }
                    });
                }
            }]
        }),
        bbar: new Ext.PagingToolbar({
            pageSize: pageSize,
            store: dsReplay,
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
    dsReplay.load();
    var win = new Ext.Window({
        modal: true,
        title: '添加新闻信息',
        width: 800,
        height: 450,
        autoHeight: false,
        closeAction: 'close',
        resizable: false,
        border: false,
        bodyStyle: 'padding:3px',
        items: [grid]
    });
    win.show();
}