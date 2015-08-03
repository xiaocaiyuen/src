
MetronicApp.controller('productcontractListCtrl', ['$scope', '$ngDataTables', '$apiProducts', '$modal', '$notify',
    function ($scope, $ngDataTables, $api, $modal, $notify) {
        $scope.deletedRows = [];
        $scope.dataTable;

        var initDataTable = function () {
            $scope.dataTable = $ngDataTables.init({
                src: "#dataTable",
                scope: $scope,
                selectable: true,
                searchable: true,
                columns: [
                    { title: '合同名称', data: 'Name' }
                ],
                getData: function (paging, success) {
                    $api.productContract(paging, success);
                },
                search: {
                    width: 150,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("合同模板信息", "已经成功刷新数据。");
        }

        $scope.add = function () {
            $modal.open({
                templateUrl: '/products/productcontract/new',
                controller: 'ProductContractNewCtrl'
            }).result.then(function (data) {
                //$scope.gridOptions.data.push(data);
                $scope.dataTable.refresh(true);
                $notify.success('合同模板信息', '已成功新建了合同!');
            });
        }

        $scope.edit = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length == 1) {
                var entity = rows[0];
                $scope.editRow(entity);
            } else {
                $notify.info("合同模板信息", "请选择一条要编辑的数据。");
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
                $notify.info("合同模板信息", "请选择要删除的数据。");
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
                templateUrl: '/products/productcontract/edit',
                controller: 'ProductdeContractEditCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
            }).result.then(function (data) {

                $scope.dataTable.refresh();
                $notify.success('合同模板信息', '已成功更新了数据!');
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
            $notify.confirm("合同模板信息", "您真的要删除选中数据吗？", function () {
                $api.DeleteProductContract(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.success("合同模板信息", "已经成功删除了数据！");
                        //$notify.warning('车辆信息', '已经成功删除，单击这里撤消删除。', function () {
                        //    $api.RestoreVehicle(ids, function (result) {
                        //        $scope.dataTable.refresh();
                        //        $notify.success("车辆信息", "已经成功撤消删除。");
                        //    });
                        //});
                    }
                });
            });
        }



        initDataTable();
    }]);

