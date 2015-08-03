var apiModule = angular.module('ng.datatables', []);

apiModule.factory('$ngDataTables', ['$log', '$compile',
    function ($log, $compile) {

        var self = this;
        self.dataTables = [];

        var initDataTable = function (options) {
            var dataTable;
            options.renderActions = function () {
                var searchCells = $("tbody>tr>td>div.actions", dataTable.table);
                $(searchCells).each(function (index) {
                    var cell = $(this);
                    var parent = cell.parent();
                    cell.remove();
                    $("[data-action]", cell).each(function () {
                        $(this).attr("data-ng-click",
                            $(this).attr("data-action") + "(" + index + ")");
                    });
                    cell = $compile(cell)(options.scope);
                    parent.append(cell);
                });
            };
            dataTable = new ngDataTable().init(options);
            self.dataTables[options.src] = dataTable;
            return dataTable;
        }

        return {
            dataTables: self.dataTables,
            init: initDataTable
        };
    }]);