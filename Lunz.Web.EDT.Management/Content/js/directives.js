/***
GLobal Directives
***/

// Route State Load Spinner(used on page or content load)
MetronicApp.directive('ngSpinnerBar', ['$rootScope',
    function ($rootScope) {
        return {
            link: function (scope, element, attrs) {
                // by defult hide the spinner bar
                element.addClass('hide'); // hide spinner bar by default

                // display the spinner bar whenever the route changes(the content part started loading)
                $rootScope.$on('$stateChangeStart', function () {
                    element.removeClass('hide'); // show spinner bar
                });

                // hide the spinner bar on rounte change success(after the content loaded)
                $rootScope.$on('$stateChangeSuccess', function () {
                    element.addClass('hide'); // hide spinner bar
                    $('body').removeClass('page-on-load'); // remove page loading indicator
                    Layout.setSidebarMenuActiveLink('match'); // activate selected link in the sidebar menu

                    // auto scorll to page top
                    setTimeout(function () {
                        Metronic.scrollTop(); // scroll to the top on content load
                    }, $rootScope.settings.layout.pageAutoScrollOnLoad);
                });

                // handle errors
                $rootScope.$on('$stateNotFound', function () {
                    element.addClass('hide'); // hide spinner bar
                });

                // handle errors
                $rootScope.$on('$stateChangeError', function () {
                    element.addClass('hide'); // hide spinner bar
                });
            }
        };
    }
])

// Handle global LINK click
MetronicApp.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault(); // prevent link click for above criteria
                });
            }
        }
    };
});

// Handle Dropdown Hover Plugin Integration
MetronicApp.directive('dropdownMenuHover', function () {
    return {
        link: function (scope, elem) {
            elem.dropdownHover();
        }
    };
});

MetronicApp.directive('nav', function ($compile, $templateCache) {
    return {
        restrict: 'AE',
        replace: true,
        scope: {
            nodes: '=ngModel'
        },
        link: function (scope, elm, attrs) {
            var html = $templateCache.get(attrs.id);
            if (!html) {
                html = $("#" + attrs.id).html();
                $templateCache.put(attrs.id, html);
            }

            var parent = elm.parent();
            parent.append($compile(html)(scope));
            elm.remove();
        }
    };
})
    .directive('navNode', function ($compile, $templateCache) {
        return {
            restrict: 'AE',
            replace: false,
            link: function (scope, elm, attrs) {
                if (scope.node.Visible == false) {
                    return;
                }

                var html = $templateCache.get(attrs.id);
                if (!html) {
                    html = $("#" + attrs.id).html();
                    $templateCache.put(attrs.id, html);
                }
                var parent = elm.parent();
                var current = $compile(html)(scope);
                parent.append(current);

                var navChildren = [];
                for (var i = 0; i < scope.node.Children.length; i++) {
                    var child = scope.node.Children[i];
                    if (child.Visible) {
                        navChildren[navChildren.length] = child;
                    }
                }
                scope.NavChildren = navChildren;
                if (scope.node.Children.length > 0) {
                    for (var i = 0; i < scope.node.Children.length; i++) {
                        scope.node.Children[i].Parent = scope.node;
                    }
                }
                if (scope.NavChildren.length > 0) {
                    var link = $("a", current);
                    link.attr("href", "javascript:;");
                    var childNode = $compile('<ul class="sub-menu"><div nav ng-model="NavChildren" id="navTemplate"></div></ul>')(scope)
                    current.append(childNode);
                } else {
                    $("span.arrow", current).remove();
                }
            }
        };
    });

// Page Title
MetronicApp.directive('pageTitle', ['$rootScope', '$location',
function ($rootScope, $location) {
    var getCurrentPage = function (menuItems, ngUrl) {
        var result;

        for (var i = 0; i < menuItems.length; i++) {
            var menuItem = menuItems[i];
            if (menuItem.NgUrl == ngUrl) {
                result = menuItem;
                break;
            } else {
                result = getCurrentPage(menuItem.Children, ngUrl);
                if (result != null) {
                    //result.Parent = menuItem;
                    break;
                }
            }
        }

        return result;
    }

    return {
        restrict: 'A',
        replace: true,
        link: function (scope, elem, attrs) {
            //$api.GetMenuItemByNgUrl($location.path(), function (result) {
            //    $rootScope.CurrentPage = result.Data;
            //    elem.text(result.Data.Name);
            //});
            var currentPage = getCurrentPage($rootScope.MenuItems, $location.path());
            $rootScope.CurrentPage = currentPage;
            elem.text(currentPage.Name);
        }
    };
}
]);

//Page Breadcrumb
MetronicApp.directive('pageBreadcrumb', ['$compile',
function ($compile) {
    return {
        restrict: 'AE',
        replace: true,
        scope: {
            node: '=ngModel'
        },
        link: function (scope, elem, attrs) {
            var array = [];
            var node = scope.node;
            while (node != null) {
                array[array.length] = node;
                node = node.Parent;
            }
            for (var i = array.length - 1; i >= 0; i--) {
                var node = array[i];
                var li = $("<li/>");
                var url = node.NgUrl;
                if (url == "" || url == "#") {
                    url = "jsvascript:";
                } else {
                    url = "#" + url;
                }
                var link = $("<a/>").attr("href", url).text(node.Name);
                li.append(link);
                if (i > 0) {
                    li.append('<i class="fa fa-angle-right"></i>');
                }
                elem.parent().append(li);
            }
            elem.remove();
        }
    };
}
]);