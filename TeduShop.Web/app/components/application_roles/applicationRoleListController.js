(function (app) {
    'use strict';

    app.controller('applicationRoleListController', applicationRoleListController);

    applicationRoleListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function applicationRoleListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.loading = true;
        $scope.data = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;
        $scope.deleteItem = deleteItem;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.Id);
            });
            var config = {
                params: {
                    checkedList: JSON.stringify(listId)
                }
            }
            apiService.del('api/applicationRole/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.data, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.data, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("data", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteItem(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?')
                .then(function () {
                    var config = {
                        params: {
                            id: id
                        }
                    }
                    apiService.del('/api/applicationRole/delete', config, function () {
                        notificationService.displaySuccess('Đã xóa thành công.');
                        search();
                    },
                    function () {
                        notificationService.displayError('Xóa không thành công.');
                    });
                });
        }
        function search(page) {
            page = page || 0;

            $scope.loading = true;
            var config = {
                params: {
                    page: page,
                    pageSize: 10,
                    filter: $scope.keyword
                }
            }

            apiService.get('api/applicationRole/getlistpaging', config, dataLoadCompleted, dataLoadFailed);
        }

        function dataLoadCompleted(result) {
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;

            if ($scope.filterExpression && $scope.filterExpression.length) {
                notificationService.displayInfo(result.data.Items.length + ' được tìm thấy');
            }
        }
        function dataLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterExpression = '';
            search();
        }




        var editPermisson = false;
        var deletePermission = false;
        var createPermission = false;

        function loadRoles() {

            var config = {
                params: {
                    permissionName: "Role"
                }
            }

            apiService.get('/api/applicationRole/getlisrolebypermissionname',
                config,
                function (response) {
                    //$scope.roles = response.data;
                    var roles = response.data;
                    roles.forEach(function (role, index) {
                        if (role.Name == "Update") {

                            editPermisson = true;

                        } else if (role.Name == "Delete") {

                            deletePermission = true;
                        } else if (role.Name == "Create") {

                            createPermission = true;
                        }

                    });
                    if (!editPermisson) {
                        //$('.deleteItem').addClass('disabled');
                        $('.deleteItem').attr('disabled', 'disabled');
                    }

                    if (!deletePermission) {
                        //$('.editItem').addClass('disabled');
                        //$('.checkboxItem').addClass('disabled');

                        $('.editItem').attr('disabled', 'disabled');
                        $('.checkboxItem').attr('disabled', 'disabled');

                    }

                    if (!createPermission) {
                        //$('.createItem').addClass('disabled');
                        $('.createItem').attr('disabled', 'disabled');
                    }

                    $scope.editItem = editPermisson;

                }, function (response) {
                    notificationService.displayError('can not load roles.');
                    return null;
                });
        }


        loadRoles();

        $scope.search();
    }
})(angular.module('tedushop.application_roles'));