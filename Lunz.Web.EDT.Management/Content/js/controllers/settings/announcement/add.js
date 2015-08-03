
MetronicApp.controller('AnnouncementNewCtrl', ['$scope', '$apiBasic', '$apiSettings', '$modalInstance', '$notify',
    function ($scope, $apiBasic, $api, $modalInstance, $notify) {

        $scope.Announcement = { Topic: '', TypeId: '', StartDate: '', EndDate: '', Text: '' };
        $scope.SelectType;
        $apiBasic.GetDataDictionaryByCode('100100', function (result) {
            $scope.SelectType = result.Data;
        });

        $scope.save = function () {
            var files = $scope.files;
            if (files != "undefine" && files != null && files.length > 0) {
                var file = files[0];
                $apiBasic.AddResourceItem(file, function (data) {
                    if (data != null && data && data.Data != null && data.Data != "") {
                        var resourceId = data.Data;
                        $scope.Announcement.AttachmentResourceItemId = resourceId;
                        $api.AddAnnouncement($scope.Announcement, function (result) {
                            if (result.Success) {
                                $modalInstance.close(result.Data);
                            }
                            else {
                                $notify.error("通知公告信息", result.AllMessages);
                            }
                        })
                    }
                });
            } else {
                $api.AddAnnouncement($scope.Announcement, function (result) {
                    if (result.Success) {
                        $modalInstance.close(result.Data);
                    }
                    else {
                        $notify.error("通知公告信息", result.AllMessages);
                    }
                })
            }
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

