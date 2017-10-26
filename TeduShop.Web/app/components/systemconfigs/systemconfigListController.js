/// <reference path="menuGroups.module.js" />
(function (app) {
    app.controller('systemconfigListController', systemconfigListController);

    systemconfigListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function systemconfigListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.systemconfigs = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getsystemconfigs = getsystemconfigs;
        $scope.keyWord = '';

        $scope.search = search;

        $scope.deleteSystemConfig = deleteSystemConfig;

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

            apiService.del('api/systemconfig/deletemulti', config, function (result) {
                notificationService.displaySuccess(result.data + ' Delete successful.');
                search();
            }, function (error) {
                notificationService.displayError('Delete was unsuccessful.');
            });
        }

        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.systemconfigs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.systemconfigs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("systemconfigs", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteSystemConfig(id) {
            $ngBootbox.confirm('Do you want to delete this item.').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/systemconfig/delete', config, function () {
                    notificationService.displaySuccess('Delete was successful.');
                    search();
                }, function () {
                    notificationService.displayError('Delete was unsuccessful.');
                });

            });
        }

        function search() {
            getsystemconfigs();
        }

        function getsystemconfigs(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/systemconfig/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('not found.');
                }
                $scope.systemconfigs = result.data.Items;
                $scope.page = result.data.page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (result) {
                console.log('Load system config failed.');
            })
        }

        getsystemconfigs();
    }

})(angular.module('tedushop.systemconfigs'));