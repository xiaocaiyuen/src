MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $stateProvider

        // List
        .state('Settings_Announcement_List', {
            url: "/settings/announcement/list",
            templateUrl: "settings/announcement",
            data: { pageTitle: '通知公告' },
            controller: "AnnouncementListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/settings/announcement/index.js',
                            '/content/js/controllers/settings/announcement/add.js',
                            '/content/js/controllers/settings/announcement/edit.js',
                            '/content/js/controllers/settings/announcement/detail.js'
                        ]
                    });
                }]
            }
        });
}]);
