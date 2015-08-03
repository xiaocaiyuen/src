MetronicApp.controller('PublicMemberUserEditCtrl', ['$scope', '$apiMembership', '$modalInstance', '$notify', '$ngDataTables', 'entity', 'isDetail',
    function ($scope, $api, $modalInstance, $notify,$ngDataTables, entity, isDetail) {
        $scope.MembershipUser = angular.copy(entity);
        
        $scope.isDetail = isDetail;//为true，表示查看 （禁用编辑界面的文本框）
        $scope.save = function () {
            console.log($scope.MembershipUser.DisplayName + "|||||" + $scope.MembershipUser.Username);
            $api.UpdateUser($scope.MembershipUser, function (result) {
                if (result.Success) {
                    $modalInstance.close(result.Data);
                } else {
                    $notify.error("会员信息", result.AllMessages);
                }
            })        
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        //验证邮箱
        $scope.validEmail = function () {
            $scope.isEmailInValid = false;
            $scope.isEmailExist = false;

            if ($scope.MembershipUser.Email != undefined) {
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

        //验证手机号
        $scope.validPhonenumber = function () {
            $scope.isPhonenumberInValid = false;
            $scope.isPhonenumberExist = false;

            if ($scope.MembershipUser.PhoneNumber != undefined) {
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


        $scope.vehicleList = [];
        //会员汽车
        $scope.oneUserVehicle = function () {
            $api.ListOneUserVehicle($scope.MembershipUser.Id,function(result){
                if (result.Success) {
                    $scope.vehicleList = result.Data;

                    for (var i = 0; i < $scope.vehicleList.length; ++i)
                    {
                        var t = new Date($scope.vehicleList[i].PurchasedAt);
                        $scope.vehicleList[i].PurchasedAt = t.getFullYear() + "-" + (t.getMonth() + 1) + "-" + t.getDate();
                    }
                }
                else {
                    console.log(result.AllMessages);
                }
            })
        }
        
        //会员优惠券
        $scope.dataTableCoupon;
        $scope.initDataTableCoupon = function () {
            $scope.dataTableCoupon = $ngDataTables.init({
                src: "#dataTableCoupon",
                scope: $scope,
                selectable: false,
                searchable: false,                
                columns: [
                    { title: '标题', data: 'CouponInfo.Title' },
                    { title: '编号', data: 'MCoupon.Code' },
                    { title: '金额/元', data: 'MCoupon.Money' },
                    { title: '截止日期', data: 'ExpireTimeFormat' },
                    { title: '状态', data: 'UsedDesc' }
                ],
                getData: function (paging, success) {
                    $api.ListOneUserCoupon($scope.MembershipUser.Id,paging, success);
                },
                search: {
                    width: 250,
                    actionTemplate: "div.actions"
                }
            });
        }

    }]);

