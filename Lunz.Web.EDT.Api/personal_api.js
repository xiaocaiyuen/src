var personalApiModule = angular.module('api.product', ['api.main']);

personalApiModule.factory('$apiPersonal', ['$api', '$http', '$log', function ($api, $http, $log) {
    var action_PersonalApply = "product/personal/apply";
    //111
    return {
        PersonalApply: function (success, error) {
            $api.Get(action_PersonalApply, null, success, error);
        }
    };
}]);