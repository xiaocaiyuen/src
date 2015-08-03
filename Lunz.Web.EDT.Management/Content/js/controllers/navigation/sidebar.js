/* Setup Layout Part - Sidebar */
MetronicApp.controller('SidebarController', ['$rootScope', '$scope', '$timeout', function ($rootScope, $scope, $timeout) {
    //$scope.MenuItems = [];
    $scope.$rootScope = $rootScope;

    $scope.$on('$includeContentLoaded', function () {
        $timeout(function () {
            Layout.initSidebar(); // init sidebar
        }, 100);
    });
}]);

