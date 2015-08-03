
MetronicApp.controller('ProductdefinitionEditCtrl', ['$scope', '$apiProducts', '$modalInstance', '$notify', 'entity',
    function ($scope, $api, $modalInstance, $notify, entity) {
        $scope.ProductDefinition = angular.copy(entity);     
      
        $scope.save = function () {
           
            $api.UpdateproductDefinition($scope.ProductDefinition, function (result) {

                if (result.Success) {
                    $modalInstance.close(result.Data);
                } else {
                    $notify.error("产品信息参数", result.AllMessages);
                }
            })
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);
