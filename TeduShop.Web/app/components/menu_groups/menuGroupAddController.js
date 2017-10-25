(function (app) {
    app.controller('menuGroupAddController', menuGroupAddController);

    menuGroupAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function menuGroupAddController(apiService, $scope, notificationService, $state, commonService) {


        $scope.AddMenuGroup = AddMenuGroup;

        function AddMenuGroup() {
            apiService.post('api/menugroup/create', $scope.menuGroup, function (result) {
                notificationService.displaySuccess(result.data.Name + ' have been added');
                $state.go('menu_groups');
            }, function (error) {
                notificationService.displayError('product can not add');
            });
        }

        function loadMenuGroupParent() {
            apiService.get('api/menugroup/getallparents', null, function (result) {
                $scope.menuGroupParents = result.data;
            }, function () {
                console.log('can not get list parent.');
            });
        }

        loadMenuGroupParent();
    }

})(angular.module('tedushop.menu_groups'));