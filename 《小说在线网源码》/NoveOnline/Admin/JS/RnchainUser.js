var RnchainUser = function (node) {
    var index = -1;

    var sm = new Ext.grid.CheckboxSelectionModel({ singleSelect: false });

    var dsUserType = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/GetUserTypeInfo.aspx' }),
        reader: new Ext.data.JsonReader({}, [{ name: 'UserTypeId', type: 'int' }, { name: 'UserTypeName', type: 'string'}])
    });

    var cm = new Ext.grid.ColumnModel([
        new Ext.grid.RowNumberer(),
        sm,
        { header: '会员编号', dataIndex: 'UserId', width: 80, align: 'center' },
        { header: '会员名称', dataIndex: 'UserName', width: 80, align: 'center' },
        { header: '会员性别', dataIndex: 'UserSex', align: 'center' },
        { header: '拥有书币', dataIndex: 'BookMoney', width: 80, align: 'center' },
        { header: '会员类型', dataIndex: 'UserTypeName', width: 80, align: 'center' },
        { header: '所在省', dataIndex: 'province', width: 80, align: 'center' },
        { header: '所在市', dataIndex: 'city', width: 80, align: 'center' }
    ]);

    var result = new Ext.data.Record.create([
        { name: 'UserId', type: 'int' },
        { name: 'UserName', type: 'string' },
        { name: 'UserSex', type: 'string' },        
        { name: 'BookMoney', type: 'string' },
        { name: 'UserTypeName', type: 'string' },
        { name: 'province', type: 'string' },
        { name: 'city', type: 'string' }
    ]);

    var dsUsers = new Ext.data.Store({
        proxy: new Ext.data.HttpProxy({ url: 'Process/GetRnchainUser.aspx' }),
        reader: new Ext.data.JsonReader({}, result),
        remoteStore: true
    });

    var grid = new Ext.grid.GridPanel({
        title: '解禁用户',
        sm: sm,
        cm: cm,
        store: dsUsers,
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
            { text: '解禁用户',
                iconCls: '',
                handler: function () {
                    var selectedRows = grid.getSelectionModel().getSelections();
                    var userIds = "";
                    var userName = "";
                    if (selectedRows.length == 0) {
                        Ext.Msg.alert('系统提示', '请选择会员');
                        return;
                    }

                    for (var i = 0; i < selectedRows.length; i++) {
                        userIds += selectedRows[i].data.UserId + ",";
                        userName += selectedRows[i].data.UserName + ",";
                    }
                    Ext.Msg.confirm('系统提示', '确定要解禁' + userName.substring(0, userName.length - 1) + '?', function (btn) {
                        if (btn == 'yes') {
                            Ext.Ajax.request({
                                url: 'Process/RnchainUser.aspx',
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
                                    Ext.Msg.alert('系统提示', '操作失败');
                                    grid.getStore().reload();
                                },
                                params: { id: userIds }
                            });
                        }
                    });
                }
            }]
        })
    });

    gridMain(node, grid);
    dsUsers.load();
}