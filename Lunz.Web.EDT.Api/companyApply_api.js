var testApiModule = angular.module('api.companyApply', ['api.main']);

testApiModule.factory('$apiCompanyApply', ['$api', '$http', '$log', function ($api, $http, $log) {
    var action_companyapplyAdd = "apply/Add";
    return {
        CompanyApply: function (success, error) {
            $api.Get(action_companyapplyAdd, null, success, error);
        }
    };
}]);