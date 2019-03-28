var AddVolume = function (item) {
    var form = new Ext.form.FormPanel({
        frame: true,
        border: true,
        autoHeight: true,
        collapsible: true,
        layout: 'form',        
        labelWidth: 60,
        labelAlign: 'right',
        defaults: { anchor: '99%' },
        items: [{ xtype: 'textfield', name: 'BookId',fieldLabel: '书本ID' },
            { xtype: 'textfield', name: 'VolumeName', fieldLabel: '新卷名称', allowBlank: false, blankText: '不能为空' }
    ]
    });
    form.getForm().loadRecord(item);
    var win = new Ext.Window({
        modal: true,
        title: '添加新卷',
        width: 500,
        height: 200,
        autoHeight: false,
        closeAction: 'close',
        resizable: false,
        border: false,
        bodyStyle: 'padding:3px',
        items: [form],
        bbar: new Ext.Toolbar(['->', {
            text: '添加',
            iconCls: 'new',
            handler: function () {
                var f = form.getForm();
                if (f.isValid()) {
                    f.submit({
                        url: 'Process/AddVolume.aspx',
                        waitMsg: '数据处理中,请稍后...',
                        success: function (ff, action) {
                            Ext.Msg.alert('系统提示', action.result.msg, function () {
                                form.getForm().reset();
                            });
                        },
                        failure: function (form, action) {
                            Ext.Msg.alert('系统提示', "添加失败");
                        }
                    });
                }
            }
        }, '-', {
            text: '重置',
            iconCls: 'reset',
            handler: function () {
                form.getForm().reset();
            }
        }, '-', {
            text: '关闭',
            iconCls: 'close',
            handler: function () {
                win.close();
            }
        }])
    });

    win.show();
}