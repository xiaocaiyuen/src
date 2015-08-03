
MetronicApp.controller('UpdateCompanyInfoEditCtrl', ['$scope', '$apiBasic', '$modalInstance', '$notify', 'entity',
    function ($scope, $api, $modalInstance, $notify, entity) {
        $scope.CompanyInfo = angular.copy(entity);
        $scope.save = function () {
            $api.UpdateCompanyInfo($scope.CompanyInfo, function (result) {
                if (result.Success) {
                    $modalInstance.close(result.Data);
                } else {
                    $notify.error("数据字典", result.AllMessages);
                }
            })
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

