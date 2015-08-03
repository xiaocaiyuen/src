
MetronicApp.controller('ProductDefinitionNewCtrl', ['$scope', '$apiProducts', '$apiSettings','$notify',
    function ($scope, $api,$apiSettings, $notify) {
        $scope.entity = null;
        $scope.SelectProductType = null;
        $scope.SelectVehicleType = null;
        $scope.SelectPaymentSumType = null;
        $scope.save = function () {
            console.log($scope.entity);
        };
        var init = function () {
            $api.ProductDefinitionInfo(function (result) {
                if (result.Success) {
                    $scope.entity = result.Data;
                }
                else {
                    $notify.error("获取对象出错");
                }
            });

            $apiSettings.ListByCode("100400", function (result) {
                if (result.Success) {
                    $scope.SelectProductType = result.Data;
                }
                else {
                    $notify.error(result.AllMessage);
                }
            });

            $apiSettings.ListByCode("100500", function (result) {
                if (result.Success) {
                    $scope.SelectVehicleType = result.Data;
                }
                else {
                    $notify.error(result.AllMessage);
                }
            });

            $apiSettings.ListByCode("100600", function (result) {
                if (result.Success) {
                    $scope.SelectPaymentSumType = result.Data;
                }
                else {
                    $notify.error(result.AllMessage);
                }
            });
        }

        init();
    }]);

