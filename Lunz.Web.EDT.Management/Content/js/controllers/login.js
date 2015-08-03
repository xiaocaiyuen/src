
MetronicApp.controller('LoginCtrl', ['$rootScope', '$scope', '$http', '$notify', 
    function ($rootScope, $scope, $http, $notify) {
        $scope.errors = [];
        $scope.submitting = false;

        $scope.returnUrl = "/";
        $scope.username = "";
        $scope.password = "";
        $scope.rememberMe = false;


        $scope.login = function () {
            $scope.errors = [];
            $scope.submitting = true;
            $notify.startLoading();

            if ($scope.username == "" || $scope.password == "") {
                $scope.errors[$scope.errors.length] = "请输入用户名和密码。";
            }

            if ($scope.errors.length == 0) {
                if ($scope.returnUrl == "") {
                    $scope.returnUrl = "/";
                }
                var params = {
                    username: $scope.username,
                    password: $scope.password,
                    rememberMe: $scope.rememberMe,
                    returnUrl: $scope.returnUrl
                };
                $http({
                    method: 'post',
                    url: 'login',
                    params: params,
                    cache: false
                }).success(function (result) {
                    $notify.stopLoading();
                    $scope.submitting = false;

                    if (result.Success) {
                        window.location.href = $scope.returnUrl;

                    } else {
                        $scope.errors.push(result.AllMessages);
                    }
                });

            } else {
                $notify.stopLoading();
                $scope.submitting = false;
            }

            return false;
        };

        $scope.init = function (returnUrl) {
            $scope.returnUrl = returnUrl;
        }
    }]);

