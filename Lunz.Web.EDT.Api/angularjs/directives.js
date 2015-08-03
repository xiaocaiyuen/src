//选车控件
apiModule.directive('selectcar', ['$apiVehicle', function ($api) {
    return {
        restrict: 'AE',
        replace: true,
        require: '^ngModel',
        scope: {
            ngModel: '='
        },
        template:
            '<div >' +
            '<span style="margin-left:30px">{{infoName_dir}}</span>' +
                    '<div style="background-color: #fff; border-bottom: 1px solid #dbdbdb; height: 30px; line-height: 30px; position: relative;">' +
                    //顶部导航
                    '<ul style="list-style:none">' +
                        '<li style="float:left;" ng-click="dirInit()">品牌</li>' +
                        '<li style="float:left;" ng-click="brandcheck_dir(brandID_dir,brandName_dir)" ng-show="navS">>车系</li>' +
                        '<li ng-show="navI" ng-click="Seriescheck_dir(seriesID_dir,seriesName_dir)">>车型</li>' +
                    '</ul>' +
                    '</div>' +
                    '<div style="border-top: 1px solid #e7e7e7;font-size: 12px;margin-left: 3px;margin-right: 4px;margin-top: -1px;padding: 7px 0;">' +
                    //顶级车型
                     '<ul ng-show="lv1">' +
                        '<li ng-click="brandcheck_dir(m.Id,m.Name)"  ng-repeat="m in MSelectBrand">{{m.Name}}</li>' +
                    '</ul>' +
                    //车系按品牌分类
                     '<ul ul ng-show="lv2">' +
                       '<li  ng-repeat="m in MSelectSeries">{{m.Name}}' +
                            '<ul>' +
                            '<li ng-click="Seriescheck_dir(s.Id,s.Name)"    ng-repeat="s in m.Vehicle_Series">{{s.Name}}</li>' +
                            '</ul>' +
                        '</li>' +
                    '</ul>' +
                    //车型按年限分类
                    '<ul ul ng-show="lv3">' +
                        '<li  ng-repeat="yearinfo in MSelectInfo">{{yearinfo.year}}' +
                            '<ul>' +
                            '<li ng-click="infocheck_dir(info.Id,info.Name)"    ng-repeat="info in yearinfo.infolist">{{info.Name}}</li>' +
                            '</ul>' +
                        '</li>' +
                    '</ul>' +
                    '</div>' +

            '</div>'
        ,
        controller: function ($scope, $element) {
            //三个div显示隐藏，后期需要根据样式修改
            $scope.lv1 = true;
            $scope.lv2 = false;
            //监视infoid变化来同步infoname
            $scope.$watch('ngModel', function () {
                if ($scope.ngModel != null && $scope.ngModel != '') {
                    $api.GetInfoModelById($scope.ngModel, function (result) {
                        $scope.infoName_dir = result.Data.Name;
                    })
                } else {
                    $scope.infoName_dir = "";
                }
            });
            //数据初始化加载父级品牌列表
            $api.TopBrandList(function (result) {
                $scope.MSelectBrand = result.Data;
            });
            if ($scope.ngModel != null && $scope.ngModel != '') {
                $api.GetInfoModelById($scope.ngModel, function (result) {
                    if (result.Data  != null)
                    {
                        $scope.infoName_dir = result.Data.Name;
                    }
                    
                })
            }
            //父级品牌列表点击事件
            $scope.brandcheck_dir = function (id, name) {
                //div及导航显隐
                $scope.lv1 = false;
                $scope.lv2 = true;
                $scope.lv3 = false;
                $scope.navS = true;
                $scope.navI = false;
                //留存所选品牌信息（父品牌）
                $scope.brandName_dir = name;
                $scope.brandID_dir = id;
                //获取子品牌及子品牌下的车系
                $api.ChildListByParentId(id, function (result) {
                    $scope.MSelectSeries = result.Data;
                });
            }
            //车系点击事件
            $scope.Seriescheck_dir = function (id, name) {
                //div及导航显隐
                $scope.lv1 = false;
                $scope.lv2 = false;
                $scope.lv3 = true;
                $scope.navI = true;
                //留存所选车系信息
                $scope.seriesName_dir = name;
                $scope.seriesID_dir = id;
                //获取车系下车型年限及年限下的车型列表
                $api.GetInfoListBySeriesID(id, function (result) {
                    $scope.MSelectInfo = result.Data;
                });
            }
            //车型点击事件
            $scope.infocheck_dir = function (id, name) {
                //div及导航显隐
                $scope.lv1 = false;
                $scope.lv2 = false;
                $scope.lv3 = false;
                //留存所选车型信息
                $scope.infoName_dir = name;
                $scope.ngModel = id;
            }
            //初始化
            $scope.dirInit = function () {
                $scope.lv1 = true;
                $scope.lv2 = false;
                $scope.lv3 = false;
                $scope.navS = false;
                $scope.navI = false;
                //数据初始化加载父级品牌列表
                $api.TopBrandList(function (result) {
                    $scope.MSelectBrand = result.Data;
                });
            }
            
        }
    };
}]);
//Tabs控件
apiModule.directive('showtab',
    function () {
        return {
            link: function (scope, element, attrs) {
                element.click(function (e) {
                    e.preventDefault();
                    $(element).tab('show');
                });
            }
        };
    });