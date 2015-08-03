var basicApiModule = angular.module('api.basic', ['api.main']);

basicApiModule.factory('$apiBasic', ['$api', '$http', '$log', function ($api, $http, $log) {

    var action_FileUpload = "ResourceItem/Add";

    // CompanyInfo
    var action_CompanyInfoList = "CompanyInfo/List";
    var action_CompanyInfoAdd = "CompanyInfo/Add";
    var action_CompanyInfoDelete = "CompanyInfo/Delete";
    var action_CompanyInfoUpdate = "CompanyInfo/Update";
    var action_CompanyInfoRestore = "CompanyInfo/Restore";
    var action_CompanyInfoMoveUp = "CompanyInfo/MoveUp";
    var action_CompanyInfoMoveDown = "CompanyInfo/MoveDown";

    //数据字典列表
    var action_DataDictionaryChildList = "DataDictionary/ChildList";
    var action_DataDictionaryChildCodeList = "DataDictionary/ChildCodeList";

    return {
        AddResourceItem: function (file, success, error) {
            $api.FileUpload(action_FileUpload, file, success, error);
        },
        ListCompanyInfo: function (paging, success, error) {
            $api.Get(action_CompanyInfoList, paging, success, error);
        },
        AddCompanyInfo: function (entity, success, error) {
            $api.Post(action_CompanyInfoAdd, entity, success, error);
        },
        UpdateCompanyInfo: function (entity, success, error) {
            $api.Post(action_CompanyInfoUpdate, entity, success, error);
        },
        DeleteCompanyInfo: function (ids, success, error) {
            $api.Delete(action_CompanyInfoDelete, { '': ids }, success, error);
        },
        RestoreCompanyInfo: function (ids, success, error) {
            $api.Post(action_CompanyInfoRestore, { '': ids }, success, error);
        },
        MoveUpCompanyInfo: function (ids, success, error) {
            $api.Post(action_CompanyInfoMoveUp, { '': ids }, success, error);
        },
        MoveDownCompanyInfo: function (ids, success, error) {
            $api.Post(action_CompanyInfoMoveDown, { '': ids }, success, error);
        },
        ChildListDataDictionary: function (id, success, error) {
            $api.Get(action_DataDictionaryChildList, { 'Id': id }, success, error);
        },
        GetDataDictionaryByCode: function (code, success, error) {
            $api.Get(action_DataDictionaryChildCodeList, { 'Code': code }, success, error);
        },
    };
}]);