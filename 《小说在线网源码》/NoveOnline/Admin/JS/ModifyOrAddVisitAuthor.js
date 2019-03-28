var AddOrModifyVisitAuthor = function (item, grid) {

    var now = new Date().toLocaleDateString();

    var dsVisitAuthorUser = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/getVisitAuthorUser.aspx' }),
        reader: new Ext.data.JsonReader({}, [{ name: 'UserName', type: 'string' }, { name: 'UserId', type: 'int'}])
    });

    Ext.QuickTips.init(); 
    var form = new Ext.form.FormPanel({
        frame: true,
        border: true,
        autoHeight: false,
        collapsible: true,
        layout: 'form',
        labelWidth: 65,
        labelAlign: 'center',
        defaults: { anchor: '100%' },
        items: [{ xtype: 'hidden', name: 'VisitId' },
                { xtype: 'combo', name: 'UserName', hiddenName: 'UserId', store: dsVisitAuthorUser, displayField: 'UserName', valueField: 'UserId',
                    allowBlank: false,
                    emptyText: '===请选择===',
                    selectOnFocus: true,
                    forceSelection: true,
                    editable: false,
                    triggerAction: 'all',
                    mode: 'remote',
                    fieldLabel:'专访作者'
                },
                { xtype: 'textfield', name: 'VisitTitle', fieldLabel: '专访主题', allowBlank: true },
                { xtype: 'datefield', name: 'VisitDate', disabled: true, emptyText: now, fieldLabel: '专访日期', allowBlank: false, blankText: '文本不能为空' },
                { xtype: 'htmleditor', name: 'Contents', fieldLabel: '专访内容', allowBlank: false, style: 'overflow:auto;', enableColors: false, enableAlignments: false, id: 'htmlconter' }
        ]
    });

    if (item == null) {

        var win = new Ext.Window({
            modal: true,
            title: '添加专访',
            width: 500,
            height: 400,
            autoHeight: true,
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
                            url: 'Process/AddVisitAuthor.aspx',
                            waitMsg: '数据处理中,请稍后...',
                            success: function (ff, action) {
                                Ext.Msg.alert('系统提示', action.result.msg, function () {
                                    grid.getStore().reload();
                                    win.close();
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
    }

    if (item != null) {

        form.getForm().loadRecord(item);
        var win = new Ext.Window({
            modal: true,
            title: '修改专访信息',
            width: 500,
            height: 490,
            autoHeight: false,
            closeAction: 'close',
            resizable: false,
            border: false,
            bodyStyle: 'padding:3px',
            items: [form],
            bbar: new Ext.Toolbar(['->', {
                text: '修改',
                iconCls: 'new',
                handler: function () {
                    var f = form.getForm();
                    if (f.isValid()) {
                        f.submit({

                            url: 'Process/UpdateVisitAuthor.aspx',
                            waitMsg: '数据处理中,请稍后...',
                            success: function (ff, action) {
                                Ext.Msg.alert('系统提示', action.result.msg, function () {
                                    grid.getStore().reload();
                                    win.close();
                                });
                            },
                            failure: function (form, action) {
                                Ext.Msg.alert('系统提示', "修改失败");
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
    }

    win.show();
    dsVisitAuthorUser
}