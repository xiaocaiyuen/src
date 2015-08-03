//var membershipApiModule = angular.module('api.membership', ['api.main']);

//membershipApiModule.factory('$apiMembership', ['$api', '$http', '$log', function ($api, $http, $log) {

//    var action_MenuItemsByUser = "membership/menuitemsbyuser";
//    var action_MenuItemByNgUrl = "membership/MenuItemByNgUrl";
//    var action_login = "membership/Login";
//    var action_ChangePassword = "membership/ChangePassword";

//    return {
//        GetMenuItemByUser: function (success, error) {
//            $api.Get(action_MenuItemsByUser, null, success, error);
//        },
//        GetMenuItemByNgUrl: function (ngUrl, success, error) {
//            $api.Get(action_MenuItemByNgUrl, {
//                ngUrl: ngUrl
//            }, success, error);
//        },
//        Login: function (username, password, remember, success, error) {
//            $api.Post(action_login, {
//                'username': username,
//                'password': password,
//                'remember': remember
//            }, success, error);
//        },
//        ChangePassword: function (oldPassword, newPassword, success, error) {
//            $api.Post(action_ChangePassword, {
//                'oldPassword': oldPassword,
//                'newPassword': newPassword
//            }, success, error);
//        },
//        ForgotPassword: function (email, success, error) { }

//    };
//}]);
var membershipApiModule = angular.module('api.membership', ['api.main']);

membershipApiModule.factory('$apiMembership', ['$api', '$http', '$log', function ($api, $http, $log) {

    var action_login = "Membership/Login";
    var action_GetUserInfo = "Membership/GetUserInfo";

    return {
        Login: function (username, password, remember, success, error) {
            console.log('this is method of api login');
            $api.Post(action_login, {
                'username': username,
                'password': password,
                'remember': remember
            }, success, error);
        },
        GetUserInfo: function (success, error) {
            $api.Get(action_GetUserInfo, null, success, error);
        }
    };
}]);