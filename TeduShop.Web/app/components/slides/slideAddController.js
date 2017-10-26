(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function slideAddController(apiService, $scope, notificationService, $state, commonService) {


        $scope.AddSlide = AddSlide;
        $scope.ckeditorOptions = {
            language: 'en',
            height: '200px',
            allowedContent: true
        }


        function AddSlide() {
            apiService.post('api/slide/create', $scope.slide, function (result) {
                notificationService.displaySuccess(result.data.Name + ' have been added');
                $state.go('slides');
            }, function (error) {
                notificationService.displayError('product can not add');
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
    }

})(angular.module('tedushop.slides'));