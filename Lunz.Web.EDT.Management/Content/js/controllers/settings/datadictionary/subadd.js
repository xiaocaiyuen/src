
MetronicApp.controller('SubDataDictionaryNewCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify', '$location',
    function ($scope, $api, $modalInstance, $notify, $location) {
        $scope.DataDictionary = { Name: '', Code: '', StringValue: '', IntValue: '', FloatValue: '', BoolValue: '', DateTimeValue: '', ParentId: $location.search().ParentId };

        $scope.save = function () {
            $api.AddDataDictionary($scope.DataDictionary, function (result) {
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

