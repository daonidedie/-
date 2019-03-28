var AddSections = function (item) {
    var form = new Ext.form.FormPanel({
        frame: true,
        method: 'post',
        border: true,
        autoHeight: true,
        collapsible: true,
        layout: 'form',
        title: '添加新章',
        labelWidth: 62,
        labelAlign: 'left',
        defaults: { anchor: '100%' },
        items: [{ xtype: 'hidden', name: 'BookId' },
            { xtype: 'textfield', name: 'BookName', fieldLabel: '书本名称', allowBlank: false, blankText: '不能为空', disabled: true },
            { xtype: 'textfield', name: 'VolumeName', fieldLabel: '章节标题', allowBlank: false, blankText: '不能为空' },
            { xtype: 'htmleditor', name: 'Contents', fieldLabel: '章节内容', allowBlank: false, blankText: '不能为空', style: 'overflow:auto', id: 'htmlconter' }
    ]
    });
    form.getForm().loadRecord(item);
    var win = new Ext.Window({
        modal: true,
        title: '添加新章',
        width: 700,
        height: 520,
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
                var contents = Ext.getCmp("htmlconter").getValue(); //获取章节内容
                contents = Ext.util.Format.htmlEncode(contents); //将 <> 转换成 &lt;&gt;解决换行就提交失败
                Ext.getCmp("htmlconter").setValue(contents); //重新给内容赋值

                if (f.isValid()) {
                    f.submit({
                        url: 'Process/AddSections.aspx',
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