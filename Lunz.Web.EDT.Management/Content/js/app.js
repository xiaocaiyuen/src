/***
Metronic AngularJS App Main Script
***/

/* Metronic App */
var MetronicApp = angular.module("MetronicApp", [
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ngSanitize",
    'ng.datatables',
    'ng.notify',
    'api.main',
    'api.basic',
    //'api.membership',
    'ui.tree',
    'api.products',
    'summernote',
    'ang-drag-drop',
    'api.settings',
    'api.business'
    //'lunz.ng.filters'
]);

/* Configure ocLazyLoader(refer: https://github.com/ocombe/ocLazyLoad) */
MetronicApp.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        // global configs go here
    });
}]);

/* Setup global settings */
MetronicApp.factory('settings', ['$rootScope', function ($rootScope) {
    // supported languages
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        layoutImgPath: Metronic.getAssetsPath() + 'admin/layout/img/',
        layoutCssPath: Metronic.getAssetsPath() + 'admin/layout/css/'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* Setup App Main Controller */
MetronicApp.controller('AppController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        Metronic.initComponents(); // init core components
        //Layout.init(); //  Init entire layout(header, footer, sidebar, etc) on page load if the partials included in server side instead of loading with ng-include directive 
    });
}]);

/***
Layout Partials.
By default the partials are loaded through AngularJS ng-include directive. In case they loaded in server side(e.g: PHP include function) then below partial 
initialization can be disabled and Layout.init() should be called on page load complete as explained above.
***/

/* Setup Layout Part - Header */
MetronicApp.controller('HeaderController', ['$rootScope', '$scope', '$http', function ($rootScope,$scope, $http) {
    $scope.User = null;

    $scope.$on('$includeContentLoaded', function () {
        Layout.initHeader(); // init header

        $http.get("/login/getuser").success(function (result) {
            if (result.Success) {
                $scope.User = result.Data.User;
                $rootScope.User = result.Data.User;
                $rootScope.MenuItems = result.Data.MenuItems;
            }
        });
    });
}]);

///* Setup Layout Part - Sidebar */
//MetronicApp.controller('SidebarController', ['$scope', function($scope) {
//    $scope.$on('$includeContentLoaded', function() {
//        Layout.initSidebar(); // init sidebar
//    });
//}]);

/* Setup Layout Part - Quick Sidebar */
MetronicApp.controller('QuickSidebarController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        setTimeout(function () {
            QuickSidebar.init(); // init quick sidebar        
        }, 2000)
    });
}]);

/* Setup Layout Part - Theme Panel */
MetronicApp.controller('ThemePanelController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Demo.init(); // init theme panel
    });
}]);

/* Setup Layout Part - Footer */
MetronicApp.controller('FooterController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initFooter(); // init footer
    });
}]);

/* Init global settings and run the app */
MetronicApp.run(["$rootScope", "settings", "$state", '$notify', '$api',
    function ($rootScope, settings, $state, $notify, $api) {

        $api.Init(apiBaseUrl, appKey);

        $rootScope.$state = $state; // state to be accessed from view

        $.ajaxSetup({
            global: true,
            beforeSend: function () {
                $notify.startLoading();
            },
            complete: function () {
                $notify.stopLoading();
            }
        });

        bootbox.setDefaults({
            locale: 'zh_CN'
        });
    }]);