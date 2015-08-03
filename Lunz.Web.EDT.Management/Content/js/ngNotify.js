/*
插件列表
- https://github.com/CodeSeven/toastr
- http://bootboxjs.com/
*/
var apiModule = angular.module('ng.notify', []);

apiModule.factory('$notify', [
    function () {

        toastr.options = {
            "closeButton": false,
            "debug": false,
            "positionClass": "toast-bottom-right",
            "showDuration": "1000",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        var showMsg = function (action, title, message, callback) {
            toastr.options.onclick = callback;
            toastr[action](message, title);
        };

        return {
            success: function (title, message, callback) {
                showMsg('success', title, message, callback);
            },
            info: function (title, message, callback) {
                showMsg('info', title, message, callback);
            },
            warning: function (title, message, callback) {
                showMsg('warning', title, message, callback);
            },
            error: function (title, message, callback) {
                showMsg('error', title, message, callback);
            },
            startLoading: function (message) {
                if (($.browser && $.browser.msie == true && $.browser.version < 9) || message) {
                    Metronic.startPageLoading({ message: "正在加载..." });
                } else {
                    Metronic.startPageLoading({ animate: true });
                }
            },
            stopLoading: function () {
                Metronic.stopPageLoading();
                Metronic.unblockUI();
            },
            alert: function (title, message, ok) {
                bootbox.dialog({
                    message: message,
                    title: title,
                    buttons: {
                        success: {
                            label: "确定",
                            className: "btn-primary",
                            callback: ok
                        }
                    }
                });
            },
            confirm: function (title, message, confirm, cancel) {
                bootbox.dialog({
                    message: message,
                    title: title,
                    buttons: {
                        success: {
                            label: "确定",
                            className: "btn-primary",
                            callback: confirm
                        },
                        cancel: {
                            label: "取消",
                            className: "btn-warning",
                            callback: cancel
                        }
                    }
                });
            },
            dialog: bootbox.dialog
        };
    }]);