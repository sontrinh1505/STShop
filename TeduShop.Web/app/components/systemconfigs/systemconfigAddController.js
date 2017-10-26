(function (app) {
    app.controller('systemconfigAddController', systemconfigAddController);

    systemconfigAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function systemconfigAddController(apiService, $scope, notificationService, $state, commonService) {


        $scope.AddSystemConfig = AddSystemConfig;

        function AddSystemConfig() {
            apiService.post('api/systemconfig/create', $scope.systemconfig, function (result) {
                notificationService.displaySuccess(result.data.Name + ' have been added');
                $state.go('systemconfigs');
            }, function (error) {
                notificationService.displayError('Item can not add');
            });
        }
    }

})(angular.module('tedushop.systemconfigs'));