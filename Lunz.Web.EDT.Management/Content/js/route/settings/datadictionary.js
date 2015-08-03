MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('DataDictionary_List', {
            url: "/settings/datadictionary/list",
            templateUrl: "datadictionary/index",
            data: { pageTitle: '数据字典' },
            controller: "dataDictionaryListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/settings/datadictionary/index.js',
                            '/content/js/controllers/settings/datadictionary/add.js',
                            '/content/js/controllers/settings/datadictionary/edit.js'
                        ]
                    });
                }]
            }
        })
        // SubList
        .state('DataDictionary_SubList', {
            url: "/settings/datadictionary/sublist",
            templateUrl: "datadictionary/sub",
            data: { pageTitle: '数据字典子节点' },
            controller: "dataDictionarySubListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/settings/datadictionary/subindex.js',
                            '/content/js/controllers/settings/datadictionary/subadd.js',
                            '/content/js/controllers/settings/datadictionary/subedit.js'
                        ]
                    });
                }]
            }
        });
}]);
