
MetronicApp.controller('BusinessCompanyCtrl', ['$scope', '$apiBusiness', '$modalInstance', '$notify',
    function ($scope, $api, $modalInstance, $notify) {
        $scope.CompanyInfo = { Name: '' };

        $scope.save = function () {
            $api.AddCompanyInfo($scope.CompanyInfo, function (result) {
                if (result.Success) {
                    $modalInstance.close(result.Data);
                } else {
                    $notify.error("出现问题！", result.AllMessages);
                }
            })
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

