var GetDelBookReplay = function (node) {
    var index = -1;
    var pageSize = 22;

    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });

    var dsBookType = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/GetBookType.aspx' }),
        reader: new Ext.data.JsonReader({}, [{ name: 'TypeName', type: 'string' }, { name: 'TypeId', type: 'int'}])
    });

    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        sm,
        { header: '书本编号', dataIndex: 'BookId', width: 80, align: 'center' },
        { header: '书本名称', dataIndex: 'BookName', width: 150, align: 'center' },
        { header: '作者', dataIndex: 'UserName', align: 'center' },
        { header: '书本封面', dataIndex: 'Images', width: 80, align: 'center' },
        { header: '添加时间', dataIndex: 'AddTime',align:'center'},
        { header: '书本状态', dataIndex: 'StateName', width: 80, align: 'center' },
        { header: '书本类型', dataIndex: 'TypeName', width: 80, align: 'center' },
        { header: '是否推荐', dataIndex: 'Recommand', width: 80, align: 'center' }
    ]);

    var record = new Ext.data.Record.create([
        { name: 'BookId', type: 'int' },
        { name: 'BookName', type: 'string' },
        { name: 'UserName', type: 'string' },
        { name: 'Images', type: 'string' },
        { name: 'AddTime', type: 'string'},
        { name: 'StateName', type: 'string' },
        { name: 'TypeName', type: 'string' },
        { name: 'Recommand', type: 'string' }
    ]);

    var dsNews = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/GetCommendBooks.aspx' }),
        reader: new Ext.data.JsonReader({
            totalProperty: 'recordCount',
            root: 'result'
        }, record),
        remoteStore: true
    });

    var grid = new Ext.grid.GridPanel({
        title: '查看书评',
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
                } else {
                    index = -1;
                }
            }
        },
        tbar: new Ext.Toolbar({
            items: [
            {
                xtype: 'tbtext',
                text: '小说类型：'
            }, { xtype: 'combo',
                fieldLabel: '小说类型',
                name: 'typeName',
                hiddenName: 'typeId',
                store: dsBookType,
                displayField: 'TypeName',
                valueField: 'TypeId',
                allowBlank: false,
                emptyText: '===请选择===',
                selectOnFocus: true,
                forceSelection: true,
                editable: false,
                triggerAction: 'all',
                mode: 'remote',
                id: 'getbookReplay',
                listeners: {
                    select: function (combo, record, rowIndex) {
                        var TypeId = combo.getValue();
                        if (TypeId && TypeId != '') {
                            dsNews.baseParams.BookType = TypeId;
                            dsNews.load({
                                callback: function (result, options, success) {
                                    if (!success) {
                                        dsNews.removeAll();
                                    }
                                }
                            });
                        }
                    }
                }
            }, { text: '查看书评',
                iconCls: '',
                handler: function () {
                    var selectedRows = grid.getSelectionModel().getSelections();
                    if (selectedRows.length == 0) {
                        Ext.Msg.alert('系统提示', '请选择要查看的小说');
                        return;
                    }
                    else {
                        var bookId = selectedRows[0].data.BookId;
                        GetBookIdReplay(bookId);
                    }
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