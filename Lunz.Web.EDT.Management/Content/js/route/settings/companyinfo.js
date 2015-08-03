MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('settings_CompanyInfo_List', {
            url: "/settings/companyInfo/list",
            templateUrl: "settings/companyinfo/index",
            data: { pageTitle: '公司信息' },
            controller: "CompanyInfoListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/settings/companyinfo/index.js',
                            '/content/js/controllers/settings/companyinfo/add.js',
                            '/content/js/controllers/settings/companyinfo/edit.js'
                        ]
                    });
                }]
            }
        });
}]);
