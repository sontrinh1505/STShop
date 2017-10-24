/// <reference path="menuGroups.module.js" />
(function (app) {
    app.controller('menuGroupListController', menuGroupListController);

    menuGroupListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function menuGroupListController($scope, apiService, notificationService, $ngBootbox, $filter) {

        $scope.menugroups = [];
        //$scope.page = 0;
        //$scope.pagesCount = 0;
        $scope.getproducts = getproducts;
        //$scope.keyWord = '';

        $scope.search = search;

        $scope.deleteProduct = deleteProduct;

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

            apiService.del('api/menugroup/deletemulti', config, function (result) {
                notificationService.displaySuccess(result.data + ' Delete successful.');
                search();
            }, function (error) {
                notificationService.displayError('Delete was unsuccessful.');
            });
        }

        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter('filter')(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteProduct(id) {
            $ngBootbox.confirm('Do you want to delete this item.').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/menugroup/delete', config, function () {
                    notificationService.displaySuccess('Delete was successful.');
                    search();
                }, function () {
                    notificationService.displayError('Delete was unsuccessful.');
                });

            });
        }

        function search() {
            getproducts();
        }

        function getproducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyWord: $scope.keyWord,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('api/menugroup/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('not found.');
                }
                $scope.menugroups = result.data.Items;
                //$scope.page = result.data.page;
                //$scope.pagesCount = result.data.TotalPages;
                //$scope.totalCount = result.data.TotalCount;
            }, function (result) {
                console.log('Load menu group failed.');
            })
        }

        $scope.getproducts();
    }

})(angular.module('tedushop.menu_groups'));