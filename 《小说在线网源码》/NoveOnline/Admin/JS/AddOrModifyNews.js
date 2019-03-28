var AddOrModifyNews = function (item) {


    var filename = "";

    var fp = new Ext.FormPanel({
        fileUpload: true,              //标志此表单数据中包含文件数据
        width: 439,
        frame: true,
        autoHeight: true,
        bodyStyle: 'padding: 10px 10px 0 10px;',
        labelWidth: 50,
        defaults: {
            anchor: '100%',
            allowBlank: false,
            msgTarget: 'side'
        },

        items: [
        {
            xtype: 'fileuploadfield',     //表单域类型
            id: 'formfile',
            emptyText: '请您选择文件',
            hideLabel: true,
            buttonCfg: {
                text: '浏览...'//,
                //iconCls: 'upload-icon'     //按钮图标
            }
        }],
        buttons: [{
            text: '保存',
            handler: function () {
                var f = Ext.getCmp("formfile");
                filename = f.value;
                var arr = filename.toString().split("\\");
                filename = arr[arr.length - 1];
                if (fp.getForm().isValid()) {
                    fp.getForm().submit({
                        url: 'NewsUpLoad.aspx',     //后台处理页面（可以是php，asp，aspx，jsp等等）
                        waitMsg: '正在上传...',
                        success: function (fp, o) {
                            if (o.result.success == "true") {
                                Ext.Msg.alert("系统提示", "上传成功");
                            }
                            else {
                                Ext.MessageBox.alert('消息', o.result.message);
                            }
                        }
                    });
                }
            }
        }
        , {
            text: '关闭',
            handler: function () {
                showWindow.hide();
            }
        }
        ]
    });


    function isImg(fileName) {
        var extArray = new Array(".jpg", ".gif", ".bmp", ".png");
        var ext = fileName.slice(fileName.indexOf(".")).toLowerCase();
        for (var i = 0; i < extArray.length; i++) {
            if (extArray[i] == ext) {
                return true;
            }
        }
        return false;
    }


    var showWindow = new Ext.Window
    ({
        id: "showWin",
        title: "文件上传窗口",
        width: 450,
        height: 110,
        resizable: false,
        closable: false,
        items:
            [fp]
    });


    if (item == null) {
        var form = new Ext.form.FormPanel({
            frame: true,
            border: true,
            autoHeight: true,
            collapsible: true,
            layout: 'form',
            labelWidth: 60,
            labelAlign: 'right',
            defaults: { anchor: '99%' },
            items: [{ xtype: 'hidden', name: 'NewsId' },
                { xtype: 'textfield', name: 'NewsTitle', fieldLabel: '新闻标题', allowBlank: false, blankText: '不能为空' },
                { fieldLabel: '书本封面', xtype: 'button', text: '上传新闻图片', handler: function () { showWindow.show(); } },
                { xtype: 'textfield', name: 'FromWhere', fieldLabel: '新闻来源', allowBlank: false, blankText: '文本不能为空' },
                { xtype: 'htmleditor', name: 'NewsContens', fieldLabel: '新闻内容', allowBlank: false, blankText: '文本不能为空', style: 'overflow:auto', enableColors: false, enableAlignments: false,id:'NewsContens' }
        ]
        });

        var win = new Ext.Window({
            modal: true,
            title: '添加新闻信息',
            width: 500,
            height: 400,
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
                    var NewsContens = Ext.getCmp("NewsContens").getValue();
                    NewsContens = Ext.util.Format.htmlEncode(NewsContens); //将 <> 转换成 &lt;&gt;解决换行就提交失败
                    Ext.getCmp("NewsContens").setValue(NewsContens); 
                    if (f.isValid()) {
                        f.submit({
                            url: 'Process/AddNovelNews.aspx?img=' + filename,
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
    }

    if (item != null) {
        var form = new Ext.form.FormPanel({
            frame: true,
            border: true,
            autoHeight: true,
            collapsible: true,
            layout: 'form',
            title: '修改新闻',
            labelWidth: 60,
            labelAlign: 'right',
            defaults: { anchor: '99%' },
            items: [{ xtype: 'hidden', name: 'NewsId' },
                { xtype: 'textfield', name: 'NewsTitle', fieldLabel: '新闻标题', allowBlank: false, blankText: '不能为空' },
                { xtype: 'textfield', name: 'NewsImages', fieldLabel: '图片路径', allowBlank: true,},
                { xtype: 'textfield', name: 'FromWhere', fieldLabel: '新闻来源', allowBlank: false, blankText: '文本不能为空' },
                { xtype: 'textfield', name: 'AddTime', fieldLabel: '发布时间', allowBlank: false, blankText: '文本不能为空' },
                { xtype: 'htmleditor', name: 'NewsContens', fieldLabel: '新闻内容', allowBlank: false, blankText: '文本不能为空', style: 'overflow:auto', enableColors: false, enableAlignments: false }
        ]
        });
        form.getForm().loadRecord(item);
        var win = new Ext.Window({
            modal: true,
            title: '修改新闻信息',
            width: 500,
            height: 400,
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
                            url: 'Process/UpdateNovelNew.aspx',
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
    }

    win.show();
}