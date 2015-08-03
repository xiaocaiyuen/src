MetronicApp.controller('PersonalApplyCtrl', ['$scope', '$ngDataTables', '$apiPersonal', '$modal', '$notify',
    function ($scope, $ngDataTables, $api, $modal, $notify) {
        $scope.deletedRows = [];
        $scope.dataTable;
        
        var initDataTable = function () {
            //alert("management层数据");
            $api.PersonalApply(function (result) {
                //alert(result.Data);
            });
        }
        initDataTable();
    }]);

