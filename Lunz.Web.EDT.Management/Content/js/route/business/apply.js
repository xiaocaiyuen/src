MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('business_apply_List', {
            url: "/product/business/list",
            templateUrl: "business/apply/index",
            data: { pageTitle: '业务申请' },
            controller: "BusinessApplyListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/business/apply/index.js',
                            '/content/js/controllers/business/apply/calculator.js',
                            '/content/js/controllers/business/apply/addcompany.js',
                            '/content/js/controllers/business/apply/addpersonage.js'
                        ]
                    });
                }]
            }
        });
}]);
