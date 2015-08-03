MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $stateProvider
        // List
        .state('MembershipUser_List', {
            url: "/Membership/Membership/ListPublicUser",//API control
            templateUrl: "Membership/Membership",//MVC control
            data: { pageTitle: '会员' },
            controller: "PublicMemberUserListCtrl",
            resolve: {
                deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                    return $ocLazyLoad.load({
                        name: 'MetronicApp',
                        files: [
                            '/content/js/controllers/membership/membershipUser/index.js',
                            '/content/js/controllers/membership/membershipUser/add.js',
                            '/content/js/controllers/membership/membershipUser/edit.js'
                        ]
                    });
                }]
            }
        });
}]);
