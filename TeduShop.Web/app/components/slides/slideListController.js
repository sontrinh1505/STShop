/// <reference path="menuGroups.module.js" />
(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function slideListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.slides = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getslides = getslides;
        $scope.keyWord = '';

        $scope.search = search;

        $scope.deleteSlide = deleteSlide;

        $scope.selectAll = selectAll;

        $scope.isAll = false;

        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }

            apiService.del('api/slide/deletemulti', config, function (result) {
                notificationService.displaySuccess(result.data + ' Delete successful.');
                search();
            }, function (error) {
                notificationService.displayError('Delete was unsuccessful.');
            });
        }

        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.slides, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.slides, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("slides", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteSlide(id) {
            $ngBootbox.confirm('Do you want to delete this item.').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/slide/delete', config, function () {
                    notificationService.displaySuccess('Delete was successful.');
                    search();
                }, function () {
                    notificationService.displayError('Delete was unsuccessful.');
                });

            });
        }

        function search() {
            getslides();
        }

        function getslides(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/slide/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('not found.');
                }
                $scope.slides = result.data.Items;
                $scope.page = result.data.page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (result) {
                console.log('Load slide group failed.');
            })
        }

        $scope.getslides();
    }

})(angular.module('tedushop.slides'));