var basicApiModule = angular.module('api.business', ['api.main']);

basicApiModule.factory('$apiBusiness', ['$api', '$http', '$log', function ($api, $http, $log) {
    // BusinessApply
    var action_ApplyList = "Apply/List";
    var action_ApplyAdd = "Apply/Add";
    var action_ApplyDelete = "Apply/Delete";
    var action_ApplyUpdate = "Apply/Update";
    var action_ApplyRestore = "Apply/Restore";
    var action_ApplyMoveUp = "Apply/MoveUp";
    var action_ApplyMoveDown = "Apply/MoveDown";

    return {
        ListApply: function (paging, success, error) {
            $api.Get(action_ApplyList, paging, success, error);
        },
        AddApply: function (entity, success, error) {
            $api.Post(action_ApplyAdd, entity, success, error);
        },
        UpdateApply: function (entity, success, error) {
            $api.Post(action_ApplyUpdate, entity, success, error);
        },
        DeleteApply: function (ids, success, error) {
            $api.Delete(action_ApplyDelete, { '': ids }, success, error);
        },
        RestoreApply: function (ids, success, error) {
            $api.Post(action_ApplyRestore, { '': ids }, success, error);
        },
        MoveUpApply: function (ids, success, error) {
            $api.Post(action_ApplyMoveUp, { '': ids }, success, error);
        },
        MoveDownApply: function (ids, success, error) {
            $api.Post(action_ApplyMoveDown, { '': ids }, success, error);
        },
    };
}]);