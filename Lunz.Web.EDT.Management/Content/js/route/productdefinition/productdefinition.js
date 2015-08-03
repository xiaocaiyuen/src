MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('productdefinition_List', {
            url: "/products/productdefinition/list",
            templateUrl: "/products/ProductDefinition/Index",
            data: { pageTitle: '产品定义信息' },
            controller: "ProductdefinitionListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/productdefinition/index.js',
                            '/content/js/controllers/productdefinition/add.js',
                            '/content/js/controllers/productdefinition/edit.js'
                        ]
                    });
                }]
            }
        }).state('productcontract_Add', {
            url: "/products/productdefinition/add",
            templateUrl: "/products/ProductDefinition/new",
            data: { pageTitle: '产品定义信息' },
            controller: "ProductDefinitionNewCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/productdefinition/add.js'
                        ]
                    });
                }]
            }
        });
}]);
