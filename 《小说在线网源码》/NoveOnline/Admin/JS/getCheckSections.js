var getCheckSections = function (node) {
    var index = -1;
    var pageSize = 22;


    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });

    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        sm,
        { header: '章节编号', dataIndex: 'SectiuonId', width: 100, align: 'center' },
        { header: '小说名称', dataIndex: 'BookName', width: 100, align:'center' },
        { header: '小说编号', dataIndex: 'BookId', width: 100, align: 'center' },
        { header: '章节标题', dataIndex: 'SectionTitle', width: 180, align: 'center' },
        { header: '字数', dataIndex: 'CharNum', width: 100, align: 'center' },
        { header: '添加日期', dataIndex: 'ShortAddTime', width: 100, align: 'center' },
        { header: '小说状态', dataIndex: 'StateName', width: 100, align: 'center' }
    ]);

    var record = new Ext.data.Record.create([
        { name: 'SectiuonId', type: 'int' },
        { name: 'BookName', type: 'string' },
        { name: 'BookId', type: 'int' },
        { name: 'SectionTitle', type: 'string' },
        { name: 'CharNum', type: 'int' },
        { name: 'ShortAddTime', type: 'string' },
        { name: 'StateName', type: 'string' }
    ]);

    var getCS = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/getCheckSections.aspx' }),
        reader: new Ext.data.JsonReader({
            totalProperty: 'recordCount',
            root: 'result'
        }, record),
        remoteStore: true
    });

    var grid = new Ext.grid.GridPanel({
        title: '新章节审核',
        region: 'center',
        sm: sm,
        cm: cm,
        store: getCS,
        autoExpandColumn: 5,
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
            store: getCS,
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
            items: [{ text: '通过审核', handler: function () {
                var rows = grid.getSelectionModel().getSelections();
                if (!rows.length >= 1) {
                    Ext.Msg.alert("系统提示", "请选择审核记录");
                }
                else {
                    var bookid = rows[0].get("BookId");
                    var sectionId = rows[0].get("SectiuonId");
                    var bookName = rows[0].get("BookName");
                    Ext.Ajax.request({
                        url: 'Process/CheckSectionYes.aspx?bookid=' + bookid + "&sectionId=" + sectionId + "&BookName=" + bookName,
                        waitMsg: '数据处理中，请稍侯......',
                        success: function (response) {
                            var json = Ext.decode(response.responseText);
                            if (json.success) {
                                Ext.Msg.alert("系统提示", "操作成功!", function () { grid.getStore().reload(); });
                            }
                        },
                        failure: function () {
                            Ext.Msg.alert("系统提示", "操作失败!");
                        }
                    });
                }
            }
            },
             { text: '删除该章节', handler: function () {
                 var rows = grid.getSelectionModel().getSelections();
                 if (!rows.length >= 1) {
                     Ext.Msg.alert("系统提示", "请选择要删除章节");
                 }
                 else {
                     var sectionsId = rows[0].get("SectiuonId");
                     Ext.Ajax.request({
                         url: 'Process/CheckSectionNo.aspx?sectionsId=' + sectionsId,
                         waitMsg: '数据处理中，请稍候.....',
                         success: function (response) {
                             var json = Ext.decode(response.responseText);
                             if (json.success) {
                                 Ext.Msg.alert("系统提示", "操作成功!", function () { grid.getStore().reload(); });
                             }
                         },
                         failure: function () {
                             Ext.Msg.alert("系统提示", "操作失败!");
                         }
                     });
                 }
             }
             }]
        })
    });
    getCS.load();
    gridMain(node, grid);

}