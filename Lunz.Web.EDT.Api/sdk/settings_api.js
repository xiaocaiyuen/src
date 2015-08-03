var settingsApiModule = angular.module('api.settings', ['api.main']);

settingsApiModule.factory('$apiSettings', ['$api', '$http', '$log', function ($api, $http, $log) {
    // DataDictionary
    var action_datadictionaryList = "DataDictionary/List";
    var action_datadictionarySubList = "DataDictionary/SubList";
    var action_datadictionaryAdd = "DataDictionary/Add";
    var action_datadictionaryDelete = "DataDictionary/Delete";
    var action_datadictionaryUpdate = "DataDictionary/Update";
    var action_datadictionaryRestore = "DataDictionary/Restore";
    var action_datadictionaryMoveUp = "DataDictionary/MoveUp";
    var action_datadictionaryMoveDown = "DataDictionary/MoveDown";
    var action_datadictionaryGetListByCode = "DataDictionary/ChildCodeList";
    // Logs
    var action_LogsList = "/settings/logs/list";
    // Announcement
    var action_AnnouncementList = "/settings/announcement/list";
    var action_AnnouncementAdd = "/settings/announcement/add";
    var action_AnnouncementUpdate = "/settings/announcement/update";
    var action_AnnouncementDelete = "/settings/announcement/delete";
    // BankInfo
    var action_BankInfoList = "/settings/bankinfo/list";
    var action_BankInfoAdd = "/settings/bankinfo/add";
    var action_BankInfoUpdate = "/settings/bankinfo/update";
    var action_BankInfoDelete = "/settings/bankinfo/delete";

    return {
        ListDataDictionary: function (paging, success, error) {
            $api.Get(action_datadictionaryList, paging, success, error);
        },
        SubListDataDictionary: function (paging, ParentId, success, error) {
            $api.Get(action_datadictionarySubList, { 'paging': paging, 'ParentId': ParentId }, success, error);
        },
        AddDataDictionary: function (entity, success, error) {
            $api.Post(action_datadictionaryAdd, entity, success, error);
        },
        UpdateDataDictionary: function (entity, success, error) {
            $api.Post(action_datadictionaryUpdate, entity, success, error);
        },
        DeleteDataDictionary: function (ids, success, error) {
            $api.Delete(action_datadictionaryDelete, { '': ids }, success, error);
        },
        RestoreDataDictionary: function (ids, success, error) {
            $api.Post(action_datadictionaryRestore, { '': ids }, success, error);
        },
        MoveUpDataDictionary: function (ids, success, error) {
            $api.Post(action_datadictionaryMoveUp, { '': ids }, success, error);
        },
        MoveDownDataDictionary: function (ids, success, error) {
            $api.Post(action_datadictionaryMoveDown, { '': ids }, success, error);
        },
        ListByCode: function (code, success, error) {
            $api.Get(action_datadictionaryGetListByCode, { 'Code': code }, success, error);
        },
        // Logs
        ListLogs: function (paging, success, error) {
            $api.Get(action_LogsList, paging, success, error);
        },
        // Announcement
        ListAnnouncement: function (paging, success, error) {
            $api.Get(action_AnnouncementList, paging, success, error);
        },
        AddAnnouncement: function (entity, success, error) {
            $api.Post(action_AnnouncementAdd, entity, success, error);
        },
        UpdateAnnouncement: function (entity, success, error) {
            $api.Post(action_AnnouncementUpdate, entity, success, error);
        },
        DeleteAnnouncement: function (ids, success, error) {
            $api.Post(action_AnnouncementDelete, { '': ids }, success, error);
        },
        // BankInfo
        ListBankInfo: function (paging, success, error) {
            $api.Get(action_BankInfoList, paging, success, error);
        },
        AddBankInfo: function (entity, success, error) {
            $api.Post(action_BankInfoAdd, entity, success, error);
        },
        UpdateBankInfo: function (entity, success, error) {
            $api.Post(action_BankInfoUpdate, entity, success, error);
        },
        DeleteBankInfo: function (ids, success, error) {
            $api.Post(action_BankInfoDelete, { '': ids }, success, error);
        }
    };
}]);