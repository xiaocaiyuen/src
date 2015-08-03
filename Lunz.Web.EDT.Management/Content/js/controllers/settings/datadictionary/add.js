
MetronicApp.controller('DataDictionaryNewCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify',
    function ($scope, $api, $modalInstance, $notify) {
        $scope.DataDictionary = { Name: '', Code: '', StringValue: '', IntValue: '', FloatValue: '', BoolValue: '', DateTimeValue: '' };

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

