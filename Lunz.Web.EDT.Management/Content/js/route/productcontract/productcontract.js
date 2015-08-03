MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('productcontract_List', {
            url: "/products/productcontract/list",
            templateUrl: "/products/productcontract",
            data: { pageTitle: '合同模板信息' },
            controller: "productcontractListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/productcontract/index.js',
                            '/content/js/controllers/productcontract/add.js',
                            '/content/js/controllers/productcontract/edit.js'
                        ]
                    });
                }]
            }
        })
}]);
