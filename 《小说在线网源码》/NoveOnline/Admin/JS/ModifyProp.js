var ModeifyProp = function (node) {

    var index = -1;
    var pageSize = 10;


    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: true });

    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        sm,
        { header: '商品编号', dataIndex: 'PropId', width: 80, align: 'center' },
        { header: '商品名称', dataIndex: 'PropName', width: 80, align: 'center' },
        { header: '商品介绍', dataIndex: 'PropIntroduction', align: 'center' },
        { header: '商品图片', dataIndex: 'PropImage', width: 80, align: 'center' },
        { header: '商品价格', dataIndex: 'PropPrice', width: 80, align: 'center' },
        { header: '发布时间', dataIndex: 'createDate', renderer: Ext.util.Format.dateRenderer('Y-m-d'), width: 100, align: 'center' },
        { header: '售出数量', dataIndex: 'outNumber', width: 80, align: 'center' }
    ]);

    var record = new Ext.data.Record.create([
        { name: 'PropId', type: 'int' },
        { name: 'PropName', type: 'string' },
        { name: 'PropIntroduction', type: 'string' },
        { name: 'PropImage', type: 'string' },
        { name: 'PropPrice', type: 'string' },
        { name: 'createDate', type: 'date', dateFormat: 'Y-m-d H:i:s' },
        { name: 'outNumber', type: 'int' }
    ]);

    var dsProps = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/getPropInfo.aspx' }),
        reader: new Ext.data.JsonReader({
            totalProperty: 'recordCount',
            root: 'result'
        }, record),
        remoteStore: true
    });

    var grid = new Ext.grid.GridPanel({
        title: '商品管理',
        region: 'center',
        sm: sm,
        cm: cm,
        store: dsProps,
        autoExpandColumn: 3,
        autoScroll: true,
        columnLines: true,
        stripeRows: true,
        loadMask: { msg: '数据加载中，请稍候...' },
        listeners: {
            rowClick: function (grid, rowIndex, event) {
                if (grid.getSelectionModel().getSelected() != null) {
                    index = rowIndex;
                    var item = dsProps.getAt(index);
                    Ext.getCmp('modify').enable();
                    Ext.getCmp('form').getForm().loadRecord(item);
                } else {
                    index = -1;
                    Ext.getCmp('modify').disable();
                }
            }
        },
        tbar: new Ext.Toolbar({
            items: [new Ext.form.FormPanel({
                width: 700,
                defaults: { anchor: '100%' },
                bodyStyle:'padding-top:10px;padding-right:20px;padding-bottom:10px;',
                labelWidth: 70,
                id: 'form',
                autoHeight: true,
                region: 'north',
                layout: 'form',
                labelAlign:'right',
                items: [{ fieldLabel: '商品名称', name: 'PropName', xtype: 'textfield' },
                        { fieldLabel: '售出数量', name: 'outNumber', xtype: 'textfield' },  
                        { fieldLabel: '商品图片', name: 'PropImage', xtype: 'textfield' },
                        { fieldLabel: '商品价格', name: 'PropPrice', xtype: 'textfield' },
                        { fieldLabel: '创建时间', name: 'createDate', xtype: 'datefield' },
                        { name: 'PropId', xtype: 'hidden' },
                        { fieldLabel: '商品介绍', name: 'PropIntroduction', xtype: 'textarea',style: 'overflow:auto'} 
                      ],

                buttons: ['->', { text: '修改',
                    id: 'modify',
                    disabled: true,
                    handler: function () {

                        var f = Ext.getCmp('form').getForm();
                        if (!f.isValid) {
                            return;
                        }

                        f.submit({
                            url: 'Process/ModifyProp.aspx',
                            waitMsg: '数据处理中，请等待...',
                            success: function (ff, action) {
                                if (action.result.success) {
                                    grid.getStore().reload();
                                    ff.reset();
                                    Ext.Msg.alert('系统提示', '操作成功！')


                                }
                                else {
                                    Ext.Msg.alert('系统提示', '操作失败！')
                                }
                            },
                            failure: function () {
                                Ext.Msg.alert('系统提示', '操作失败！');
                            }
                        })
                    }

                }]
            })]
        }),
        bbar: new Ext.PagingToolbar({
            pageSize: pageSize,
            store: dsProps,
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
    dsProps.load();
}