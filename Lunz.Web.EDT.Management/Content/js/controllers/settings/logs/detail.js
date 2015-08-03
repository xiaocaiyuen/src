
MetronicApp.controller('LogsViewCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify', 'entity',
    function ($scope, $api, $modalInstance, $notify, entity) {
        $scope.Logs = angular.copy(entity);
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);



