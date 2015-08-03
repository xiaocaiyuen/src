MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('Product_Personal_Apply', {
            url: "/product/personal/apply",
            templateUrl: "product/personal",
            data: { pageTitle: '申请表格-个人版' },
            controller: "PersonalApplyCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/personal/index.js'
                        ]
                    });
                }]
            }
        });
}]);
