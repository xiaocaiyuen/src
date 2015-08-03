
MetronicApp.controller('productcontractprintListCtrl', ['$scope', '$ngDataTables', '$apiProducts', '$modal', '$notify', '$location',
    function ($scope, $ngDataTables, $api, $modal, $notify,$location) {
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

        //$scope.editRow = function () {
        //    $location.path("/products/ProductContractPrint/Print");
        //}

        $scope.print = function (row) {
            //var rows = $scope.dataTable.getSelectedData();
            var entity = entity = $scope.dataTable.data[row];
            var contractId = entity.Id;
            $location.path('/products/ProductContractPrint/Print/' + contractId);
        }

        initDataTable();
    }]);

