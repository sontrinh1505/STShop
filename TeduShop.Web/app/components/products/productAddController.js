(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {

        $scope.product = {
            CreateDate: new Date(),
            Status: true
        }
        $scope.ckeditorOptions = {
            language: 'en',
            height: '200px'
        }

        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.AddProduct = AddProduct;

        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' have been added');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('product can not add');
            });
        }

        

        function loadProductCategories() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log('can not get list parent.');
            });
        }


        function loadBrands() {
           
            apiService.get('api/brand/getall', null,  function (result) {
                $scope.brands = result.data;
            }, function (error) {
                console.log('can not get list parent.');
            });
        }
        
        $scope.loadModelBrands = function() {
            var config = {
                params: {
                    brandId: $scope.product.Brand.ID
                }
            }
            apiService.get('api/brand/getmodelbybrandid', config, function (result) {
                $scope.modelbrands = result.data;
            }, function (error) {
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

        $scope.moreImages = [];
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })              
            }
            finder.popup();
        }

        //loadModelBrands();
        loadBrands();
        loadProductCategories();
    }

})(angular.module('tedushop.products'));