MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('Settings_BankInfo_List', {
            url: "/settings/bankinfo/list",
            templateUrl: "settings/bankinfo",
            data: { pageTitle: '银行信息' },
            controller: "BankInfoListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/settings/bankinfo/index.js',
                            '/content/js/controllers/settings/bankinfo/add.js',
                            '/content/js/controllers/settings/bankinfo/edit.js',
                            '/content/js/controllers/settings/bankinfo/detail.js'
                        ]
                    });
                }]
            }
        });
}]);
