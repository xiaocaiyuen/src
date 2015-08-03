
var ngDataTable = function () {

    var self = this;
    var tableOptions;
    var dataTable;
    var table;
    var the;

    var setFilter = function (filters, input, value) {
        if (value && value != "") {
            var field = input.attr('data-filter-field');
            var op = input.attr("data-filter-op");
            var term = input.attr('name');

            var filter;
            $(filters).each(function () {
                if (this.field == field) {
                    filter = this;
                    return false;
                }
            });
            if (filter == undefined) {
                filter = {};
                filters[filters.length] = filter;
            }
            filter["field"] = field;
            filter["op"] = op;
            filter[term] = value;
        }
    }

    return {

        init: function (options) {

            if (!$().dataTable) {
                return;
            }

            the = this;

            options = $.extend(true, {
                dom: 't<"row"<"col-md-12"<"pull-left"l><"pull-left"i><"pull-right"p>>>',
                stateSave: false,
                orderCellsTop: true,
                paging: true,
                pagingType: "full_numbers",
                serverSide: true,
                selectable: true,
                searchable: true,
                search: {
                    width: 100,
                    actionTemplate: undefined,
                    render: function (data, type, full, meta) {
                        if (options.search.actionTemplate) {
                            var actionDiv = $(options.search.actionTemplate).clone();
                            actionDiv.show();
                            return actionDiv[0].outerHTML;
                        }
                        return ' ';
                    },
                },
                lengthMenu: [
                    [10, 25, 50, -1],
                    [10, 25, 50, "全部"]
                ],
                getData: function (paging, success) {
                },
                ajax: function (request, drawCallback, settings) {
                    var sort = [];
                    if (request.order) {
                        $(request.order).each(function () {
                            sort[sort.length] = {
                                field: request.columns[this.column].data,
                                sort: this.dir
                            };
                        });
                    }
                    var filters = [];
                    if (self.tableOptions.searchable) {
                        $('textarea.form-filter, select.form-filter, input.form-filter:not([type="radio"],[type="checkbox"])', table).each(function () {
                            setFilter(filters, $(this), $(this).val());
                        });

                        $('input.form-filter[type="checkbox"]:checked', table).each(function () {
                            setFilter(filters, $(this), $(this).val());
                        });

                        $('input.form-filter[type="radio"]:checked', table).each(function () {
                            setFilter(filters, $(this), $(this).val());
                        });
                    }
                    var paging = {
                        pageIndex: request.start / request.length + 1,
                        pageSize: request.length,
                        sort: sort,
                        filters: filters
                    };
                    self.tableOptions.getData(paging, function (result) {
                        if (result.Data) {
                            the.data = result.Data;
                        } else {
                            the.data = [];
                        }


                        drawCallback({
                            draw: request.draw,
                            recordsTotal: result.RowsCount,
                            recordsFiltered: result.RowsCount,
                            data: the.data
                        });
                    });
                },
                "order": [],
                "drawCallback": function (settings) {
                    options.renderActions();

                    Metronic.initUniform($('input[type="checkbox"]', table));
                },
                "language": {
                    "lengthMenu": "_MENU_ 行 / 页",
                    "info": "&nbsp;&nbsp;|&nbsp;&nbsp;当前 _START_ - _END_ / _TOTAL_ 行",
                    "infoEmpty": "当前无记录。",
                    "emptyTable": "当前无记录。",
                    "zeroRecords": "当前无记录。",
                    "paginate": {
                        "previous": "&lt;",
                        "next": ">",
                        "last": ">|",
                        "first": "|&lt;",
                        "page": "页",
                        "pageOf": "/"
                    }
                },
            }, options);

            self.table = $(options.src);

            if ($("thead>tr.heading", self.table).length == 0) {
                $("<tr class='heading'/>").appendTo($("thead", self.table));
            }

            if ($("thead>tr.heading>th", self.table).length == 0) {
                $(options.columns).each(function () {
                    $("<th/>").text(this.title).appendTo($("thead>tr.heading", self.table));
                });
            }

            if (options.selectable) {
                options.columns.splice(0, 0, {
                    orderable: false,
                    data: null,
                    width: 25,
                    render: function (data, type, full, meta) {
                        return '<input type="checkbox" value="' + meta.row + '"/>';
                    }
                });

                var chkGroup = $("thead>tr>th>input.group-checkable", self.table);
                if (chkGroup.length == 0) {
                    $("<th><input type='checkbox' class='group-checkable'/></th>")
                            .insertBefore($("thead>tr.heading>th:first", self.table));
                }
            }

            if (options.searchable) {
                options.columns.push({
                    orderable: false,
                    data: null,
                    width: options.search.width,
                    render: options.search.render
                });

                $("<th/>").appendTo($("thead>tr.heading", self.table));

                if ($("thead>tr.filter", self.table).length == 0) {
                    $("<tr class='filter'/>").appendTo($("thead", self.table));
                }

                if ($("thead>tr.filter>td", self.table).length == 0) {
                    $(options.columns).each(function (index, column) {
                        var td = $("<td/>");
                        if (column.data || column.filter) {
                            var field = column.data;
                            var op = 'cn';
                            var render;
                            if (column.filter) {
                                var filter = column.filter;
                                if (filter.field && filter.field != null && filter.field != "") {
                                    field = filter.field;
                                }
                                if (filter.op && filter.op != null && filter.op != "") {
                                    op = filter.op;
                                }
                                if (filter.render) {
                                    render = filter.render;
                                }
                            }

                            var filterBox;
                            if (render) {
                                filterBox = render(column);
                                if (filterBox) {
                                    $(filterBox)
                                        .attr('data-filter-field', field)
                                        .attr('data-filter-op', op)
                                        .attr('name', 'term');
                                }
                            } else {
                                filterBox = $("<input type='text'/>")
                                    .addClass("form-control input-sm");
                            }
                            if (filterBox) {
                                $(filterBox)
                                    .addClass('form-filter')
                                    .attr('data-filter-field', field)
                                    .attr('data-filter-op', op)
                                    .attr('name', 'term')
                                    .appendTo(td);
                            }
                        }
                        if (index + 1 == options.columns.length) {
                            var btnGroup = $('<div class="btn-group btn-group-sm btn-group-solid">');
                            $('<button class="btn green filter-submit "><i class="fa fa-search"></i> 搜索</button>').appendTo(btnGroup);
                            $('<button class="btn red filter-cancel"><i class="fa fa-times"></i> </button>').appendTo(btnGroup);
                            btnGroup.appendTo(td);
                        }
                        td.appendTo($("thead>tr.filter", self.table));
                    });
                }

                //$("thead>tr.filter>td", self.table).each(function (index) {
                //    var filterBox = $(".form-filter", this);
                //    if (filterBox.length > 0) {
                //        filterBox.attr("data-filter-field", options.columns[index].data);
                //    }
                //});
            }

            self.tableOptions = options;

            self.dataTable = self.table.DataTable(options);

            self.table.find('.group-checkable').change(function () {
                var set = $('tbody > tr > td:nth-child(1) input[type="checkbox"]', self.table);
                var checked = $(this).is(":checked");
                $(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                        $(this).parents('tr').addClass("active");
                    } else {
                        $(this).attr("checked", false);
                        $(this).parents('tr').removeClass("active");
                    }
                });
                $.uniform.update(set);
            });

            self.table.on('change', 'tbody > tr > td:nth-child(1) input[type="checkbox"]', function () {
                $(this).parents('tr').toggleClass("active");
            });

            self.table.on('click', '.filter-submit', function (e) {
                the.refresh();
            });

            self.table.on('click', '.filter-cancel', function (e) {
                the.resetFilter();
            });

            the.dataTable = self.dataTable;
            the.table = self.table;

            return the;
        },

        dataTable: null,

        table: null,

        data: [],

        refresh: function (reset) {
            if (reset) {
                the.resetFilter();
                the.dataTable.ajax.reload();
            } else {
                the.dataTable.draw(false);
            }
        },

        resetFilter: function () {
            $('textarea.form-filter, select.form-filter, input.form-filter', table).each(function () {
                $(this).val("");
            });
            $('input.form-filter[type="checkbox"]', table).each(function () {
                $(this).attr("checked", false);
            });

            the.dataTable.ajax.reload();
        },

        submitFilter: function () {
            the.dataTable.ajax.reload();
        },

        getSelectedRows: function () {
            return the.dataTable.rows(".active");
        },

        getSelectedData: function () {
            return the.dataTable.rows(".active").data();
        },

        //setSelectedRows: function(rows){
        //    if (typeof rows == 'function') {
        //        var checkGroups = $('tbody > tr > td:nth-child(1) input[type="checkbox"]');
        //        $(the.data).each(function (index, item) {
        //            if (rows(index, item)) {
        //                checkGroups.eq(index).attr("checked", "checked");
        //                //checkGroups.eq(index).change();
        //            }
        //        });
        //    }
        //},

        addData: function (data) {
            the.dataTable.row.add(data);
            the.refresh();
        }
    };
};