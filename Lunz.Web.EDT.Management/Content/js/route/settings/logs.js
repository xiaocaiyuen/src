MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('Settings_Logs_List', {
            url: "/settings/logs/list",
            templateUrl: "settings/logs",
            data: { pageTitle: '操作日志' },
            controller: "LogsListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/settings/logs/index.js',
                            '/content/js/controllers/settings/logs/detail.js'
                        ]
                    });
                }]
            }
        });
}]);
