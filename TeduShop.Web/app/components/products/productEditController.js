(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

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
            apiService.get('api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
                loadModelBrands();
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' had been updated');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('product can not update');
            });
        }

        function loadProductCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
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

        function loadBrands() {

            apiService.get('api/brand/getall', null, function (result) {
                $scope.brands = result.data;
            }, function (error) {
                console.log('can not get list parent.');
            });
        }

        

        function loadModelBrands() {
            var config = {
                params: {
                    brandId: $scope.product.BrandID
                }
            }
            apiService.get('api/brand/getmodelbybrandid', config, function (result) {
                $scope.modelbrands = result.data;
            }, function (error) {
                console.log('can not get list parent.');
            });
        }
        loadBrands();
        loadProductCategory();
        loadProductDetail();
        $scope.loadModelBrands = loadModelBrands;
    }

})(angular.module('tedushop.products'));