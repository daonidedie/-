var CheckAuthor = function (node) {

    //创建一个复选框
    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: false });
    var index = -1;

    //创建是实体类
    var record = Ext.data.Record.create([
        { name: 'UserId', type: 'int' },
        { name: 'UserName', type: 'string' },
        { name: 'UserType', type: 'string' },
        { name: 'UserSex', type: 'string' },
        { name: 'IdentityCardNumber', type: 'string' },
        { name: 'UserAddress', type: 'string' }
    ]);

    //创建表结构
    var cm = new Ext.grid.ColumnModel([
    //创建一个自动编号列
        new Ext.grid.RowNumberer(),
        sm,
        { header: '会员编号', dataIndex: 'UserId', align: 'center', width: 80 },
        { header: '昵称', dataIndex: 'UserName', align: 'center', width: 80 },
        { header: '会员类型', dataIndex: 'UserType', align: 'center', width: 80 },
        { header: '性别', dataIndex: 'UserSex', align: 'center', width: 80 },
        { header: '身份证号码', dataIndex: 'IdentityCardNumber', align: 'center', width: 150 },
        { header: '居住地址', dataIndex: 'UserAddress', align: 'center', width: 80 }
    ]);

    //创建数据源
    var checkAuthorStore = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/getCheckAuthorList.aspx' }),
        reader: new Ext.data.JsonReader({
            totalProperty: 'recordCount',
            root: 'result'
        }, record),
        remoteStore: true
    });

    var pageSize = 22;

    var grid = new Ext.grid.GridPanel({
        title: '作者审核',
        region: 'center',
        sm: sm,
        cm: cm,
        store: checkAuthorStore,
        autoExpandColumn: 7,
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
        bbar: new Ext.PagingToolbar({
            pageSize: pageSize,
            store: checkAuthorStore,
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
        }),
        tbar: new Ext.Toolbar({
            layoutConfig: { animate: true }, //可以有图标

            items: [{ text: '通过申请', handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (!rows.length >= 1) {
                    Ext.Msg.alert("系统提示", "请选择审核记录");
                }
                else {
                    var ids = "";
                    for (var i = 0; i < rows.length; i++) {
                        ids += rows[i].data.UserId + ",";
                    }
                    Ext.Ajax.request({
                        url: 'Process/AuthorCheckYes.aspx',
                        waitMsg: '数据处理中，请稍侯......',
                        success: function (response) {
                            var json = Ext.decode(response.responseText);
                            if (json.success) {
                                Ext.Msg.alert("系统提示", "操作成功!", function () { grid.getStore().reload(); });
                            }
                        },
                        failure: function () {
                            Ext.Msg.alert("系统提示", "操作失败!");
                        },
                        params: { ids: ids }
                    });
                }
            }
            }, '-',
            { text: '否定申请', handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (!rows.length >= 1) {
                    Ext.Msg.alert("系统提示", "请选择审核记录");
                }
                else {
                    var ids = "";
                    for (var i = 0; i < rows.length; i++) {
                        ids += rows[i].data.UserId + ",";
                    }
                    Ext.Ajax.request({
                        url: 'Process/AuthorCheckNo.aspx',
                        waitMsg: '数据处理中，请稍候.....',
                        success: function (response) {
                            var json = Ext.decode(response.responseText);
                            if (json.success) {
                                Ext.Msg.alert("系统提示", "操作成功!", function () { grid.getStore().reload(); });
                            }
                        },
                        failure: function () {
                            Ext.Msg.alert("系统提示", "操作失败!");
                        },
                        params:{ids:ids}
                    });
                }
            }
            }]
        })
    });

    gridMain(node, grid);
    checkAuthorStore.load();
}