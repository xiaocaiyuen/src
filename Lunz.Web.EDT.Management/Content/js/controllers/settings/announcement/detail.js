
MetronicApp.controller('AnnouncementDetailCtrl', ['$scope', '$apiSettings', '$modalInstance', '$notify', 'entity',
    function ($scope, $api, $modalInstance, $notify, entity) {
        $scope.Announcement = angular.copy(entity);
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);

