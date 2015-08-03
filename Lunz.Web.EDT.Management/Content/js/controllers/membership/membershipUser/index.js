MetronicApp.controller('PublicMemberUserListCtrl', ['$scope', '$ngDataTables', '$apiMembership', '$modal', '$notify',
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
                    { title: '用户名', data: 'User.Username' },
                    { title: '显示名', data: 'User.DisplayName' },
                    { title: '邮箱', data: 'User.Email' },
                    { title: '手机', data: 'User.PhoneNumber' },
                    //{ title: '创建时间', data: 'CreatedAt' },
                    { title: '状态', data: 'ActivedDesc' }
                ],
                getData: function (paging, success) {
                    $api.ListPublicMemberUser(paging, success);
                },
                search: {
                    width: 250,
                    actionTemplate: "div.actions"
                }
            });
        }

        $scope.refresh = function () {
            $scope.dataTable.refresh(true);
            $notify.success("会员信息", "已经成功刷新数据。");
        }

        $scope.add = function () {
            $modal.open({
                templateUrl: '/Membership/Membership/New',
                controller: 'PublicMemberUserNewCtrl'
            }).result.then(function (data) {
                $scope.dataTable.addData(data);
                $notify.success('会员信息', '已成功新建了数据!');
            });
        }

        //编辑
        $scope.edit = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length == 1) {
                var entity = rows[0];
                $scope.editRow(entity);
            } else {
                $notify.info("会员信息", "请选择一条要编辑的会员。");
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
                templateUrl: '/Membership/Membership/Edit',
                controller: 'PublicMemberUserEditCtrl',
                resolve: {
                    entity: function () {
                        return entity.User;
                    },
                    isDetail: function () {
                        return false;
                    }
                }
            }).result.then(function (data) {
                $scope.dataTable.refresh();
                $notify.success('会员信息', '已成功更新了数据!');
            });
        }

        //进入查看界面
        $scope.detailRow = function (row) {
            var entity;
            if (typeof row == "object") {
                entity = row;
            } else {
                entity = $scope.dataTable.data[row];                
            }

            $modal.open({
                templateUrl: '/Membership/Membership/Edit',
                controller: 'PublicMemberUserEditCtrl',
                resolve: {
                    entity: function () {
                        return entity.User;
                    },
                    isDetail: function () {
                        return true;
                    }
                }
            }).result.then(function (data) {
            });
        }

        //启用账号
        $scope.activeUser = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length > 0) {
                var usernames = [];

                angular.forEach(rows, function (item, i) {
                    usernames[usernames.length] = item.User.Username;
                });
                $scope.activeUserRow(usernames);

            }
            else {
                $notify.info("会员信息", "请选择要启用的账号");
            }
        }
        $scope.activeUserRow = function (rows) {
            var ids;
            if (typeof rows == 'object') {
                ids = rows;
            } else {
                var entity = $scope.dataTable.data[rows];
                ids = [entity.User.Username];
            }
            $notify.confirm("会员信息", "您真的要启用该用户吗？", function () {
                $api.ActiveUser(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.success('会员信息', '帐号已启用!');
                    }
                });
            });
        }

        //禁用账号
        $scope.disactiveUser = function () {
            var rows = $scope.dataTable.getSelectedData();
            if (rows.length > 0) {
                var usernames = [];

                angular.forEach(rows, function (item, i) {
                    usernames[usernames.length] = item.User.Username;
                });
                $scope.disactiveUserRow(usernames);

            }
            else {
                $notify.info("会员信息", "请选择要禁用的帐号");
            }
        }
        $scope.disactiveUserRow = function (rows) {
            var ids;
            if (typeof rows == 'object') {
                ids = rows;
            } else {
                var entity = $scope.dataTable.data[rows];
                ids = [entity.User.Username];
            }
            $notify.confirm("会员信息", "您真的要禁用该用户吗？", function () {
                $api.DisactiveUser(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.success('会员信息', '帐号已禁用!');
                    }
                });
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
                $notify.info("汽车图片分类", "请选择要删除的数据。");
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
            $notify.confirm("汽车图片分类", "您真的要删除选中数据吗？", function () {
                $api.DeletePhotoCategory(ids, function (result) {
                    if (result.Success) {
                        $scope.dataTable.refresh();
                        $notify.warning('汽车图片分类', '已经成功删除，单击这里撤消删除。', function () {
                            $api.RestorePhotoCategory(ids, function (result) {
                                $scope.dataTable.refresh();
                                $notify.success("汽车图片分类", "已经成功撤消删除。");
                            });
                        });
                    }
                });
            });
        }

        $scope.moveUpRow = function (row) {
            var entity = $scope.dataTable.data[row];

            var ids = [entity.Id];

            $api.MoveUpPhotoCategory(ids, function (result) {
                if (result.Success) {
                    $scope.dataTable.refresh();
                } else {
                    $notify.warning("汽车图片分类", result.AllMessages);
                }
            });
        }

        $scope.moveDownRow = function (row) {
            var entity = $scope.dataTable.data[row];

            var ids = [entity.Id];

            $api.MoveDownPhotoCategory(ids, function (result) {
                if (result.Success) {
                    $scope.dataTable.refresh();
                } else {
                    $notify.warning("汽车图片分类", result.AllMessages);
                }
            });
        }

        initDataTable();
    }]);

