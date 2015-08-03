
MetronicApp.controller('CompanyInfoNewCtrl', ['$scope', '$apiBasic', '$modalInstance', '$notify',
    function ($scope, $api, $modalInstance, $notify) {
        $scope.CompanyInfo = { Name: '', NameEN: '', Abbreviation: '', Number: '', TypeCode: '', TypeName: '', BusinessLicense: '', BusinessTerm: '', TaxRegistrationNo: '', Founded: '', RegistrationCapital: '', BusinessScope: '', Telephone: '', Address: '', Fax: '', PostalCode: '', OrganizationCode: '', RegisteredAddress: '', CompanyEmail: '', Remarks: '', LegalName: '', LegalIDCard: '', LegalTel: '', LegalPostalCode: '', LegalAddress: '', LegalPhone: '', LegaiEmail: '', ContactName: '', ContactIDCard: '',ContactTel:'',ContactPostalCode:'', ContactAddress: '', ContactPhone: '', ContactEmail: '' };
        $scope.DataDictionary;
        $scope.Symbol = "@";
        $api.ChildListDataDictionary('1D54F794-16BB-470E-8014-C6F070E99F81', function (result) {
            $scope.DataDictionary = result.Data;
        });

        $scope.save = function () {
            $api.AddCompanyInfo($scope.CompanyInfo, function (result) {
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

