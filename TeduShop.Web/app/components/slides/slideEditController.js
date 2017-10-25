(function (app) {
    app.controller('slideEditController', slideEditController);

    slideEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService', '$stateParams'];

    function slideEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {

        $scope.slide = {};
        $scope.Image ='';
        $scope.ckeditorOptions = {
            language: 'en',
            height: '200px',
            allowedContent: true
        }

        $scope.UpdateSlide = UpdateSlide;

        function loadSlideDetail() {
            apiService.get('api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;
                $scope.Image = result.data.Image;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSlide() {
            apiService.put('api/slide/update', $scope.slide, function (result) {
                notificationService.displaySuccess(result.data.Name + ' had been updated');
                $state.go('slides');
            }, function (error) {
                notificationService.displayError('slide can not update');
            });
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                })
            }
            finder.popup();
        }

        loadSlideDetail();
    }

})(angular.module('tedushop.slides'));