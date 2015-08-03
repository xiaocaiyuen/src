
MetronicApp.controller('dataDictionaryListCtrl', ['$scope', '$ngDataTables', '$apiSettings', '$modal', '$notify', '$location',
    function ($scope, $ngDataTables, $api, $modal, $notify, $location) {
        $scope.deletedRows = [];
        $scope.ParentId = $location.search().ParentId;
        $scope.dataTable;

        var initDataTable = function () {
            $scope.dataTable = $ngDataTables.init({
                src: "#dataTable",
                scope: $scope,
                selectable: true,
                searchable: true,
                columns: [
                    { title: '名称', data: 'Name' },
                    { title: '标识', data: 'Code' }
                    //{ title: '排序', data: 'Order' }
                ],
                getData: function (paging, success) {
                    $api.ListDataDictionary(paging, success);
                },
                search: {
                    width: 228,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("数据字典", "已经成功刷新数据。");
        }

        $scope.add = function () {
            $modal.open({
                templateUrl: 'Settings/DataDictionary/new',
                controller: 'DataDictionaryNewCtrl'
            }).result.then(function (data) {
                //$scope.gridOptions.data.push(data);
                $scope.dataTable.addData(data);
                $notify.success('数据字典', '已成功新建了数据!');
            });
        }

        $scope.edit = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length == 1) {
                var entity = rows[0];
                $scope.editRow(entity);
            } else {
                $notify.info("数据字典", "请选择一条要编辑的数据。");
            }
        }

        $scope.delete = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length > 0) {
                var ids = [];

                angular.forEach(rows, function (item, i) {
                    ids[ids.length] = item.Id;
                });

                $scope.deleteRow(ids);
            } else {
                $notify.info("数据字典", "请选择要删除的数据。");
            }
        }

        $scope.editRow = function (row) {

            var entity;
            if (typeof row == "object") {
                entity = row;
            } else {
                entity = $scope.dataTable.data[row];
            }

            $modal.open({
                templateUrl: 'Settings/DataDictionary/edit',
                controller: 'UpdateDataDictionaryEditCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
            }).result.then(function (data) {
                $scope.dataTable.refresh();
                $notify.success('数据字典', '已成功更新了数据!');
            });
        }

        $scope.deleteRow = function (rows) {
            var ids;
            if (typeof rows == 'object') {
                ids = rows;
            } else {
                var entity = $scope.dataTable.data[rows];
                ids = [entity.Id];
            }
            $notify.confirm("数据字典", "您真的要删除选中数据吗？", function () {
                $api.DeleteDataDictionary(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.warning('数据字典', '已经成功删除，单击这里撤消删除。', function () {
                            $api.RestoreDataDictionary(ids, function (result) {
                                $scope.dataTable.refresh();
                                $notify.success("数据字典", "已经成功撤消删除。");
                            });
                        });
                        
                    }
                });
            });
        }

        $scope.moveUpRow = function (row) {
            var entity = $scope.dataTable.data[row];

            var ids = [entity.Id];

            $api.MoveUpDataDictionary(ids, function (result) {
                if (result.Success) {
                    $scope.dataTable.refresh();
                } else {
                    $notify.warning("数据字典", result.AllMessages);
                }
            });
        }

        $scope.moveDownRow = function (row) {
            var entity = $scope.dataTable.data[row];

            var ids = [entity.Id];

            $api.MoveDownDataDictionary(ids, function (result) {
                if (result.Success) {
                    $scope.dataTable.refresh();
                } else {
                    $notify.warning("数据字典", result.AllMessages);
                }
            });
        }

        $scope.editNextLevelRow = function (row) {
            var entity;
            if (typeof row == "object") {
                entity = row;
            }
            else {
                entity = $scope.dataTable.data[row];
            }
            $location.path('/settings/datadictionary/sublist').search({ ParentId: entity.Id });
        }

        initDataTable();
    }]);

