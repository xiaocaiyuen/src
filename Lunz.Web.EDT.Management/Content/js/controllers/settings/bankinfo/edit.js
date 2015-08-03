
MetronicApp.controller('BankInfoEditCtrl', ['$scope',"$apiBasic", '$apiSettings', '$modalInstance', '$notify', 'entity',
    function ($scope, $apiBasic,$api, $modalInstance, $notify, entity) {
        $scope.BankInfo = angular.copy(entity);
        $scope.SelectType;
        $apiBasic.GetDataDictionaryByCode('100300', function (result) {
            $scope.SelectType = result.Data;
        });
        $scope.save = function () {
            $api.UpdateBankInfo($scope.BankInfo, function (result) {

                if (result.Success) {
                    $modalInstance.close(result.Data);
                } else {
                    $notify.error("银行信息", result.AllMessages);
                }
            })
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);
