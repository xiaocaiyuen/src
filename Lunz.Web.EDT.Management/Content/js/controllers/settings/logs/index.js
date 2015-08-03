
MetronicApp.controller('LogsListCtrl', ['$scope','$filter', '$ngDataTables', '$apiSettings', '$modal', '$notify',
    function ($scope,$filter, $ngDataTables, $api, $modal, $notify) {
        $scope.deletedRows = [];
        $scope.dataTable;

        var initDataTable = function () {
            $scope.dataTable = $ngDataTables.init({
                src: "#dataTable",
                scope: $scope,
                selectable: false,
                searchable: true,
                columns: [
                    { title: '级别', data: 'LogLevel', width: $(this).width() * 0.1 },
                    { title: '操作文本', data: 'Message' },
                    {
                        title: '时间', data: 'LogTime', width: $(this).width() * 0.15,
                        render: function (value) { return $filter('date')(value, 'yyyy-MM-dd HH:mm:ss') }
                    }
                ],
                getData: function (paging, success) {
                    $api.ListLogs(paging, success);
                },
                search: {
                    width: 150,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("操作日志", "已经成功刷新数据。");
        }

        $scope.detail = function (row) {
            var entity;
            if (typeof row == "object") {
                entity = row;
            } else {
                entity = $scope.dataTable.data[row];
            }
            $modal.open({
                templateUrl: '/settings/logs/detail',
                controller: 'LogsViewCtrl',
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

