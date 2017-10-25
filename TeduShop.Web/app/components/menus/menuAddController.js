(function (app) {
    app.controller('menuAddController', menuAddController);

    menuAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function menuAddController(apiService, $scope, notificationService, $state, commonService) {


        $scope.AddMenu = AddMenu;

        function AddMenu() {
            apiService.post('api/menu/create', $scope.menu, function (result) {
                notificationService.displaySuccess(result.data.Name + ' have been added');
                $state.go('menus');
            }, function (error) {
                notificationService.displayError('product can not add');
            });
        }

        function loadMenuParents() {
            apiService.get('api/menugroup/getallparents', null, function (result) {
                $scope.menuParents = result.data;
            }, function () {
                console.log('can not get list parent.');
            });
        }

        loadMenuParents();
    }

})(angular.module('tedushop.menus'));