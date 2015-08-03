
MetronicApp.controller('BankInfoDetailCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify', 'entity',
    function ($scope, $api, $modalInstance, $notify, entity) {
        $scope.BankInfo = angular.copy(entity);
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

