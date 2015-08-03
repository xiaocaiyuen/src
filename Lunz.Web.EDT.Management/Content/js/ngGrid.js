var apiModule = angular.module('ng.ui.grid', [
    'ui.grid',
    'ui.grid.paging',
    'ui.grid.selection',
    'ui.grid.resizeColumns'
]);

apiModule.factory('$ngGrid', ['$http', '$log', 'i18nService',
    function ($http, $log, i18nService) {

        var self = this;

        var settings = {
            enableColumnMenus: true,
            enableRowSelection: true,
            enableRowHeaderSelection: true,
            multiSelect: true,
            selectionRowHeaderWidth: 35,
            enableColumnResizing: true,
            pagingPageSizes: [10, 20, 50],
            pagingPageSize: 10,
            enableFiltering: true,
            useExternalFiltering: true,
            useExternalPaging: true,
            useExternalSorting: true,
            columnDefs: [],
            pageData: null,
            onRegisterApi: null
        };

        var pagingOptions = {
            pageIndex: 1,
            pageSize: 20,
            sort: [],
            filters: []
        };

        var init = function ($scope, gridOptions) {

            self.$scope = $scope;
            settings = $.extend(settings, gridOptions);

            i18nService.setCurrentLang('zh-cn');

            $scope.gridOptions = {
                enableColumnMenus: settings.enableColumnMenus,
                enableRowSelection: settings.enableRowSelection,
                enableRowHeaderSelection: settings.enableRowHeaderSelection,
                selectionRowHeaderWidth: settings.selectionRowHeaderWidth,
                multiSelect: settings.multiSelect,
                enableColumnResizing: settings.enableColumnResizing,
                pagingPageSizes: settings.pagingPageSizes,
                pagingPageSize: settings.pagingPageSize,
                enableFiltering: settings.enableFiltering,
                useExternalFiltering: settings.useExternalFiltering,
                useExternalPaging: settings.useExternalPaging,
                useExternalSorting: settings.useExternalSorting,
                columnDefs: settings.columnDefs,
                onRegisterApi: function (gridApi) {
                    $scope.gridApi = gridApi;
                    //$scope.xGrid.gridApi = gridApi;
                    if (settings.onRegisterApi) {
                        settings.onRegisterApi(gridApi);
                    }
                    $scope.gridApi.core.on.sortChanged($scope, function (grid, sortColumns) {
                        if (sortColumns.length == 0) {
                            pagingOptions.sort = [];
                        } else {
                            pagingOptions.sort = [{
                                field: sortColumns[0].field,
                                sort: sortColumns[0].sort.direction
                            }];
                        }

                        getPageData();
                    });
                    $scope.gridApi.core.on.filterChanged($scope, function () {
                        var grid = this.grid;
                        var filters = [];
                        $(grid.columns).each(function () {
                            if (this.filters.length > 0
                                && this.filters[0].term != null
                                && this.filters[0].term != "") {
                                filters[filters.length] = {
                                    field: this.field,
                                    op: this.filters[0].op,
                                    term: this.filters[0].term
                                };
                            }
                        });
                        pagingOptions.pageIndex = 1;
                        pagingOptions.filters = filters;

                        getPageData();
                    });
                    $scope.gridApi.paging.on.pagingChanged($scope, function (newPage, pageSize) {
                        pagingOptions.pageIndex = newPage;
                        pagingOptions.pageSize = pageSize;

                        getPageData();
                    });
                }
            };

            getPageData();
        };

        var getPageData = function () {
            if (settings.pageData) {
                settings.pageData(pagingOptions, function (data) {
                    self.$scope.gridOptions.totalItems = data.RowsCount;
                    self.$scope.gridOptions.data = data.Data;
                });
            }
        };

        return {
            Render: init,
            Refresh: function () {
                pagingOptions.filters = [];
                pagingOptions.sort = [];
                getPageData();
            },
            GetSelectedRows: function () {
                return self.$scope.gridApi.selection.getSelectedRows();
            }
        };
    }]);