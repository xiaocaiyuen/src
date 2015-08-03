
MetronicApp.controller('BankInfoNewCtrl', ['$scope',"$apiBasic", '$apiSettings', '$modalInstance', '$notify',
    function ($scope,$apiBasic, $api, $modalInstance, $notify) {

        $scope.BankInfo = { Name: '', TypeId: '', ContactPerson: '', Telephone: '', PostalCode: '', Address: '' };
        $scope.SelectType;
        $apiBasic.GetDataDictionaryByCode('100300', function (result) {
            $scope.SelectType = result.Data;
        });
        $scope.save = function () {
            $api.AddBankInfo($scope.BankInfo, function (result) {
                if (result.Success) {
                    $modalInstance.close(result.Data);
                }
                else {
                    $notify.error("银行信息", result.AllMessages);
                }
            })
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

