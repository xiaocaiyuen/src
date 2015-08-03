var productsApiModule = angular.module('api.products', ['api.main']);

productsApiModule.factory('$apiProducts', ['$api', '$http', '$log', function ($api, $http, $log) {

    // ProductDefinition
    var action_productDefinitionList = "ProductDefinition/List";
    var action_productDefinitionAdd = "ProductDefinition/Add";
    var action_productDefinitionUpdate = "ProductDefinition/Update";
    var action_productDefinitionDelete = "ProductDefinition/Delete";
    var action_ProductDefinitionInfo = "ProductDefinition/GetProductDefinitionModel";
    //ProductContract
    var action_ProductContractList = "ProductContract/List";
    var action_ProductContractAdd = "ProductContract/Add";
    var action_ProductContractUpdate = "ProductContract/Update";
    var action_ProductContractDelete = "ProductContract/Delete";
    var action_ProductContractSelect = "ProductContract/GetProductContractById";

    return {
        // ProductDefinition
        productDefinition: function (paging, success, error) {
            $api.Get(action_productDefinitionList, paging, success, error);
        },
        AddproductDefinition: function (entity, success, error) {
            $api.Post(action_productDefinitionAdd, entity, success, error);
        },
        UpdateproductDefinition: function (entity, success, error) {
            $api.Post(action_productDefinitionUpdate, entity, success, error);
        },
        DeleteproductDefinition: function (ids, success, error) {
            $api.Delete(action_productDefinitionDelete, { '': ids }, success, error);
        },
        ProductDefinitionInfo: function (success, error) {
            $api.Post(action_ProductDefinitionInfo, null, success, error);
        }, //ProductContract
        productContract: function (paging, success, error) {
            $api.Get(action_ProductContractList, paging, success, error);
        },
        AddProductContract: function (entity, success, error) {
            $api.Post(action_ProductContractAdd, entity, success, error);
        },
        UpdateProductContract: function (entity, success, error) {
            $api.Post(action_ProductContractUpdate, entity, success, error);
        },
        DeleteProductContract: function (ids, success, error) {
            $api.Delete(action_ProductContractDelete, { '': ids }, success, error);
        },
        GetProductContractById: function (ids, success, error) {
            $api.Get(action_ProductContractSelect, { 'id': ids }, success, error);
        }
        //,
        //RestoreVehicle_CHH: function (ids, success, error) {
        //    $api.Post(action_SeatRestore, { '': ids }, success, error);
        //}
    };
}]);