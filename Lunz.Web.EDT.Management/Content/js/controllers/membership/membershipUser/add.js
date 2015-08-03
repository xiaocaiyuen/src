MetronicApp.controller('PublicMemberUserNewCtrl', ['$scope', '$apiMembership', '$modalInstance', '$notify',
        function ($scope, $api, $modalInstance, $notify) {
            $scope.MembershipUser = { DisplayName: '', Username: '', Email: '', PhoneNumber: '', MemberType: 2, Password: '' };

            //验证用户名
            $scope.validUsername = function () {
                $scope.isUserNameExist = false;
                $api.IsUserNameExist($scope.MembershipUser.Username, function (result) {
                    if (!result.Success) {//用户名已经存在
                        $scope.isUserNameExist = true;
                        console.log(result.AllMessages);
                    }
                })
            };

            //验证邮箱
            $scope.validEmail = function () {
                $scope.isEmailInValid = false;
                $scope.isEmailExist = false;

                if ($scope.MembershipUser.Email != undefined)
                {
                    var regex = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+/;
                    if (!regex.test($scope.MembershipUser.Email)) {
                        $scope.isEmailInValid = true;
                        return;
                    }

                    $api.IsEmailExist($scope.MembershipUser.Email, function (result) {
                        if (!result.Success) {//邮箱已经存在
                            $scope.isEmailExist = true;
                            console.log(result.AllMessages);
                        }
                    });

                }
            }

            //验证密码
            $scope.validPassword = function (pwdIndex)
            {
                $scope.isPwd0Different = false;
                $scope.isPwd1Different = false;
                if ($scope.pwd1 != undefined && $scope.MembershipUser.Password != undefined)
                {
                    if ($scope.pwd1 != $scope.MembershipUser.Password)
                    {
                        if (pwdIndex == 0) {
                            $scope.isPwd0Different = true;
                            return;
                        }
                        if (pwdIndex == 1) {
                            $scope.isPwd1Different = true;
                            return;
                        }                        
                    }
                }
            }

            //验证手机号
            $scope.validPhonenumber = function ()
            {
                $scope.isPhonenumberInValid = false;
                $scope.isPhonenumberExist = false;

                if ($scope.MembershipUser.PhoneNumber != undefined)
                {
                    var regex = /^(((13[0-9]{1})|147|150|151|152|153|155|156|157|158|159|176|177|178|180|182|183|185|186|187|188|189)+\d{8})$/;
                    if (!regex.test($scope.MembershipUser.PhoneNumber)) {
                        $scope.isPhonenumberInValid = true;
                        return;
                    }

                    $api.IsPhoneNumberExist($scope.MembershipUser.PhoneNumber, function (result) {
                        if (!result.Success) {//手机号码已存在
                            $scope.isPhonenumberExist = true;
                            console.log(result.AllMessages);
                        }
                    });
                }
            }

            //新建保存
            $scope.save = function () {
                //验证
                if ($scope.pwd1.length <= 6) {
                    $notify.error("会员新增", "密码长度不能小于6");
                    return;
                }

                $api.Register($scope.MembershipUser, function (result) {
                    console.log('add result:' + result.Success);
                    if (result.Success) {
                        $modalInstance.close(result.Data);
                    } else {
                        $notify.error("会员信息", result.AllMessages);
                    }
                })
            };

            //新建取消
            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            };

        }]);

