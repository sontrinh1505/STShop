(function (app) {
    app.controller('systemconfigEditController', systemconfigEditController);

    systemconfigEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function systemconfigEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

        $scope.systemconfig = {};
        //$scope.ckeditorOptions = {
        //    language: 'en',
        //    height: '200px',
        //    allowedContent: true
        //}

        $scope.UpdateSystemConfig = UpdateSystemConfig;

        function loadSlideDetail() {
            apiService.get('api/systemconfig/getbyid/' + $stateParams.id, null, function (result) {
                $scope.systemconfig = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSystemConfig() {
            apiService.put('api/systemconfig/update', $scope.systemconfig, function (result) {
                notificationService.displaySuccess(result.data.Name + ' had been updated');
                $state.go('systemconfigs');
            }, function (error) {
                notificationService.displayError('system config can not update');
            });
        }

        //$scope.chooseImage = function () {
        //    var finder = new CKFinder();
        //    finder.selectActionFunction = function (fileUrl) {
        //        $scope.$apply(function () {
        //            $scope.slide.Image = fileUrl;
        //        })
        //    }
        //    finder.popup();
        //}

        loadSlideDetail();
    }

})(angular.module('tedushop.systemconfigs'));