
MetronicApp.controller('BankInfoListCtrl', ['$scope', '$filter', '$ngDataTables', '$apiSettings', '$modal', '$notify',
    function ($scope, $filter, $ngDataTables, $api, $modal, $notify) {
        $scope.deletedRows = [];
        $scope.dataTable;

        var initDataTable = function () {
            $scope.dataTable = $ngDataTables.init({
                src: "#dataTable",
                scope: $scope,
                selectable: true,
                searchable: true,
                columns: [
                    { title: '开户行全称', data: 'Name' },
                    { title: '银行类型', data: 'Basic_DataDictionary.Name' },
                    { title: '联系人', data: 'ContactPerson' },
                    { title: '联系电话', data: 'Telephone' }
                ],
                getData: function (paging, success) {
                    console.log(success);
                    $api.ListBankInfo(paging, success);
                },
                search: {
                    width: 180,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("银行信息", "已经成功刷新数据。");
        }

        $scope.add = function () {
            $modal.open({
                templateUrl: '/settings/bankinfo/new',
                controller: 'BankInfoNewCtrl'
            }).result.then(function (data) {
                $scope.dataTable.refresh(true);
                $notify.success('银行信息', '已成功新建了银行信息!');
            });
        }

        $scope.edit = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length == 1) {
                var entity = rows[0];
                $scope.editRow(entity);
            } else {
                $notify.info("银行信息", "请选择一条要编辑的数据。");
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
                templateUrl: '/settings/bankinfo/edit',
                controller: 'BankInfoEditCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
            }).result.then(function (data) {
                $scope.dataTable.refresh();
                $notify.success('银行信息', '已成功更新了数据!');
            });
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
                $notify.info("银行信息", "请选择要删除的数据。");
            }
        }

        $scope.deleteRow = function (rows) {
            var ids;
            if (typeof rows == 'object') {
                ids = rows;
            } else {
                var entity = $scope.dataTable.data[rows];
                ids = [entity.Id];
            }
            $notify.confirm("银行信息", "您真的要删除选中数据吗？", function () {
                $api.DeleteBankInfo(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.success("银行信息", "已经成功删除了数据！");
                    }
                });
            });
        }

        $scope.detail = function (row) {
            var entity;
            if (typeof row == "object") {
                entity = row;
            } else {
                entity = $scope.dataTable.data[row];
            }
            $modal.open({
                templateUrl: '/settings/bankinfo/detail',
                controller: 'BankInfoDetailCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
            }).result.then(function (data) {
            });
        }

        initDataTable();
    }]);

