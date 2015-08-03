
MetronicApp.controller('AnnouncementListCtrl', ['$scope', '$filter', '$ngDataTables', '$apiSettings', '$modal', '$notify',
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
                    { title: '标题', data: 'Topic' },
                    { title: '类型', data: 'Basic_DataDictionary.Name' },
                    { title: '开始时间', data: 'StartDate',
                        render: function (value) { return $filter('date')(value, 'yyyy-MM-dd HH:mm') }},
                    { title: '结束时间', data: 'EndDate',
                        render: function (value) { return $filter('date')(value, 'yyyy-MM-dd HH:mm') }}
                ],
                getData: function (paging, success) {
                    console.log(success);
                    $api.ListAnnouncement(paging, success);
                },
                search: {
                    width: 180,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("通知公告", "已经成功刷新数据。");
        }

        $scope.add = function () {
            $modal.open({
                templateUrl: '/settings/announcement/new',
                controller: 'AnnouncementNewCtrl'
            }).result.then(function (data) {
                $scope.dataTable.refresh(true);
                $notify.success('通知公告信息', '已成功新建了通知公告!');
            });
        }

        $scope.edit = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length == 1) {
                var entity = rows[0];
                $scope.editRow(entity);
            } else {
                $notify.info("通知公告信息", "请选择一条要编辑的数据。");
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
                templateUrl: '/settings/announcement/edit',
                controller: 'AnnouncementEditCtrl',
                resolve: {
                    entity: function () {
                        return entity;
                    }
                }
            }).result.then(function (data) {
                $scope.dataTable.refresh();
                $notify.success('通知公告信息', '已成功更新了数据!');
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
                $notify.info("通知公告信息", "请选择要删除的数据。");
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
            $notify.confirm("通知公告信息", "您真的要删除选中数据吗？", function () {
                $api.DeleteAnnouncement(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.success("通知公告信息", "已经成功删除了数据！");
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
                templateUrl: '/settings/announcement/detail',
                controller: 'AnnouncementDetailCtrl',
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

