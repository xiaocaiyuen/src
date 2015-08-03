MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('productcontractprint_List', {
            url: "/products/contractprint/list",
            templateUrl: "/products/ProductContractPrint",
            data: { pageTitle: '合同模板信息' },
            controller: "productcontractprintListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/productcontractprint/index.js',
                            '/content/js/controllers/productcontractprint/print.js'
                        ]
                    });
                }]
            }
        }).state('productcontractprint_print', {
            url: "/products/ProductContractPrint/Print/{ContractId}",
            templateUrl: "/products/ProductContractPrint/Print",
            data: { pageTitle: '合同信息' },
            controller: "ProductContractPrintCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/productcontractprint/print.js'
                        ]
                    });
                }],
                params: function ($stateParams) {
                    return $stateParams;
                }
            }
        });
}]);
