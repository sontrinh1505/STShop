(function (app) {
    app.controller('menuEditController', menuEditController);

    menuEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function menuEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

        $scope.menu = {};

       

        $scope.UpdateMenu = UpdateMenu;

        function loadMenuDetail() {
            apiService.get('api/menu/getbyid/' + $stateParams.id, null, function (result) {
                $scope.menu = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateMenu() {
            apiService.put('api/menu/update', $scope.menu, function (result) {
                notificationService.displaySuccess(result.data.Name + ' had been updated');
                $state.go('menus');
            }, function (error) {
                notificationService.displayError('menu can not update');
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
        loadMenuDetail();
    }

})(angular.module('tedushop.menus'));