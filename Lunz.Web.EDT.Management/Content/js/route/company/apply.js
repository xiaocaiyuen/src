MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider
        // List
        .state('Company_Apply', {
            url: "/product/company/apply",
            templateUrl: "company/apply",
            data: { pageTitle: '企业汽车融资租赁申请表' },
            controller: "CompanyApplyCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/company/apply.js'
                        ]
                    });
                }]
            }
        });
}]);
