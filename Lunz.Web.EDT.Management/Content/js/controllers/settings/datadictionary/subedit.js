
MetronicApp.controller('SubUpdateDataDictionaryEditCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify', 'entity', '$location',
    function ($scope, $api, $modalInstance, $notify, entity, $location) {
        entity.ParentId = $location.search().ParentId;
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

