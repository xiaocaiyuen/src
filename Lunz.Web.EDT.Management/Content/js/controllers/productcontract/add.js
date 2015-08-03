
MetronicApp.controller('ProductContractNewCtrl', ['$scope', '$apiProducts', '$modalInstance', '$notify',
    function ($scope, $api, $modalInstance, $notify) {
        
        $scope.ProductContract = { Name: '', TemplateHtml: '', SortOrder:0};
       
        $scope.save = function () {
            
            $api.AddProductContract($scope.ProductContract, function (result) {
              
                if (result.Success)
                {
                   $modalInstance.close(result.Data);

                }
                else
                {
                    $notify.error("模板信息", result.AllMessages);
                }
            })
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

