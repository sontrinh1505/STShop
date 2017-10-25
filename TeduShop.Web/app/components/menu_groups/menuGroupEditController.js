(function (app) {
    app.controller('menuGroupEditController', menuGroupEditController);

    menuGroupEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function menuGroupEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

        $scope.menuGroup = {};

       

        $scope.UpdateMenuGroup = UpdateMenuGroup;

        function loadMenuGroupDetail() {
            apiService.get('api/menugroup/getbyid/' + $stateParams.id, null, function (result) {
                $scope.menuGroup = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateMenuGroup() {
            apiService.put('api/menugroup/update', $scope.menuGroup, function (result) {
                notificationService.displaySuccess(result.data.Name + ' had been updated');
                $state.go('menu_groups');
            }, function (error) {
                notificationService.displayError('menuGroup can not update');
            });
        }

        function loadMenuGroupParents() {
            apiService.get('api/menugroup/getallparents', null, function (result) {
                $scope.menuGroupParents = result.data;
            }, function () {
                console.log('can not get list parent.');
            });
        }

        
        loadMenuGroupParents();
        loadMenuGroupDetail();
    }

})(angular.module('tedushop.menu_groups'));