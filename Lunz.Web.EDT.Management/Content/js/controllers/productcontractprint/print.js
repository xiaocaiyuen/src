
MetronicApp.controller('ProductContractPrintCtrl', ['$scope', '$apiProducts', '$notify', 'params',
    function ($scope, $api,$notify, $params) {
        var contractId = $params.ContractId;

        $api.GetProductContractById(contractId, function (result) {
            $scope.text = result.Data.TemplateHtml;
        });

    }]);

