

var allEvents = function (node) {
    switch (node.id) {
        case 11:
            NovelNews(node);
            break;
        case 24:
            CheckAuthor(node);
            break;
        case 29:
            VisitAuthorManager(node);
            break;
        case 31:
            CommendBooks(node);
            break;
        case 33:
            EnterBook(node);
            break;
        case 51:
            ForbidUser(node);
            break;
        case 52:
            RnchainUser(node);
            break;
        case 61:
            GetDelBookReplay(node);
            break;
        case 62:
            ModeifyProp(node);
            break;
        case 32:
            getCheckSections(node);
            break;


    }
}

//================左侧功能导航列表=================

//Tree节点的单击事件处理方法
var nodeClick = function (node, event) {
    event.stopEvent();
    if (node.leaf) {
        allEvents(node);
    } else {
        node.toggle();
    }
}

//新闻管理树
var tree1 = new Ext.tree.TreePanel({
    rootVisible: false,
    autoScroll: false,
    border: false,
    bodyStyle: 'padding-top:6px',
    listeners: {
        click: function (node, event) {
            nodeClick(node, event);
        }
    }
});
var rootNode1 = new Ext.tree.TreeNode({ text: '新闻管理', leaf: false });
var childNode11 = new Ext.tree.TreeNode({ text: '新闻管理', iconCls: 'toolbardepartment', leaf: true, id: 11 });
rootNode1.appendChild(childNode11);
tree1.setRootNode(rootNode1);

//小说管理树
var tree3 = new Ext.tree.TreePanel({
    rootVisible: false,
    autoScroll: false,
    border: false,
    bodyStyle: 'padding-top:6px',
    listeners: {
        click: function (node, event) {
            nodeClick(node, event);
        }
    }
});
var rootNode3 = new Ext.tree.TreeNode({ text: '小说管理', leaf: false });
var childNode31 = new Ext.tree.TreeNode({ text: '小说推荐/取消推荐', leaf: true, id: 31 });
var childNode32 = new Ext.tree.TreeNode({ text: '新章节审核', leaf: true, id: 32 });
var childNode33 = new Ext.tree.TreeNode({ text: '增删小说', leaf: true, id: 33 });
rootNode3.appendChild(childNode31);
rootNode3.appendChild(childNode32);
rootNode3.appendChild(childNode33);
tree3.setRootNode(rootNode3);

//作者专访
var tree4 = new Ext.tree.TreePanel({
    rootVisible: false,
    autoScroll: false,
    border: false,
    bodyStyle: 'padding-top:6px',
    listeners: {
        click: function (node, event) {
            nodeClick(node, event);
        }
    }
});
var rootNode4 = new Ext.tree.TreeNode({ text: '作者专访', leaf: false });
var childNode29 = new Ext.tree.TreeNode({ text: '管理专访', leaf: true, id: 29 });

rootNode4.appendChild(childNode29);
tree4.setRootNode(rootNode4);

//会员管理
var tree5 = new Ext.tree.TreePanel({
    rootVisible: false,
    autoScroll: false,
    border: false,
    bodyStyle: 'padding-top:6px',
    listeners: {
        click: function (node, event) {
            nodeClick(node, event);
        }
    }
});
var rootNode5 = new Ext.tree.TreeNode({ text: '会员管理', leaf: false });
var childNode51 = new Ext.tree.TreeNode({ text: '禁用会员\\添加管理员', leaf: true, id: 51 });
var childNode52 = new Ext.tree.TreeNode({ text: '解禁会员', leaf: true, id: 52 });
var chlidNode44 = new Ext.tree.TreeNode({ text: '作者申请审核', leaf: true, id: 24 });
rootNode5.appendChild(childNode51);
rootNode5.appendChild(childNode52);
rootNode5.appendChild(chlidNode44);
tree5.setRootNode(rootNode5);

//书评管理
var tree6 = new Ext.tree.TreePanel({
    rootVisible: false,
    autoScroll: false,
    border: false,
    bodyStyle: 'padding-top:6px',
    listeners: {
        click: function (node, event) {
            nodeClick(node, event);
        }
    }
});
var rootNode6 = new Ext.tree.TreeNode({ text: '书评管理', leaf: false });
var childNode61 = new Ext.tree.TreeNode({ text: '查看\\删除书评', leaf: true, id: 61 });
rootNode6.appendChild(childNode61);
tree6.setRootNode(rootNode6);


//商品管理
var tree7 = new Ext.tree.TreePanel({
    rootVisible: false,
    autoScroll: false,
    border: false,
    bodyStyle: 'padding-top:6px',
    listeners: {
        click: function (node, event) {
            nodeClick(node, event);
        }
    }
});
var rootNode7 = new Ext.tree.TreeNode({ text: '商品管理', leaf: false });
var childNode62 = new Ext.tree.TreeNode({ text: '修改商品', leaf: true, id: 62 });
rootNode7.appendChild(childNode62);
tree7.setRootNode(rootNode7);

var west = new Ext.Panel({
    layout: 'accordion',
    region: 'west',
    title: '后台管理系统',
    width: 180,
    minWidth: 180,
    maxWidth: 280,
    collapsible: true,
    split: true,
    border: true,
    layoutConfig: { animate: true },
    items: [{
        title: '新闻管理',
        iconCls: 'catalogmanager',
        items: [tree1]
    }, {
        title: '小说管理',
        iconCls: 'systemsetup',
        items: [tree3]
    }, {
        title: '作者专访',
        iconCls: 'systemsetup',
        items: [tree4]
    }, {
        title: '会员管理',
        iconCls: 'systemsetup',
        items: [tree5]
    }, {
        title: '书评管理',
        iconCls: 'systemsetup',
        items: [tree6]
    }, {
        title: '商品管理',
        iconCls: 'systemsetup',
        items: [tree7]
    }]
});

function gridMain(node, panel) {
    var tab = center.getItem(node.id);
    if (!tab) {
        tab = center.add({
            xtype: 'panel',
            border: false,
            id: node.id,
            title: node.text,
            closable: true,
            layout: 'fit',
            items: [panel]
        });
    }

    center.setActiveTab(tab);
}

var toolbar = new Ext.Panel({
    region: 'north',
    height: 28,
    border: false,
    tbar: new Ext.Toolbar([{
        text: '新闻管理',
        iconCls: 'toolbardepartment',
        handler: function () {
            NovelNews(childNode11);
        }
    }, '-', {
        text: '小类管理',
        iconCls: 'toolbarcategory',
        handler: function () {
            categoryManager(childNode12);
        }
    }, '-', {
        text: '商品管理',
        iconCls: 'toolbarproduct',
        handler: function () {
            productManager(childNode13);
        }
    }])
});

var center = new Ext.TabPanel({
    region: 'center',
    border: false,
    enableTabScroll: true,
    items: [{
        title: '小说在线'
    }],
    activeTab: 0
});

Ext.onReady(function () {
    var viewport = new Ext.Viewport({
        layout: 'border',
        items: [{
            region: 'north',
            el: 'north',
            border: false
        }, west, {
            region: 'center',
            layout: 'border',
            //items: [toolbar, center],
            items:[center],
            border: true
        }]
    });
});