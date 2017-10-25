(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function slideAddController(apiService, $scope, notificationService, $state, commonService) {


        $scope.AddSlide = AddSlide;

        function AddSlide() {
            apiService.post('api/slide/create', $scope.slide, function (result) {
                notificationService.displaySuccess(result.data.Name + ' have been added');
                $state.go('menus');
            }, function (error) {
                notificationService.displayError('product can not add');
            });
        }

        //function loadMenuParents() {
        //    apiService.get('api/menugroup/getallparents', null, function (result) {
        //        $scope.menuParents = result.data;
        //    }, function () {
        //        console.log('can not get list parent.');
        //    });
        //}

        //loadMenuParents();
    }

})(angular.module('tedushop.slides'));