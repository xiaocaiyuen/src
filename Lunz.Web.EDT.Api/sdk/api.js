var apiModule = angular.module('api.main', ['angularFileUpload',
    'ngCookies']);

apiModule.factory('$api', ['$rootScope', '$http', '$log', '$upload', '$cookies',
    function ($rootScope, $http, $log, $upload, $cookies) {

    var baseUrl = '';
    var appKey = '';

    var handleGet = function (action, data, success, error) {
        handleAjax(action, 'GET', data, success, error);
    }

    var handlePost = function (action, data, success, error) {
        handleAjax(action, 'POST', data, success, error);
    }

    var handleDelete = function (action, data, success, error) {
        handleAjax(action, 'POST', data, success, error);
    }

    var handleFileUpload = function (action, file, success, error) {
        var authToken = $cookies.AUTHTOKEN;
        var authTokenParms = "";
        if (authToken && authToken != null && authToken != "") {
            authTokenParms = "&authToken=" + escape(authToken);

        }
        url = baseUrl + action + "?appKey=" + escape(appKey)+authTokenParms;
        $upload.upload({
            url: url,
            file: file,
        }).success(function (data) {
            if (success != null) {
                success(data);
            }
        })
        .error(function (error) {
            error(error);
        });
    }

    var handleAjax = function (action, type, data, success, error) {
        var authToken = $cookies.AUTHTOKEN;
        var authTokenParms = "";
        if (authToken && authToken != null && authToken != "") {
            authTokenParms = "&authToken=" + escape(authToken);
            
        }
        $.ajax({
            global: true,
            url: baseUrl + action + "?appKey=" + escape(appKey) + authTokenParms,
            data: data,
            type: type,
            dataType: 'json',
            success: function (result) {
                if (success != null) {
                    $rootScope.$apply(function () {
                        success(result);
                    });
                }
            },
            error: error
        });
    }

    //var call = function (action, data, success, error) {
    //    var params = data;
    //    if (params == null) {
    //        params = {};
    //    }
    //    params['callback'] = 'JSON_CALLBACK';

    //    $http({
    //        method: 'jsonp',
    //        url: baseUrl + action,
    //        params: params,
    //        cache: false
    //    }).success(success).error(error);
    //}

    return {
        Init: function (url, key) {
            jQuery.support.cors = true;

            baseUrl = url + "api/";
            appKey = key;
        },
        Get: handleGet,
        Post: handlePost,
        Delete: handleDelete,
        FileUpload: handleFileUpload
    };
}]);

