
MetronicApp.controller('UpdateDataDictionaryEditCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify', 'entity',
    function ($scope, $api, $modalInstance, $notify, entity) {
        $scope.DataDictionary = angular.copy(entity);

        $scope.save = function () {
            $api.UpdateDataDictionary($scope.DataDictionary, function (result) {
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

