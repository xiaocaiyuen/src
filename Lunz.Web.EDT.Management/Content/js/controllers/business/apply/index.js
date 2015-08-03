
MetronicApp.controller('BusinessApplyListCtrl', ['$scope', '$ngDataTables', '$apiBusiness', '$modal', '$notify',
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
                    { title: '申请状态', data: '' },
                    { title: '经销商', data: '' },
                    { title: '项目编号', data: '' },
                    { title: '车辆类型', data: '' },
                    { title: '车型车系', data: '' },
                    { title: '零售价', data: '' },
                    { title: '每期租金', data: '' },
                    { title: '融资金额', data: '' },
                    { title: '金融专员', data: '' },
                    { title: '申请时间', data: '' }
                ],
                getData: function (paging, success) {
                    $api.ListCompanyInfo(paging, success);
                },
                search: {
                    width: 228,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("业务申请", "已经成功刷新数据。");
        }

        $scope.Calculator = function () {
            $modal.open({
                templateUrl: 'business/apply/calculator',
                controller: 'BusinessCalculatorCtrl'
            });
        }

        $scope.addPersonage = function () {
            $modal.open({
                templateUrl: 'business/apply/personagenew',
                controller: 'BusinessPersonageCtrl'
            }).result.then(function (data) {
                $scope.dataTable.addData(data);
                $notify.success('业务申请>>个人', '已成功新建了数据!');
            });
        }

        $scope.addCompany = function () {
            $modal.open({
                templateUrl: 'business/apply/companynew',
                controller: 'BusinessCompanyCtrl'
            }).result.then(function (data) {
                $scope.dataTable.addData(data);
                $notify.success('业务申请>>企业', '已成功新建了数据!');
            });
        }

        $scope.edit = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length == 1) {
                var entity = rows[0];
                $scope.editRow(entity);
            } else {
                $notify.info("业务申请", "请选择一条要编辑的数据。");
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
                $notify.info("业务申请", "请选择要删除的数据。");
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
                templateUrl: 'Settings/CompanyInfo/edit',
                controller: 'UpdateCompanyInfoEditCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
            }).result.then(function (data) {
                $scope.dataTable.refresh();
                $notify.success('业务申请', '已成功更新了数据!');
            });
        }

        $scope.detailRow = function (row) {
            var entity;
            if (typeof row == "object") {
                entity = row;
            } else {
                entity = $scope.dataTable.data[row];
            }

            $modal.open({
                templateUrl: 'Settings/CompanyInfo/detail',
                controller: 'UpdateCompanyInfoEditCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
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
            $notify.confirm("业务申请", "您真的要删除选中数据吗？", function () {
                $api.DeleteCompanyInfo(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.warning('数据字典', '已经成功删除，单击这里撤消删除。', function () {
                            $api.RestoreCompanyInfo(ids, function (result) {
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

            $api.MoveUpCompanyInfo(ids, function (result) {
                if (result.Success) {
                    $scope.dataTable.refresh();
                } else {
                    $notify.warning("业务申请", result.AllMessages);
                }
            });
        }

        $scope.moveDownRow = function (row) {
            var entity = $scope.dataTable.data[row];

            var ids = [entity.Id];

            $api.MoveDownCompanyInfo(ids, function (result) {
                if (result.Success) {
                    $scope.dataTable.refresh();
                } else {
                    $notify.warning("业务申请", result.AllMessages);
                }
            });
        }
        initDataTable();
    }]);

