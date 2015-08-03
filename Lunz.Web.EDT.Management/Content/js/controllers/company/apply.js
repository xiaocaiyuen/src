MetronicApp.controller('CompanyApplyCtrl', ['$scope', '$ngDataTables', '$apiCompanyApply', '$modal', '$notify',
    function ($scope, $ngDataTables, $api, $modal, $notify) {
        $scope.deletedRows = [];
        $scope.dataTable;
        var initDataTable = function () {
            $api.CompanyApply(function (result) {
                //alert(result.Data);
            });
        }
        initDataTable();
    }]);
