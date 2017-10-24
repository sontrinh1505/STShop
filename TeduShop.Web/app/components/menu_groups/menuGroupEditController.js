(function (app) {
    app.controller('menuGroupEditController', menuGroupEditController);

    menuGroupEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function menuGroupEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

        $scope.product = {};

        $scope.ckeditorOptions = {
            language: 'en',
            height: '200px'
        }
        $scope.moreImages = [];
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.UpdateProduct = UpdateProduct;

        function loadProductDetail() {
            apiService.get('api/menugroup/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('api/menugroup/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' had been updated');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('product can not update');
            });
        }

        function loadProductCategory() {
            apiService.get('api/menugroup/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('can not get list parent.');
            });
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })  
            }
            finder.popup();
        }

        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }

        loadProductCategory();
        loadProductDetail();
    }

})(angular.module('tedushop.menu_groups'));