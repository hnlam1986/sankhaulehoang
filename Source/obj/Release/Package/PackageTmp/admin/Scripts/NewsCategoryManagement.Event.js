var NewsCategoryManagementEvent = {
    isCreateNode: false,
    treeView: "NewsCate",
    CustomMenu: function ($node) {
        this.treeView = this.element.get(0).id;
        var tree = $("#" + this.treeView).jstree(true);
        var items = {
            createItem: {
                label: "Thêm",
                "action": function (obj) {
                    $node = tree.create_node($node);
                    tree.edit($node);
                }
            },
            renameItem: { // The "rename" menu item
                label: "Đổi tên",
                "action": function (obj) { tree.edit($node); }
            },
            deleteItem: { // The "delete" menu item
                label: "Xóa",
                "action": function (obj) {
                    tree.delete_node($node);
                }
            },
            displayItem: { // The "delete" menu item
                label: "Hien Menu",

                "action": function (obj) {
                    var Url = "/ajax.aspx?action=changestatus&id=" + $node.id;
                    $.ajax({
                        type: "POST",
                        url: Url,
                        dataType: 'text',
                        error: function (msg) {
                            this.result = false;
                        }
                    });
                }
            },
            hideItem: { // The "delete" menu item
                label: "An Menu",

                "action": function (obj) {
                    var Url = "/ajax.aspx?action=changestatus&id=" + $node.id;
                    $.ajax({
                        type: "POST",
                        url: Url,
                        dataType: 'text',
                        error: function (msg) {
                            this.result = false;
                        }
                    });
                }
            },
            changelink: { // The "delete" menu item
                label: "Chuyen link",

                "action": function (obj) {
                  

                    //-----------------
                    var $dialog = $('<div></div>')
                .html('<iframe style="border: 0px; " src="UpdateNaviLink.aspx?id=' + $node.id + '" width="100%" height="100%"></iframe>')
                .dialog({
                    autoOpen: false,
                    modal: true,
                    height: 130,
                    width: 550,
                    title: "Chuyen huong link"
                });
                $dialog.dialog('open');
                    //--------------------
                }
            }

        };
        if ($node.parents.length == 1) {
            delete items.deleteItem;
            delete items.renameItem;
        } else if ($node.parents.length == 3) {
            delete items.createItem;
        }
        if ($("#" + $node.id).attr("data-visible") == "True") {
            delete items.displayItem;
        } else {
            delete items.hideItem;
        }
        return items;
    },
    InitJSTreeView: function (tree) {
        this.treeView = tree;
        $('#' + tree).on('changed.jstree', function (e, data) {
            var id = undefined;
            if (data.node && data.node.id) {
                id = data.node.id;
            }
            if (id) {
                var level = data.node.parents.length;
                var Url = "";
                if (level >= 3) {
                    Url = "/ajax.aspx?action=getnewslist&isLeaf=true&id=" + id;
                } else {
                    Url = "/ajax.aspx?action=getnewslist&isLeaf=false&id=" + id;
                }
                $.ajax({
                    type: "POST",
                    url: Url,
                    dataType: 'text',
                    success: function (data) {
                        if (data != 'False') {
                            $("#NewsListFrame").get(0).innerHTML = data;
                        }

                    },
                    error: function (msg) {
                        this.result = false;
                    }
                });
            }
        }).jstree({
            "plugins": ["contextmenu", "dnd", "state"], contextmenu: { items: this.CustomMenu },

            'core': {
                "check_callback": function (operation, node, node_parent, node_name, move_to) {
                    if (operation == 'rename_node') {
                        var action = "rename";
                        var isleaf = false;
                        if (NewsCategoryManagementEvent.isCreateNode) {
                            action = "create";
                        }
                        if (node.parents.length >= 3) {
                            isleaf = true;
                        }
                        $.ajax({
                            type: "POST",
                            url: "/ajax.aspx?action=" + action,
                            dataType: 'text',
                            data: { id: NewsCategoryManagementEvent.isCreateNode ? node.parent : node.id, name: node_name, leaf: isleaf },
                            success: function (data) {
                                if (data == 'True' && action == "rename") {
                                    return true;
                                }
                                else {
                                    NewsCategoryManagementEvent.isCreateNode = false;
                                    $('#' + tree).jstree('refresh');
                                }
                            },
                            error: function (msg) {
                                this.result = false;
                            }
                        });

                    } else if (operation == 'delete_node') {
                        $.ajax({
                            type: "POST",
                            url: "/ajax.aspx?action=delete",
                            dataType: 'text',
                            data: { id: node.id },
                            success: function (data) {
                                if (data == 'True') {
                                    return true;
                                }
                                else {
                                    $('#' + tree).jstree('refresh');
                                }
                            },
                            error: function (msg) {
                                this.result = false;
                            }
                        });
                    } else if (operation == 'create_node') {
                        NewsCategoryManagementEvent.isCreateNode = true;
                    } else if (operation == 'move_node') {
                        if (!move_to) {
                            if (arguments[1].children.length > 0 && arguments[2].parents.length == 2 || arguments[2].parents.length > 2) {
                                alert('Vuot qua so cap quy dinh!');
                                //$('#' + tree).jstree('refresh');
                                return false;
                            }
                            var isLeaf = false;
                            if (arguments[2].parents.length == 2) {
                                isLeaf = true;
                            }
                            $.ajax({
                                type: "POST",
                                url: "/ajax.aspx?action=order",
                                dataType: 'text',
                                data: { selected: arguments[1].id, children: arguments[2].children.toString(), children_sort: arguments[2].children_d.toString(),
                                    parent_node: arguments[2].id, order: arguments[3], is_leaf: isLeaf
                                },
                                success: function (data) {
                                    if (data == 'True') {
                                        return true;
                                    }
                                    else {
                                        $('#' + tree).jstree('refresh');
                                    }
                                },
                                error: function (msg) {
                                    this.result = false;
                                }
                            });
                        } else {

                        }
                    }
                },
                'data': {
                    'url': '/ajax.aspx',
                    'data': function (node) {
                        return {
                            'action': 'get',
                            'id': node.id
                        };
                    }
                }
            }

        });
    },
    DeleteNewsItem: function (id, image) {
        if (id) {
            $("#news_indicator_" + id).css("display", "");
            $.ajax({
                type: "POST",
                url: "/ajax.aspx?action=deletenews",
                dataType: 'text',
                data: { "id": id, img: image },
                success: function (data) {
                    if (data == 'True') {
                        $("#news_" + id).remove();
                    }
                },
                error: function (msg) {
                    this.result = false;
                }
            });
        }
    }
}