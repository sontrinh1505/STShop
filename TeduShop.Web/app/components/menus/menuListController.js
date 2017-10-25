/// <reference path="menuGroups.module.js" />
(function (app) {
    app.controller('menuListController', menuListController);

    menuListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function menuListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.menus = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getmenus = getmenus;
        $scope.keyWord = '';

        $scope.search = search;

        $scope.deleteMenu = deleteMenu;

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

            apiService.del('api/menu/deletemulti', config, function (result) {
                notificationService.displaySuccess(result.data + ' Delete successful.');
                search();
            }, function (error) {
                notificationService.displayError('Delete was unsuccessful.');
            });
        }

        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.menus, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.menus, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("menus", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteMenu(id) {
            $ngBootbox.confirm('Do you want to delete this item.').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/menu/delete', config, function () {
                    notificationService.displaySuccess('Delete was successful.');
                    search();
                }, function () {
                    notificationService.displayError('Delete was unsuccessful.');
                });

            });
        }

        function search() {
            getmenus();
        }

        function getmenus(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/menu/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('not found.');
                }
                $scope.menus = result.data.Items;
                $scope.page = result.data.page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function (result) {
                console.log('Load menu group failed.');
            })
        }

        $scope.getmenus();
    }

})(angular.module('tedushop.menus'));