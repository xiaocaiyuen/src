angular.module('lunz.ng.filters', [])
    .filter('img', function () {
        return function (input, type) {
            if (input && input != null && input != ""
                && type && type != null && type != "") {

                var index = String(input).lastIndexOf(".");
                if (index > 0) {
                    return String(input).substring(0, index) + '_' + type + String(input).substring(index);
                }
            }
            return input;
        };
    });
